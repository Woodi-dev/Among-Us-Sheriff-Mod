using HarmonyLib;

namespace SheriffMod
{
    //Here we display the Sheriff role
    [HarmonyPatch]
    public static class IntroCutScenePatch
    {
         [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__11), "MoveNext")]
         public static void Postfix(IntroCutscene._CoBegin_d__11 __instance) 
         {
             if (Sheriff.isSheriff(PlayerController.LocalPlayer))
             {
                 if (__instance.__4__this == null) return;
                 __instance.__4__this.Title.text = "Sheriff";
                 __instance.__4__this.Title.color = Sheriff.color;
                 __instance.__4__this.ImpostorText.text = "Shoot the <color=#FF0000FF>Impostor</color>";
                 __instance.__4__this.BackgroundBar.material.color = Sheriff.color;
             }
         }
    }
}
