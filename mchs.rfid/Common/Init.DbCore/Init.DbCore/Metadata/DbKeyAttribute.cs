// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbKeyAttribute.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   ���� ��������� ���������� �������� ����
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Metadata
{
    using System;

    /// <summary>
    /// ���� ��������� ���������� �������� ����
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DbKeyAttribute : DbAttribute
    {
    }
}