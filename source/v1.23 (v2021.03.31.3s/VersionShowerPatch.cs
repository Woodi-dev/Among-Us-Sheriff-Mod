using HarmonyLib;


namespace SheriffMod
{
    [HarmonyPatch(typeof(VersionShower), "Start")]
    public static class VersionShowerPatch
    {
        
        public static void Postfix(VersionShower __instance)
        {
            __instance.text.Text += "\nloaded Sheriff Mod v1.23 by Woodi ";

        }
    }

}
