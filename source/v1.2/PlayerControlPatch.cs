using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;

using InnerPlayerControl = FFGALNAPKCD;
using AmongUsClient = FMLLKEACGIO;
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
		public static List<Player> getCrewMates(Il2CppReferenceArray<EGLJNOMOGNP.DCJMABDDJCF> infection)
		{

			List<Player> Crewmates = new List<Player>();
			foreach (Player player in PlayerController.players)
			{

				bool isInfected = false;
				foreach (EGLJNOMOGNP.DCJMABDDJCF infected in infection)
				{

					if (player.playerdata.PlayerId == infected.LAOEJKHLKAI.PlayerId)
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
		[HarmonyPatch(typeof(InnerPlayerControl),"HandleRpc")]
		public static void Postfix(byte HKHMBLJFLMC, MessageReader ALMCIJKELCP)
        {
			
            switch (HKHMBLJFLMC){

				case (byte)CustomRPC.SetSheriff:
					{
						PlayerController.InitPlayers();
						byte SheriffId = ALMCIJKELCP.ReadByte();
						Player p = PlayerController.getPlayerById(SheriffId);
						p.components.Add(new Sheriff(p));
						break;
					}
				case (byte)CustomRPC.SyncCustomSettings:
					{
						CustomGameOptions.showSheriff = ALMCIJKELCP.ReadBoolean();
						CustomGameOptions.SheriffKillCD = System.BitConverter.ToSingle(ALMCIJKELCP.ReadBytes(4).ToArray(), 0);
						break;
					}
				case (byte)CustomRPC.SheriffKill:
					{
						Player killer = PlayerController.getPlayerById(ALMCIJKELCP.ReadByte());
						Player target = PlayerController.getPlayerById(ALMCIJKELCP.ReadByte());
						if (killer.hasComponent("Sheriff"))
						{
							killer.playerdata.MurderPlayer(target.playerdata);
						}
						break;
					}
		


			}
        }

		[HarmonyPatch(typeof(InnerPlayerControl), "RpcSetInfected")]
		public static void Postfix(Il2CppReferenceArray<EGLJNOMOGNP.DCJMABDDJCF> JPGEIBIBJPJ)
        {
			PlayerController.InitPlayers();
			List<Player> crewmates = getCrewMates(JPGEIBIBJPJ);	
			var sheriffidx = new System.Random().Next(0, crewmates.Count);
			Player sheriff = crewmates[sheriffidx];

			MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetSheriff, Hazel.SendOption.Reliable);
			writer.Write(sheriff.playerdata.PlayerId);
			writer.EndMessage();
			sheriff.components.Add(new Sheriff(sheriff));
						

		}

		[HarmonyPatch(typeof(InnerPlayerControl), "MurderPlayer")]
		public static bool Prefix(InnerPlayerControl __instance, InnerPlayerControl CAKODNGLPDF)
		{
			if (Sheriff.instance != null)
			{
				if (__instance.PlayerId == Sheriff.instance.parent.PlayerId)
				{
					__instance.NDGFFHMFGIG.DAPKNDBLKIA = true;

				}
			}
			return true;
		}

		[HarmonyPatch(typeof(InnerPlayerControl), "MurderPlayer")]
		public static void Postfix(InnerPlayerControl __instance, InnerPlayerControl CAKODNGLPDF)
		{

			if (Sheriff.instance != null)
			{
				if (__instance.PlayerId == Sheriff.instance.parent.PlayerId)
				{

					__instance.NDGFFHMFGIG.DAPKNDBLKIA = false;




				}
			}



		}
		

		[HarmonyPatch(typeof(InnerPlayerControl), "RpcSyncSettings")]
		public static void Postfix(KMOGFLPJLLK IOFBPLNIJIC)
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
