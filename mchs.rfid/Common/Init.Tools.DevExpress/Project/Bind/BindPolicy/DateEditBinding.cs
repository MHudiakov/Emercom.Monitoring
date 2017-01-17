// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateEditBinding.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Привязка к DateEdit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Привязка к DateEdit
    /// </summary>
    /// <typeparam name="TBindContext">
    /// Тип объекта-источника данных
    /// </typeparam>
    public class DateEditBinding<TBindContext> : ControlBinding<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// Целевой DateEdit, к которому производится привязка
        /// </summary>
        public new DateEdit Target
        {
            get
            {
                return base.Target as DateEdit;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DateEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// Целевой DateEdit, к которому производится привязка
        /// </param>
        /// <param name="context">
        /// Контекст
        /// </param>
        /// <param name="property">
        /// Свойства
        /// </param>
        public DateEditBinding(DateEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// Метод загрузки данных в контрол
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
        /// Метод сохранения данных из контрола в контекст
        /// </summary>
        protected override void DoSave()
        {
            // если свойство - nullable
            if (Nullable.GetUnderlyingType(this.Property.PropertyType) == typeof(DateTime)
                && this.Target.EditValue == null)
                this.Property.SetValue(this.Context, null, null);
            else
                this.Property.SetValue(this.Context, this.Target.DateTime, null);
        }
    }
}