//------------------------------------------
// ArrowEnds.cs (c) 2007 by Charles Petzold
//------------------------------------------
using System;

namespace xGraphic.ArrowLines
{
    /// <summary>
    ///     Indicates which end of the line has an arrow.
    /// </summary>
    [Flags]
    public enum ArrowEnds
    {
        /// <summary>
        /// ќбычна€ лини€
        /// </summary>
        None = 0,
        /// <summary>
        /// «нак на начале
        /// </summary>
        Start = 1,
        /// <summary>
        /// «нак в конце
        /// </summary>
        End = 2,
        /// <summary>
        /// «нак и в начале и в конце
        /// </summary>
        Both = 3
    }
}