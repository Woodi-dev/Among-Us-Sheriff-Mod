using HarmonyLib;

namespace SheriffMod
{
    [HarmonyPatch(typeof(GameOptionsData))]
    public class GameOptionsDataPatch
    {
        //Update lobby options text
        [HarmonyPostfix]
        [HarmonyPatch("ToHudString")]
        public static void Postfix1(GameOptionsData __instance, ref string __result, int numPlayers)
        {
            if (CustomGameOptions.showSheriff)
               __result +=  "Show Sheriff: On" + "\n";
            else
               __result+="Show Sheriff: Off" + "\n";
           __result+= "Sheriff Kill Cooldown: " + CustomGameOptions.SheriffKillCD.ToString() + "s";
        }
    }
}
