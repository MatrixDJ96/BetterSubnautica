#if BELOWZERO
using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(FlashlightHelmet))]
    [HarmonyPatch(nameof(FlashlightHelmet.Awake))]
    class FlashlightHelmetAwakePatch
    {
        static void Postfix(FlashlightHelmet __instance)
        {
            if (__instance.gameObject.GetComponent<FlashlightHelmetLightsController>() == null)
            {
                __instance.gameObject.AddComponent<FlashlightHelmetLightsController>();
            }

            if (__instance.gameObject.GetComponent<FlashlightHelmetToggleLightsController>() == null)
            {
                __instance.gameObject.AddComponent<FlashlightHelmetToggleLightsController>();
            }
        }
    }
}
#endif
