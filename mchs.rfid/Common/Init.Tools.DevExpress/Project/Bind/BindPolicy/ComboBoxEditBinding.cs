// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComboBoxEditBinding.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Привязка к ComboBox
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System.Collections;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Привязка к ComboBox
    /// </summary>
    /// <typeparam name="TBindContext">
    /// Тип объекта-источника данных
    /// </typeparam>
    public class ComboBoxEditBinding<TBindContext> : ControlBinding<TBindContext> where TBindContext : class
    {
        /// <summary>
        /// Целевой ComboBoxEdit, к которому производится привязка
        /// </summary>
        public new ComboBoxEdit Target
        {
            get
            {
                return base.Target as ComboBoxEdit;
            }
        }

        /// <summary>
        /// Коллекция элементов
        /// </summary>
        public ICollection Items { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ComboBoxEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// Целевой ComboBoxEdit, к которому производится привязка
        /// </param>
        /// <param name="context">
        /// Контекст
        /// </param>
        /// <param name="property">
        /// Свойства
        /// </param>
        /// <param name="items">
        /// Коллекция элементов
        /// </param>
        public ComboBoxEditBinding(ComboBoxEdit target, TBindContext context, PropertyInfo property, ICollection items)
            : base(target, context, property)
        {
            this.Items = items;
        }

        /// <summary>
        /// Метод загрузки данных в контрол
        /// </summary>
        protected override void DoLoad()
        {
            if (this.Items != null)
                this.Target.Properties.Items.AddRange(this.Items);

            // если привязка к строке, т.е. простой выбор текста из справочника
            if (this.Property.PropertyType == typeof(string))
            {
                this.Target.Text = this.Property.GetValue(this.Context, null) as string;
            }
            else
                if (this.Property.PropertyType == typeof(int))
                {
                    // если привязка к индексу. т.е. выбор элемента перечисления
                    this.Target.SelectedIndex = (int)this.Property.GetValue(this.Context, null);
                }
                else

                    // все осталные - бинд к объекту
                    this.Target.SelectedItem = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// Метод сохранения данных из контрола в контекст
        /// </summary>
        protected override void DoSave()
        {
            // если бинд к строке, т.е. простой выбор текста из справочника
            if (this.Property.PropertyType == typeof(string))
            {
                this.Property.SetValue(this.Context, this.Target.Text, null);
            }
            else
                if (this.Property.PropertyType == typeof(int))
                {
                    // если бинд к индексу. т.е. выбор елемента enum
                    this.Property.SetValue(this.Context, this.Target.SelectedIndex, null);
                }
                else

                    // все осталные - бинд к объекту
                    this.Property.SetValue(this.Context, this.Target.SelectedItem, null);
        }
    }
}