using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Dal.SQL.Repositories
{
    using System.Data.SqlClient;

    using DAL;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Init.Tools;

    using Server.Dal.SQL.DataObjects;

    public class TripRepository : Repository<Trip>
    {
          /// <summary>
        /// Конструктор репозитория поездок
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public TripRepository(DalDataManager dataManager)
            : base(new MsSqlDataAccess<Trip>(dataManager.GetContext), dataManager)
        {
        }

        /// <summary>
        /// Получение последней поездки машины
        /// </summary>
        /// <param name="unitId">Идентификатор машины</param>
        /// <returns>Поездка (вызов)</returns>
        public Trip GetLastTrip(int unitId)
        {
            try
            {
                var context = DalContainer.DataManager.GetContext();
                const string TableName = "Trip";
                var dt = context.ExecuteDataTable(
                    string.Format("SELECT * FROM [{0}] WHERE UnitId=@UnitId ORDER BY StartTime DESC", TableName),
                    new SqlParameter("UnitId", unitId));
                if (dt.HasErrors)
                    throw new Exception(
                        string.Format("Ошибка выполнения запроса к {0} с параметрами:{1}", TableName, unitId));

                if (dt.Rows.Count == 0)
                    return null;

                var row = dt.Rows[0];

                var lastTrip = new Trip
                {
                    Id = row["Id"].ToInt(),
                    StartTime = row["StartTime"].ToDateTime(),
                    EndTime = row["EndTime"].ToDateTime(),
                    UnitId = row["UnitId"].ToInt()
                };

                return lastTrip;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format(
                        "Ошибка создания списка вызовов {0} по идентификатору объекта {1} ",
                        typeof(Trip).FullName,
                        unitId,
                     ex));
            }
        }
    }
}
