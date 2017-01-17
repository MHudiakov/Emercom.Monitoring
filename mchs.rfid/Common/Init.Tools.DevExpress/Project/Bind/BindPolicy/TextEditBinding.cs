// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextEditBinding.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Привязка к TextEdit
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind.BindPolicy
{
    using System;
    using System.Reflection;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Привязка к TextEdit
    /// </summary>
    /// <typeparam name="TBindContext">
    /// Тип объекта-источника данных
    /// </typeparam>
    public class TextEditBinding<TBindContext> : ControlBinding<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// Целевой TextEdit, к которому производится привязка
        /// </summary>
        public new TextEdit Target
        {
            get
            {
                return base.Target as TextEdit;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TextEditBinding{TBindContext}"/>.
        /// </summary>
        /// <param name="target">
        /// Целевой TextEdit, к которому производится привязка
        /// </param>
        /// <param name="context">
        /// Контекст
        /// </param>
        /// <param name="property">
        /// Свойства
        /// </param>
        public TextEditBinding(TextEdit target, TBindContext context, PropertyInfo property)
            : base(target, context, property)
        {
        }

        /// <summary>
        /// Метод загрузки данных в контрол
        /// </summary>
        protected override void DoLoad()
        {
            if (this.Property.PropertyType == typeof(string))
                this.Target.Text = this.Property.GetValue(this.Context, null) as string;
            else
                this.Target.EditValue = this.Property.GetValue(this.Context, null);
        }

        /// <summary>
        /// Метод сохранения данных из контрола в контекст
        /// </summary>
        protected override void DoSave()
        {
            if (this.Property.PropertyType == typeof(string))
                this.Property.SetValue(this.Context, this.Target.Text, null);
            {
                // получаем чистый тип свойства
                var pType = this.Property.PropertyType;
                if (Nullable.GetUnderlyingType(this.Property.PropertyType) != null)
                    pType = Nullable.GetUnderlyingType(this.Property.PropertyType);

                // если ничего не введено и поле допускает null
                if (Nullable.GetUnderlyingType(this.Property.PropertyType) == null && string.IsNullOrEmpty(this.Target.Text))
                    this.Property.SetValue(this.Context, null, null);
                else
                {
                    if (pType == typeof(int)) // преобразование к int
                        this.Property.SetValue(this.Context, this.Target.EditValue.ToInt(), null);
                    else if (pType == typeof(double)) // преобразование к double
                        this.Property.SetValue(this.Context, this.Target.EditValue.ToDouble(), null);
                    else // без преобразования
                        this.Property.SetValue(this.Context, this.Target.EditValue, null);
                }
            }
        }
    }
}