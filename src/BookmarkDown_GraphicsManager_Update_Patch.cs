using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BookmarkTravel
{

    [HarmonyPatch(typeof(GraphicsManager), "Update")]
    public static class BookmarkDown_GraphicsManager_Update_Patch
    {
        public static void Prefix()
        {

            BookmarkTravelInfo.SetBookmarkKeyHeld();
            
        }
    }
}
