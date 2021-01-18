using HarmonyLib;
using Hazel;
using Il2CppSystem.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheriffMod
{
    [HarmonyPatch]
    public static class KillButtonPatch
    {
 

        [HarmonyPatch(typeof(MLPJGKEACMM), "PerformKill")]
        static bool Prefix(MethodBase __originalMethod)
        {
            if (PlayerControlPatch.isSheriff(FFGALNAPKCD.LocalPlayer))
            {
                if (PlayerControlPatch.SheriffKillTimer() == 0)
                {
                    var dist = PlayerControlPatch.getDistBetweenPlayers(FFGALNAPKCD.LocalPlayer, PlayerControlPatch.closestPlayer);
                    if (dist < KMOGFLPJLLK.JMLGACIOLIK[FFGALNAPKCD.GameOptions.DLIBONBKPKL])
                    {


                        if (PlayerControlPatch.closestPlayer.NDGFFHMFGIG.DAPKNDBLKIA == false)
                        {
                            MessageWriter writer = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.None, -1);
                            writer.Write(FFGALNAPKCD.LocalPlayer.PlayerId);
                            writer.Write(FFGALNAPKCD.LocalPlayer.PlayerId);
                            FMLLKEACGIO.Instance.FinishRpcImmediately(writer);
                            FFGALNAPKCD.LocalPlayer.MurderPlayer(FFGALNAPKCD.LocalPlayer);

                        }
                        else
                        {
                            MessageWriter writer = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte)CustomRPC.SheriffKill, Hazel.SendOption.None, -1);
                            writer.Write(FFGALNAPKCD.LocalPlayer.PlayerId);
                            writer.Write(PlayerControlPatch.closestPlayer.PlayerId);
                            FMLLKEACGIO.Instance.FinishRpcImmediately(writer);
                            FFGALNAPKCD.LocalPlayer.MurderPlayer(PlayerControlPatch.closestPlayer);

                        }

                        PlayerControlPatch.lastKilled = DateTime.UtcNow;
                    }
                }

                return false;
            }
            return true;



        }


    }

}
