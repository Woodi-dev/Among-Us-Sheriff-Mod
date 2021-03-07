using HarmonyLib;
using System;
using System.Linq;


namespace SheriffMod
{
    [HarmonyPatch]
    class HudManagerPatch
    {

        public static HudManager HUD;
        public static KillButtonManager KillButton;



      
        public static void updateMeetingHUD(MeetingHud __instance)
        {

            if (Sheriff.instance == null) return;

            foreach (PlayerVoteArea area in __instance.NCEKLHJIDEG)
            {
               
                {
                    if (area.NameText.Text == Sheriff.instance.parent.playerdata.name)
                    {
                        if (Sheriff.instance.parent == PlayerController.LocalPlayer || CustomGameOptions.showSheriff)
                        {
                            area.NameText.Color = Sheriff.color;

                        }

                    }
                }
               
               
            }
        }




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

