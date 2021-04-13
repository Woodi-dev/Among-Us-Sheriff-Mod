using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnhollowerBaseLib;

using LanguageUnit = LCCPIGHHLOH;
namespace SheriffMod
{


    public enum CustomStringNames {

        CustomServerName = 3000
    }
    [HarmonyPatch]
    public class LanguageUnitPatch
    {

        //There might be a better solution to this. Here we return string value "custom server name" for server regions.
        [HarmonyPatch(typeof(LanguageUnit), "KCCAEECMGNJ")]
        public static bool Prefix(StringNames BPHOFEKLDLF, string BIHKOFGIABL, Il2CppReferenceArray<Il2CppSystem.Object> FHLKFONKJLH, ref string __result)
        {

            if (BPHOFEKLDLF == (StringNames)CustomStringNames.CustomServerName)
            {
                __result = SheriffMod.Name.Value;
                return false;
            }         
            return true;
        }
  
    }
}
