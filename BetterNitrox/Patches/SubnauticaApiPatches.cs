#if BELOWZERO_MULTI
using HarmonyLib;
using Subnautica.API.Features;

namespace BetterNitrox.Patches
{
    [HarmonyPatch(typeof(Tools))]
    [HarmonyPatch(nameof(Tools.IsBepinexInstalled))]
    class ToolsIsBepinexInstalledPatch
    {
        static bool Prefix(ref bool __result)
        {
            Core.Logger.LogInfo("Tools.IsBepinexInstalled Patch called");
            
            return __result = false;
        }
    }
}
#endif
