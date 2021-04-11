using HarmonyLib;

namespace SheriffMod
{

    //Here we display the Sheriff role
    [HarmonyPatch]
    public static class IntroCutScenePatch
    {
         [HarmonyPatch(typeof(IntroCutscene.MDIMNFHLFBN), "MoveNext")]
         public static void Postfix(IntroCutscene.MDIMNFHLFBN __instance)
         {
            var localplayer = PlayerController.getLocalPlayer();
            if (localplayer == null) return;
             if (localplayer.hasComponent("Sheriff"))
             {
                if (__instance.__4__this == null) return;
                 __instance.__4__this.Title.Text = "Sheriff";
                 __instance.__4__this.Title.Color = Sheriff.color;
                 __instance.__4__this.ImpostorText.Text = "Shoot the [FF0000FF]Impostor";
                 __instance.__4__this.BackgroundBar.material.color = Sheriff.color;

             }


         }
        

    }
}
