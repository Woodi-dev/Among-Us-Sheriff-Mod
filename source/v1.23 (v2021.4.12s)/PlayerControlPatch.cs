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
	

	[HarmonyPatch]
    class PlayerControlPatch
    {
		//get Crews via Impostors
		public static List<Player> getCrewMates(Il2CppReferenceArray<GameData.LGBOMGHJELL> infection)
		{

			List<Player> Crewmates = new List<Player>();
			foreach (Player player in PlayerController.players)
			{

				bool isInfected = false;
				foreach (var infected in infection)
				{

					if (player.playerdata.PlayerId == infected.GJPBCGFPMOD.PlayerId)
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
		public static void Postfix(byte ONIABIILFGF, MessageReader JIGFBHFFNFI)
        {
			
            switch (ONIABIILFGF)
			{

				case (byte)CustomRPC.SetSheriff:
					{
						PlayerController.InitPlayers();
						byte SheriffId = JIGFBHFFNFI.ReadByte();
						Player p = PlayerController.getPlayerById(SheriffId);
						p.components.Add(new Sheriff(p));
						break;
					}
				case (byte)CustomRPC.SyncCustomSettings:
					{
						CustomGameOptions.showSheriff = JIGFBHFFNFI.ReadBoolean();
						CustomGameOptions.SheriffKillCD = System.BitConverter.ToSingle(JIGFBHFFNFI.ReadBytes(4).ToArray(), 0); //readFloat
						break;
					}
				case (byte)CustomRPC.SheriffKill:
					{
						Player killer = PlayerController.getPlayerById(JIGFBHFFNFI.ReadByte());
						Player target = PlayerController.getPlayerById(JIGFBHFFNFI.ReadByte());
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
		public static void Postfix(Il2CppReferenceArray<GameData.LGBOMGHJELL> BHNEINNHPIJ)
        {
			PlayerController.InitPlayers();
			List<Player> crewmates = getCrewMates(BHNEINNHPIJ);	
		
			var sheriffidx = new System.Random().Next(0, crewmates.Count);
			Player sheriff = crewmates[sheriffidx];

			MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetSheriff, Hazel.SendOption.Reliable);
			writer.Write(sheriff.playerdata.PlayerId);
			writer.EndMessage();
			sheriff.components.Add(new Sheriff(sheriff));
						

		}

		//How can a Crewmate (Sheriff) kill another player? We simply set the killer to an Impostor and revert it afterwards.
		[HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
		public static bool Prefix(PlayerControl __instance, PlayerControl DGDGDKCCKHJ)
		{
			if (Sheriff.instance != null)
			{
				if (__instance.PlayerId == Sheriff.instance.parent.PlayerId)
				{
					__instance.FIMGDJOCIGD.FDNMBJOAPFL = true;

				}
			}
			return true;
		}

		[HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
		public static void Postfix(PlayerControl __instance, PlayerControl DGDGDKCCKHJ)
		{

			if (Sheriff.instance != null)
			{
				if (__instance.PlayerId == Sheriff.instance.parent.PlayerId)
				{

					__instance.FIMGDJOCIGD.FDNMBJOAPFL = false;




				}
			}



		}
		
		//Sync custom settings to other players

		[HarmonyPatch(typeof(PlayerControl), "RpcSyncSettings")]
		public static void Postfix(CEIOGGEDKAN DJGAEEMDIDF)
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
