using System.Collections.Generic;

namespace BetterVehicles.MonoBehaviours
{
    public class ExosuitStorageController : AbstractVehicleStorageController
    {
        public override IItemsContainer[] GetDefaultStorage()
        {
            return new[] { (component as Exosuit).storageContainer.container };
        }

        public override IItemsContainer[] GetTorpedoStorage()
        {
            return GetStorageContainers(TechType.ExosuitTorpedoArmModule);
        }

        public override IItemsContainer[] GetVehicleStorage()
        {
            var totalStorage = new List<IItemsContainer>(GetDefaultStorage());

            if (base.GetVehicleStorage() is IItemsContainer[] vehicleStorage)
            {
                totalStorage.AddRange(vehicleStorage);
            }

            return totalStorage.ToArray();
        }

    }
}
