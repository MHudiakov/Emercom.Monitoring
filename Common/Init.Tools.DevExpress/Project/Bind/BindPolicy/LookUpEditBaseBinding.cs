// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LookUpEditBaseBinding.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Стратегия привязки к лукап эдиту
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System.Collections;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Стратегия привязки к лукап эдиту
    /// </summary>
    /// <typeparam name="TBindContext">Контуекст привязки</typeparam>
    public class LookUpEditBaseBinding<TBindContext> : ControlBinding<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// Целевой контрол к которому производится привязка
        /// </summary>
        public new LookUpEditBase Target
        {
            get
            {
                return base.Target as LookUpEditBase;
            }
        }

        /// <summary>
        /// Коллекция элементов, загруженная в лукап
        /// </summary>
        public ICollection Items { get; private set; }

        /// <summary>
        /// Стратегия загрузки данныз в лукап
        /// </summary>
        /// <param name="target">
        /// Целевой контрол
        /// </param>
        /// <param name="context">
        /// Контекст привязки
        /// </param>
        /// <param name="property">
        /// Свойство контекста для загрузки
        /// </param>
        /// <param name="items">
        /// Коллекция элемнтов лукапа
        /// </param>
        public LookUpEditBaseBinding(LookUpEditBase target, TBindContext context, PropertyInfo property, ICollection items)
            : base(target, context, property)
        {
            this.Items = items;
        }

        /// <summary>
        /// Внутренняя реализация метода загрузки данных в контрол.
        /// </summary>
        protected override void DoLoad()
        {
            if (this.Items != null)
                this.Target.Properties.DataSource = this.Items;

            this.Target.EditValue = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// Внутренняя реализация метода сохранения данных из контрола в контекст
        /// </summary>
        protected override void DoSave()
        {
            this.Property.SetValue(this.Context, this.Target.EditValue, null);
        }
    }
}
