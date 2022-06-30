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
                __instance.gameObject.AddComponent<ExosuitToggleLightsController>();
            }

            if (__instance.gameObject.GetComponent<ExosuitVolumetricLightsController>() == null)
            {
                __instance.gameObject.AddComponent<ExosuitVolumetricLightsController>();
            }
        }
    }

#if SUBNAUTICA_STABLE || BELOWZERO_STABLE
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
            if (__instance is Exosuit && __instance.gameObject.GetComponent<IToggleLightsController>() is IToggleLightsController toggleLightsController)
            {
                toggleLightsController.SetLightsActive(true);
            }
        }
    }

    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch(nameof(Exosuit.OnPilotModeBegin))]
    class ExosuitOnPilotModeBeginPatch
    {
        static void Postfix(Exosuit __instance)
        {
            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is IVolumetricLightsController volumetricLightsController)
            {
                foreach (var volumetricLight in volumetricLightsController.VolumetricLights)
                {
                    if (volumetricLight != null)
                    {
                        volumetricLight.DisableVolume();
                    }
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
            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is IVolumetricLightsController volumetricLightsController)
            {
                foreach (var volumetricLight in volumetricLightsController.VolumetricLights)
                {
                    if (volumetricLight != null)
                    {
                        volumetricLight.RestoreVolume();
                    }
                }
            }
        }
    }
}
