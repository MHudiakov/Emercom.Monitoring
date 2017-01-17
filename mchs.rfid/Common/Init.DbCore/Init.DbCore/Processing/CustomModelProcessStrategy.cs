// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomModelProcessStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базовый класс стратегии обработки с предпроверкой типов свойств
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;

    using Init.Tools;

    /// <summary>
    /// Базовый класс стратегии обработки с предпроверкой типов свойств
    /// </summary>
    /// <typeparam name="T">Тип модели</typeparam>
    /// <typeparam name="TField">Тип проверяемого ствойства</typeparam>
    public abstract class CustomModelProcessStrategy<T, TField> : ModelProcessStrategy<T>
        where T : class
    {
        /// <summary>
        /// Произвести обработку над полем объекта в соответствии со значение ProcessAttribute
        /// </summary>
        /// <param name="item">Объект над полем которого производится обработка</param>
        /// <param name="property">Данные по свойству над которым производится обработка</param>
        /// <param name="args">Данные по атрибуту</param>
        public override void Process(T item, PropertyHelper<T> property, Dictionary<string, object> args)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (property == null)
                throw new ArgumentNullException("property");

            if (args == null)
                throw new ArgumentNullException("args");

            this.ProcessOverride(item, property, args);
        }

        /// <summary>
        /// Произвести обработку над полем объекта в соответствии со значение ProcessAttribute
        /// </summary>
        /// <param name="item">Объект над полем которого производится обработка</param>
        /// <param name="property">Данные по свойству над которым производится обработка</param>
        /// <param name="args">Данные по атрибуту</param>
        public abstract void ProcessOverride(T item, PropertyHelper<T> property, Dictionary<string, object> args);

        /// <summary>
        /// Проверка на возможность выполнения обработки
        /// </summary>
        /// <param name="fields">Данные по полю над которым должна проводиться обработка</param>
        public override void Validate(List<PropertyHelper<T>> fields)
        {
            if (fields == null)
                throw new ArgumentNullException("fields");

            foreach (var field in fields)
            {
                if (!field.PropertyInfo.PropertyType.IsSubclassOf(typeof(TField))
                    && field.PropertyInfo.PropertyType != typeof(TField))
                    throw new ArgumentException(string.Format("Тип свойства [{0}:{1}] не соответствует типу свойств обрабатываемых стратегией [{2}]", field.PropertyInfo.Name, field.PropertyInfo.PropertyType, typeof(TField)), "fields");
                this.ValidateOverride(field);
            }
        }

        /// <summary>
        /// Проверка на возможность выполнения обработки
        /// </summary>
        /// <param name="properties">Данные по полю над которым должна проводиться обработка</param>
        public virtual void ValidateOverride(PropertyHelper<T> properties)
        {
        }
    }
}