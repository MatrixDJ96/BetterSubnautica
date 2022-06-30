using System;
using System.Collections.Generic;
using UnityEngine;

namespace BetterVehicles.MonoBehaviours
{
    public abstract class AbstractVehicleStorageController : MonoBehaviour
    {
        protected Vehicle vehicle;

        protected virtual void Awake()
        {
            vehicle = gameObject.GetComponent<Vehicle>();
        }

        protected virtual void Update()
        {
            if (Player.main.GetVehicle() == vehicle)
            {
                var upgrade = Input.GetKeyDown(Core.GlobalSettings.UpgradeModules);
                var torpedo = Input.GetKeyDown(Core.GlobalSettings.TorpedoStorage);
                var storage = Input.GetKeyDown(Core.GlobalSettings.VehicleStorage);

                if (upgrade || torpedo || storage)
                {
                    Inventory.main.ClearUsedStorage();

                    IItemsContainer[] storageContainers = Array.Empty<IItemsContainer>();

                    if (upgrade)
                    {
                        storageContainers = GetUpgradeModules();
                    }
                    else if (torpedo)
                    {
                        storageContainers = GetTorpedoStorage();
                    }
                    else if (storage)
                    {
                        storageContainers = GetVehicleStorage();
                    }

                    foreach (var storageContainer in storageContainers)
                    {
                        if (storageContainers != null)
                        {
                            Inventory.main.SetUsedStorage(storageContainer, true);
                        }
                    }
                }

                if (Inventory.main.GetUsedStorageCount() != 0)
                {
#if SUBNAUTICA_STABLE
                    Player.main.GetPDA().Open(PDATab.Inventory, null, null, -1f);
#else
                    Player.main.GetPDA().Open(PDATab.Inventory, null, null);
#endif
                }
            }
        }

        protected virtual IItemsContainer[] GetStorageContainers(TechType techType)
        {
            List<IItemsContainer> storages = new();

            for (int i = 0; i < vehicle.GetSlotCount(); i++)
            {
                if (vehicle.GetStorageInSlot(i, techType) is IItemsContainer container)
                {
                    storages.Add(container);
                }
            }

            return storages.ToArray();
        }

        public abstract IItemsContainer[] GetDefaultStorage();

        public virtual IItemsContainer[] GetUpgradeModules()
        {
            return new[] { vehicle.upgradesInput.equipment };
        }

        public abstract IItemsContainer[] GetTorpedoStorage();

        public virtual IItemsContainer[] GetVehicleStorage()
        {
            return GetStorageContainers(TechType.VehicleStorageModule);
        }
    }
}
