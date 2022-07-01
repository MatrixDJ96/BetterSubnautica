#if SUBNAUTICA_STABLE
using BetterSubnautica.Utility;
using HarmonyLib;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(Survival))]
    [HarmonyPatch(nameof(Survival.Eat))]
    class SurvivalEatPatch
    {
        static void Postfix(Survival __instance, bool __result)
        {
            if (__result && Core.Settings.EnablePDAPause && __instance.player.GetPDA() is PDA pda)
            {
                PDAUtility.FreezeEnd(pda);
                PDAUtility.FreezeBeginCoroutine(pda, 150);
            }
        }
    }

    [HarmonyPatch(typeof(Survival))]
    [HarmonyPatch(nameof(Survival.Use))]
    class SurvivalUsePatch
    {
        static void Postfix(Survival __instance, bool __result)
        {
            if (__result && Core.Settings.EnablePDAPause && __instance.player.GetPDA() is PDA pda)
            {
                PDAUtility.FreezeEnd(pda);
                PDAUtility.FreezeBeginCoroutine(pda, 150);
            }
        }
    }
}
#endif
