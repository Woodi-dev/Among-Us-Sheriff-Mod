using HarmonyLib;

namespace SheriffMod
{
    [HarmonyPatch]
    public static class IntroCutScenePatch
    {
         [HarmonyPatch(typeof(IntroCutscene.OIBLPHFGCPC), "MoveNext")]
         public static void Postfix(IntroCutscene.OIBLPHFGCPC __instance)
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
