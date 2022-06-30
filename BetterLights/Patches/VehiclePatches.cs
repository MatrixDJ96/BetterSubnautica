using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;
using static Vehicle;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch(nameof(Vehicle.OnDockedChanged))]
    class VehicleOnDockedChangedPatch
    {
        static void Prefix(Vehicle __instance, bool docked, DockType dockType)
        {
            if (__instance.gameObject.GetComponent<IToggleLightsController>() is IToggleLightsController toggleLightsController)
            {
                if (docked || (!docked && Core.VehiclesSettings.EnableLightsOnUndocking))
                {
                    toggleLightsController.SetLightsActive(!docked);
                }
            }
        }
    }
}
