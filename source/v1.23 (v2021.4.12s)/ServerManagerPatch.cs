using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace SheriffMod
{
    [HarmonyPatch(typeof(ServerManager))]
    public class ServerManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("LoadServers")]

        public static bool Prefix1(ServerManager __instance) {

            __instance.StartCoroutine(__instance.ReselectRegionFromDefaults());
               
            return false;

        }



       
    }
}
