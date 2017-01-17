// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TcpClientBase.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс для прослушивания клиента
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web.Tcp
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    using Init.Tools;

    /// <summary>
    /// Класс для прослушивания клиента
    /// </summary>
    public abstract class TcpClientBase
    {
        /// <summary>
        /// Клиент TCP
        /// </summary>
        private readonly TcpClient _tcpClient;

        /// <summary>
        /// Базовый поток для доступа к сети
        /// </summary>
        private readonly NetworkStream _networkStream;

        /// <summary>
        /// Время (в секундах), через которое сокет считается неактивным и отключается
        /// </summary>
        private readonly int _clientInactiveTimeout;

        /// <summary>
        /// Поток обработки команд
        /// </summary>
        private Thread _worker;

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        public Loger Logger { get; private set; }

        /// <summary>
        /// Флаг: true, если поток обработки уже запущен
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this._worker != null;
            }
        }

        /// <summary>
        /// Дата последнего получения данных
        /// </summary>
        public DateTime DtLastActivity { get; private set; }

        #region Disconnected event
        /// <summary>
        /// Событие отключения клиента. Генерируется после отключения TcpКлиента
        /// </summary>
        public event Action<TcpClientBase> Disconnected;

        /// <summary>
        /// Генерирует собитые Disconnected
        /// </summary>
        protected virtual void OnDisconnected()
        {
            var handler = this.Disconnected;
            if (handler != null)
                handler(this);
        }
        #endregion

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TcpClientBase"/> со значениями по умолчанию
        /// </summary>
        /// <param name="client">Переданный службой TCP клиент</param>
        protected TcpClientBase(TcpClient client)
            : this(client, 30, 30, 300)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TcpClientBase"/>
        /// </summary>
        /// <param name="client">Переданный службой TCP клиент</param>
        /// <param name="readTimeout">Таймаут чтения в секундах</param>
        /// <param name="writeTimeout">Таймаут записи в секундах</param>
        /// <param name="clientInactiveTimeout">Таймаут, после которого клиент считается неактивным. Если 0, то игнорируется</param>
        protected TcpClientBase(TcpClient client, int readTimeout, int writeTimeout, int clientInactiveTimeout)
        {
            this.DtLastActivity = DateTime.Now;
            if (client == null)
                throw new ArgumentNullException();
            this.Logger = new Loger();

            this._tcpClient = client;
            this._networkStream = this._tcpClient.GetStream();
            this._networkStream.ReadTimeout = readTimeout * 1000;
            this._networkStream.WriteTimeout = writeTimeout * 1000;
            this._clientInactiveTimeout = clientInactiveTimeout;
            this._tcpClient.NoDelay = true;
        }

        /// <summary>
        /// Запуск потока обработки команд
        /// </summary>
        public void Start()
        {
            if (this._worker != null)
                throw new InvalidOperationException("Поток обработки команд уже запущен.");
            if (!this._tcpClient.Connected)
                throw new InvalidOperationException("Невозможно запустить поток обработки команд, т.к. клиент отлючен.");

            this.DtLastActivity = DateTime.Now;
            this._worker = new Thread(this.ExecuteClient) { IsBackground = true };
            this._worker.Start();
        }

        /// <summary>
        /// Метод работы с потоком данных
        /// </summary>
        private void ExecuteClient()
        {
            try
            {
                while (true)
                {
                    if (!this._networkStream.DataAvailable)
                    {
                        Thread.Sleep(100);

                        // если клиент не шлет данные дольше _clientInactiveTimeout, переводим его в оффлайн и отключаем
                        if (this._clientInactiveTimeout != 0 &&
                            (DateTime.Now - this.DtLastActivity).TotalSeconds > this._clientInactiveTimeout)
                        {
                            this.Logger.LogMsg(string.Format("Клиент был неактивен в течение {0} сек. Обработка команд будет прекращена.", this._clientInactiveTimeout));
                            this.Close();
                            return;
                        }

                        continue;
                    }

                    this.DtLastActivity = DateTime.Now;

                    try
                    {
                        this.ReadData();
                    }
                    catch (IOException ex)
                    {
                        this.Logger.LogException(new Exception("Ошибка транспортного соединения. Соединение будет закрыто.", ex));
                        this.Close();
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
                this.Logger.LogMsg("Завершения потока обработки команд");
            }
            catch (Exception ex)
            {
                this.Logger.LogException(new Exception("Непредвиденная ошибка в потоке обработки команд. Соединение будет закрыто.", ex));
                this.Close();
            }
            finally
            {
                this._tcpClient.Close();
            }
        }

        /// <summary>
        /// Функция, реализующая чтение порции данных из транспортного соединения
        /// </summary>
        protected abstract void ReadData();

        /// <summary>
        /// Завершает работу потока обработки команд и закрывает соединение
        /// </summary>
        public void Close()
        {
            if (this._worker != null)
            {
                this._worker.Interrupt();
                this._tcpClient.Close();
                this.OnDisconnected();
            }

            this._worker = null;
        }
    }
}
