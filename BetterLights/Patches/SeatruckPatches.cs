#if BELOWZERO
using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using BetterSubnautica.Extensions;
using HarmonyLib;
using System.Collections.Generic;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(SeaTruckSegment))]
    [HarmonyPatch(nameof(SeaTruckSegment.Start))]
    class SeaTruckSegmentStartPatch
    {
        static void Postfix(SeaTruckSegment __instance)
        {
            if (__instance.IsMainSegment())
            {
                if (__instance.gameObject.GetComponent<SeatruckLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<SeatruckLightsController>();
                }

                if (__instance.gameObject.GetComponent<SeatruckToggleLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<SeatruckToggleLightsController>();
                }

                if (__instance.gameObject.GetComponent<SeatruckVolumetricLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<SeatruckVolumetricLightsController>();
                }
            }
        }
    }

    [HarmonyPatch(typeof(SeaTruckSegment))]
    [HarmonyPatch(nameof(SeaTruckSegment.SetPlayerInsideState))]
    class SeamothSubConstructionCompletePatch
    {
        static void Postfix(SeaTruckSegment __instance, bool state)
        {
            if (SeaTruckSegment.GetHead(__instance) is SeaTruckSegment seaTruck)
            {
                if (__instance.gameObject.GetComponentInParent<IVolumetricLightsController>() is IVolumetricLightsController volumetricLightsController)
                {
                    var volumetricLight = new List<VFXVolumetricLight>(volumetricLightsController.VolumetricLights).FindAll(x => x != null);

                    if (state)
                    {
                        volumetricLight.ForEach(x => x.DisableVolume());
                    }
                    else
                    {
                        volumetricLight.ForEach(x => x.RestoreVolume());
                    }
                }
            }
        }
    }
}
#endif
