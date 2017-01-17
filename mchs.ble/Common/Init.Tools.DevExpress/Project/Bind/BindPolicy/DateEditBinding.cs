// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateEditBinding.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   �������� � DateEdit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// �������� � DateEdit
    /// </summary>
    /// <typeparam name="TBindContext">
    /// ��� �������-��������� ������
    /// </typeparam>
    public class DateEditBinding<TBindContext> : ControlBinding<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// ������� DateEdit, � �������� ������������ ��������
        /// </summary>
        public new DateEdit Target
        {
            get
            {
                return base.Target as DateEdit;
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="DateEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// ������� DateEdit, � �������� ������������ ��������
        /// </param>
        /// <param name="context">
        /// ��������
        /// </param>
        /// <param name="property">
        /// ��������
        /// </param>
        public DateEditBinding(DateEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// ����� �������� ������ � �������
        /// </summary>
        protected override void DoLoad()
        {
            var value = this.Property.GetValue(this.Context, null);
            if (value != null)
                this.Target.DateTime = (DateTime)value;
            else
                this.Target.EditValue = null;
        }

        /// <summary>
        /// ����� ���������� ������ �� �������� � ��������
        /// </summary>
        protected override void DoSave()
        {
            // ���� �������� - nullable
            if (Nullable.GetUnderlyingType(this.Property.PropertyType) == typeof(DateTime)
                && this.Target.EditValue == null)
                this.Property.SetValue(this.Context, null, null);
            else
                this.Property.SetValue(this.Context, this.Target.DateTime, null);
        }
    }
}