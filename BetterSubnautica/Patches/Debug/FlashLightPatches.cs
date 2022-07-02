using BetterSubnautica.MonoBehaviours.Debug;
using HarmonyLib;

namespace BetterSubnautica.Patches.Debug
{
    [HarmonyPatch(typeof(PlayerTool))]
    [HarmonyPatch(nameof(PlayerTool.Awake))]
    class FlashlightAwakePatch
    {
        static void Postfix(PlayerTool __instance)
        {
            if (__instance is FlashLight)
            {
                if (__instance.gameObject.GetComponent<FlashlightDebuggerController>() == null)
                {
                    __instance.gameObject.AddComponent<FlashlightDebuggerController>();
                }
            }
        }
    }

#if BELOWZERO
    [HarmonyPatch(typeof(FlashlightHelmet))]
    [HarmonyPatch(nameof(FlashlightHelmet.Awake))]
    class FlashlightHelmetAwakePatch
    {
        static void Postfix(FlashlightHelmet __instance)
        {
            if (__instance.gameObject.GetComponent<FlashlightHelmetDebuggerController>() == null)
            {
                __instance.gameObject.AddComponent<FlashlightHelmetDebuggerController>();
            }
        }
    }
#endif
}
