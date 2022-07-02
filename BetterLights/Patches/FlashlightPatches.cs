using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    [HarmonyPatch(nameof(PlayerTool.Awake))]
    class FlashlightAwakePatch
    {
        static void Postfix(PlayerTool __instance)
        {
            if (__instance is FlashLight)
            {
                if (__instance.gameObject.GetComponent<FlashlightLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<FlashlightLightsController>();
                }

                if (__instance.gameObject.GetComponent<FlashlightToggleLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<FlashlightToggleLightsController>();
                }
            }
        }
    }

    [HarmonyPatch(typeof(FlashLight))]
    [HarmonyPatch(nameof(FlashLight.OnToolUseAnim))]
    class FlashlightOnToolUseAnimPatch
    {
        static bool Prefix(FlashLight __instance)
        {
            return __instance.gameObject.GetComponent<IToggleLightsController>() == null;
        }
    }

#if BELOWZERO
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
#endif
}
