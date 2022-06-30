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
}
