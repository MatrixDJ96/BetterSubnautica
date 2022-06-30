#if SUBNAUTICA
using HarmonyLib;

namespace BetterSubnautica.Extensions
{
    public static class CyclopsExternalCamsExtensions
    {
        public static int GetLightState(this CyclopsExternalCams __instance)
        {
            return (int)Traverse.Create(__instance).Field("lightState").GetValue();
        }

        public static void SetLightState(this CyclopsExternalCams __instance, int lightState)
        {
            var traverse = Traverse.Create(__instance);

            traverse.Field("lightState").SetValue(lightState);
            traverse.Method("SetLight").GetValue();
        }
    }
}
#endif
