using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnhollowerBaseLib;

using LanguageUnit = DBHDFNDBHKE;
namespace SheriffMod
{


    public enum CustomStringNames {

        CustomServerName = 3000
    }
    [HarmonyPatch]
    public class LanguageUnitPatch
    {

        //There might be a better solution to this. Here we return string value "custom server name" for server regions.
        [HarmonyPatch(typeof(LanguageUnit), "JJAOKBPDCFG")]
        public static bool Prefix(StringNames MBPOLHMKKJC, string FFCEMBMJGIP, Il2CppReferenceArray<Il2CppSystem.Object> BPBFAAEIABN, ref string __result)
        {

            if (MBPOLHMKKJC == (StringNames)CustomStringNames.CustomServerName)
            {
                __result = SheriffMod.Name.Value;
                return false;
            }         
            return true;
        }
  
    }
}
