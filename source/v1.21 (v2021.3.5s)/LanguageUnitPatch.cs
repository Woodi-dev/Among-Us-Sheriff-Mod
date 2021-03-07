using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnhollowerBaseLib;

namespace SheriffMod
{


    public enum CustomStringNames {

        CustomServerName = 3000
        }
    [HarmonyPatch]
    public class LanguageUnitPatch
    {


        [HarmonyPatch(typeof(FLHICDBPHLO),"HOGGPANBLGP")]
        public static bool Prefix(StringNames LENBLADGELB, string NNLLKLDPBAI, Il2CppReferenceArray<Il2CppSystem.Object> DKBJCINDDCD, ref string __result)
        {

            if (LENBLADGELB == (StringNames)CustomStringNames.CustomServerName)
            {
                __result = SheriffMod.Name.Value;
                return false;
            }         
            return true;
        }
  
    }
}
