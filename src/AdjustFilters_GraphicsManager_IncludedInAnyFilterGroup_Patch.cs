using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;

namespace MoreFilteredCards
{
    [HarmonyPatch(typeof(GraphicsManager), "IncludedInAnyFilterGroup")]
    public static class AdjustFilters_GraphicsManager_IncludedInAnyFilterGroup_Patch
    {
        public static void Postfix(GraphicsManager __instance, CardSlot _Slot, ref bool __result)
        {
            try
            {
                if (__result) { return; }


                //Pre-created slots with no cards assigned.
                if (_Slot.AssignedCard == null)
                {
                    __result = true;
                    return;
                }

                CardData cardModel = _Slot.AssignedCard.CardModel;

                //Containers like baskets
                if (Plugin.IncludeContainers.Value && (cardModel?.CardTags?.ToList().Any(x => x != null && x.name == "tag_Bag") ?? false))
                {
                    __result = true;
                    return;
                }

                if (Plugin.IncludeBlueprints.Value && _Slot.AssignedCard.IsBlueprintInstance)
                {
                    __result = true;
                    return;
                }

                //Include sheds, cellars, travel destinations.
                if (Plugin.IncludeTravelLocations.Value && cardModel.GetTravelDestination != null)
                {
                    __result = true;
                    return;
                }

                return;
            }
            catch (Exception ex)
            {
                Plugin.LogInfo(ex.ToString());
                throw;
            }
        }

    }
}
