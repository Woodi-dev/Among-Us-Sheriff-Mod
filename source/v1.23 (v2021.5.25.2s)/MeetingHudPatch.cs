using HarmonyLib;

namespace SheriffMod
{
    [HarmonyPatch]
    public class MeetingHudPatch 
    {
        //Reset timers
        [HarmonyPatch(typeof(MeetingHud), "Close")]
        public static void Postfix(MeetingHud __instance)
        {
            if (Sheriff.instance == null) return;
            Sheriff.instance.killTimer = CustomGameOptions.SheriffKillCD;
        }
    }
}
