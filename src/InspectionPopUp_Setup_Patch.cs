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

                //Check if it is too dark to transit. 
                //  Currently most of the logic is in creating the Dismantle buttons on the inspection popup it self,
                //  so this is a hack, but works.
                List<DismantleActionButton> optionButtons = new Traverse(__instance).Field<List<DismantleActionButton>>("OptionsButtons")
                    .Value;

                if(optionButtons?.Count > 0 && optionButtons[0].Interactable == true)
                {
                    //Travel cards are expected to always have one button, which is to travel.
                    __instance.OnButtonClicked(0, false);
                }
            }
        }

    }
}
