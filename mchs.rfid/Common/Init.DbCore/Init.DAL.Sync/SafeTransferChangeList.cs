// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeTransferChangeList.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ������ �������������� �������� ��������� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ������ �������������� �������� ��������� �������
    /// </summary>
    public class SafeTransferChangeList
    {
        /// <summary>
        /// ���� �� ������ ������� ���� ������� ���������
        /// </summary>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        public List<Change> ChangeList { get; private set; }

        /// <summary>
        /// ������� ������ ��������������� ��������
        /// </summary>
        public SafeTransferChangeList()
        {
            this.LastActivityDate = DateTime.Now;
            this.ChangeList = new List<Change>();
        }
    }
}
