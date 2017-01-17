// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelTrimStrategy.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ��������� Trim ��� �����
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing.Attributes
{
    using System.Collections.Generic;

    using Init.Tools;

    /// <summary>
    /// ��������� Trim ��� �����
    /// </summary>
    /// <typeparam name="T">��� ������</typeparam>
    internal class ModelTrimStrategy<T> : CustomModelProcessStrategy<T, string>
        where T : class
    {
        /// <summary>
        /// ���������� ��������� ��� ����� ������� � ������������ �� �������� ProcessAttribute
        /// </summary>
        /// <param name="item">������ ��� ����� �������� ������������ ���������</param>
        /// <param name="property">������ �� �������� ��� ������� ������������ ���������</param>
        /// <param name="args">������ �� ��������</param>
        public override void ProcessOverride(T item, PropertyHelper<T> property, Dictionary<string, object> args)
        {
            var fieldValue = (string)property.Getter(item);

            var tetimLen = args["Lenght"].ToInt();
            if (fieldValue.Length <= tetimLen)
                return;

            fieldValue = fieldValue.Substring(0, tetimLen);
            property.Setter(item, fieldValue);
        }
    }
}