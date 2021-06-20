using HarmonyLib;
using System;
using System.Linq;
using UnityEngine;

namespace SheriffMod
{
    [HarmonyPatch]
    class HudManagerPatch
    {
        public static HudManager HUD;
        public static KillButtonManager KillButton;

        //highlight the Sheriff in meetings
        public static void updateMeetingHUD(MeetingHud __instance)
        {
            if (Sheriff.instance == null) return;

            foreach (PlayerVoteArea area in __instance.playerStates)
            {
                if (area.NameText.text == Sheriff.instance.parent.playerdata.name)
                {
                    if (Sheriff.isSheriff(PlayerController.LocalPlayer) || CustomGameOptions.showSheriff)
                    {
                        area.NameText.color = Sheriff.color;
                    }
                }
            }
        }

        //main update loop
        [HarmonyPatch(typeof(HudManager), "Update")]
        public static void Postfix(HudManager __instance)
        {
            HUD = __instance;

            KillButton = __instance.KillButton;

            PlayerController.Update();
            if (MeetingHud.Instance != null)
            {
                updateMeetingHUD(MeetingHud.Instance);
            }
        }
    }
}
