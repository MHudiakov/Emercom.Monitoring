// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProcessor.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Обработчик свойств обектов класса DbObject
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Init.DbCore.Processing.Attributes;
    using Init.Tools;

    /// <summary>
    /// Обработчик свойств обектов класса DbObject
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта для обработки
    /// </typeparam>
    public class ModelProcessor<T>
        where T : class
    {
        #region SingletoneInstance
        /// <summary>
        /// SingletoneInstance lazzy
        /// </summary>
        private static readonly Lazy<ModelProcessor<T>> s_lazy = new Lazy<ModelProcessor<T>>(() => new ModelProcessor<T>(), true);

        /// <summary>
        /// Процессор модели T
        /// </summary>
        public static ModelProcessor<T> Active
        {
            get
            {
                return s_lazy.Value;
            }
        }
        #endregion

        #region PropertyValidation class
        /// <summary>
        /// Пара, описывающая валидируемое свойство
        /// </summary>
        /// <typeparam name="TOwner">Тип класса-владельца свойства</typeparam>
        internal class PropertyValidation<TOwner>
            where TOwner : class
        {
            [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
            public PropertyValidation(ProcessAttribute attribute, PropertyHelper<TOwner> propertyHelper)
            {
                this.PropertyHelper = propertyHelper;
                this.Attribute = attribute;
                AttributeType = this.Attribute.GetType();
            }

            /// <summary>
            /// Тип атрибута
            /// </summary>
            public Type AttributeType { get; private set; }

            /// <summary>
            /// Атрибут свойства
            /// </summary>
            public ProcessAttribute Attribute { get; private set; }

            /// <summary>
            /// Обертка свойства
            /// </summary>
            public PropertyHelper<TOwner> PropertyHelper { get; private set; }
        }
        #endregion

        /// <summary>
        /// Связь атрибутов со стратегией реализующей обработку
        /// </summary>
        private readonly Dictionary<Type, ModelProcessStrategy<T>> _strategies;

        /// <summary>
        /// Свойства, отмеченые атрибутами
        /// </summary>
        private readonly List<PropertyValidation<T>> _propertyHelpers;

        /// <summary>
        /// Обработчик свойств обектов класса DbObject
        /// </summary>
        protected ModelProcessor()
        {
            this._strategies = new Dictionary<Type, ModelProcessStrategy<T>>();

            this._propertyHelpers = new List<PropertyValidation<T>>();

            // Перебираем все аттрибуты у свойств объекта
            var modelType = typeof(T);

            foreach (var propertyInfo in modelType.GetProperties())
                foreach (var attribute in propertyInfo.GetCustomAttributes(true).OfType<ProcessAttribute>())
                    this._propertyHelpers.Add(new PropertyValidation<T>(attribute, new PropertyHelper<T>(propertyInfo, "ModelProcessor")));

            // Выбираем расширенные атрибуты ExtAttribute класса
            foreach (var extAttribute in modelType.GetCustomAttributes(true).OfType<ExtProcessAttribute>())
            {
                var propertyInfo = modelType.GetProperty(extAttribute.PropertyName);

                // Атрибут указывает на несуществующее поле, прекращаем обработку
                if (propertyInfo == null)
                    throw new InvalidOperationException(string.Format("Не найдено свойство : {0} у объекта : {1}, помеченного атрибутом : {2}", extAttribute.PropertyName, modelType, extAttribute));

                this._propertyHelpers.Add(new PropertyValidation<T>(extAttribute, new PropertyHelper<T>(propertyInfo, "ModelProcessor")));
            }

            this.RegisterStrategies();
        }

        /// <summary>
        /// Выполняет регистрацию стратегий по умолчанию
        /// </summary>
        private void RegisterStrategies()
        {
            this.RegisterStrategy<DbTrimToAttribute>(new ModelTrimStrategy<T>());
            this.RegisterStrategy<ExtDbTrimTo>(new ModelTrimStrategy<T>());

            this.RegisterStrategiesOverride();
        }

        /// <summary>
        /// Выполняет регистрацию стратегий
        /// </summary>
        protected virtual void RegisterStrategiesOverride()
        {
        }

        /// <summary>
        /// Выполнить обработку свойств объекта DbObject
        /// </summary>
        /// <param name="item">
        /// Объект для обработки
        /// </param>
        /// <param name="throwError">
        /// Флаг: если true, то вместо коллекции ошибок модели будет сгенерировано исключение
        /// </param>
        /// <returns>
        /// Список ошибок валидации модели
        /// </returns>
        public List<ModelValidationError> Process(T item, bool throwError = true)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var errors = new List<ModelValidationError>();

            foreach (var validation in _propertyHelpers)
            {
                ModelProcessStrategy<T> strategy;
                _strategies.TryGetValue(validation.AttributeType, out strategy);
                if (strategy == null)
                    throw new InvalidOperationException(string.Format("Не зарегистрирована стратегия валидаци модели для атрибута {0}", validation.AttributeType));
                try
                {
                    strategy.Process(item, validation.PropertyHelper, validation.Attribute.Args);
                }
                catch (ModelPropertyValidationException ex)
                {
                    errors.Add(new ModelValidationError(ex.PropertyName, ex.Message));
                }
            }

            if (errors.Count > 0 && throwError)
                throw new ModelProcessException(errors);

            return errors;
        }

        /// <summary>
        /// Зарегистрировать стратегию выполнения обработки над свойством объекта
        /// </summary>
        /// <typeparam name="TAttribute">Стратегия выполнения обработки свойства</typeparam>
        /// <param name="strategy">Атрибут свойства над которым будет выполняться обработка</param>
        public void RegisterStrategy<TAttribute>(ModelProcessStrategy<T> strategy)
            where TAttribute : ProcessAttribute
        {
            if (strategy == null)
                throw new ArgumentNullException("strategy");

            var propertyHelpers = this._propertyHelpers.Where(e => e.AttributeType == typeof(TAttribute)).Select(e => e.PropertyHelper).ToList();

            // Проверка применения данной стратегия к свойствам объекта отмеченным атрибутами
            strategy.Validate(propertyHelpers);

            // регистрируем связь атрибут-стратегия
            this._strategies.Add(typeof(TAttribute), strategy);
        }
    }
}
