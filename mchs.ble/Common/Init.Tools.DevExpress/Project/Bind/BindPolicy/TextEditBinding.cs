// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextEditBinding.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   �������� � TextEdit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// �������� � TextEdit
    /// </summary>
    /// <typeparam name="TBindContext">
    /// ��� �������-��������� ������
    /// </typeparam>
    public class TextEditBinding<TBindContext> : ControlBinding<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// ������� TextEdit, � �������� ������������ ��������
        /// </summary>
        public new TextEdit Target
        {
            get
            {
                return base.Target as TextEdit;
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="TextEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// ������� TextEdit, � �������� ������������ ��������
        /// </param>
        /// <param name="context">
        /// ��������
        /// </param>
        /// <param name="property">
        /// ��������
        /// </param>
        public TextEditBinding(TextEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// ����� �������� ������ � �������
        /// </summary>
        protected override void DoLoad()
        {
            if (this.Property.PropertyType == typeof(string))
                this.Target.Text = this.Property.GetValue(this.Context, null) as string;
            else
                this.Target.EditValue = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// ����� ���������� ������ �� �������� � ��������
        /// </summary>
        protected override void DoSave()
        {
            if (this.Property.PropertyType == typeof(string))
                this.Property.SetValue(this.Context, this.Target.Text, null);
            {
                // �������� ������ ��� ��������
                var pType = this.Property.PropertyType;
                if (Nullable.GetUnderlyingType(this.Property.PropertyType) != null)
                    pType = Nullable.GetUnderlyingType(this.Property.PropertyType);

                // ���� ������ �� ������� � ���� ��������� null
                if (Nullable.GetUnderlyingType(this.Property.PropertyType) == null && string.IsNullOrEmpty(this.Target.Text))
                    this.Property.SetValue(this.Context, null, null);
                else
                {
                    if (pType == typeof(int)) // �������������� � int
                        this.Property.SetValue(this.Context, this.Target.EditValue.ToInt(), null);
                    else if (pType == typeof(double)) // �������������� � double
                        this.Property.SetValue(this.Context, this.Target.EditValue.ToDouble(), null);
                    else // ��� ��������������
                        this.Property.SetValue(this.Context, this.Target.EditValue, null);
                }
            }
        }
    }
}