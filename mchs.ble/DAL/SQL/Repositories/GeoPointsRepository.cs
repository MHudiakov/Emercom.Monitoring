using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Dal.SQL.Repositories
{
    using System.Data;
    using System.Data.Odbc;
    using System.Data.SqlClient;

    using DAL;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Init.Tools;

    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий Гео меток
    /// </summary>
    public sealed class GeoPointsRepository : Repository<GeoPoints>
    {
        
        /// <summary>
        /// Конструктор репозитория гео меток
        /// </summary>
        /// <param name="dataManager"></param>
        public GeoPointsRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<GeoPoints>(dataManager.GetContext), dataManager)
        {
        }

        /// <summary>
        /// Получения списка координат по объекту и промежутку времени
        /// </summary>
        /// <param name="unitId">Машина</param>
        /// <param name="minTime">Начальное время</param>
        /// <param name="maxTime">Конечное время</param>
        /// <returns>Список гео-точек</returns>
        public List<GeoPoints> GetGeoPointListByTime(int unitId, DateTime minTime, DateTime maxTime)
        {
            try
            {
                var context = DalContainer.DataManager.GetContext();
                const string TableName = "GeoPoints";
                var dt =
                    context.ExecuteDataTable(
                        string.Format(
                            "SELECT * FROM [{0}] WHERE[UnitId] = @UnitId AND  [Time] >= @from AND [Time] < @to",
                            TableName),
                        new SqlParameter("UnitId", unitId),
                        new SqlParameter("from", minTime),
                        new SqlParameter("to", maxTime));

                if (dt.HasErrors)
                    throw new Exception(
                        string.Format("Ошибка выполнения запроса к {0} с параметрами:{1}", TableName, unitId));


                var listPoints = (from DataRow row in dt.Rows
                                  select
                                      new GeoPoints
                                          {
                                              Id = row["Id"].ToInt(),
                                              UnitId = row["UnitId"].ToInt(),
                                              Time = row["Time"].ToDateTime(),
                                              Latitude = row["Latitude"].ToDouble(),
                                              Longitude = row["Longitude"].ToDouble(),
                                              TripId=row["TripId"].ToInt()
                                          }).ToList();
                return listPoints;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format(
                        "Ошибка создания списка объектов {0} по идентификаторам объекта {1} за промежуток времени от {2} по {3}",
                        typeof(GeoPoints).FullName,
                        unitId,
                        minTime,
                        maxTime),
                    ex);
            }
        }

        /// <summary>
        /// Получение списка координат по поездке
        /// </summary>
        /// <param name="tripId">Поездка (вызов)</param>
        /// <param name="idFrom">Идектификатор начальной гео-точки</param>
        /// <param name="count">Количество получаемых гео-точек</param>
        /// <returns>Список гео-точек</returns>
        public List<GeoPoints> GetGeoPointsByTripId(int tripId, int idFrom, int count = -1)
        {
            try
            {
                var sql = count != -1
                                 ? string.Format(@"Select TOP {0} * FROM GeoPoints Where TripId = @tripId AND Id >= @idFrom", count)
                                 : @"Select * FROM GeoPoints Where TripId = @tripId AND Id >= @idFrom";

                var context = DalContainer.DataManager.GetContext();
                var dt = context.ExecuteDataTable(sql, new[] { new OdbcParameter("TripId", tripId), new OdbcParameter("idFrom", idFrom) });

                var listPoints = (from DataRow row in dt.Rows
                                  select
                                      new GeoPoints
                                      {
                                          Id = row["Id"].ToInt(),
                                          UnitId = row["UnitId"].ToInt(),
                                          Time = row["Time"].ToDateTime(),
                                          Latitude = row["Latitude"].ToDouble(),
                                          Longitude = row["Longitude"].ToDouble(),
                                          TripId = row["TripId"].ToInt()
                                      }).ToList();
                return listPoints;
            }
            catch (Exception ex)
            {
                throw new Exception(
                      string.Format(
                          "Ошибка создания списка гео-точек {0} по идентификатору вызова {1}",
                          typeof(GeoPoints).FullName,
                          tripId),
                      ex);
            }
        }
    }
}
