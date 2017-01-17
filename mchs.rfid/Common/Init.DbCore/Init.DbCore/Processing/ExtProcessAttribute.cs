// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtProcessAttribute.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ����������� ������� ��� ������� ��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ����������� ������� ��� ������� ��������
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public abstract class ExtProcessAttribute : ProcessAttribute
    {
        /// <summary>
        /// ����������� ������� ��� ������� ��������
        /// </summary>
        /// <param name="propertyName">�������� ���� � �������� ����� ����������� ���������</param>
        /// <param name="args">��������� ����������</param>
        protected ExtProcessAttribute(string propertyName, Dictionary<string, object> args)
            : base(args)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");

            this.PropertyName = propertyName;
        }

        /// <summary>
        /// �������� ���� � �������� ����� ����������� ���������
        /// </summary>
        public string PropertyName { get; private set; }
    }
}