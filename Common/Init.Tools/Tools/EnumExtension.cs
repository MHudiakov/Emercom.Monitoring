// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для класса Enum
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Расширение для класса Enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Возвращает описание перечисления
        /// </summary>
        /// <param name="enumElement">Перечисление</param>
        /// <returns>Описание</returns>
        public static string GetDescription(this Enum enumElement)
        {
            var enumType = enumElement.GetType();

            var memberInfo = enumType.GetMember(enumElement.ToString()).SingleOrDefault();

            if (memberInfo != null)
            {
                var descriptionAttribute = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault();

                if (descriptionAttribute != null)
                    return descriptionAttribute.Description;
            }

            return enumElement.ToString();
        }
    }
}
