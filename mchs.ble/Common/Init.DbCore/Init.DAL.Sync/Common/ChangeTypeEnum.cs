﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeTypeEnum.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Init.DAL.Sync.Common
{
    /// <summary>
    /// Тип операции над объектом
    /// </summary>
    public enum ChangeTypeEnum
    {
        /// <summary>
        /// Добавление
        /// </summary>
        Add = 0, 

        /// <summary>
        /// Редактирование
        /// </summary>
        Edit = 1, 

        /// <summary>
        /// Удаление
        /// </summary>
        Delete = 2
    }
}