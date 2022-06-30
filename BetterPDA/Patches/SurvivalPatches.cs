#if SUBNAUTICA_STABLE
using BetterSubnautica.Utility;
using HarmonyLib;
using System.Collections;
using UWE;
using CoroutineUtility = BetterSubnautica.Utility.CoroutineUtility;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(Survival))]
    [HarmonyPatch(nameof(Survival.Eat))]
    class SurvivalEatPatch
    {
        static void Postfix(Survival __instance, bool __result)
        {
            if (__result && Core.Settings.EnablePDAPause && !__instance.player.GetPDA().ui.introActive)
            {
                FreezeTime.End(PDAUtility.FreezeId);
                __instance.StartCoroutine(PauseCoroutine());
            }
        }

        public static IEnumerator PauseCoroutine()
        {
            yield return CoroutineUtility.WaitForMilliseconds(150);
            FreezeTime.Begin(PDAUtility.FreezeId, true);
        }
    }

    [HarmonyPatch(typeof(Survival))]
    [HarmonyPatch(nameof(Survival.Use))]
    class SurvivalUsePatch
    {
        static void Postfix(Survival __instance, bool __result)
        {
            if (__result && Core.Settings.EnablePDAPause && !__instance.player.GetPDA().ui.introActive)
            {
                FreezeTime.End(PDAUtility.FreezeId);
                __instance.StartCoroutine(PauseCoroutine());
            }
        }

        public static IEnumerator PauseCoroutine()
        {
            yield return CoroutineUtility.WaitForMilliseconds(150);
            FreezeTime.Begin(PDAUtility.FreezeId, true);
        }
    }
}
#endif
