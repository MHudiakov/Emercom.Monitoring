// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProcessStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Стратегия обработки полей объекта
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System.Collections.Generic;

    using Init.Tools;

    /// <summary>
    /// Стратегия обработки полей объекта
    /// </summary>
    /// <typeparam name="T">
    /// Тип модели
    /// </typeparam>
    public abstract class ModelProcessStrategy<T>
        where T : class
    {
        /// <summary>
        /// Выолняет проврку правильности применения атрибута
        /// </summary>
        /// <param name="fields">
        /// Атрибуты обработки
        /// </param>
        public abstract void Validate(List<PropertyHelper<T>> fields);

        /// <summary>
        /// Произвести обработку над полем модели в соответствии со значением ProcessAttribute
        /// </summary>
        /// <param name="item">Объект над полем которого производится обработка</param>
        /// <param name="property">Свойство над которым производится обработка</param>
        /// <param name="args">Аргументы обработки</param>
        public abstract void Process(T item, PropertyHelper<T> property, Dictionary<string, object> args);
    }
}
