// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComboBoxEditBinding.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   �������� � ComboBox
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System.Collections;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// �������� � ComboBox
    /// </summary>
    /// <typeparam name="TBindContext">
    /// ��� �������-��������� ������
    /// </typeparam>
    public class ComboBoxEditBinding<TBindContext> : ControlBinding<TBindContext> where TBindContext : class
    {
        /// <summary>
        /// ������� ComboBoxEdit, � �������� ������������ ��������
        /// </summary>
        public new ComboBoxEdit Target
        {
            get
            {
                return base.Target as ComboBoxEdit;
            }
        }

        /// <summary>
        /// ��������� ���������
        /// </summary>
        public ICollection Items { get; private set; }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="ComboBoxEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// ������� ComboBoxEdit, � �������� ������������ ��������
        /// </param>
        /// <param name="context">
        /// ��������
        /// </param>
        /// <param name="property">
        /// ��������
        /// </param>
        /// <param name="items">
        /// ��������� ���������
        /// </param>
        public ComboBoxEditBinding(ComboBoxEdit target, TBindContext context, PropertyInfo property, ICollection items)
            : base(target, context, property)
        {
            this.Items = items;
        }

        /// <summary>
        /// ����� �������� ������ � �������
        /// </summary>
        protected override void DoLoad()
        {
            if (this.Items != null)
                this.Target.Properties.Items.AddRange(this.Items);

            // ���� �������� � ������, �.�. ������� ����� ������ �� �����������
            if (this.Property.PropertyType == typeof(string))
            {
                this.Target.Text = this.Property.GetValue(this.Context, null) as string;
            }
            else
                if (this.Property.PropertyType == typeof(int))
                {
                    // ���� �������� � �������. �.�. ����� �������� ������������
                    this.Target.SelectedIndex = (int)this.Property.GetValue(this.Context, null);
                }
                else

                    // ��� �������� - ���� � �������
                    this.Target.SelectedItem = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// ����� ���������� ������ �� �������� � ��������
        /// </summary>
        protected override void DoSave()
        {
            // ���� ���� � ������, �.�. ������� ����� ������ �� �����������
            if (this.Property.PropertyType == typeof(string))
            {
                this.Property.SetValue(this.Context, this.Target.Text, null);
            }
            else
                if (this.Property.PropertyType == typeof(int))
                {
                    // ���� ���� � �������. �.�. ����� �������� enum
                    this.Property.SetValue(this.Context, this.Target.SelectedIndex, null);
                }
                else

                    // ��� �������� - ���� � �������
                    this.Property.SetValue(this.Context, this.Target.SelectedItem, null);
        }
    }
}