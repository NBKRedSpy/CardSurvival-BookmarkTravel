using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace BookmarkTravel
{
    [HarmonyPatch(typeof(InspectionPopup), nameof(InspectionPopup.Setup), typeof(InGameCardBase))]
    public static class InspectionPopUp_Setup_Patch
    {
        public static void Postfix(InspectionPopup __instance)
        {
            if (BookmarkTravelInfo.InvokeTransit)
            {
                BookmarkTravelInfo.InvokeTransit = false;

                //Travel cards are expected to always have one button, which is to travel.
                __instance.OnButtonClicked(0, false);
            }
        }

    }
}
