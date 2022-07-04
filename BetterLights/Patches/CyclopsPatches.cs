#if SUBNAUTICA
using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch(nameof(SubRoot.Awake))]
    class CyclopsAwakePatch
    {
        static void Postfix(SubRoot __instance)
        {
            if (__instance.isCyclops)
            {
                if (__instance.gameObject.GetComponent<CyclopsLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<CyclopsLightsController>();
                }

                if (__instance.gameObject.GetComponent<CyclopsToggleLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<CyclopsToggleLightsController>();
                }

                if (__instance.gameObject.GetComponent<CyclopsVolumetricLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<CyclopsVolumetricLightsController>();
                }
            }
        }
    }

    [HarmonyPatch(typeof(CyclopsLightingPanel))]
    [HarmonyPatch(nameof(CyclopsLightingPanel.SubConstructionComplete))]
    class CyclopsSubConstructionCompletePatch
    {
        static void Postfix(CyclopsLightingPanel __instance)
        {
            if (__instance.gameObject.GetComponentInParent<IToggleLightsController>() is IToggleLightsController toggleLightsController)
            {
                toggleLightsController.SetLightsActive(true);
            }
        }
    }

    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch(nameof(SubRoot.OnPlayerEntered))]
    class CyclopsOnPlayerEnteredPatch
    {
        static void Postfix(SubRoot __instance, Player player)
        {
            if (__instance.isCyclops && __instance.gameObject.GetComponentInParent<IVolumetricLightsController>() is IVolumetricLightsController volumetricLightsController)
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

    [HarmonyPatch(typeof(SubRoot))]
    [HarmonyPatch(nameof(SubRoot.OnPlayerExited))]
    class CyclopsOnPlayerExitedPatch
    {
        static void Postfix(SubRoot __instance, Player player)
        {
            if (__instance.isCyclops && __instance.gameObject.GetComponentInParent<IVolumetricLightsController>() is IVolumetricLightsController volumetricLightsController)
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
#endif
