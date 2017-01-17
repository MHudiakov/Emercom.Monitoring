// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncStrategy.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// // <summary>
//   �������� ���������� ��� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Strategy
{
    using System;

    /// <summary>
    /// ��������� ���������� ��������� � ��
    /// </summary>
    public abstract class SyncStrategy
    {
        /// <summary>
        /// ���������� ������ �������
        /// </summary>
        /// <param name="change">
        /// ���������� � ���������� ������ �������
        /// </param>
        public void AddItem(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");
            this.AddItemOverride(change);
        }

        /// <summary>
        /// ���������� ������ �������
        /// </summary>
        /// <param name="change">
        /// ���������� � ���������� ������ �������
        /// </param>
        protected abstract void AddItemOverride(Change change);

        /// <summary>
        /// �������������� �������
        /// </summary>
        /// <param name="change">
        /// ���������� � �������������� �������
        /// </param>
        public void EditItem(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");
            if (change.GetFilterProps().Count == 0)
                throw new NotSupportedException(string.Format("������ ��������� �� ����� ��������� ����."));
            this.EditItemOverride(change);
        }

        /// <summary>
        /// �������������� �������
        /// </summary>
        /// <param name="change">
        /// ���������� � �������������� �������
        /// </param>
        protected abstract void EditItemOverride(Change change);

        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="change">
        /// ���������� �� �������� �������
        /// </param>
        public void RemoveItem(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");
            if (change.GetFilterProps().Count == 0)
                throw new NotSupportedException(string.Format("������ ��������� �� ����� ��������� ����."));
            this.RemoveItemOverride(change);
        }

        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="change">
        /// ���������� �� �������� �������
        /// </param>
        protected abstract void RemoveItemOverride(Change change);
    }
}