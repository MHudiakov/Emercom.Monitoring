// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataSourceUpdatedEventArgs.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Параметры события обновления источника данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Параметры события обновления источника данных
    /// </summary>
    /// <typeparam name="T">Тип элемента источника данных</typeparam>
    public class DataSourceUpdatedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Параметры события обновления источника данных
        /// </summary>
        /// <param name="newDataSource">Список элементов источника данных</param>
        public DataSourceUpdatedEventArgs(List<T> newDataSource)
        {
            if (newDataSource == null)
                throw new ArgumentNullException("newDataSource");

            this.NewDataSource = newDataSource;
        }

        /// <summary>
        /// Список элементов источника данных
        /// </summary>
        public List<T> NewDataSource { get; private set; }
    }
}