// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlBinding.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//  Стратегия привязки объекта к элементу формы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// Стратегия привязки объекта к элементу формы
    /// </summary>
    /// <typeparam name="TBindContext">Тип объекта - источника данных</typeparam>
    public class ControlBinding<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// Целевой контрол к которому производится привязка
        /// </summary>
        public virtual Control Target { get; private set; }

        /// <summary>
        /// Контекст привязки (источник данных)
        /// </summary>
        public TBindContext Context { get; private set; }

        /// <summary>
        /// Свойство в контексте для привязки
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Стратегия привязки объекта к элементу формы
        /// </summary>
        /// <param name="target">Целевой контрол к которому производится привязка</param>
        /// <param name="context">Контекст привязки (источник данных)</param>
        /// <param name="property">Свойство в контексте для привязки</param>
        public ControlBinding(Control target, TBindContext context, PropertyInfo property)
        {
            this.Target = target;
            this.Context = context;
            this.Property = property;
        }

        /// <summary>
        /// Выполняет загрузку данных в контрол
        /// </summary>
        public virtual void Load()
        {
            if (this.Property.CanRead)
                this.DoLoad();
        }

        /// <summary>
        /// Выполняет сохроанение данных из контрола в контекст
        /// </summary>
        public virtual void Save()
        {
            if (this.Property.CanWrite)
                this.DoSave();
        }

        /// <summary>
        /// Внутренняя реализация метода загрузки данных в контрол.
        /// </summary>
        protected virtual void DoLoad()
        {
            var value = this.Property.GetValue(this.Context, null);
            if (value != null)
                this.Target.Text = value.ToString();
        }

        /// <summary>
        /// Внутренняя реализация метода сохранения данных из контрола в контекст
        /// </summary>
        protected virtual void DoSave()
        {
            if (this.Property.CanWrite)
                this.Property.SetValue(this.Context, this.Target.Text, null);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Control: {0}=> Object: {1}", this.Target != null ? this.Target.Name : "null", this.Property != null ? this.Property.Name : "null");
        }
    }
}
