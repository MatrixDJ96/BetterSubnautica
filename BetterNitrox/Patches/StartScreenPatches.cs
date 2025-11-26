#if BELOWZERO_MULTI
using HarmonyLib;

namespace BetterNitrox.Patches
{
    [HarmonyPatch(typeof(StartScreen))]
    [HarmonyPatch(nameof(StartScreen.TryToShowDisclaimer))]
    class StartScreenTryToShowDisclaimerPatch
    {
        static bool Prefix(StartScreen __instance)
        {
            Core.Logger.LogInfo("StartScreen.TryToShowDisclaimer Patch called");

            return false;
        }
    }
}
#endif
