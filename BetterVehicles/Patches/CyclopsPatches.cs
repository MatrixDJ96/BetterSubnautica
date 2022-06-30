#if SUBNAUTICA
using HarmonyLib;

namespace BetterVehicles.Patches
{
    [HarmonyPatch(typeof(CyclopsCameraInput))]
    [HarmonyPatch(nameof(CyclopsCameraInput.Update))]
    class CyclopsCameraInputUpdatePatch
    {
        static void Postfix(CyclopsCameraInput __instance)
        {
            if (__instance.rotationSpeedDamper != Core.CyclopsSettings.CameraRotationSpeedDamper)
            {
                __instance.rotationSpeedDamper = Core.CyclopsSettings.CameraRotationSpeedDamper;
            }
        }
    }
}
#endif
