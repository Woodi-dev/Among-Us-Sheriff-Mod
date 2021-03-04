using HarmonyLib;
using Hazel;
using Il2CppSystem.Reflection;

using InnerPlayerControl = FFGALNAPKCD;
using AmongUsCleint = FMLLKEACGIO;
namespace SheriffMod
{
    [HarmonyPatch]
    public static class KillButtonPatch
    {
 

        [HarmonyPatch(typeof(MLPJGKEACMM), "PerformKill")]
        static bool Prefix(MethodBase __originalMethod)
        {
            if (Sheriff.instance == null) return true;
            if (PlayerController.LocalPlayer == Sheriff.instance.parent)
            {
                if (Sheriff.instance.SheriffKillTimer() == 0)
                {

                    if (InnerPlayerControl.LocalPlayer.GEBLLBHGHLD)
                    {

                        //Target is Crewmate
                        if (Sheriff.instance.closestPlayer.NDGFFHMFGIG.DAPKNDBLKIA == false)
                        {
                            MessageWriter writer = AmongUsCleint.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.Reliable);
                            writer.Write(InnerPlayerControl.LocalPlayer.PlayerId);
                            writer.Write(InnerPlayerControl.LocalPlayer.PlayerId);
                            writer.EndMessage();
                            InnerPlayerControl.LocalPlayer.MurderPlayer(InnerPlayerControl.LocalPlayer);

                        }
                        else
                        {
                            MessageWriter writer = AmongUsCleint.Instance.StartRpc(InnerPlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.Reliable);
                            writer.Write(InnerPlayerControl.LocalPlayer.PlayerId);
                            writer.Write(Sheriff.instance.closestPlayer.PlayerId);
                            writer.EndMessage();
                            InnerPlayerControl.LocalPlayer.MurderPlayer(Sheriff.instance.closestPlayer);

                        }
                        InnerPlayerControl.LocalPlayer.SetKillTimer(CustomGameOptions.SheriffKillCD);
                        Sheriff.instance.killTimer = CustomGameOptions.SheriffKillCD;
                            

                    }
                }

                return false;
            }
            return true;



        }


    }

}
