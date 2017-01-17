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
        /// ������� �����
        /// </summary>
        None = 0,
        /// <summary>
        /// ���� �� ������
        /// </summary>
        Start = 1,
        /// <summary>
        /// ���� � �����
        /// </summary>
        End = 2,
        /// <summary>
        /// ���� � � ������ � � �����
        /// </summary>
        Both = 3
    }
}