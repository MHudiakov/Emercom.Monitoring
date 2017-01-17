// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbIdentityAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Этим атрибутом отмечаются автогенерируемые базой поля
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Metadata
{
    using System;

    /// <summary>
    /// Этим атрибутом отмечаются автогенерируемые базой поля
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DbIdentityAttribute : DbAttribute
    {
    }
}