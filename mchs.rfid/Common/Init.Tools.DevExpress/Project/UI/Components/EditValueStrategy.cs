// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditValueStrategy.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   ��������� ���������� �������� parent ���������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI.Components
{
    /// <summary>
    /// ��������� ���������� �������� parent ���������
    /// </summary>
    public enum EditValueStrategy
    {
        /// <summary>
        /// ������� �� ����, � ����� �������� ����������� �������
        /// </summary>
        Hybrid = 0,

        /// <summary>
        /// �������� � ���� �������
        /// </summary>
        FilterEditValue = 1,

        /// <summary>
        /// �������� ������ �� ������ �������
        /// </summary>
        GridRowObject = 2,

        /// <summary>
        /// ��������� ������������� ������� �� ������ �������
        /// </summary>
        GridRowText = 3
    }
}