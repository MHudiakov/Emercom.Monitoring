using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public class MovementRepository : Repository<Movement>
    {
        public MovementRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Movement>(dataManager.GetContext), dataManager)
        {}

        //private string TableName = "Movement";

        ///// <summary>
        ///// Изменение последнего передвижения
        ///// </summary>
        ///// <param name="item"></param>
        //protected override void AddOverride(Movement item)
        //{
        //    base.AddOverride(item);
        //    var equipment =
        //        DalContainer.DataManager.EquipmentRepository.GetAll()
        //            .FirstOrDefault(eq => eq.Id == item.Equipment.Id);
        //    equipment.LastMovementId = item.Id;
        //    DalContainer.DataManager.EquipmentRepository.Edit(equipment);
        //}

        ///// <summary>
        ///// Метод получения списка передвижений по ид оборудования и промежутку времени
        ///// </summary>
        ///// <param name="equipmentId"></param>
        ///// <param name="minTime"></param>
        ///// <param name="maxTime"></param>
        ///// <returns></returns>
        //public List<Movement> GetByTimeAndUnitId(int unitId, DateTime minTime, DateTime maxTime)
        //{
        //    try
        //    {
        //        var context = DalContainer.DataManager.GetContext();
        //        //проверка наличия времени
        //        minTime = minTime < (DateTime)SqlDateTime.MinValue ? (DateTime)SqlDateTime.MinValue : minTime;
        //        maxTime = maxTime <= (DateTime)SqlDateTime.MinValue ? DateTime.Now : maxTime;
        //        var dt =
        //            context.ExecuteDataTable(
        //                string.Format(
        //                    "SELECT * FROM [{0}] WHERE[DateOfMovement] >= @from AND [DateOfMovement] < @to",
        //                    TableName),
        //                new SqlParameter("from", minTime),
        //                new SqlParameter("to", maxTime));

        //        if (dt.HasErrors)
        //            throw new Exception(
        //                string.Format("Ошибка выполнения запроса к {0} с параметрами:{1}", TableName, unitId));

        //        var listMovement = GetListMovement(dt);
        //        return unitId != 0 ? listMovement.Where(x => x.UnitId == unitId).ToList() : listMovement;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(
        //            string.Format(
        //                "Ошибка создания списка объектов {0} по идентификаторам объекта {1} за промежуток времени от {2} по {3}",
        //                typeof(Movement).FullName,
        //                unitId,
        //                minTime,
        //                maxTime),
        //            ex);
        //    }
        //}

        ///// <summary>
        ///// Метод получения списка движений оборудования по ид оборудования
        ///// </summary>
        ///// <param name="equipmentId"></param>
        ///// <returns></returns>
        //public List<Movement> GetByEquipmentId(int? equipmentId)
        //{
        //    try
        //    {
        //        var context = DalContainer.DataManager.GetContext();
        //        var dt =
        //            context.ExecuteDataTable(
        //                string.Format("SELECT * FROM [{0}] WHERE[EquipmentId] = @EquipmentId", TableName),
        //                new SqlParameter("EquipmentId", equipmentId));

        //        if (dt.HasErrors)
        //            throw new Exception(
        //                string.Format("Ошибка выполнения запроса к {0} с параметрами:{1}", TableName, equipmentId));

        //        return GetListMovement(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(
        //            string.Format(
        //                "Ошибка создания списка объектов {0} по идентификаторам оборудования {1}",
        //                typeof(Movement).FullName,
        //                equipmentId),
        //            ex);
        //    }
        //}

        ///// <summary>
        ///// получение списка передвижений из таблицы
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //private static List<Movement> GetListMovement(DataTable dt)
        //{
        //    var listMovement = (from DataRow row in dt.Rows
        //                        select
        //                            new Movement
        //                                {
        //                                    Id = row["Id"].ToInt(),
        //                                    UnitId = row["UnitId"].ToInt(),
        //                                    DateOfMovement = row["DateOfMovement"].ToDateTime(),
        //                                    EquipmentId = row["EquipmentId"].ToInt(),
        //                                    IsArrived = row["IsArrived"].ToBoolean()
        //                                }).ToList();
        //    return listMovement;
        //}

        ///// <summary>
        /////Поулчение списка последних передвижений длявсего оборудования
        ///// </summary>
        ///// <returns></returns>
        //public List<Movement> GetLastMovements()
        //{
        //    var equipmentList = DalContainer.DataManager.EquipmentRepository.GetAll();
        //    return equipmentList.Select(equipment => equipment.LastMovement).ToList();
        //}
    }
}
