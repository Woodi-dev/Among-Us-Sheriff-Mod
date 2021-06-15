using HarmonyLib;
using Hazel;
using Il2CppSystem.Reflection;

namespace SheriffMod
{
    [HarmonyPatch]
    public static class KillButtonPatch
    {
        //Change the KillButton behaviour for the Sheriff
        [HarmonyPatch(typeof(KillButtonManager), "PerformKill")]
        static bool Prefix(MethodBase __originalMethod)
        {
            if (Sheriff.isSheriff(PlayerController.LocalPlayer))
            {
                if (Sheriff.instance.SheriffKillTimer() == 0 && PlayerControl.LocalPlayer.CanMove)
                {
                    var closesetPlayer = Sheriff.instance.closestPlayer;
                    if (closesetPlayer != null)
                    {
                        if (closesetPlayer.Data.IsImpostor == false)
                        {
                            closesetPlayer = PlayerControl.LocalPlayer;
                        }
                        MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.Reliable);
                        writer.Write(PlayerControl.LocalPlayer.PlayerId);
                        writer.Write(closesetPlayer.PlayerId);
                        writer.EndMessage();
                        PlayerControl.LocalPlayer.MurderPlayer(PlayerControl.LocalPlayer);

                        PlayerControl.LocalPlayer.SetKillTimer(CustomGameOptions.SheriffKillCD);
                        Sheriff.instance.killTimer = CustomGameOptions.SheriffKillCD;
                    }
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
