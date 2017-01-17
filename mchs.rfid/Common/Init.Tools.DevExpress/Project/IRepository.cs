// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   ������������ ������ ��� ������ � �������������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System.Collections.Generic;

    /// <summary>
    /// ������ ������ �������, ���������� ������ ��� ������������� ������� OnChangeList
    /// </summary>
    /// <param name="list">
    /// ������ ��������
    /// </param>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public delegate void OnChangeListDeleagte<TEntity>(List<TEntity> list)
            where TEntity : class, new();

    /// <summary>
    /// ������������ ������ ��� ������ � �������������
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public interface IRepository<TEntity>
            where TEntity : class, new()
    {
        /// <summary>
        /// �������, ����������� ��� ��������� ������ ������������� ��������� �����������
        /// </summary>
        event OnChangeListDeleagte<TEntity> OnChangeList;

        /// <summary>
        /// ������ ���������
        /// </summary>
        List<TEntity> List { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="item">
        /// ����������� �������
        /// </param>
        /// <returns>
        /// True - ���� ���������� ������ �������, ����� false
        /// </returns>
        bool Add(TEntity item);

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="item">
        /// ���������� �������
        /// </param>
        void Edit(TEntity item);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="item">
        /// ��������� �������
        /// </param>
        /// <returns>
        /// True - ���� �������� ������ �������, ����� false
        /// </returns>
        bool Delete(TEntity item);
    }
}