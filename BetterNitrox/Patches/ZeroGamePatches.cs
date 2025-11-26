#if BELOWZERO_MULTI
using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;
using Subnautica.API.Features;

namespace BetterNitrox.Patches
{
    [HarmonyPatch(typeof(ZeroGame))]
    [HarmonyPatch(nameof(ZeroGame.SetLightsActive), new[] { typeof(ToggleLights), typeof(bool), typeof(bool) })]
    class ZeroGameSetLightsActivePatch
    {
        static bool Prefix(ToggleLights toggleLights, bool isActive, bool infinityEnergy)
        {
            Core.Logger.LogInfo("[ZeroGame.SetLightsActivePatch] Method called");

            if (toggleLights.gameObject.GetComponentInParent<IToggleLightsController>() is { } controller)
            {
                Core.Logger.LogInfo($"[ZeroGame.SetLightsActivePatch] IToggleLightsController called ({isActive})");
                
                controller.SetLightsActive(isActive);

                return false;
            }
            
            Core.Logger.LogWarning($"[ZeroGame.SetLightsActivePatch] IToggleLightsController not found ({isActive})");

            return true;
        }
    }
}
#endif
