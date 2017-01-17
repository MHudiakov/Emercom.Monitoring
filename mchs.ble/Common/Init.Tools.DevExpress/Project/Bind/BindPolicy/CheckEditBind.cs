// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckEditBind.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Привязка к CheckEdit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Привязка к CheckEdit
    /// </summary>
    /// <typeparam name="TBindContext">
    /// Тип объекта-источника данных
    /// </typeparam>
    public class CheckEditBind<TBindContext> : ControlBinding<TBindContext> where TBindContext : class
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CheckEditBind{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// Целевой CheckEdit, к которому производится привязка
        /// </param>
        /// <param name="context">
        /// Контекст
        /// </param>
        /// <param name="property">
        /// Свойства
        /// </param>
        public CheckEditBind(CheckEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// Целевой CheckEdit, к которому производится привязка
        /// </summary>
        public new CheckEdit Target
        {
            get
            {
                return base.Target as CheckEdit;
            }
        }

        /// <summary>
        /// Метод загрузки данных в контрол
        /// </summary>
        protected override void DoLoad()
        {
            this.Target.EditValue = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// Метод сохранения данных из контрола в контекст
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