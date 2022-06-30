#if SUBNAUTICA
using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using BetterLights.MonoBehaviours.VolumetricLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch(nameof(SeaMoth.Start))]
    class SeamothStartPatch
    {
        static void Postfix(SeaMoth __instance)
        {
            if (__instance.gameObject.GetComponent<SeamothLightsController>() == null)
            {
                __instance.gameObject.AddComponent<SeamothLightsController>();
            }

            if (__instance.gameObject.GetComponent<SeamothToggleLightsController>() == null)
            {
                __instance.gameObject.AddComponent<SeamothToggleLightsController>();
            }

            if (__instance.gameObject.GetComponent<SeamothVolumetricLightsController>() == null)
            {
                __instance.gameObject.AddComponent<SeamothVolumetricLightsController>();
            }
        }
    }

    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch(nameof(SeaMoth.SubConstructionComplete))]
    class SeamothSubConstructionCompletePatch
    {
        static void Postfix(SeaMoth __instance)
        {
            if (__instance.gameObject.GetComponent<IToggleLightsController>() is IToggleLightsController toggleLightsController)
            {
                toggleLightsController.SetLightsActive(true);
            }
        }
    }
}
#endif
