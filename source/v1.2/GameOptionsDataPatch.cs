using HarmonyLib;
using GameOptionsData = KMOGFLPJLLK;
namespace SheriffMod
{
    [HarmonyPatch(typeof(GameOptionsData))]
    public class GameOptionsDataPatch
    {



        [HarmonyPostfix]
        [HarmonyPatch("CKKJMLEDCJB")]
        public static void Postfix1(GameOptionsData __instance, ref string __result, int FAEAEAMNEEO)
        {
            if (CustomGameOptions.showSheriff)
               __result +=  "Show Sheriff: On" + "\n";
            else
               __result+="Show Sheriff: Off" + "\n";
           __result+= "Sheriff Kill Cooldown: " + CustomGameOptions.SheriffKillCD.ToString() + "s";

        }

       
    }
}
