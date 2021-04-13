using HarmonyLib;

namespace SheriffMod
{

    //Here we display the Sheriff role
    [HarmonyPatch]
    public static class IntroCutScenePatch
    {
         [HarmonyPatch(typeof(IntroCutscene.EMGDLDOHGCK), "MoveNext")]
         public static void Postfix(IntroCutscene.EMGDLDOHGCK __instance) 
         {
            var localplayer = PlayerController.getLocalPlayer();
            if (localplayer == null) return;
             if (localplayer.hasComponent("Sheriff"))
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
