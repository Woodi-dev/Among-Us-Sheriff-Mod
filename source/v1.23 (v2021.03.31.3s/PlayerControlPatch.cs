using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;

using InnerPlayerControl = PlayerControl;
namespace SheriffMod
{
	enum CustomRPC
	{
		SetSheriff = 40,
		SyncCustomSettings = 41,
		SheriffKill = 42

	}
	enum RPC
	{

		PlayAnimation = 0,
		CompleteTask = 1,
		SyncSettings = 2,
		SetInfected = 3,
		Exiled = 4,
		CheckName = 5,
		SetName = 6,
		CheckColor = 7,
		SetColor = 8,
		SetHat = 9,
		SetSkin = 10,
		ReportDeadBody = 11,
		MurderPlayer = 12,
		SendChat = 13,
		StartMeeting = 14,
		SetScanner = 15,
		SendChatNote = 16,
		SetPet = 17,
		SetStartCounter = 18,
		EnterVent = 19,
		ExitVent = 20,
		SnapTo = 21,
		Close = 22,
		VotingComplete = 23,
		CastVote = 24,
		ClearVote = 25,
		AddVote = 26,
		CloseDoorsOfType = 27,
		RepairSystem = 28,
		SetTasks = 29,
		UpdateGameData = 30,


	}

	[HarmonyPatch]
    class PlayerControlPatch
    {
		//get Crews via Impostors
		public static List<Player> getCrewMates(Il2CppReferenceArray<GameData.OFKOJOKOOAK> infection)
		{

			List<Player> Crewmates = new List<Player>();
			foreach (Player player in PlayerController.players)
			{

				bool isInfected = false;
				foreach (var infected in infection)
				{

					if (player.playerdata.PlayerId == infected.GPBBCHGPABL.PlayerId)
					{
						isInfected = true;

						break;
					}

				}
				if (!isInfected)
				{
					Crewmates.Add(player);
				}


			}
			return Crewmates;

		}
		//Here we handle all intercommunication
		[HarmonyPatch(typeof(PlayerControl),"HandleRpc")]
		public static void Postfix(byte GIICFHKILOB, MessageReader DOOILGKLBBF)
        {
			
            switch (GIICFHKILOB)
			{

				case (byte)CustomRPC.SetSheriff:
					{
						PlayerController.InitPlayers();
						byte SheriffId = DOOILGKLBBF.ReadByte();
						Player p = PlayerController.getPlayerById(SheriffId);
						p.components.Add(new Sheriff(p));
						break;
					}
				case (byte)CustomRPC.SyncCustomSettings:
					{
						CustomGameOptions.showSheriff = DOOILGKLBBF.ReadBoolean();
						CustomGameOptions.SheriffKillCD = System.BitConverter.ToSingle(DOOILGKLBBF.ReadBytes(4).ToArray(), 0); //readFloat
						break;
					}
				case (byte)CustomRPC.SheriffKill:
					{
						Player killer = PlayerController.getPlayerById(DOOILGKLBBF.ReadByte());
						Player target = PlayerController.getPlayerById(DOOILGKLBBF.ReadByte());
						if (killer.hasComponent("Sheriff"))
						{
							killer.playerdata.MurderPlayer(target.playerdata);
						}
						break;
					}
		


			}
        }
		//Here we select the Sheriff. Parameter list contains impostors.

		[HarmonyPatch(typeof(PlayerControl), "RpcSetInfected")]
		public static void Postfix(Il2CppReferenceArray<GameData.OFKOJOKOOAK> LBJJHLMFDJL)
        {
			PlayerController.InitPlayers();
			List<Player> crewmates = getCrewMates(LBJJHLMFDJL);	
		
			var sheriffidx = new System.Random().Next(0, crewmates.Count);
			Player sheriff = crewmates[sheriffidx];

			MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetSheriff, Hazel.SendOption.Reliable);
			writer.Write(sheriff.playerdata.PlayerId);
			writer.EndMessage();
			sheriff.components.Add(new Sheriff(sheriff));
						

		}

		//How can a Crewmate (Sheriff) kill another player? We simply set the killer to an Impostor and revert it afterwards.
		[HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
		public static bool Prefix(PlayerControl __instance, PlayerControl IGLDJOKKFJE)
		{
			if (Sheriff.instance != null)
			{
				if (__instance.PlayerId == Sheriff.instance.parent.PlayerId)
				{
					__instance.JCLEEAHKPKG.CIDDOFDJHJH = true;

				}
			}
			return true;
		}

		[HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
		public static void Postfix(PlayerControl __instance, PlayerControl IGLDJOKKFJE)
		{

			if (Sheriff.instance != null)
			{
				if (__instance.PlayerId == Sheriff.instance.parent.PlayerId)
				{

					__instance.JCLEEAHKPKG.CIDDOFDJHJH = false;




				}
			}



		}
		
		//Sync custom settings to other players

		[HarmonyPatch(typeof(PlayerControl), "RpcSyncSettings")]
		public static void Postfix(IGDMNKLDEPI PAOKIAHKEMG)
		{
			if (InnerPlayerControl.AllPlayerControls.Count > 1)
			{
				MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SyncCustomSettings, Hazel.SendOption.Reliable);

				writer.Write(CustomGameOptions.showSheriff);
				writer.Write(CustomGameOptions.SheriffKillCD);
				writer.EndMessage();
			}

		}

	}

}
