#if SUBNAUTICA_STABLE
using BetterSubnautica.Utility;
using HarmonyLib;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(PDA))]
    [HarmonyPatch(nameof(PDA.Activated))]
    class PDAActivatedPatch
    {
        static void Postfix(PDA __instance)
        {
            if (Core.Settings.EnablePDAPause)
            {
                PDAUtility.FreezeBeginCoroutine(__instance, 500);
            }
        }
    }

    [HarmonyPatch(typeof(PDA))]
    [HarmonyPatch(nameof(PDA.Close))]
    class PDAClosePatch
    {
        static void Postfix(PDA __instance)
        {
            if (Core.Settings.EnablePDAPause)
            {
                PDAUtility.FreezeEnd(__instance);
            }
        }
    }
}
#endif
