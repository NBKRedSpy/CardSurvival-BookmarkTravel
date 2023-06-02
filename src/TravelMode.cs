using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkTravel
{
    public enum TravelMode
    {
        /// <summary>
        ///A single click will travel immediately.
        ////// </summary>
        SingleClick  = 1,

        /// <summary>
        ///Double click will instantly travel.
        ////// </summary>
        DoubleClick,

        /// <summary>
        /// Holding the key will instantly travel.
        /// </summary>
        Hold
    }
}
