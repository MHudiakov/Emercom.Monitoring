// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Core.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Helper класс с набором вспомагательных методов для класса Object
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System.Linq;
    using System.Windows.Forms;

    using Init.Tools.UI.Forms;

    /// <summary>
    /// Helper класс с набором вспомагательных методов для класса Object
    /// </summary>
    public static class Core
    {
        /// <summary>
        /// Сравнивает два объекта по значению свойств с целью определить их равенство
        /// </summary>
        /// <param name="obj">
        /// Первый объект
        /// </param>
        /// <param name="equalObject">
        /// Второй объект
        /// </param>
        /// <returns>
        /// Возвращает true при равенстве всех свойств объектов, иначе возвращается false 
        /// </returns>
        public static bool AreEqual(this object obj, object equalObject)
        {
            if (obj.GetType() == equalObject.GetType())
            {
                var properties = obj.GetType().GetProperties().ToList();

                foreach (var propertyInfo in properties)
                {
                    var thisProperty = propertyInfo.GetValue(obj, null);
                    var otherProperty = propertyInfo.GetValue(equalObject, null);
                    if (thisProperty != null && otherProperty != null)
                    {
                        if (thisProperty.ToString() != otherProperty.ToString())
                        {
                            return false;
                        }
                    }
                    else if ((thisProperty != null) || (otherProperty != null))
                    {
                        return false;
                    }
                }
            }
            else
                return false;

            return true;
        }

        /// <summary>
        /// Переводит контрол в неактивное состояние
        /// </summary>
        /// <param name="control">
        /// Контрол
        /// </param>
        public static void Disable(this Control control)
        {
            var sb = control;
            sb.Enabled = false;
            if (sb.Parent.Controls.Count > 0)
                sb.Parent.Controls[0].Focus();
        }

        /// <summary>
        /// Переводит конрол в активное состояние
        /// </summary>
        /// <param name="control">
        /// Контрол
        /// </param>
        public static void Enable(this Control control)
        {
            var sb = control;
            sb.Enabled = true;
            sb.Focus();
        }

        /// <summary>
        /// Отображает плашку загрузки для формы, пока форма не будет полностью загружена
        /// </summary>
        /// <param name="fm">
        /// Форма
        /// </param>
        public static void ShowLoadingSplash(this Form fm)
        {
            fmLoading.BeginDisplay();
            fm.ControlAdded += (sender, e) => fmLoading.EndDisplay();
            fm.Shown += (sender, args) => fmLoading.EndDisplay();
        }
    }
}
