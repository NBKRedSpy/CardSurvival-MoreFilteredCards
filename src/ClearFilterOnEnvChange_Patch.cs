using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace MoreFilteredCards
{
    //private IEnumerator ChangeEnvironment()

    [HarmonyPatch]
    public static class ClearFilterOnEnvChange_Patch
    {
        [HarmonyPatch(typeof(GameManager), "ChangeEnvironment")]
        public static void Prefix()
        {

            // When changing the environment, clear out any existing filters.
            //Fixes issue where exiting a structure with a filter will cause the location card line to disappear.
            MBSingleton<GraphicsManager>.Instance.ClearFilterTags();
        }
    }
}
