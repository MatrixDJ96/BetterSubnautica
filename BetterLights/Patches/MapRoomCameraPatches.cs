using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(MapRoomCamera))]
    [HarmonyPatch(nameof(MapRoomCamera.Start))]
    class MapRoomCameraStartPatch
    {
        static void Postfix(MapRoomCamera __instance)
        {
            if (__instance.gameObject.GetComponent<MapRoomCameraLightsController>() == null)
            {
                __instance.gameObject.AddComponent<MapRoomCameraLightsController>();
            }

            if (__instance.gameObject.GetComponent<MapRoomCameraToggleLightsController>() == null)
            {
                //__instance.gameObject.AddComponent<MapRoomCameraToggleLightsController>();
            }

            if (__instance.gameObject.GetComponent<MapRoomCameraVolumetricLightsController>() == null)
            {
                //__instance.gameObject.AddComponent<MapRoomCameraVolumetricLightsController>();
            }
        }
    }

    [HarmonyPatch(typeof(MapRoomCamera))]
    [HarmonyPatch(nameof(MapRoomCamera.ControlCamera))]
    class MapRoomCameraControlCameraPatch
    {
#if BELOWZERO
        static void Postfix(MapRoomCamera __instance, Player player, MapRoomScreen screen)
#else
        static void Postfix(MapRoomCamera __instance, MapRoomScreen screen)
#endif
        {
            if (__instance.gameObject.GetComponent<IToggleLightsController>() is { } toggleLightsController)
            {
                toggleLightsController.SetLightsActive(toggleLightsController.LightsActive);
            }

            if (__instance.gameObject.GetComponent<IVolumetricLightsController>() is { } volumetricLightsController)
            {
                foreach (var volumetricLight in volumetricLightsController.VolumetricLights)
                {
                    volumetricLight.DisableVolume();
                }
            }
        }
    }

    [HarmonyPatch(typeof(MapRoomCamera))]
    [HarmonyPatch(nameof(MapRoomCamera.FreeCamera))]
    class MapRoomCameraFreeCameraPatch
    {
        static void Postfix(MapRoomCamera __instance)
        {
            if (__instance.gameObject.GetComponent<IToggleLightsController>() is { } toggleLightsController)
            {
                toggleLightsController.SetLightsActive(__instance.dockingPoint == null && toggleLightsController.LightsActive);
            }

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
