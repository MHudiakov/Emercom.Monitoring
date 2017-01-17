// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBUpdate.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ����� ���������� ���������� ��
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB
{
    using System;
    using System.Data.Common;
    using System.Diagnostics.CodeAnalysis;

    using Init.Tools;

    /// <summary>
    /// ����� ���������� ���������� ��
    /// </summary>
    public class DbUpdate
    {
        /// <summary>
        /// �������� ������� � ������
        /// </summary>
        public CoreDbBase Context { get; private set; }

        /// <summary>
        ///  ����� ���������� ���������� ��
        /// </summary>
        /// <param name="context">�������� ������� � ������</param>
        public DbUpdate(CoreDbBase context)
        {
            this.Context = context;
            this.Major = 0;
            this.Minor = 0;
            this.Logger = new Loger();
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// ������� ����
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// ������� ������� �����������
        /// </summary>
        public Loger Logger { get; private set; }

        /// <summary>
        /// ��������� ���������� � ������� ������� �� ���������
        /// </summary>
        /// <param name="current">������� ������</param>
        /// <param name="to">������ ���� �����������</param>
        /// <param name="doUpdate">������� ���������� ��</param>
        protected void Update(Version current, Version to, Action<DbTransaction> doUpdate)
        {
            if (this.Major == current.Major && this.Minor == current.Minor)
            {
                var connection = this.Context.CreateConnection();
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    this.Logger.LogMsg(string.Format("������ �� �� v{0}.{1}", to.Major, to.Minor));
                    doUpdate(transaction);
                    this.Context.ExecuteScalar("Update dbVersion set Major=" + to.Major + ", Minor=" + to.Minor, transaction);
                    transaction.Commit();
                    this.Major = to.Major;
                    this.Minor = to.Minor;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(string.Format("������ ��� ������� �� �� v{0}.{1}", to.Major, to.Minor), ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// ��������� ���������� ���������� ��
        /// </summary>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements",
            Justification = "Reviewed. Suppression is OK here.")]
        public virtual void UpdateMetadata()
        {
            try
            {
                this.Major = Convert.ToInt32(this.Context.ExecuteScalar("select Major From dbVersion"));
                this.Minor = Convert.ToInt32(this.Context.ExecuteScalar("select Minor From dbVersion"));
            }
            catch
            {
                this.Major = 0;
                this.Minor = 0;
            }

            this.Update(
                new Version(0, 0),
                new Version(1, 0),
                trans =>
                {
                    this.Context.ExecuteScalar(
                        "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'dbversion') DROP TABLE [dbVersion];",
                        trans);
                    const string SQL = @"CREATE TABLE [dbo].[dbVersion](
                                    [Major] [int] NOT NULL,
                                    [Minor] [int] NOT NULL);";
                    this.Context.ExecuteScalar(SQL, trans);
                    this.Context.ExecuteScalar("Insert into dbVersion VALUES (1, 0);", trans);
                });
        }
    }

    [Flags]
    public enum IndexType
    {
        Unique=2,
        Clustered =4,
        Asc =8,
    }
}
