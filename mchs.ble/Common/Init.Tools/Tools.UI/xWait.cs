// --------------------------------------------------------------------------------------------------------------------
// <copyright file="xWait.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Helper для создания анимированной плашки ожадания на любом контроле
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Forms;

    using Init.Tools.UI.Forms;

    /// <summary>
    /// Helper для создания анимированной плашки ожадания на любом контроле
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
    // ReSharper disable once InconsistentNaming
    public static class xWait
    {
        /// <summary>
        /// Создать плашку ожидания на контроле
        /// </summary>
        /// <param name="control">
        /// Визуальный компонент
        /// </param>
        public static void BeginWait(this Control control)
        {
            if (control.Controls.OfType<ucWaitControl>().Any())
                return;

            var wc = new ucWaitControl();
            var x = control.Width / 2 - wc.Width / 2;
            var y = control.Height / 2 - wc.Height / 2;
            wc.Location = new System.Drawing.Point(x, y);

            foreach (Control t in control.Controls)
                t.Enabled = false;

            control.Controls.Add(wc);
            wc.BringToFront();
        }

        /// <summary>
        /// Удалить плашку ожидания с контрола
        /// </summary>
        /// <param name="control">
        /// Визуальный компонент
        /// </param>
        public static void EndWait(this Control control)
        {
            try
            {
                foreach (var waitControl in control.Controls.OfType<ucWaitControl>().ToList())
                    control.Controls.Remove(waitControl);
            }
            finally
            {
                foreach (Control t in control.Controls)
                    t.Enabled = true;
            }
        }
    }
}
