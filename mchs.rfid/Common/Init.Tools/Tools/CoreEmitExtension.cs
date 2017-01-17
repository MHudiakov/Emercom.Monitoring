// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreEmitExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для Reflection.Emit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Расширение для Reflection.Emit
    /// </summary>
    public static class CoreEmitExtension
    {
        /// <summary>
        /// Генерирует метод установки значения
        /// </summary>
        /// <remarks>
        /// Метод будет иметь имя {namePreffix}{propertyInfo.Name}Setter
        /// И область видимости как у метода ownerType
        /// </remarks>
        /// <param name="propertyName">
        /// Имя свойства
        /// </param>
        /// <param name="namePreffix">
        /// Префикс имени метода. Если не указан, использвется T.FullName
        /// </param>
        /// <typeparam name="T">
        /// Тип свойства
        /// </typeparam>
        /// <returns>
        /// Метод установки свойства
        /// </returns>
        public static Action<T, object> GenerateSetter<T>(string propertyName, string namePreffix = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException(@"Свойство не найдено", "propertyName");
            if (propertyInfo.PropertyType != typeof(T))
                throw new ArgumentException(@"Тип свойства не соответствует типу метода", "propertyName");
            return GenerateSetter<T>(propertyInfo, namePreffix);
        }

        /// <summary>
        /// Генерирует метод установки значения
        /// </summary>
        /// <remarks>
        /// Метод будет иметь имя {namePreffix}{propertyInfo.Name}Setter
        /// И область видимости как у метода ownerType
        /// </remarks>
        /// <param name="propertyInfo">
        /// Свойство
        /// </param>
        /// <param name="namePreffix">
        /// Префикс имени метода. Если не указан, использвется T.FullName
        /// </param>
        /// <typeparam name="T">
        /// Тип свойства
        /// </typeparam>
        /// <returns>
        /// Метод установки свойства
        /// </returns>
        public static Action<T, object> GenerateSetter<T>(PropertyInfo propertyInfo, string namePreffix = "")
        {
            var name = string.Format("{0}{1}Setter", namePreffix, propertyInfo.Name);
            if (string.IsNullOrWhiteSpace(namePreffix))
                name = typeof(T).FullName + name;

            var identSetMethod = new DynamicMethod(name, typeof(void), new[] { typeof(T), typeof(object) }, typeof(T));

            identSetMethod.DefineParameter(1, ParameterAttributes.None, "item");
            identSetMethod.DefineParameter(2, ParameterAttributes.None, "value");
            var g = identSetMethod.GetILGenerator();
            g.Emit(OpCodes.Ldarg_0);
            g.Emit(OpCodes.Ldarg_1);
            if (propertyInfo.PropertyType.IsValueType)
                g.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
            g.EmitCall(OpCodes.Callvirt, propertyInfo.GetSetMethod(), null);
            g.Emit(OpCodes.Ret);
            var setter = (Action<T, object>)identSetMethod.CreateDelegate(typeof(Action<T, object>));
            return setter;
        }

        /// <summary>
        /// Генерирует метод получения значения
        /// </summary>
        /// <remarks>
        /// Метод будет иметь имя {namePreffix}{propertyInfo.Name}Setter
        /// И область видимости как у метода ownerType
        /// </remarks>
        /// <param name="propertyName">
        /// Свойство
        /// </param>
        /// <param name="namePreffix">
        /// Префикс имени метода. Если не указан, использвется T.FullName
        /// </param>
        /// <typeparam name="T">
        /// Тип свойства
        /// </typeparam>
        /// <returns>
        /// Метод получения свойства
        /// </returns>
        public static Func<T, object> GenerateGetter<T>(string propertyName, string namePreffix = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException(@"Свойство не найдено", "propertyName");
            if (propertyInfo.PropertyType != typeof(T))
                throw new ArgumentException(@"Тип свойства не соответствует типу метода", "propertyName");
            return GenerateGetter<T>(propertyInfo, namePreffix);
        }

        /// <summary>
        /// Генерирует метод получения значения
        /// </summary>
        /// <remarks>
        /// Метод будет иметь имя {namePreffix}{propertyInfo.Name}Setter
        /// И область видимости как у метода ownerType
        /// </remarks>
        /// <param name="propertyInfo">
        /// Свойство
        /// </param>
        /// <param name="namePreffix">
        /// Префикс имени метода. Если не указан, использвется T.FullName
        /// </param>
        /// <typeparam name="T">
        /// Тип свойства
        /// </typeparam>
        /// <returns>
        /// Метод получения свойства
        /// </returns>
        public static Func<T, object> GenerateGetter<T>(PropertyInfo propertyInfo, string namePreffix = "")
        {
            var name = string.Format("{0}{1}Getter", namePreffix, propertyInfo.Name);
            if (string.IsNullOrWhiteSpace(namePreffix))
                name = typeof(T).FullName + name;

            var getMethod = new DynamicMethod(name, typeof(object), new[] { typeof(T) }, typeof(T));

            getMethod.DefineParameter(1, ParameterAttributes.None, "item");
            var getGenerator = getMethod.GetILGenerator();
            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.EmitCall(OpCodes.Callvirt, propertyInfo.GetGetMethod(), null);
            getGenerator.Emit(OpCodes.Box, propertyInfo.PropertyType);
            getGenerator.Emit(OpCodes.Ret);
            var getter = (Func<T, object>)getMethod.CreateDelegate(typeof(Func<T, object>));
            return getter;
        }
    }
}
