// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpListenerBase.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Прослушиватель подключений треккеров
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web.Tcp
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using Init.Tools;

    /// <summary>
    /// Прослушиватель подключений треккеров
    /// </summary>
    public abstract class TcpListenerBase
    {
        #region Container
        /// <summary>
        /// Активный TrackingTcpListener
        /// </summary>
        private static TcpListenerBase s_current;

        /// <summary>
        /// Активный прослушиватель подключений треккеров
        /// </summary>
        public static TcpListenerBase Current
        {
            get
            {
                if (s_current == null)
                    throw new InvalidOperationException("Прослушиватель подключений треккеров не зарегистрирован");
                return s_current;
            }
        }

        /// <summary>
        /// Регистрирует прослушиватель подключений треккеров
        /// </summary>
        /// <param name="listenerBase">Прослушиватель подключений треккеров</param>
        public static void Register(TcpListenerBase listenerBase)
        {
            if (listenerBase == null)
                throw new ArgumentNullException("listenerBase");
            s_current = listenerBase;
        }
        #endregion

        /// <summary>
        /// Список клиентов
        /// </summary>
        private readonly List<TcpClientBase> _trackingClients;

        /// <summary>
        /// Адрес просоушивателя
        /// </summary>
        private readonly IPEndPoint _endPoint;

        /// <summary>
        /// Прослушивающий объект
        /// </summary>
        private TcpListener _tcpListener;

        /// <summary>
        /// Поток прослушивания подключений
        /// </summary>
        private Thread _worker;

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        public Loger Logger { get; private set; }

        /// <summary>
        /// Флаг: true, если поток просулшивания уже запущен
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this._worker != null;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TcpListenerBase"/>.
        /// </summary>
        /// <param name="endPoint">Адрес прослушивателя</param>
        protected TcpListenerBase(IPEndPoint endPoint)
        {
            this.Logger = new Loger();
            this._endPoint = endPoint;
            this._trackingClients = new List<TcpClientBase>();
        }

        /// <summary>
        /// Запуск потока прослушивания подключений
        /// </summary>
        public void Start()
        {
            if (this._worker != null)
                throw new InvalidOperationException("Прослушивание уже запущено.");

            this.Logger.LogMsg(string.Format("Запуск прослушивания по адресу: {0}", this._endPoint));

            this._tcpListener = new TcpListener(this._endPoint);

            try
            {
                this._tcpListener.Start();
                this.Logger.LogMsg(string.Format("Прослушивание {0} успешно открыто.", this._endPoint));
            }
            catch (Exception ex)
            {
                var err = new Exception("Ошибка открытия прослушивателя.", ex);
                this.Logger.LogException(err);
                throw;
            }

            this._worker = new Thread(this.TcpListenTask) { IsBackground = true };
            this._worker.Start();
        }

        /// <summary>
        /// Метод прослушивания потока до принятия сообщения от клиента
        /// </summary>
        private void TcpListenTask()
        {
            while (true)
            {
                try
                {
                    TcpClient tcpClient = this._tcpListener.AcceptTcpClient();

                    // создаем нового клиента
                    var client = this.CreateClient(tcpClient);

                    client.Logger.OnLogMsg += this.ClientLogMsg;
                    client.Logger.OnLogException += this.ClietnLogException;
                    client.Disconnected += this.OnClientDisconnected;

                    lock (this._trackingClients)
                        this._trackingClients.Add(client);

                    client.Start();

                    this.Logger.LogMsg(string.Format("Клиент подключен: {0}", tcpClient.Client.RemoteEndPoint));
                }
                catch (Exception ex)
                {
                    this.Logger.LogException(new Exception("Ошиюбка подключения клиента", ex));
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        /// <summary>
        /// Фабричиый метод создания клиента
        /// </summary>
        /// <param name="tcpClient">TCP сокет</param>
        /// <returns>TCP клиента</returns>
        protected abstract TcpClientBase CreateClient(TcpClient tcpClient);

        /// <summary>
        /// Обработчик отключения клиента
        /// </summary>
        /// <param name="clientBase">Отключенный клиент</param>
        private void OnClientDisconnected(TcpClientBase clientBase)
        {
            lock (this._trackingClients)
                this._trackingClients.Remove(clientBase);
            clientBase.Logger.OnLogException -= this.ClietnLogException;
            clientBase.Logger.OnLogMsg -= this.ClientLogMsg;
        }

        /// <summary>
        /// Перенаправление ошибок клиента
        /// </summary>
        /// <param name="ex">Ошибка на клиенте</param>
        private void ClietnLogException(Exception ex)
        {
            this.Logger.LogException(new Exception("Ошибка работы с клиентом.", ex));
        }

        /// <summary>
        /// Перенаправление сообщений клиента
        /// </summary>
        /// <param name="msg">Сообщение клиента</param>
        private void ClientLogMsg(string msg)
        {
            this.Logger.LogMsg("Client: " + msg);
        }
    }
}
