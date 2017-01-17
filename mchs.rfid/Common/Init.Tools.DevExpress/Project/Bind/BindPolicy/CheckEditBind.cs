// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckEditBind.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   �������� � CheckEdit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// �������� � CheckEdit
    /// </summary>
    /// <typeparam name="TBindContext">
    /// ��� �������-��������� ������
    /// </typeparam>
    public class CheckEditBind<TBindContext> : ControlBinding<TBindContext> where TBindContext : class
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="CheckEditBind{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// ������� CheckEdit, � �������� ������������ ��������
        /// </param>
        /// <param name="context">
        /// ��������
        /// </param>
        /// <param name="property">
        /// ��������
        /// </param>
        public CheckEditBind(CheckEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// ������� CheckEdit, � �������� ������������ ��������
        /// </summary>
        public new CheckEdit Target
        {
            get
            {
                return base.Target as CheckEdit;
            }
        }

        /// <summary>
        /// ����� �������� ������ � �������
        /// </summary>
        protected override void DoLoad()
        {
            this.Target.EditValue = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// ����� ���������� ������ �� �������� � ��������
        /// </summary>
        protected override void DoSave()
        {
            var pType = Nullable.GetUnderlyingType(this.Property.PropertyType) ?? this.Property.PropertyType;

            if (Nullable.GetUnderlyingType(this.Property.PropertyType) != null && this.Target.EditValue == null)
                this.Property.SetValue(this.Context, this.Target.EditValue, null);
            else
            {
                if (pType == typeof(bool))
                    this.Property.SetValue(this.Context, this.Target.Checked, null);
                else
                    this.Property.SetValue(this.Context, this.Target.EditValue, null);
            }
        }
    }
}