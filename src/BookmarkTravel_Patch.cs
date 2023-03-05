using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace BookmarkTravel
{
    [HarmonyPatch(typeof(BookmarkGraphics), nameof(BookmarkGraphics.FindBookmark))]
    public static class BookmarkTravel_Patch
    {
        private static FieldInfo LatestFoundSlotInfo; 

        static BookmarkTravel_Patch()
        {
            LatestFoundSlotInfo = AccessTools.Field(typeof(BookmarkGraphics), "LatestFoundSlot");
        }

        public static void Postfix(BookmarkGraphics __instance)
        {
            BookmarkTravelInfo.AutoTravel((DynamicLayoutSlot)LatestFoundSlotInfo.GetValue(__instance));
        }
    }
}
