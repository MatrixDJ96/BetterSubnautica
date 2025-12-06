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
#if MULTI
            if (__instance.gameObject.GetComponent<SaveController>() == null)
            {
                __instance.gameObject.AddComponent<SaveController>();
            }

            if (__instance.gameObject.GetComponent<PingController>() != null)
            {
                __instance.gameObject.AddComponent<PingController>();
            }
#endif
        }
    }

    [HarmonyPatch(typeof(Controller))]
    [HarmonyPatch(nameof(Controller.ReloadMaps))]
    class ControllerReloadMapsPatch
    {
        static void Postfix(Controller __instance)
        {
            if (PingController.Instance != null)
            {
                PingController.Instance.ReloadPings();
            }
        }
    }
}
