// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEditBinding.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Привязка к BaseEdit (привязывается к полю EditValue)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Привязка к BaseEdit (привязывается к полю EditValue)
    /// </summary>
    /// <typeparam name="TBindContext">
    /// Тип объекта-источника данных
    /// </typeparam>
    public class BaseEditBinding<TBindContext> : ControlBinding<TBindContext> where TBindContext : class
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// Целевой BaseEdit, к которому производится привязка
        /// </param>
        /// <param name="context">
        /// Контекст
        /// </param>
        /// <param name="property">
        /// Свойства
        /// </param>
        public BaseEditBinding(BaseEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// Целевой CheckEdit, к которому производится привязка
        /// </summary>
        public new BaseEdit Target
        {
            get
            {
                return base.Target as BaseEdit;
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
            this.Property.SetValue(this.Context, this.Target.EditValue, null);
        }
    }
}