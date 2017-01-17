namespace Client
{
    using System;
    using System.Windows.Forms;

    using DevExpress.Skins;

    using Init.Tools;
    using Init.Tools.UI;

    using Client.UI;
    using DAL.WCF;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender1, args) =>
            {
                var error = args.ExceptionObject as Exception ?? new Exception("Неизвестная ошибка.");
                Log.AddException(new Exception("Необработанная ошибка в домене приложения", error));
            };

            Application.ThreadException += (sender, args) =>
            {
                var exception = new Exception("Необработанная ошибка в приложении", args.Exception);
                xMsg.Error(exception);
                Log.AddException(exception);
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SkinManager.EnableFormSkins();

            // Инициализируем WCF службу
            try
            {
                Log.Add("main", "создание контекста доступа к серверу приложений");
                var context = new WcfDataContext(Settings.Default.ServiceOperationAddress);

                var dataManager = new WcfDataManager(context);
                DalContainer.RegisterWcfDataMager(dataManager);

              //dataManager.ServiceOperationClient.TestConnection();
            }
            catch (Exception ex)
            {
                var error = new Exception("Не удалось подключиться к серверу", ex);
                Log.AddException(new Exception("Ошибка подключения к серверу", ex));
                xMsg.Error(error);
                return;
            }

            try
            {
                Log.Add("main", "Открытие главной формы");
                Log.AddException("error", new Exception("Ошибка открытия главного окна приложения"));
                Application.Run(new fmMain());
            }
            catch (Exception ex)
            {
                var error = new Exception("Ошибка открытия главного окна приложения", ex);
                Log.AddException("error" , new Exception("Ошибка открытия главного окна приложения", ex));
                xMsg.Error(error);
            }
            
        }
    }
}
