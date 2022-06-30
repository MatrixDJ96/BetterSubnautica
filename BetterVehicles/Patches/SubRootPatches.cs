using HarmonyLib;

namespace BetterVehicles.Patches
{
    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch(nameof(SubRoot.Start))]
    class SubRootStartPatch
    {
        static void Postfix(SubRoot __instance)
        {
            if (!__instance.isCyclops)
            {
                __instance.vehicleRepairUpgrade = true;
            }
        }
    }

    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch(nameof(SubRoot.SetCyclopsUpgrades))]
    class SubRootSetCyclopsUpgradesPatch
    {
        static void Postfix(SubRoot __instance)
        {
            if (!__instance.isCyclops)
            {
                __instance.vehicleRepairUpgrade = true;
            }
        }
    }
}
