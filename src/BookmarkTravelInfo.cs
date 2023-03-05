using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

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

        public static void AutoTravel(DynamicLayoutSlot latestFoundSlot)
        {
            InvokeTransit = false;

            if (latestFoundSlot?.AssignedCard?.CardModel?.IsTravellingCard ?? false)
            {
                if (Plugin.UseDoubleClick.Value == false)
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
                            if (_StopWatch.ElapsedMilliseconds < Plugin.DoubleClickMilliseconds.Value)
                            {
                                //bookmark was double clicked.
                                if (Plugin.ShowCardDialog.Value == false) InvokeTransit = true;

                                GraphicsManager.Instance.InspectCard(latestFoundSlot.AssignedCard);
                            }

                            _StopWatch.Restart();

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
    }
}
