using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch(nameof(Exosuit.Awake))]
    class ExosuitAwakePatch
    {
        static void Postfix(Exosuit __instance)
        {
            if (__instance.gameObject.GetComponent<ExosuitLightsController>() == null)
            {
                __instance.gameObject.AddComponent<ExosuitLightsController>();
            }

            if (__instance.gameObject.GetComponent<ExosuitToggleLightsController>() == null)
            {
                //__instance.gameObject.AddComponent<ExosuitToggleLightsController>();
            }

            if (__instance.gameObject.GetComponent<ExosuitVolumetricLightsController>() == null)
            {
                //__instance.gameObject.AddComponent<ExosuitVolumetricLightsController>();
            }
        }
    }

#if BELOWZERO
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch(nameof(Exosuit.SubConstructionComplete))]
#else
    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch(nameof(Vehicle.SubConstructionComplete))]
#endif

    class ExosuitSubConstructionCompletePatch
    {
        static void Postfix(Vehicle __instance)
        {
            if (__instance is Exosuit && __instance.gameObject.GetComponent<IToggleLightsController>() is { } controller)
            {
                controller.SetLightsActive(true, true);
            }
        }
    }

    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch(nameof(Exosuit.OnPilotModeBegin))]
    class ExosuitOnPilotModeBeginPatch
    {
        static void Postfix(Exosuit __instance)
        {
            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is { } controller)
            {
                foreach (var volumetricLight in controller.VolumetricLights)
                {
                    volumetricLight.DisableVolume();
                }
            }
        }
    }

    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch(nameof(Exosuit.OnPilotModeEnd))]
    class ExosuitOnPilotModeEndPatch
    {
        static void Postfix(Exosuit __instance)
        {
            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is { } controller)
            {
                foreach (var volumetricLight in controller.VolumetricLights)
                {
                    volumetricLight.RestoreVolume();
                }
            }
        }
    }
}
