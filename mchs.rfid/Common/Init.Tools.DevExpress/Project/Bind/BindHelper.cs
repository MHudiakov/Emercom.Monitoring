// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Помошник создания привязок
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.Bind
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using global::DevExpress.XtraEditors;

    using Init.Tools.DevExpress.Bind.BindPolicy;

    /// <summary>
    /// Помошник создания привязок
    /// </summary>
    /// <typeparam name="TBindContext">Тип оббъекта - источника данных</typeparam>
    public class BindHelper<TBindContext>
        where TBindContext : class
    {
        /// <summary>
        /// Элемент управления
        /// </summary>
        public Control Control { get; private set; }

        /// <summary>
        /// Контекст 
        /// </summary>
        public TBindContext Context { get; private set; }

        /// <summary>
        /// Помошник создания привязок
        /// </summary>
        /// <param name="control">Родительский элемент, на котором ищем связываемые контролы</param>
        /// <param name="context">Объект - источник данных</param>
        public BindHelper(Control control, TBindContext context)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            if (context == null)
                throw new ArgumentNullException("context");

            this.Control = control;
            this.Context = context;
            this.ControlNameMatchFilter = @"(\A[a-z]{2,4})(?<PropName>(?:_[a-z])?[A-Z]+\w+)";
            this.Bindings = new List<ControlBinding<TBindContext>>();
            this.ExcludeFromBindingControls = new List<Control>();
        }

        /// <summary>
        /// RegExp фильтр. должен совпадать для имени контрола, в результатах фильтр
        /// должен иметь группу PropName, совпадающую с именем свойства в объекте
        /// </summary>
        public string ControlNameMatchFilter { get; set; }

        /// <summary>
        /// Привязки
        /// </summary>
        public List<ControlBinding<TBindContext>> Bindings { get; private set; }

        /// <summary>
        /// Контролы, которые нужно исключить при образовании привязок
        /// </summary>
        public List<Control> ExcludeFromBindingControls { get; private set; }

        /// <summary>
        /// Создание привязок
        /// </summary>
        /// <exception cref="Exception">
        /// Исключение отстствия контекста привязки
        /// </exception>
        public void CreateBindings()
        {
            if (this.Context == null)
                throw new Exception("Контекст привязки не задан");

            var properties = this.Context.GetType().GetProperties().ToList();
            var bindedControls = this.GetAllAcceptableControls(this.Control).ToList();
            foreach (var binding in from bindedControl in bindedControls
                                    let match = Regex.Match(bindedControl.Name, this.ControlNameMatchFilter)
                                    where match.Success
                                    where match.Groups["PropName"] != null
                                    let prop =
                                        properties.SingleOrDefault(
                                            p => string.Equals(p.Name, match.Groups["PropName"].Value.Trim('_'), StringComparison.CurrentCultureIgnoreCase))
                                    where prop != null
                                    select this.CreateBinding(bindedControl, prop))
                this.Bindings.Add(binding);
        }

        /// <summary>
        /// Создание привязки
        /// </summary>
        /// <param name="edit">
        /// Поле ввода
        /// </param>
        /// <param name="property">
        /// Свойства
        /// </param>
        /// <returns>
        /// Возвращает созданную привязку объекта к элементу формы
        /// </returns>
        protected virtual ControlBinding<TBindContext> CreateBinding(Control edit, PropertyInfo property)
        {
            if (edit is LookUpEditBase)
                return new LookUpEditBaseBinding<TBindContext>(edit as LookUpEditBase, this.Context, property, this.ResolveItemsCollection(edit, property));

            if (edit is DateEdit)
                return new DateEditBinding<TBindContext>(edit as DateEdit, this.Context, property);

            if (edit is ComboBoxEdit)
                return new ComboBoxEditBinding<TBindContext>(edit as ComboBoxEdit, this.Context, property, this.ResolveItemsCollection(edit, property));

            if (edit is TextEdit)
                return new TextEditBinding<TBindContext>(edit as TextEdit, this.Context, property);

            if (edit is CheckEdit)
                return new CheckEditBind<TBindContext>(edit as CheckEdit, this.Context, property);

            if (edit is BaseEdit)
                return new BaseEditBinding<TBindContext>(edit as BaseEdit, this.Context, property);

            return new ControlBinding<TBindContext>(edit, this.Context, property);
        }

        /// <summary>
        /// Событие поиска коллекции элементов для компонента, редактируюущего список элементов
        /// </summary>
        public event Func<Control, PropertyInfo, ICollection> OnResolveItemsCollection;

        /// <summary>
        /// Поиск коллекции элементов для компонента, редактируюущего список элементов
        /// </summary>
        /// <param name="edit">
        /// Поле ввода
        /// </param>
        /// <param name="prop">
        /// Свойства
        /// </param>
        /// <returns>
        /// Коллекции элементов для компонента, редактируюущего список элементов
        /// </returns>
        protected virtual ICollection ResolveItemsCollection(Control edit, PropertyInfo prop)
        {
            return this.OnResolveItemsCollection != null ? this.OnResolveItemsCollection(edit, prop) : null;
        }

        /// <summary>
        /// Получение всех дочерних компонентов управления 
        /// </summary>
        /// <param name="parent">
        /// Родительский контрол
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<Control> GetAllAcceptableControls(Control parent)
        {
            var list = new List<Control>();

            if (this.ExcludeFromBindingControls.Contains(parent))
                return list;

            // добаляем все подходящие контролы в список
            list.AddRange(parent.Controls.OfType<Control>().Where(c => Regex.IsMatch(c.Name, this.ControlNameMatchFilter)).ToList());

            // ищем на вложенных компонентах
            var chieldControls = parent.Controls.OfType<Control>().ToList();
            foreach (var chieldControl in chieldControls)
                list.AddRange(this.GetAllAcceptableControls(chieldControl));

            list.RemoveAll(c => this.ExcludeFromBindingControls.Contains(c));
            return list;
        }

        /// <summary>
        /// Загрузка данных в связанные контролы
        /// </summary>
        public void LoadBindings()
        {
            foreach (var binding in this.Bindings)
                binding.Load();
        }

        /// <summary>
        /// Сохранение данных в объект
        /// </summary>
        public void SaveBindings()
        {
            foreach (var binding in this.Bindings)
                binding.Save();
        }
    }
}