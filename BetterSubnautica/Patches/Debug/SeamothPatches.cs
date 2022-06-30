#if SUBNAUTICA
using BetterSubnautica.MonoBehaviours.Debug;
using HarmonyLib;

namespace BetterSubnautica.Patches.Debug
{
    [HarmonyPatch(typeof(SeaMoth))]
    [HarmonyPatch(nameof(SeaMoth.Start))]
    class SeamothStartPatch
    {
        static void Postfix(SeaMoth __instance)
        {
            if (__instance.gameObject.GetComponent<SeamothDebuggerController>() == null)
            {
                __instance.gameObject.AddComponent<SeamothDebuggerController>();
            }
        }
    }
}
#endif
