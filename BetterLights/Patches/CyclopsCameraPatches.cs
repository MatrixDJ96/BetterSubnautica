#if SUBNAUTICA
using BetterLights.MonoBehaviours.Lights;
using BetterSubnautica.Extensions;
using HarmonyLib;
using UnityEngine;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(CyclopsExternalCams))]
    [HarmonyPatch(nameof(CyclopsExternalCams.Start))]
    class CyclopsExternalCamsStartPatch
    {
        static void Postfix(CyclopsExternalCams __instance)
        {
            if (__instance.gameObject.GetComponent<CyclopsCameraLightsController>() == null)
            {
                __instance.gameObject.AddComponent<CyclopsCameraLightsController>();
            }
        }
    }

    [HarmonyPatch(typeof(CyclopsExternalCams))]
    [HarmonyPatch(nameof(CyclopsExternalCams.SetLight))]
    class CyclopsExternalCamsSetLightPatch
    {
        static void Postfix(CyclopsExternalCams __instance)
        {
            if (__instance.GetLightState() > 0 && __instance.gameObject.GetComponent<CyclopsCameraLightsController>() is CyclopsCameraLightsController lightsController)
            {
                var color = lightsController.Color;

                if (__instance.GetLightState() == 2)
                {
                    color /= 2;
                    color.a = 1;
                }

                __instance.cameraLight.color = color;
            }
        }
    }

    [HarmonyPatch(typeof(CyclopsExternalCams))]
    [HarmonyPatch(nameof(CyclopsExternalCams.EnterCameraView))]
    class CyclopsExternalCamsEnterCameraViewPatch
    {
        static void Postfix(CyclopsExternalCams __instance)
        {
            if (__instance.lightingPanel != null && __instance.lightingPanel.floodlightsOn)
            {
                //FMODUWE.PlayOneShot(__instance.lightingPanel.vn_floodlightsOff, __instance.externalCamPositions[__instance.GetCameraIndex()].transform.position, 0.5f);
            }
        }
    }

    [HarmonyPatch(typeof(CyclopsExternalCams))]
    [HarmonyPatch(nameof(CyclopsExternalCams.ChangeCamera))]
    class CyclopsExternalCamsChangeCameraPatch
    {
        static void Prefix(CyclopsExternalCams __instance, out int __state, int iterate)
        {
            __state = __instance.GetLightState();
        }

        static void Postfix(CyclopsExternalCams __instance, int __state, int iterate)
        {
            __instance.SetLightState(__state);
        }
    }

    [HarmonyPatch(typeof(CyclopsExternalCams))]
    [HarmonyPatch(nameof(CyclopsExternalCams.ExitCamera))]
    class CyclopsExternalCamsExitCameraPatch
    {
        static void Postfix(CyclopsExternalCams __instance)
        {
            if (__instance.lightingPanel != null && __instance.lightingPanel.floodlightsOn)
            {
                //FMODUWE.PlayOneShot(__instance.lightingPanel.vn_floodlightsOn, __instance.lightingPanel.transform.position);
            }

            __instance.externalCamPositions[__instance.cameraIndex].SendMessage("DeactivateCamera", null, SendMessageOptions.RequireReceiver);
        }
    }
}
#endif
