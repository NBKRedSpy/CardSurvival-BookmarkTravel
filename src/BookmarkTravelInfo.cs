using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BookmarkTravel
{
    public static class BookmarkTravelInfo
    {
        /// <summary>
        /// Stopwatch for double click test
        /// </summary>
        private static Stopwatch _StopWatch = new Stopwatch();

        /// <summary>
        /// Last bookmark invoked.
        /// </summary>
        private static WeakReference<DynamicLayoutSlot> _PreviousBookmark = new WeakReference<DynamicLayoutSlot>(null);

        /// <summary>
        /// True if the code should invoke a transit
        /// </summary>
        public static bool InvokeTransit = false;

        private static void ResetLastCard()
        {
            _PreviousBookmark.SetTarget(null);
            _StopWatch.Restart();
        }

        /// <summary>
        /// This code handles the TravelMode.Hold invoke.
        /// If a key is held for the hold time duration, the location will be traveled to.
        /// </summary>
        public static void SetBookmarkKeyHeld()
        {


            if (Plugin.TravelMode.Value != TravelMode.Hold) return;

            InvokeTransit = false;

            var bookmarks = GraphicsManager.Instance?.Bookmarks;
            int bookmarkIndex = -1;

            if(bookmarks != null)
            {
                //Get the bookmark index for the key that is being pressed.
                int key = (int)KeyCode.Alpha1;  

                for (int i = 0; i < bookmarks.Length; i++, key++)
                {
                    if (Input.GetKey((KeyCode)key))
                    {
                        bookmarkIndex = i;
                        break;
                    }
                }
            }

            if (bookmarkIndex != -1)
            {
                var currentBookmark = bookmarks[bookmarkIndex];
                

                if (currentBookmark?.MarkedCard?.IsTravellingCard == false)
                {
                    ResetLastCard();
                    return;
                }

                if (_PreviousBookmark.TryGetTarget(out var previousBookmark))
                {

                    if (previousBookmark != currentBookmark)
                    {
                        ResetLastCard();
                        return;
                    }
                    else
                    {
                        if (_StopWatch.ElapsedMilliseconds > Plugin.DoubleClickMilliseconds.Value)
                        {
                            InvokeTransit = true;
                            GraphicsManager.Instance.InspectCard(previousBookmark.AssignedCard);

                            ResetLastCard();
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                ResetLastCard();
                return;
            }

        }
        public static void AutoTravel(DynamicLayoutSlot latestFoundSlot)
        {
            InvokeTransit = false;

            TravelMode travelMode = Plugin.TravelMode.Value;

            try
            {

                if (latestFoundSlot?.AssignedCard?.CardModel?.IsTravellingCard ?? false)
                {

                    if (travelMode == TravelMode.SingleClick)
                    {
                        if (Plugin.ShowCardDialog.Value == false) InvokeTransit = true;
                        GraphicsManager.Instance.InspectCard(latestFoundSlot.AssignedCard);
                    }
                    else
                    {
                        if (_PreviousBookmark.TryGetTarget(out DynamicLayoutSlot previousSlot))
                        {
                            if (previousSlot != latestFoundSlot)
                            {
                                //Different bookmark last time.  Not a double click
                                _PreviousBookmark.SetTarget(latestFoundSlot);
                                _StopWatch.Restart();
                            }
                            else
                            {
                                if (travelMode == TravelMode.DoubleClick)
                                {
                                    if (_StopWatch.ElapsedMilliseconds < Plugin.DoubleClickMilliseconds.Value)
                                    {
                                        //bookmark was double clicked.
                                        if (Plugin.ShowCardDialog.Value == false) InvokeTransit = true;

                                        GraphicsManager.Instance.InspectCard(latestFoundSlot.AssignedCard);

                                    }

                                    _StopWatch.Restart();
                                }
                                ////TravelMode.Hold is handled in SetBookmarkKeyHeld(). 
                                ////  However, can't filter it out at the top of this function because it seems the _PreviousBookmark must be set in the 
                                ////  FindBookmark() context.  
                                /// It could be filtered out, but it would duplicate the logic already present.
                                else
                                {
                                    throw new ApplicationException($"Unexpected TravelMode: {travelMode}");
                                }
                            }
                        }
                        else
                        {
                            //Null value.  Lost reference or not set yet.
                            _PreviousBookmark.SetTarget(latestFoundSlot);
                            _StopWatch.Restart();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Plugin.Log.LogError($"Error processing auto travel {ex}");
            }

        }
    }
}
