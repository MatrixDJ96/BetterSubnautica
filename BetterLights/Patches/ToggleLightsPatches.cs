using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(ToggleLights))]
    [HarmonyPatch(nameof(ToggleLights.SetLightsActive))]
    class ToggleLightsSetLightsActivePatch
    {
        static bool Prefix(ToggleLights __instance, bool isActive)
        {
            Core.Logger.LogInfo($"[ToggleLights.SetLightsActivePatch] active: {isActive}");

#if STABLE
            return __instance.gameObject.GetComponentInParent<IToggleLightsController>() == null;
#else
            return true;
#endif
        }
    }
}
