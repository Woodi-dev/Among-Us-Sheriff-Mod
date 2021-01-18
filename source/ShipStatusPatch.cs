using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheriffMod
{

    [HarmonyPatch]
    public static class ShipStatusPatch
    {

        [HarmonyPatch(typeof(HLBNNHFCNAJ), "Start")]
        public static void Postfix(HLBNNHFCNAJ __instance)
        {
            PlayerControlPatch.lastKilled = DateTime.UtcNow;
            PlayerControlPatch.lastKilled = PlayerControlPatch.lastKilled.AddSeconds(-10);

        }
    }
}
