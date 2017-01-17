// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtDbAttribute.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   ������� ����� �������� �������� ��
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Metadata
{
    using System;

    /// <summary>
    /// ������� ����� �������� �������� ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public abstract class ExtDbAttribute : DbAttribute
    {
        /// <summary>
        /// ��� �������� � �������� ����������� �������
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// ������� ����� �������� �������� ��
        /// </summary>
        /// <param name="fieldName">��� �������� � �������� ����������� �������</param>
        protected ExtDbAttribute(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentNullException("fieldName");

            this.FieldName = fieldName;
        }
    }
}