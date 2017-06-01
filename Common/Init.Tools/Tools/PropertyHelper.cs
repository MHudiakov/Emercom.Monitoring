// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Вспомогательный класс для выполнения операций со cвойствами объекта
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Вспомогательный класс для выполнения операций со cвойствами объекта
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта-владельца свойства
    /// </typeparam>
    public class PropertyHelper<T> : IEquatable<PropertyHelper<T>>
    {
        /// <summary>
        /// Вспомогательный класс для выполнения операций со Свойствами объекта
        /// </summary>
        /// <param name="propertyName">
        /// Название свойства
        /// </param>
        /// <param name="namePreffix">
        /// Префикс имени методов установки/чтения значений
        /// </param>
        public PropertyHelper(string propertyName, string namePreffix = "")
            : this(typeof(T).GetProperty(propertyName), namePreffix)
        {
        }

        /// <summary>
        /// Вспомогательный класс для выполнения операций со Свойствами объекта
        /// </summary>
        /// <param name="propertyInfo">
        /// Свойство в CLR объекте, соответствуюущее колонке в БД
        /// </param>
        /// <param name="namePreffix">
        /// Префикс имени методов установки/чтения значений
        /// </param>
        public PropertyHelper(PropertyInfo propertyInfo, string namePreffix = "")
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");
            if (!(propertyInfo.DeclaringType == typeof(T) || propertyInfo.DeclaringType.IsSubclassOf(typeof(T))))
                throw new ArgumentException(string.Format(@"Тип владельца свойства [{0}] свойства не соответсвует типу [{1}]", propertyInfo.DeclaringType.FullName, typeof(T).FullName), "propertyInfo");

            this.PropertyInfo = propertyInfo;

            // generate Setter
            var setter = CoreEmitExtension.GenerateSetter<T>(propertyInfo, namePreffix);
            this.Setter = setter;

            // generate getter
            var getter = CoreEmitExtension.GenerateGetter<T>(propertyInfo, namePreffix);
            this.Getter = getter;
        }

        /// <summary>
        /// Метод получения значения
        /// </summary>
        public Func<T, object> Getter { get; private set; }

        /// <summary>
        /// Метод установки значения
        /// </summary>
        public Action<T, object> Setter { get; private set; }

        /// <summary>
        /// Свойство в CLR объекте
        /// </summary>
        public PropertyInfo PropertyInfo { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals(PropertyHelper<T> other)
        {
            if (other == null) return false;
            return this.PropertyInfo == other.PropertyInfo;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is PropertyHelper<T>)) return false;
            return this.Equals((PropertyHelper<T>)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return this.PropertyInfo.GetHashCode();
        }
    }
}