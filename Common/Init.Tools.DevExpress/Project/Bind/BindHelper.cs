// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindHelper.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   �������� �������� ��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using global::DevExpress.XtraEditors;

    using Init.Tools.DevExpress.Bind.BindPolicy;

    /// <summary>
    /// �������� �������� ��������
    /// </summary>
    /// <typeparam name="TBindContext">��� �������� - ��������� ������</typeparam>
    public class BindHelper<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// ������� ����������
        /// </summary>
        public Control Control { get; private set; }

        /// <summary>
        /// �������� 
        /// </summary>
        public TBindContext Context { get; private set; }

        /// <summary>
        /// �������� �������� ��������
        /// </summary>
        /// <param name="control">������������ �������, �� ������� ���� ����������� ��������</param>
        /// <param name="context">������ - �������� ������</param>
        public BindHelper(Control control, TBindContext context)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            if (context == null)
                throw new ArgumentNullException("context");

            this.Control = control;
            this.Context = context;
            this.ControlNameMatchFilter = @"(\A[a-z]{2,4})(?<PropName>(?:_[a-z])?[A-Z]+\w+)";
            this.Bindings = new List<ControlBinding<TBindContext>>();
            this.ExcludeFromBindingControls = new List<Control>();
        }

        /// <summary>
        /// RegExp ������. ������ ��������� ��� ����� ��������, � ����������� ������
        /// ������ ����� ������ PropName, ����������� � ������ �������� � �������
        /// </summary>
        public string ControlNameMatchFilter { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public List<ControlBinding<TBindContext>> Bindings { get; private set; }

        /// <summary>
        /// ��������, ������� ����� ��������� ��� ����������� ��������
        /// </summary>
        public List<Control> ExcludeFromBindingControls { get; private set; }

        /// <summary>
        /// �������� ��������
        /// </summary>
        /// <exception cref="Exception">
        /// ���������� ��������� ��������� ��������
        /// </exception>
        public void CreateBindings()
        {
            if (this.Context == null)
                throw new Exception("�������� �������� �� �����");

            var properties = this.Context.GetType().GetProperties().ToList();
            var bindedControls = this.GetAllAcceptableControls(this.Control).ToList();
            foreach (var binding in from bindedControl in bindedControls
                                    let match = Regex.Match(bindedControl.Name, this.ControlNameMatchFilter)
                                    where match.Success
                                    where match.Groups["PropName"] != null
                                    let prop =
                                        properties.SingleOrDefault(
                                            p => string.Equals(p.Name, match.Groups["PropName"].Value.Trim('_'), StringComparison.CurrentCultureIgnoreCase))
                                    where prop != null
                                    select this.CreateBinding(bindedControl, prop))
                this.Bindings.Add(binding);
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        /// <param name="edit">
        /// ���� �����
        /// </param>
        /// <param name="property">
        /// ��������
        /// </param>
        /// <returns>
        /// ���������� ��������� �������� ������� � �������� �����
        /// </returns>
        protected virtual ControlBinding<TBindContext> CreateBinding(Control edit, PropertyInfo property)
        {
            if (edit is LookUpEditBase)
                return new LookUpEditBaseBinding<TBindContext>(edit as LookUpEditBase, this.Context, property, this.ResolveItemsCollection(edit, property));

            if (edit is DateEdit)
                return new DateEditBinding<TBindContext>(edit as DateEdit, this.Context, property);

            if (edit is ComboBoxEdit)
                return new ComboBoxEditBinding<TBindContext>(edit as ComboBoxEdit, this.Context, property, this.ResolveItemsCollection(edit, property));

            if (edit is TextEdit)
                return new TextEditBinding<TBindContext>(edit as TextEdit, this.Context, property);

            if (edit is CheckEdit)
                return new CheckEditBind<TBindContext>(edit as CheckEdit, this.Context, property);

            if (edit is BaseEdit)
                return new BaseEditBinding<TBindContext>(edit as BaseEdit, this.Context, property);

            return new ControlBinding<TBindContext>(edit, this.Context, property);
        }

        /// <summary>
        /// ������� ������ ��������� ��������� ��� ����������, ��������������� ������ ���������
        /// </summary>
        public event Func<Control, PropertyInfo, ICollection> OnResolveItemsCollection;

        /// <summary>
        /// ����� ��������� ��������� ��� ����������, ��������������� ������ ���������
        /// </summary>
        /// <param name="edit">
        /// ���� �����
        /// </param>
        /// <param name="prop">
        /// ��������
        /// </param>
        /// <returns>
        /// ��������� ��������� ��� ����������, ��������������� ������ ���������
        /// </returns>
        protected virtual ICollection ResolveItemsCollection(Control edit, PropertyInfo prop)
        {
            return this.OnResolveItemsCollection != null ? this.OnResolveItemsCollection(edit, prop) : null;
        }

        /// <summary>
        /// ��������� ���� �������� ����������� ���������� 
        /// </summary>
        /// <param name="parent">
        /// ������������ �������
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<Control> GetAllAcceptableControls(Control parent)
        {
            var list = new List<Control>();

            if (this.ExcludeFromBindingControls.Contains(parent))
                return list;

            // �������� ��� ���������� �������� � ������
            list.AddRange(parent.Controls.OfType<Control>().Where(c => Regex.IsMatch(c.Name, this.ControlNameMatchFilter)).ToList());

            // ���� �� ��������� �����������
            var chieldControls = parent.Controls.OfType<Control>().ToList();
            foreach (var chieldControl in chieldControls)
                list.AddRange(this.GetAllAcceptableControls(chieldControl));

            list.RemoveAll(c => this.ExcludeFromBindingControls.Contains(c));
            return list;
        }

        /// <summary>
        /// �������� ������ � ��������� ��������
        /// </summary>
        public void LoadBindings()
        {
            foreach (var binding in this.Bindings)
                binding.Load();
        }

        /// <summary>
        /// ���������� ������ � ������
        /// </summary>
        public void SaveBindings()
        {
            foreach (var binding in this.Bindings)
                binding.Save();
        }
    }
}