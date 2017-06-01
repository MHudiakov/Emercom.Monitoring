// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseWebService.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базоый класс веб сервиса
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Dispatcher;
    using System.ServiceModel.Web;

    using Init.Tools;

    /// <summary>
    /// Базоый класс веб сервиса
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
                 InstanceContextMode = InstanceContextMode.Single,
                 IncludeExceptionDetailInFaults = true)]
    public abstract class BaseWebService
    {
        /// <summary>
        /// Базоый класс веб сервиса
        /// </summary>
        /// <param name="serviceType">
        /// Тип объекта сервиса
        /// </param>
        protected BaseWebService(Type serviceType)
        {
            this.Logger = new Loger();
            this._serviceType = serviceType;
        }

        /// <summary>
        /// Событие перехода хоста в состояние faulted 
        /// </summary>
        public event EventHandler Faulted;

        /// <summary>
        /// Событие перехода хоста в состояние closed
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Переход хоста в состояние closed
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnClosed(object sender, EventArgs args)
        {
            var handler = this.Closed;
            if (handler != null)
                handler(sender, args);
        }

        /// <summary>
        /// Переход хоста в состояние faulted
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnFaulted(object sender, EventArgs args)
        {
            var handler = this.Faulted;
            if (handler != null)
                handler(sender, args);
        }

        /// <summary>
        /// Хост сервиса
        /// </summary>
        public ServiceHost Host { get; private set; }

        #region ServiceObject
        /// <summary>
        /// Тип объекта сервиса
        /// </summary>
        private readonly Type _serviceType;

        /// <summary>
        /// Тип объекта сервиса
        /// </summary>
        protected Type ServiceType
        {
            get
            {
                return this._serviceType;
            }
        }
        #endregion

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        public Loger Logger { get; private set; }

        #region Service functions

        /// <summary>
        /// Запуск сервиса
        /// </summary>
        public virtual void Start()
        {
            try
            {
                this.Logger.LogMsg("Создание сервиса.");
                this.Host = this.CreateServiceHost();

                this.Logger.LogMsg("Настройка параметров подключения...");
                this.ConfigureCore();
                this.Logger.LogMsg("Настройка параметров подключения завершена.");

                this.Logger.LogMsg("Подписка на изменение состояния сервиса");

                // подписываемся на события хоста
                this.Host.Closed += this.HostOnClosed;
                this.Host.Faulted += this.HostOnFaulted;
                this.Logger.LogMsg("Запуск сервиса");
                this.Host.Open();

                this.Logger.LogMsg("Описание методов сервиса:");
                foreach (var disp in this.Host.ChannelDispatchers.OfType<ChannelDispatcher>())
                {
                    foreach (var endp in disp.Endpoints)
                    {
                        this.Logger.LogMsg(string.Format("\tКонечная точка: {0}\n\tПодключение: {1}:", endp.EndpointAddress.Uri, disp.BindingName));
                        foreach (var op in endp.DispatchRuntime.Operations)
                        {
                            this.Logger.LogMsg(string.Format("\t\t операция: {0,-20} action: {1,-20}", op.Name, op.Action));
                        }
                    }
                }

                foreach (var interf in this.ServiceType.GetInterfaces().Where(e => e.GetCustomAttributes(true).OfType<ServiceContractAttribute>().Any()))
                {
                    this.Logger.LogMsg(string.Format("Описание Web интерфейсов:"));
                    var methods = interf.GetMethods().Where(e => e.GetCustomAttributes(true).OfType<WebInvokeAttribute>().Any()).ToList();
                    foreach (var method in methods)
                    {
                        var att = method.GetCustomAttributes(true).OfType<WebInvokeAttribute>().Single();
                        this.Logger.LogMsg(string.Format("\tметод: {0}", att.UriTemplate));
                    }
                }

                this.Logger.LogMsg("cервис запущен.");
            }
            catch (Exception ex)
            {
                this.Logger.LogException(new Exception("Произошла ошибка в процессе запуска сервиса", ex));
                throw;
            }
        }

        /// <summary>
        /// Функция обработки события faulted у хоста
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void HostOnFaulted(object sender1, EventArgs args)
        {
            this.Logger.LogMsg("WEB служба перешла в состояние:" + this.Host.State);
            this.Host.Faulted -= this.HostOnFaulted;
            this.OnFaulted(sender1, args);
        }

        /// <summary>
        /// Функция обработки события closed у хоста
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void HostOnClosed(object sender1, EventArgs args)
        {
            this.Logger.LogMsg("WEB служба перешла в состояние:" + this.Host.State);
            this.Host.Closed -= this.HostOnClosed;
            this.OnClosed(sender1, args);
        }

        /// <summary>
        /// Метод создания хоста сервиса.
        /// </summary>
        /// <remarks>
        /// По умолчаню создается хост для SingletoneInstance
        /// </remarks>
        /// <returns>Экземпляр хоста</returns>
        protected virtual ServiceHost CreateServiceHost()
        {
            var serviceHost = new ServiceHost(this);
            return serviceHost;
        }

        /// <summary>
        /// Остановка сервиса
        /// </summary>
        public virtual void Stop()
        {
            try
            {
                if (this.Host != null)
                    this.Host.Close();
                this.Host = null;
            }
            catch (Exception ex)
            {
                this.Logger.LogException(ex);
            }
        }

        /// <summary>
        /// Конфигурация хоста.
        /// Устанавливает механизмы наблюдения и обработки ошибок
        /// </summary>
        private void ConfigureCore()
        {
            this.Configure();
            this.Logger.LogMsg("Настройка наблюдения вызова методов сервиса");
            foreach (var endpoint in Host.Description.Endpoints)
            {
                // настройка наблюдения вызова методов сервиса
                var parametrInspector = new OperationParameterInspector();
                parametrInspector.OnMethodBegin += (method, inputs) =>
                {
                    var inputString = (ReferenceEquals(inputs, null) || inputs.Length == 0)
                                          ? "null"
                                          : inputs.Select(item => item != null ? item.ToString() : "null")
                                                .Aggregate(
                                                    (current, item) =>
                                                    (!string.IsNullOrEmpty(current) ? current + "," : string.Empty) + item);

                    this.Logger.LogMsg(string.Format("{0}({1}) >>", method, inputString));
                };

                parametrInspector.OnMethodEnd +=
                    (methodName, outputs, ret, executingTime) =>
                    this.Logger.LogMsg(string.Format("{0}(...): {1} << [{2}]", methodName, ret ?? "null", executingTime));

                this.Logger.LogMsg("Настройка обработчика ошибок канала");

                // Настройка обработчика ошибок канала
                var errorHandler = new DispatcherErrorHandler();
                errorHandler.OnHandleError += this.Logger.LogException;
                var behavior = new EnpointDispathcherBehavior(parametrInspector, errorHandler);

                this.Logger.LogMsg("Подключение настроек конечной точки");

                // подключение настроек конечной точки
                endpoint.Behaviors.Add(behavior);
            }
        }

        /// <summary>
        /// Выполняет настройку хоста перед запуском
        /// </summary>
        protected virtual void Configure()
        {
        }

        #endregion
    }
}
