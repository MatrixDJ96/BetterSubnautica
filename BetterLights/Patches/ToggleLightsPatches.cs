using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(ToggleLights))]
    [HarmonyPatch(nameof(ToggleLights.SetLightsActive))]
    class ToggleLightsSetLightsActivePatch
    {
        static bool Prefix(ToggleLights __instance)
        {
            return __instance.gameObject.GetComponentInParent<IToggleLightsController>() == null;
        }
    }
}
