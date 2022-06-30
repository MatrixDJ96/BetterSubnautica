using BetterSubnautica.MonoBehaviours.Debug;
using HarmonyLib;

namespace BetterSubnautica.Patches.Debug
{
    [HarmonyPatch(typeof(Exosuit))]
    [HarmonyPatch(nameof(Exosuit.Awake))]
    class ExosuitAwakePatch
    {
        static void Postfix(Exosuit __instance)
        {
            if (__instance.gameObject.GetComponent<ExosuitDebuggerController>() == null)
            {
                __instance.gameObject.AddComponent<ExosuitDebuggerController>();
            }
        }
    }
}
