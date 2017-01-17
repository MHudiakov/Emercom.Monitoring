// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObservableDataAccess.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   ����������� ����������.
//   ��� ���������� �������� � ��� ����� ������������ �������������� ���������� ������������� ������� � ��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DataAccess
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ����������� ����������. 
    /// ��� ���������� �������� � ��� ����� ������������ �������������� ���������� ������������� ������� � ��������
    /// </summary>
    /// <typeparam name="T">��� ����� ������</typeparam>
    public interface IObservableDataAccess<T>
        where T : class
    {
        /// <summary>
        /// ���������� ����� ���������� ��������
        /// </summary>
        event Action<T> AfterAdd;

        /// <summary>
        /// ���������� ����� ����������� ��������
        /// </summary>
        event Action<T> BeforeAdd;

        /// <summary>
        /// ���������� ����� �������� ��������
        /// </summary>
        event Action<Dictionary<string, object>> AfterDelete;

        /// <summary>
        /// ���������� ����� ��������� ��������
        /// </summary>
        event Action<Dictionary<string, object>> BeforeDelete;

        /// <summary>
        /// ���������� ����� �������������� �������
        /// </summary>
        event Action<T> AfterEdit;

        /// <summary>
        /// ���������� ����� ��������������� ��������
        /// </summary>
        event Action<T> BeforeEdit;
    }
}