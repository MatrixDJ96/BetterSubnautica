#if BELOWZERO
using BetterSubnautica.MonoBehaviours.Debug;
using HarmonyLib;

namespace BetterSubnautica.Patches.Debug
{
    [HarmonyPatch(typeof(Hoverbike))]
    [HarmonyPatch(nameof(Hoverbike.Awake))]
    class HoverbikeAwakePatch
    {
        static void Postfix(Hoverbike __instance)
        {
            if (__instance.gameObject.GetComponent<HoverbikeDebuggerController>() == null)
            {
                __instance.gameObject.AddComponent<HoverbikeDebuggerController>();
            }
        }
    }
}
#endif
