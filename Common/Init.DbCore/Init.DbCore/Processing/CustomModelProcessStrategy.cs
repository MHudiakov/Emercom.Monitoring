// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomModelProcessStrategy.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ������� ����� ��������� ��������� � ������������� ����� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;

    using Init.Tools;

    /// <summary>
    /// ������� ����� ��������� ��������� � ������������� ����� �������
    /// </summary>
    /// <typeparam name="T">��� ������</typeparam>
    /// <typeparam name="TField">��� ������������ ���������</typeparam>
    public abstract class CustomModelProcessStrategy<T, TField> : ModelProcessStrategy<T>
        where T : class
    {
        /// <summary>
        /// ���������� ��������� ��� ����� ������� � ������������ �� �������� ProcessAttribute
        /// </summary>
        /// <param name="item">������ ��� ����� �������� ������������ ���������</param>
        /// <param name="property">������ �� �������� ��� ������� ������������ ���������</param>
        /// <param name="args">������ �� ��������</param>
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
        /// ���������� ��������� ��� ����� ������� � ������������ �� �������� ProcessAttribute
        /// </summary>
        /// <param name="item">������ ��� ����� �������� ������������ ���������</param>
        /// <param name="property">������ �� �������� ��� ������� ������������ ���������</param>
        /// <param name="args">������ �� ��������</param>
        public abstract void ProcessOverride(T item, PropertyHelper<T> property, Dictionary<string, object> args);

        /// <summary>
        /// �������� �� ����������� ���������� ���������
        /// </summary>
        /// <param name="fields">������ �� ���� ��� ������� ������ ����������� ���������</param>
        public override void Validate(List<PropertyHelper<T>> fields)
        {
            if (fields == null)
                throw new ArgumentNullException("fields");

            foreach (var field in fields)
            {
                if (!field.PropertyInfo.PropertyType.IsSubclassOf(typeof(TField))
                    && field.PropertyInfo.PropertyType != typeof(TField))
                    throw new ArgumentException(string.Format("��� �������� [{0}:{1}] �� ������������� ���� ������� �������������� ���������� [{2}]", field.PropertyInfo.Name, field.PropertyInfo.PropertyType, typeof(TField)), "fields");
                this.ValidateOverride(field);
            }
        }

        /// <summary>
        /// �������� �� ����������� ���������� ���������
        /// </summary>
        /// <param name="properties">������ �� ���� ��� ������� ������ ����������� ���������</param>
        public virtual void ValidateOverride(PropertyHelper<T> properties)
        {
        }
    }
}