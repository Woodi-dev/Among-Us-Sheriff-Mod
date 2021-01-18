using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;







namespace SheriffMod
{
    [HarmonyPatch(typeof(PHCKLDDNJNP))]
    public static class GameOptionMenuPatch
    {
        public static BCLDBBKFJPK showSheriffOption;
        public static PCGDGFIAJJI SheriffCooldown;


        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Postfix1(PHCKLDDNJNP __instance)
        {
            if (GameObject.FindObjectsOfType<BCLDBBKFJPK>().Count == 4)
            {
               
                    BCLDBBKFJPK showAnonymousvote = GameObject.FindObjectsOfType<BCLDBBKFJPK>().ToList().Where(x => x.TitleText.Text == "Anonymous Votes").First();
                    showSheriffOption = GameObject.Instantiate(showAnonymousvote);

                    showSheriffOption.TitleText.Text = "Show Sheriff";

                    showSheriffOption.NHLMDAOEOAE = CustomGameOptions.showSheriff;
                    showSheriffOption.CheckMark.enabled = CustomGameOptions.showSheriff;

                    PCGDGFIAJJI killcd = GameObject.FindObjectsOfType<PCGDGFIAJJI>().ToList().Where(x => x.TitleText.Text == "Kill Cooldown").First();

                    SheriffCooldown = GameObject.Instantiate(killcd);
                    SheriffCooldown.gameObject.name = "SheriffCDText";
                    SheriffCooldown.TitleText.Text = "Sheriff Kill Cooldown";
                    SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
                    SheriffCooldown.ValueText.Text = CustomGameOptions.SheriffKillCD.ToString();


                    LLKOLCLGCBD[] options = new LLKOLCLGCBD[__instance.KJFHAPEDEBH.Count + 2];
                    __instance.KJFHAPEDEBH.ToArray().CopyTo(options, 0);
                    options[options.Length - 2] = showSheriffOption;
                    options[options.Length - 1] = SheriffCooldown;
                    __instance.KJFHAPEDEBH = new Il2CppReferenceArray<LLKOLCLGCBD>(options);
                
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void Postfix2(PHCKLDDNJNP __instance)
        {
            BCLDBBKFJPK showAnonymousvote = GameObject.FindObjectsOfType<BCLDBBKFJPK>().ToList().Where(x => x.TitleText.Text == "Anonymous Votes").First();
            if (SheriffCooldown != null & showSheriffOption != null){
                showSheriffOption.transform.position = showAnonymousvote.transform.position - new Vector3(0, 5.5f, 0);
                SheriffCooldown.transform.position = showAnonymousvote.transform.position - new Vector3(0, 6f, 0);
            }

        }
    }
    [HarmonyPatch]
    public static class ToggleButtonPatch
    {
        [HarmonyPatch(typeof(BCLDBBKFJPK), "Toggle")]
        public static bool Prefix(BCLDBBKFJPK __instance)
        {

            if (__instance.TitleText.Text == "Show Sheriff")
            {
                CustomGameOptions.showSheriff = !CustomGameOptions.showSheriff;
                FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);

                __instance.NHLMDAOEOAE = CustomGameOptions.showSheriff;
                __instance.CheckMark.enabled = CustomGameOptions.showSheriff;
                return false;

            }
            return true;

        }

    }
    [HarmonyPatch(typeof(PCGDGFIAJJI))]
    public static class NumberOptionPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Increase")]
        public static bool Prefix1(PCGDGFIAJJI __instance)
        {
            if (__instance.TitleText.Text == "Sheriff Kill Cooldown")
            {
                CustomGameOptions.SheriffKillCD = Math.Min(CustomGameOptions.SheriffKillCD + 2.5f, 40);
                FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                GameOptionMenuPatch.SheriffCooldown.NHLMDAOEOAE = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.ValueText.Text = CustomGameOptions.SheriffKillCD.ToString();
                return false;
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Decrease")]
        public static bool Prefix2(PCGDGFIAJJI __instance)
        {
            if (__instance.TitleText.Text == "Sheriff Kill Cooldown")
            {
                CustomGameOptions.SheriffKillCD = Math.Max(CustomGameOptions.SheriffKillCD - 2.5f, 10);

                FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                GameOptionMenuPatch.SheriffCooldown.NHLMDAOEOAE = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.ValueText.Text = CustomGameOptions.SheriffKillCD.ToString();


                return false;
            }

            return true;
        }
    }
}

