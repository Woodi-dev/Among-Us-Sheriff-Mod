using HarmonyLib;
using IntroCutScene = PENEIDJGGAF;
namespace SheriffMod
{
    [HarmonyPatch]
    public static class IntroCutScenePatch
    {
        [HarmonyPatch(typeof(IntroCutScene.CKACLKCOJFO), "MoveNext")]
        public static void Postfix(IntroCutScene.CKACLKCOJFO __instance)
        {
            if (PlayerController.getLocalPlayer().hasComponent("Sheriff"))
            {
                __instance.__this.Title.Text = "Sheriff";
                __instance.__this.Title.Color = Sheriff.color;
                __instance.__this.ImpostorText.Text = "Shoot the [FF0000FF]Impostor";
                __instance.__this.BackgroundBar.material.color = Sheriff.color;

            }
   

        }


    }
}
