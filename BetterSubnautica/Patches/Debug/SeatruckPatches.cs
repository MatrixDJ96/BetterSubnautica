#if BELOWZERO
using BetterSubnautica.MonoBehaviours.Debug;
using HarmonyLib;

namespace BetterSubnautica.Patches.Debug
{
    [HarmonyPatch(typeof(SeaTruckSegment))]
    [HarmonyPatch(nameof(SeaTruckSegment.Start))]
    class SeaTruckSegmentStartPatch
    {
        static void Postfix(SeaTruckSegment __instance)
        {
            if (SeaTruckSegment.GetHead(__instance) == __instance)
            {
                if (__instance.gameObject.GetComponent<SeatruckDebuggerController>() == null)
                {
                    __instance.gameObject.AddComponent<SeatruckDebuggerController>();
                }
            }
        }
    }
}
#endif
