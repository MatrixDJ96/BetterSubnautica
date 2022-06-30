using BetterVehicles.MonoBehaviours;
using HarmonyLib;
using System;

namespace BetterVehicles.Patches
{
    [HarmonyPatch(typeof(PDA))]
    [HarmonyPatch(nameof(PDA.Open))]
    class PDAOpenPatch
    {
        static void Prefix(PDA __instance)
        {
            if (Player.main.GetVehicle() is Vehicle vehicle && Inventory.main.GetUsedStorageCount() == 0)
            {
                IItemsContainer[] storageContainers = Array.Empty<IItemsContainer>();

                if (vehicle is Exosuit)
                {
                    storageContainers = vehicle.gameObject.GetComponent<ExosuitStorageController>()?.GetDefaultStorage();
                }
#if SUBNAUTICA
                else if (vehicle is SeaMoth)
                {
                    storageContainers = vehicle.gameObject.GetComponent<SeamothStorageController>()?.GetVehicleStorage();
                }
#endif

                foreach (var storageContainer in storageContainers)
                {
                    if (storageContainers != null)
                    {
                        Inventory.main.SetUsedStorage(storageContainer, true);
                    }
                }
            }
        }
    }
}
