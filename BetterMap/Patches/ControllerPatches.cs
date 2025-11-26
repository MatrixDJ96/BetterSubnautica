#if BELOWZERO_MULTI
using BetterMap.MonoBehaviours;
using HarmonyLib;
using SubnauticaMap;

namespace BetterMap.Patches
{
    [HarmonyPatch(typeof(Controller))]
    [HarmonyPatch(nameof(Controller.Run))]
    class ControllerRunPatch
    {
        static void Postfix(Controller __instance)
        {
            if (__instance.gameObject.GetComponent<SaveController>() == null)
            {
                __instance.gameObject.AddComponent<SaveController>();
            }
        }
    }
}
#endif
