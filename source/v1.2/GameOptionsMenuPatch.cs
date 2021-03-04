using HarmonyLib;
using System;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;

using OptionBevavior = LLKOLCLGCBD;

using ToggleOption = BCLDBBKFJPK;
using NumberOption = PCGDGFIAJJI;
using CustomPlayerMenu = AANMMDGMFEL;
using GameOptionsMenu = PHCKLDDNJNP;

namespace SheriffMod
{
    [HarmonyPatch(typeof(GameOptionsMenu))]
    public static class GameOptionMenuPatch
    {
        public static ToggleOption showSheriffOption;
        public static NumberOption SheriffCooldown;
        public static GameOptionsMenu instance;

        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Postfix1(GameOptionsMenu __instance)
        {
            instance = __instance;
            CustomPlayerMenuPatch.AddOptions();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void Postfix2(PHCKLDDNJNP __instance)
        {

            OptionBevavior option = __instance.KJFHAPEDEBH[__instance.KJFHAPEDEBH.Count-3];
            if (SheriffCooldown != null & showSheriffOption != null){
                showSheriffOption.transform.position = option.transform.position - new Vector3(0, 2f, 0);
                SheriffCooldown.transform.position = option.transform.position - new Vector3(0, 2.5f, 0);
            }

        }
    }
    [HarmonyPatch]
    public static class ToggleButtonPatch
    {
        [HarmonyPatch(typeof(ToggleOption), "Toggle")]
        public static bool Prefix(ToggleOption __instance)
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
    [HarmonyPatch(typeof(NumberOption))]
    public static class NumberOptionPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Increase")]
        public static bool Prefix1(NumberOption __instance)
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
        public static bool Prefix2(NumberOption __instance)
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

    [HarmonyPatch(typeof(CustomPlayerMenu))]
    public class CustomPlayerMenuPatch
    {
        public static void deleteOptions(bool destroy)
        {
            if (GameOptionMenuPatch.showSheriffOption != null && GameOptionMenuPatch.SheriffCooldown)
            {
                GameOptionMenuPatch.SheriffCooldown.gameObject.SetActive(false);
                GameOptionMenuPatch.showSheriffOption.gameObject.SetActive(false);
                if (destroy) { 
                  GameObject.Destroy(GameOptionMenuPatch.SheriffCooldown);
                  GameObject.Destroy(GameOptionMenuPatch.showSheriffOption);
                  GameOptionMenuPatch.SheriffCooldown = null;
                  GameOptionMenuPatch.showSheriffOption = null;
                }
            }

        }
        public static void AddOptions()
        {
            if (GameOptionMenuPatch.showSheriffOption == null | GameOptionMenuPatch.SheriffCooldown == null)
            {
                var showAnonymousvote = GameObject.FindObjectsOfType<ToggleOption>().ToList().Where(x => x.TitleText.Text == "Anonymous Votes").First();
                GameOptionMenuPatch.showSheriffOption = GameObject.Instantiate(showAnonymousvote);
                var killcd = GameObject.FindObjectsOfType<NumberOption>().ToList().Where(x => x.TitleText.Text == "Kill Cooldown").First();
                GameOptionMenuPatch.SheriffCooldown = GameObject.Instantiate(killcd);

                OptionBevavior[] options = new OptionBevavior[GameOptionMenuPatch.instance.KJFHAPEDEBH.Count + 2];
                GameOptionMenuPatch.instance.KJFHAPEDEBH.ToArray().CopyTo(options, 0);
                options[options.Length - 2] = GameOptionMenuPatch.showSheriffOption;
                options[options.Length - 1] = GameOptionMenuPatch.SheriffCooldown;
                GameOptionMenuPatch.instance.KJFHAPEDEBH = new Il2CppReferenceArray<OptionBevavior>(options);

            }
            else
            {
                GameOptionMenuPatch.SheriffCooldown.gameObject.SetActive(true);
                GameOptionMenuPatch.showSheriffOption.gameObject.SetActive(true);
            }

            GameOptionMenuPatch.showSheriffOption.TitleText.Text = "Show Sheriff";
            GameOptionMenuPatch.showSheriffOption.NHLMDAOEOAE = CustomGameOptions.showSheriff;
            GameOptionMenuPatch.showSheriffOption.CheckMark.enabled = CustomGameOptions.showSheriff;

            GameOptionMenuPatch.SheriffCooldown.TitleText.Text = "Sheriff Kill Cooldown";
            GameOptionMenuPatch.SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
            GameOptionMenuPatch.SheriffCooldown.ValueText.Text = CustomGameOptions.SheriffKillCD.ToString();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Close")]
        public static void Postfix1(CustomPlayerMenu __instance, bool MDKEGLADENC)
        {

            deleteOptions(true);
        }

        [HarmonyPrefix]
        [HarmonyPatch("OpenTab")]
        public static void Prefix1(GameObject JDBBMFAJIII)
        {

            if (JDBBMFAJIII.name == "GameGroup" && GameOptionMenuPatch.instance!=null)
            {
                AddOptions();
            }
            else {
                deleteOptions(false);
            }
            
        }

    }
}

