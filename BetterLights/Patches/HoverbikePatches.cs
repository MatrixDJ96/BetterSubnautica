#if BELOWZERO
using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(Hoverbike))]
    [HarmonyPatch(nameof(Hoverbike.Awake))]
    class HoverbikeAwakePatch
    {
        static void Postfix(Hoverbike __instance)
        {
            if (__instance.gameObject.GetComponent<HoverbikeLightsController>() == null)
            {
                __instance.gameObject.AddComponent<HoverbikeLightsController>();
            }

            if (__instance.gameObject.GetComponent<HoverbikeToggleLightsController>() == null)
            {
                //__instance.gameObject.AddComponent<HoverbikeToggleLightsController>();
            }

            if (__instance.gameObject.GetComponent<HoverbikeVolumetricLightsController>() == null)
            {
                //__instance.gameObject.AddComponent<HoverbikeVolumetricLightsController>();
            }
        }
    }


    [HarmonyPatch(typeof(Hoverbike))]
    [HarmonyPatch(nameof(Hoverbike.EnterVehicle))]
    class HoverbikeEnterVehiclePatch
    {
        static void Postfix(Hoverbike __instance)
        {
            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is { } volumetricLightsController)
            {
                foreach (var volumetricLight in volumetricLightsController.VolumetricLights)
                {
                    volumetricLight.DisableVolume();
                }
            }
        }
    }

    [HarmonyPatch(typeof(Hoverbike))]
    [HarmonyPatch(nameof(Hoverbike.ExitVehicle))]
    class HoverbikeExitVehiclePatch
    {
        static void Postfix(Hoverbike __instance)
        {
            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is { } volumetricLightsController)
            {
                foreach (var volumetricLight in volumetricLightsController.VolumetricLights)
                {
                    volumetricLight.RestoreVolume();
                }
            }
        }
    }
}
#endif
