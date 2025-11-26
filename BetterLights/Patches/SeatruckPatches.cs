#if BELOWZERO
using BetterLights.MonoBehaviours;
using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using BetterSubnautica.Extensions;
using HarmonyLib;

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
                    //__instance.gameObject.AddComponent<SeatruckToggleLightsController>();
                }

                if (__instance.gameObject.GetComponent<SeatruckVolumetricLightsController>() == null)
                {
                    //__instance.gameObject.AddComponent<SeatruckVolumetricLightsController>();
                }
            }
        }
    }

    [HarmonyPatch(typeof(SeaTruckSegment))]
    [HarmonyPatch(nameof(SeaTruckSegment.SetPlayerInsideState))]
    class SeaTruckSegmentSetPlayerInsideStatePatch
    {
        static void Postfix(SeaTruckSegment __instance, bool state)
        {
            if (state && __instance.IsMainSegment() && __instance.gameObject.GetComponent<IVolumetricLightsController>() is { } controller)
            {
                foreach (var volumetricLight in controller.VolumetricLights)
                {
                    volumetricLight.DisableVolume();
                }
            }
        }
    }

    [HarmonyPatch(typeof(SeaTruckLights))]
    [HarmonyPatch(nameof(SeaTruckLights.Update))]
    class SeaTruckLightsUpdatePatch
    {
        static bool Prefix(SeaTruckLights __instance)
        {
            SeatruckLightsContainer.Instance.Dict.TryGetValue(__instance.GetInstanceID(), out var controller);

            if (controller != null)
            {
                if (__instance.lightingController != null)
                {
                    __instance.lightingController.LerpToState(controller.IsPowered() ? 2 : 0);
                }
            }

            return controller == null;
        }
    }
}
#endif
