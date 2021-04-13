using HarmonyLib;
using GameOptionsData = CEIOGGEDKAN;
namespace SheriffMod
{
    [HarmonyPatch(typeof(GameOptionsData))]
    public class GameOptionsDataPatch
    {


        //Update lobby options text
        [HarmonyPostfix]
        [HarmonyPatch("ONCLFHFDADB")]
        public static void Postfix1(GameOptionsData __instance, ref string __result, int JFGKGCCMCNK)
        {
            if (CustomGameOptions.showSheriff)
               __result +=  "Show Sheriff: On" + "\n";
            else
               __result+="Show Sheriff: Off" + "\n";
           __result+= "Sheriff Kill Cooldown: " + CustomGameOptions.SheriffKillCD.ToString() + "s";

        }

       
    }
    
    
}
