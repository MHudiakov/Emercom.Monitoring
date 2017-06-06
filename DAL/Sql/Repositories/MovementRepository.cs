using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using Init.DbCore.DB.Metadata;
using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Init.Tools;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public class MovementRepository : Repository<Movement>
    {
        public MovementRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Movement>(dataManager.GetContext), dataManager)
        {}

        private string MovementTableName => ((DbTableAttribute)Attribute.GetCustomAttribute(typeof(Movement), 
            typeof(DbTableAttribute))).TableName;

        /// <summary>
        /// Изменение последнего передвижения у оборудования
        /// </summary>
        /// <param name="movement"></param>
        protected override void AddOverride(Movement movement)
        {
            base.AddOverride(movement);
            var equipment = movement.GetEquipment;
            equipment.LastMovementId = movement.Id;
            DalContainer.GetDataManager.EquipmentRepository.Edit(equipment);
        }

        /// <summary>
        /// Метод получения списка передвижений по ид оборудования и промежутку времени
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="minTime"></param>
        /// <param name="maxTime"></param>
        /// <returns></returns>
        public List<Movement> GetByTimeAndUnitId(int unitId, DateTime minTime, DateTime maxTime)
        {
            try
            {
                var context = DalContainer.GetDataManager.GetContext();
                
                // Проверка корректности времени
                if (minTime < (DateTime) SqlDateTime.MinValue)
                    minTime = (DateTime) SqlDateTime.MinValue;

                if (maxTime <= minTime)
                    maxTime = DateTime.Now;
                
                var dt =
                    context.ExecuteDataTable(
                        $"SELECT * FROM [{MovementTableName}] WHERE [Date] >= @from AND [Date] < @to and UnitId = @unitId",
                        new SqlParameter("from", minTime),
                        new SqlParameter("to", maxTime),
                        new SqlParameter("unitId", unitId));

                if (dt.HasErrors)
                    throw new Exception($"Ошибка выполнения запроса к {MovementTableName} с параметрами: {unitId}");

                var listMovement = GetMovementList(dt);
                return listMovement;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Ошибка создания списка объектов {typeof(Movement).FullName} по идентификаторам объекта {unitId} за промежуток времени от {minTime} по {maxTime}",
                    ex);
            }
        }

        /// <summary>
        /// Метод получения списка движений оборудования по ид оборудования
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public List<Movement> GetByEquipmentId(int? equipmentId)
        {
            try
            {
                var result = this.GetItemsWhere(new Dictionary<string, object> {{"EquipmentId", equipmentId}});
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Ошибка создания списка объектов {typeof(Movement).FullName} по идентификаторам оборудования {equipmentId}",
                    ex);
            }
        }

        /// <summary>
        /// получение списка передвижений из таблицы
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<Movement> GetMovementList(DataTable dt)
        {
            var listMovement = (from DataRow row in dt.Rows
                                select
                                    new Movement
                                    {
                                        Id = row["Id"].ToInt(),
                                        UnitId = row["UnitId"].ToInt(),
                                        Date = row["Date"].ToDateTime(),
                                        EquipmentId = row["EquipmentId"].ToInt(),
                                        IsArrived = row["IsArrived"].ToBoolean()
                                    }).ToList();
            return listMovement;
        }

    }
}
