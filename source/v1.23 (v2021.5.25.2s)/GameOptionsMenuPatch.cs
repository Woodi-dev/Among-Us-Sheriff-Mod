using HarmonyLib;
using System;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;

namespace SheriffMod
{
    [HarmonyPatch(typeof(GameOptionsMenu))]
    public static class GameOptionMenuPatch
    {
        public static ToggleOption showSheriffOption;
        public static NumberOption SheriffCooldown;
        public static GameOptionsMenu instance;

        //Lobby options menu get opened
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Postfix1(GameOptionsMenu __instance)
        {
            instance = __instance;
            CustomPlayerMenuPatch.AddOptions();
        }

        //Not a good solution, but works. Set both options to the bottom.
        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void Postfix2(GameOptionsMenu __instance)
        {
            OptionBehaviour option = __instance.Children[__instance.Children.Count - 3];

            if (SheriffCooldown != null & showSheriffOption != null){
                showSheriffOption.transform.position = option.transform.position - new Vector3(0, 0.5f, 0);
                SheriffCooldown.transform.position = option.transform.position - new Vector3(0, 1f, 0);
            }
        }
    }

    //Change the toggle behaviour of our custom options.
    [HarmonyPatch]
    public static class ToggleButtonPatch
    {
        [HarmonyPatch(typeof(ToggleOption), "Toggle")]
        public static bool Prefix(ToggleOption __instance)
        {
            if (__instance.TitleText.text == "Show Sheriff")
            {
                CustomGameOptions.showSheriff = !CustomGameOptions.showSheriff;
                PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);

                __instance.oldValue = CustomGameOptions.showSheriff;
                __instance.CheckMark.enabled = CustomGameOptions.showSheriff;
                return false;
            }
            return true;
        }
    }

    //Change the increase/decrease behaviour of our custom options.
    [HarmonyPatch(typeof(NumberOption))]
    public static class NumberOptionPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Increase")]
        public static bool Prefix1(NumberOption __instance)
        {
            if (__instance.TitleText.text == "Sheriff Kill Cooldown")
            {
                CustomGameOptions.SheriffKillCD = Math.Min(CustomGameOptions.SheriffKillCD + 2.5f, 40);
                PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);
                GameOptionMenuPatch.SheriffCooldown.oldValue = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.ValueText.text = CustomGameOptions.SheriffKillCD.ToString();
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Decrease")]
        public static bool Prefix2(NumberOption __instance)
        {
            if (__instance.TitleText.text == "Sheriff Kill Cooldown")
            {
                CustomGameOptions.SheriffKillCD = Math.Max(CustomGameOptions.SheriffKillCD - 2.5f, 10);
                PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);
                GameOptionMenuPatch.SheriffCooldown.oldValue = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
                GameOptionMenuPatch.SheriffCooldown.ValueText.text = CustomGameOptions.SheriffKillCD.ToString();
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
                //we simply copy existing options...
                var toggleopt = GameObject.FindObjectsOfType<ToggleOption>().ToList().First();
                GameOptionMenuPatch.showSheriffOption = GameObject.Instantiate(toggleopt);
                var killcd = GameObject.FindObjectsOfType<NumberOption>().ToList().First();
                GameOptionMenuPatch.SheriffCooldown = GameObject.Instantiate(killcd);

                OptionBehaviour[] options = new OptionBehaviour[GameOptionMenuPatch.instance.Children.Count + 2];
                GameOptionMenuPatch.instance.Children.ToArray().CopyTo(options, 0);
                options[options.Length - 2] = GameOptionMenuPatch.showSheriffOption;
                options[options.Length - 1] = GameOptionMenuPatch.SheriffCooldown;
                GameOptionMenuPatch.instance.Children = new Il2CppReferenceArray<OptionBehaviour>(options);
            }
            else
            {
                GameOptionMenuPatch.SheriffCooldown.gameObject.SetActive(true);
                GameOptionMenuPatch.showSheriffOption.gameObject.SetActive(true);
            }

            GameOptionMenuPatch.showSheriffOption.TitleText.text = "Show Sheriff";
            GameOptionMenuPatch.showSheriffOption.oldValue = CustomGameOptions.showSheriff;
            GameOptionMenuPatch.showSheriffOption.CheckMark.enabled = CustomGameOptions.showSheriff;

            GameOptionMenuPatch.SheriffCooldown.TitleText.text = "Sheriff Kill Cooldown";
            GameOptionMenuPatch.SheriffCooldown.Value = CustomGameOptions.SheriffKillCD;
            GameOptionMenuPatch.SheriffCooldown.ValueText.text = CustomGameOptions.SheriffKillCD.ToString();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Close")]
        public static void Postfix1(CustomPlayerMenu __instance, bool canMove)
        {
            deleteOptions(true);
        }

        //if we switch tabs our custom buttons get deleted
        [HarmonyPrefix]
        [HarmonyPatch("OpenTab")]
        public static void Prefix1(GameObject tab)
        {
            if (tab.name == "GameGroup" && GameOptionMenuPatch.instance != null)
            {
                AddOptions();
            }
            else {
                deleteOptions(false);
            }
        }
    }
}
