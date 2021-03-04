using HarmonyLib;
using System;
using System.Linq;
using HudManager = PIEFJFEOGOL;
namespace SheriffMod
{
    [HarmonyPatch]
    class HudManagerPatch
    {
        static string GameSettingsText = null;

        public static HudManager HUD;
        public static MLPJGKEACMM KillButton;
  
      

      
        public static void updateMeetingHUD(OOCJALPKPEP __instance)
        {

            if (Sheriff.instance == null) return;

            foreach (HDJGDMFCHDN HDJGDMFCHDN in __instance.HBDFFAHBIGI)
            {
               
                {
                    if (HDJGDMFCHDN.NameText.Text == Sheriff.instance.parent.playerdata.name)
                    {
                        if (Sheriff.instance.parent == PlayerController.LocalPlayer || CustomGameOptions.showSheriff)
                        {
                            HDJGDMFCHDN.NameText.Color = Sheriff.color;

                        }

                    }
                }
               
               
            }
        }
        public static void UpdateGameSettingsText(HudManager __instance)
        {



            if (__instance.GameSettings.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Count() == 19)
            {
                GameSettingsText = __instance.GameSettings.Text;


            }
            if (GameSettingsText != null)
            {
                if (CustomGameOptions.showSheriff)
                    __instance.GameSettings.Text = GameSettingsText + "Show Sheriff: On" + "\n";
                else
                    __instance.GameSettings.Text = GameSettingsText + "Show Sheriff: Off" + "\n";
                __instance.GameSettings.Text += "Sheriff Kill Cooldown: " + CustomGameOptions.SheriffKillCD.ToString() + "s";
            }

        }




        [HarmonyPatch(typeof(HudManager), "Update")]


        public static void Postfix(HudManager __instance)
        {
            HUD = __instance;

            KillButton = __instance.KillButton;

            PlayerController.Update();
            if (OOCJALPKPEP.Instance != null)
            {

                updateMeetingHUD(OOCJALPKPEP.Instance);
            }


        }
    }

   

}

