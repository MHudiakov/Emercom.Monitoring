// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashedRepository.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ����������� �����������. ���� ������ � ����, ���� �� ���, �� � ��
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.DbCore.DataAccess;
    using Init.Tools;

    /// <summary>
    /// ����������� �����������. ���� ������ � ����, ���� �� ���, �� � ��
    /// </summary>
    /// <typeparam name="T">
    /// ��� �������, ���������� � �����������
    /// </typeparam>
    public class CashedRepository<T> : Repository<T>
        where T : DbObject
    {
        /// <summary>
        /// ����������� �����������. ���� ������ � ����, ���� �� ���, �� � ��
        /// </summary>
        /// <param name="dataAccess">������ ������� � ������</param>
        /// <param name="dataManager">DataManager</param>
        public CashedRepository(DataAccess<T> dataAccess, DataManager dataManager)
            : base(dataAccess, dataManager)
        {
            this._cache = new Cashe<T>();
        }

        /// <summary>
        /// ������ �������� � �����
        /// </summary>
        protected virtual Cashe<T> Cache
        {
            get
            {
                return this._cache;
            }
        }

        /// <summary>
        /// ������ �������� � �����
        /// </summary>
        private readonly Cashe<T> _cache;

        /// <summary>
        /// ��������� ���������� ��������
        /// </summary>
        /// <returns>
        /// ���������� �������� � ��
        /// </returns>
        public override long GetCount()
        {
            if (this.Cache.Count == 0)
                return base.GetCount();
            return this.Cache.Count;
        }

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected override void AddOverride(T item)
        {
            if (this.Cache.Where(e => e.Equals(item)).Any())
                throw new ArgumentException(string.Format("������ �������� ��������� ������� {0} �.�. �� ��� ��������.", typeof(T).FullName), "item").AddData("item", item);

            // ����� � ����
            base.AddOverride(item);

            // ��������� � ���
            this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d));

            try
            {
                this.UpdateCacheOnAdd(item);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("C������������ ���� ��� ���������� ������ [{0}]", typeof(T).FullName), ex).AddData("item", item);
            }
        }

        /// <summary>
        /// ��������� �������������� �������� ������������� ���� ��� ����������
        /// </summary>
        /// <param name="item">
        /// ���������� �������
        /// </param>
        protected virtual void UpdateCacheOnAdd(T item)
        {
        }

        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="whereArgs">
        /// ����� �������
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            var cachedItems = this.Cache.Where(e => e.PropEqualas(whereArgs)).ToList();

            // ������� �� ����
            base.DeleteWhereOverride(whereArgs);

            // ������ ���
            this.Cache.DeleteWhere(e => e.PropEqualas(whereArgs));

            foreach (var item in cachedItems)
                this.UpdateCacheOnDelete(item);
        }

        /// <summary>
        /// ��������� �������������� �������� ������������� ���� ��� ��������
        /// </summary>
        /// <param name="item">��������� ������</param>
        protected virtual void UpdateCacheOnDelete(T item)
        {
        }

        /// <summary>
        /// ������������� ������ � ���� � � ����
        /// </summary>
        /// <param name="item">������</param>
        protected override void EditOverride(T item)
        {
            T oldItem = this.Cache.Where(e => e.Equals(item)).SingleOrDefault();

            if (oldItem == null || ReferenceEquals(oldItem, item))
                oldItem = this.DataAccess.Get(item.KeyValue);

            if (oldItem == null)
                throw new ArgumentException(string.Format("���������� ��������������� ������ [{0}] �.�. ��� ������ �� ������� � ����.", typeof(T).FullName), "item").AddData("item", item);

            // ����������� � ����
            base.EditOverride(item);

            // ����������� � ����
            this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d));
            try
            {
                this.UpdateCacheOnEdit(item, oldItem);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("������ ������������� ���� ��� �������������� ������ [{0}]", typeof(T).FullName), ex).AddData("item", item);
            }
        }

        /// <summary>
        /// ��������� �������������� �������� ������������� ���� ��� ��������������
        /// </summary>
        /// <param name="item">����������������� ������</param>
        /// <param name="oldItem">������������� ������ </param>
        protected virtual void UpdateCacheOnEdit(T item, T oldItem)
        {
        }

        /// <summary>
        /// ��������� ����� �������� �� �����
        /// </summary>
        /// <param name="whereArgs">
        /// ���� ������ ��������
        /// </param>
        /// <returns>
        /// ������ �������� ���������� ��� ��������
        /// </returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            // ��������� ���
            var cashedItems = this.Cache.Where(e =>
                {
                    var itemFields = e.GetProperties();
                    return whereArgs.All(k =>
                        {
                            // ���� ���� ������ ���, �� ������ �� �������� 
                            if (!itemFields.ContainsKey(k.Key))
                                return false;

                            // ���� ���� ���� � ������, ��������� �� ��������
                            var field = itemFields[k.Key];
                            if (field != null)
                                return itemFields[k.Key].Equals(k.Value);

                            // ���� ���� �� ������, �� � ���� ������ ���� �� �����
                            return k.Value == null;
                        });
                });

            if (cashedItems.Any())
                return cashedItems;

            // ���� � ���� ������ ��� �� ���� ����� � ����
            cashedItems = base.GetItemsWhereOverride(whereArgs);

            if (cashedItems.Any())
                cashedItems = cashedItems.Select(item => this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d))).ToList();

            return cashedItems;
        }

        /// <summary>
        /// ��������� ����� ��������
        /// </summary>
        /// <param name="pageIndex">
        /// ����� ��������
        /// </param>
        /// <param name="pageSize">
        /// ���������� ��������
        /// </param>
        /// <returns>
        /// ������ �������� ������� � idFrom ������� �� ����� pageSize
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            var cachedItems = this.GetAll();

            // todo: ������� ��� ���������������
            cachedItems.Sort(
                     (left, right) =>
                     {
                         var propertyInfo = this.Metadata.Key;
                         var leftValue = (IComparable)propertyInfo.Getter(left);
                         var rightValue = (IComparable)propertyInfo.Getter(right);
                         return leftValue.CompareTo(rightValue);
                     });
            var catcedItems = cachedItems.Skip(pageSize * pageIndex).Take(pageSize).ToList();
            if (!catcedItems.Any())
                catcedItems = base.GetPageOverride(pageIndex, pageSize).Select(e => this.Cache.UpdateOrInsert(e, (s, d) => s.CopyTo(d))).ToList();

            return catcedItems;
        }

        /// <summary>
        /// ��������� ���� ��������
        /// </summary>
        /// <returns>
        /// ������ ���� �������� � �������
        /// </returns>
        public override List<T> GetAll()
        {
            var list = this.Cache.Where(e => true);
            if (list.Count == 0)
                list = base.GetAll().Select(arg => this.Cache.UpdateOrInsert(arg, (s, d) => s.CopyTo(d))).ToList();
            return list;
        }
    }
}