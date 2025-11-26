#if BELOWZERO_MULTI
using HarmonyLib;
using Subnautica.API.Features;

namespace BetterNitrox.Patches
{
    [HarmonyPatch(typeof(ZeroGame))]
    [HarmonyPatch(nameof(ZeroGame.SetLightsActive), typeof(ToggleLights), typeof(bool), typeof(bool))]
    class ZeroGameSetLightsActivePatch
    {
        static bool Prefix(ToggleLights toggleLights, bool isActive, bool infinityEnergy)
        {
            /*if (toggleLights.gameObject.GetComponentInParent<IToggleLightsController>() is { } controller)
            {
                Core.Logger.LogInfo($"[ZeroGame.SetLightsActivePatch] IToggleLightsController called ({isActive})");

                controller.SetLightsActive(isActive);

                return false;
            }*/

            Core.Logger.LogWarning($"[ZeroGame.SetLightsActivePatch] active: {isActive}, infinityEnergy: {infinityEnergy}");

            return true;
        }
    }
}
#endif
