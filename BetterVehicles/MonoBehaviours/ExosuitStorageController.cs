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
    }
}
