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
        public static List<Player> getCrewMates(Il2CppReferenceArray<GameData.PlayerInfo> infection)
        {
            List<Player> crewmates = new List<Player>();
            foreach (Player player in PlayerController.players)
            {
                bool isInfected = false;
                foreach (var infected in infection)
                {
                    if (player.playerdata.PlayerId == infected._object.PlayerId)
                    {
                        isInfected = true;
                        break;
                    }
                }

                if (!isInfected)
                {
                    crewmates.Add(player);
                }
            }
            return crewmates;
        }

        //Here we handle all intercommunication
        [HarmonyPatch(typeof(PlayerControl),"HandleRpc")]
        public static void Postfix(byte callId, MessageReader reader)
        {
            switch (callId)
            {
                case (byte)CustomRPC.SetSheriff:
                    PlayerController.InitPlayers();
                    byte SheriffId = reader.ReadByte();
                    Player p = PlayerController.getPlayerById(SheriffId);
                    p.setComponent(new Sheriff(p));
                    break;
                case (byte)CustomRPC.SyncCustomSettings:
                    CustomGameOptions.showSheriff = reader.ReadBoolean();
                    CustomGameOptions.SheriffKillCD = System.BitConverter.ToSingle(reader.ReadBytes(4).ToArray(), 0); //readFloat
                    break;
                case (byte)CustomRPC.SheriffKill:
                    Player killer = PlayerController.getPlayerById(reader.ReadByte());
                    Player target = PlayerController.getPlayerById(reader.ReadByte());
                    if (killer.isSheriff())
                    {
                        killer.playerdata.MurderPlayer(target.playerdata);
                    }
                    break;
            }
        }

        //Here we select the Sheriff. Parameter list contains impostors.
        [HarmonyPatch(typeof(PlayerControl), "RpcSetInfected")]
        public static void Postfix(Il2CppReferenceArray<GameData.PlayerInfo> infected)
        {
            PlayerController.InitPlayers();
            List<Player> crewmates = getCrewMates(infected);    
        
            var sheriffidx = new System.Random().Next(0, crewmates.Count);
            Player sheriff = crewmates[sheriffidx];

            MessageWriter writer = AmongUsClient.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetSheriff, Hazel.SendOption.Reliable);
            writer.Write(sheriff.playerdata.PlayerId);
            writer.EndMessage();
            sheriff.setComponent(new Sheriff(sheriff));
        }

        //How can a Crewmate (Sheriff) kill another player? We simply set the killer to an Impostor and revert it afterwards.
        [HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
        public static bool Prefix(PlayerControl __instance, PlayerControl target)
        {
            if (Sheriff.isSheriff(__instance))
            {
                __instance.Data.IsImpostor = true;
            }
            return true;
        }

        [HarmonyPatch(typeof(PlayerControl), "MurderPlayer")]
        public static void Postfix(PlayerControl __instance, PlayerControl target)
        {
            if (Sheriff.isSheriff(__instance))
            {
                __instance.Data.IsImpostor = false;
            }
        }

        //Sync custom settings to other players
        [HarmonyPatch(typeof(PlayerControl), "RpcSyncSettings")]
        public static void Postfix(GameOptionsData gameOptions)
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
