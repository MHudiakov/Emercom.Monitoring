// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEditBinding.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   �������� � BaseEdit (������������� � ���� EditValue)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// �������� � BaseEdit (������������� � ���� EditValue)
    /// </summary>
    /// <typeparam name="TBindContext">
    /// ��� �������-��������� ������
    /// </typeparam>
    public class BaseEditBinding<TBindContext> : ControlBinding<TBindContext> where TBindContext : class
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="BaseEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// ������� BaseEdit, � �������� ������������ ��������
        /// </param>
        /// <param name="context">
        /// ��������
        /// </param>
        /// <param name="property">
        /// ��������
        /// </param>
        public BaseEditBinding(BaseEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// ������� CheckEdit, � �������� ������������ ��������
        /// </summary>
        public new BaseEdit Target
        {
            get
            {
                return base.Target as BaseEdit;
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
            this.Property.SetValue(this.Context, this.Target.EditValue, null);
        }
    }
}