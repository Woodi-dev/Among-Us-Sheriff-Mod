using HarmonyLib;
using GameOptionsData = PAMOPBEDCNI;
namespace SheriffMod
{
    [HarmonyPatch(typeof(GameOptionsData))]
    public class GameOptionsDataPatch
    {



        [HarmonyPostfix]
        [HarmonyPatch("NHJLMAAHKJF")]
        public static void Postfix1(GameOptionsData __instance, ref string __result, int MKGPLPMAKLO)
        {
            if (CustomGameOptions.showSheriff)
               __result +=  "Show Sheriff: On" + "\n";
            else
               __result+="Show Sheriff: Off" + "\n";
           __result+= "Sheriff Kill Cooldown: " + CustomGameOptions.SheriffKillCD.ToString() + "s";

        }

       
    }
}
