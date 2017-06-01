// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnpointDispathcherBehavior.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс - диспетчер инициализации конечной точки.
//   Нужен для логированя вызовов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web
{
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Класс - диспетчер инициализации конечной точки.
    /// Нужен для логированя вызовов
    /// </summary>
    public class EnpointDispathcherBehavior : IEndpointBehavior
    {
        /// <summary>
        /// Кэшированная ссылка на инспектор парметров
        /// </summary>
        public IParameterInspector ParametrInspector { get; private set; }

        /// <summary>
        /// Кэшированная ссылка на обработчик ошибок
        /// </summary>
        public IErrorHandler ErrorHandler { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EnpointDispathcherBehavior"/>.
        /// </summary>
        /// <param name="parametrInpector">
        /// Инспектор парметров
        /// </param>
        /// <param name="errorHandler">
        /// Обработчик ошибок
        /// </param>
        public EnpointDispathcherBehavior(IParameterInspector parametrInpector, IErrorHandler errorHandler)
        {
            this.ParametrInspector = parametrInpector;
            this.ErrorHandler = errorHandler;
        }

        /// <summary>
        /// Добавление параметров привязки
        /// </summary>
        /// <param name="endpoint">
        /// Конечная точка
        /// </param>
        /// <param name="bindingParameters">
        /// Параметры привязки
        /// </param>
        public void AddBindingParameters(
            ServiceEndpoint endpoint,
            System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Позволяет поведению включать расширения прокси (клиента)
        /// </summary>
        /// <param name="endpoint">
        /// Конечная точка
        /// </param>
        /// <param name="clientRuntime">
        /// Среда выполнения клиента 
        /// </param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            if (ParametrInspector != null)
                foreach (var item in clientRuntime.Operations)
                    item.ParameterInspectors.Add(this.ParametrInspector);
        }

        /// <summary>
        /// Реализует изменение или оснастку расширения службы на протяжении всей конечной точки
        /// </summary>
        /// <param name="endpoint">
        /// Конечная точка
        /// </param>
        /// <param name="endpointDispatcher">
        /// Расширенный диспетчер конечной точки, который необходимо изменить
        /// </param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            if (ParametrInspector != null)
                foreach (var item in endpointDispatcher.DispatchRuntime.Operations)
                    item.ParameterInspectors.Add(this.ParametrInspector);

            if (ErrorHandler != null)
                endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(this.ErrorHandler);
        }

        /// <summary>
        /// Валидация
        /// </summary>
        /// <param name="endpoint">
        /// Конечная точка
        /// </param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}