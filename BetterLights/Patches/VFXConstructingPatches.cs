#if BELOWZERO
using BetterLights.MonoBehaviours.ToggleLights;
using BetterSubnautica.Extensions;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(VFXConstructing))]
    [HarmonyPatch(nameof(VFXConstructing.WakeUpSubmarine))]
    class VFXConstructingWakeUpSubmarinePatch
    {
        static void Postfix(VFXConstructing __instance)
        {
            if (__instance.gameObject.GetComponent<SeaTruckSegment>() is SeaTruckSegment seatruck && seatruck.IsMainSegment())
            {
                if (seatruck.gameObject.GetComponent<IToggleLightsController>() is IToggleLightsController controller)
                {
                    controller.SetLightsActive(true, true);
                }
            }
        }
    }
}
#endif
