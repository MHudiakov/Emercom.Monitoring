// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeHelperWF.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ������ ��������� ����������������� ����� �� ������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows.Forms;

    using global::DevExpress.XtraEditors;
    using global::DevExpress.XtraTreeList;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// ������ ��������� ����������������� ����� �� ������
    /// </summary>
    /// <typeparam name="T">
    /// ��� ������� �������
    /// </typeparam>
    public class TreeHelperWF<T>
            where T : class
    {
        /// <summary>
        /// �������, ������������ ������
        /// </summary>
        private readonly TreeList _treeList;

        /// <summary>
        /// ��������� �������������� � ��
        /// </summary>
        private readonly GhStrategy<T> _strategy;

        /// <summary>
        /// ������ ����������
        /// </summary>
        private readonly SimpleButton _addButton;

        /// <summary>
        /// ������ ��������������
        /// </summary>
        private readonly SimpleButton _editButton;

        /// <summary>
        /// ������ ��������
        /// </summary>
        private readonly SimpleButton _deleteButton;

        /// <summary>
        /// ����� ������ ����� ��������������
        /// </summary>
        private readonly Func<T, bool> _executerVoid;

        /// <summary>
        /// ���������� ���������� �������
        /// </summary>
        public T SelectedItem
        {
            get
            {
                var node = this._treeList.FocusedNode;
                if (node == null)
                    return null;
                return this._treeList.GetDataRecordByNode(node) as T;
            }

            set
            {
                this._treeList.NodesIterator.DoOperation(node =>
                {
                    var item = this._treeList.GetDataRecordByNode(node);
                    if (item.Equals(value))
                    {
                        node.Selected = true;
                        this._treeList.FocusedNode = node;
                    }
                });
            }
        }

        /// <summary>
        /// ������� ����� ����������� ��������
        /// </summary>
        public event Action<T> SelectedItemChannged;

        /// <summary>
        /// ���������� ������� ����� ����������� ��������
        /// </summary>
        /// <param name="item">
        /// ����� �������� ����������� ��������
        /// </param>
        protected virtual void OnSelectedItemChannged(T item)
        {
            var handler = this.SelectedItemChannged;
            if (handler != null)
                handler(item);
        }

        /// <summary>
        /// ���������� � ����� ����������� �� ����������, �������������� � �������� ��������
        /// </summary>
        /// <param name="treeList">
        /// ����
        /// </param>
        /// <param name="addButton">
        /// ������ "��������"
        /// </param>
        /// <param name="editButton">
        /// ������ "��������"
        /// </param>
        /// <param name="deleteButton">
        /// ������ "�������"
        /// </param>
        /// <param name="executerVoid">
        /// ����� ������ ����� ��������������
        /// </param>
        /// <param name="strategy">
        /// ��������� �������������� � ��
        /// </param>
        public TreeHelperWF(
            TreeList treeList,
            SimpleButton addButton,
            SimpleButton editButton,
            SimpleButton deleteButton,
            Func<T, bool> executerVoid,
            GhStrategy<T> strategy)
        {
            if (treeList == null)
                throw new ArgumentNullException("treeList");

            if (executerVoid == null)
                throw new ArgumentNullException("executerVoid");

            if (strategy == null)
                throw new ArgumentNullException("strategy");

            this._treeList = treeList;
            this._addButton = addButton;
            this._editButton = editButton;
            this._deleteButton = deleteButton;
            this._executerVoid = executerVoid;
            this._strategy = strategy;

            this._treeList.FocusedNodeChanged += (s, args) =>
                {
                    T selected = null;
                    var node = this._treeList.FocusedNode;
                    if (node != null)
                        selected = this._treeList.GetDataRecordByNode(node) as T;
                    this.OnSelectedItemChannged(selected);
                };

            this._treeList.KeyDown += this.TreeListOnKeyDown;

            this._treeList.DoubleClick += (sender, args) => this.EditAction();

            if (addButton != null)
                addButton.Click += (sender, args) => this.AddAction();
            if (editButton != null)
                editButton.Click += (sender, args) => this.EditAction();
            if (deleteButton != null)
                deleteButton.Click += (sender, args) => this.DeleteAction();
        }

        /// <summary>
        /// ��������� ������� ������ � �����
        /// </summary>
        /// <param name="sender">
        /// �������� �������
        /// </param>
        /// <param name="args">
        /// ��������� �������
        /// </param>
        private void TreeListOnKeyDown(object sender, KeyEventArgs args)
        {
            // �� ��������� �������� �� ���������������� ��������
            if (args.KeyCode == Keys.Delete)
                if (this._deleteButton != null)
                    this.DeleteAction();
            if (args.KeyCode == Keys.F2)
                if (this._editButton != null)
                    this.EditAction();
            if (args.KeyCode == Keys.Insert)
                if (this._addButton != null)
                    this.AddAction();
        }

        /// <summary>
        /// ���������� ������
        /// </summary>
        public void AsyncUpdateData()
        {
            this._treeList.BeginWait();
            this._treeList.BeginUpdate();
            if (this._addButton != null)
                this._addButton.Enabled = false;
            if (this._editButton != null)
                this._editButton.Enabled = false;
            if (this._deleteButton != null)
                this._deleteButton.Enabled = false;

            try
            {
                var worker = new Thread(this.UpdateData) { IsBackground = true };
                worker.Start();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("������ �������� ������ � GridHelperWPF<{0}>", typeof(T).Name), ex));
                this.UnlockInterface();
            }
        }

        /// <summary>
        /// ������� �� ���������� ���������� ������
        /// </summary>
        public event Action<TreeHelperWF<T>> UpdateDataCompleted;

        /// <summary>
        /// ���������� ������� �� ���������� ���������� ������
        /// </summary>
        /// <param name="helperWf">
        /// ������ � ������� ��������� ��������
        /// </param>
        protected virtual void OnUpdateDataCompleted(TreeHelperWF<T> helperWf)
        {
            Action<TreeHelperWF<T>> handler = this.UpdateDataCompleted;
            if (handler != null)
                handler(helperWf);
        }

        /// <summary>
        /// ����� ���������� ������
        /// </summary>
        private void UpdateData()
        {
            try
            {
                T selItem = null;
                this._treeList.Invoke(new Action(() => selItem = this.SelectedItem));

                var items = this._strategy.GetList();
                this._treeList.Invoke(new Action(() =>
                    {
                        this._treeList.DataSource = items;
                        this.SelectedItem = selItem;
                        this.OnUpdateDataCompleted(this);
                    }));
                this._treeList.ExpandAll();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("������ ����������� �������� ������ � GridHelperWF<{0}>", typeof(T).Name), ex));
            }
            finally
            {
                this._treeList.Invoke(new Action(this.UnlockInterface));
            }
        }

        /// <summary>
        /// ������������� ����������
        /// </summary>
        private void UnlockInterface()
        {
            this._treeList.EndWait();
            this._treeList.EndUpdate();
            if (this._addButton != null)
                this._addButton.Enabled = true;
            if (this._editButton != null)
                this._editButton.Enabled = true;
            if (this._deleteButton != null)
                this._deleteButton.Enabled = true;
        }

        /// <summary>
        /// ��������� �������� �������� ����������� �������
        /// </summary>
        public void DeleteAction()
        {
            var selectedItem = this.SelectedItem;
            if (selectedItem == null)
            {
                xMsg.MsgEmptyDeleteData();
                return;
            }

            if (!xMsg.MsgWithConfirmDelete())
                return;

            try
            {
                this._strategy.Delete(selectedItem);

                var items = this._treeList.DataSource as IList<T>;
                if (items != null)
                {
                    var parent = this._treeList.FocusedNode != null ? this._treeList.FocusedNode.ParentNode : null;

                    items.Remove(selectedItem);

                    this._treeList.RefreshDataSource();
                    this._treeList.FocusedNode = parent;
                }
            }
            catch (FaultException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                var err = new Exception(string.Format(@"������ �������� �� ������� {0}", typeof(T).Name), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }

        /// <summary>
        /// ��������� ������� �������������� �������
        /// </summary>
        public void EditAction()
        {
            var selectedItem = this.SelectedItem;
            if (selectedItem == null)
            {
                xMsg.MsgEmptyEditData();
                return;
            }

            try
            {
                if (this._executerVoid(selectedItem))
                {
                    this._strategy.Edit(selectedItem);
                    this._treeList.RefreshDataSource();

                    this._treeList.NodesIterator.DoOperation(node =>
                        {
                            var item = this._treeList.GetDataRecordByNode(node);
                            if (item == selectedItem)
                                this._treeList.FocusedNode = node;
                        });
                }
            }
            catch (FaultException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                var err = new Exception(string.Format(@"������ �������������� ������� {0}", typeof(T).Name), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }

        /// <summary>
        /// ��������� �������� ���������� �������
        /// </summary>
        public void AddAction()
        {
            var item = this._strategy.CreateItem();

            try
            {
                if (this._executerVoid(item))
                {
                    this._strategy.Add(item);
                    var items = this._treeList.DataSource as IList<T>;
                    if (items != null)
                    {
                        // ���������� ������ � �����
                        if (!items.Contains(item))
                            items.Add(item);

                        this._treeList.RefreshDataSource();

                        this._treeList.NodesIterator.DoOperation(node =>
                        {
                            var dataRow = this._treeList.GetDataRecordByNode(node);
                            if (dataRow == item)
                                this._treeList.FocusedNode = node;
                        });
                    }
                }
            }
            catch (FaultException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                var err = new Exception(string.Format(@"������ ���������� �  ������� {0}", typeof(T).FullName), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }
    }
}
