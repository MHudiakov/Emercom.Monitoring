using System.Collections.Generic;
using System.Linq;
using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public class EquipmentRepository : ClassifierRepository<Equipment>
    {
        public EquipmentRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Equipment>(dataManager.GetContext), dataManager)
        { }

        public List<Equipment> GetEquipmentListForUnit(int unitId)
        {
            return this.GetAll().Where(item => item.UnitId == unitId).ToList();
        }

        /// <summary>
        /// Получить текущую комплектация для юнита
        /// </summary>
        /// <param name="unitId">
        /// Ид юнита
        /// </param>
        /// <returns>
        /// Текущая комплектация для юнита
        /// </returns>
        public List<Equipment> GetCurrentComplectationForUnit(int unitId)
        {
            // Получаем список всех последних передвижений
            var lastMovements = DalContainer.GetDataManager.MovementRepository.GetLastMovementsForUnit(unitId);

            // Получаем формуляр ПТВ для юнита
            var equipmentListForUnit = GetEquipmentListForUnit(unitId);

            // Заполняем/обновляем все последние передвижения для формуляра ПТВ
            foreach (var equipment in equipmentListForUnit)
            {
                var lastMovement = lastMovements.FirstOrDefault(movement => movement.Id == equipment.LastMovementId);
                equipment.LastMovement = lastMovement;
            }

            // Отбираем из формуляра ПТВ то, что сейчас находится в текущем юните
            var currentComplectationForUnit =
                equipmentListForUnit.Where(
                    equipment => equipment.LastMovement != null &&
                    equipment.LastMovement.UnitId == unitId &&
                    equipment.LastMovement.IsArrived).ToList();

            return currentComplectationForUnit;
        }
    }
}