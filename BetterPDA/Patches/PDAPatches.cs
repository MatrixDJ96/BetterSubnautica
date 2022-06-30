#if SUBNAUTICA_STABLE
using BetterSubnautica.Utility;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using UWE;
using CoroutineUtility = BetterSubnautica.Utility.CoroutineUtility;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(PDA))]
    [HarmonyPatch(nameof(PDA.Activated))]
    class PDAActivatedPatch
    {
        static void Postfix(PDA __instance)
        {
            if (Core.Settings.EnablePDAPause && !__instance.ui.introActive)
            {
                __instance.StartCoroutine(Wait(__instance));
            }
        }

        static IEnumerator Wait(PDA __instance)
        {
            yield return CoroutineUtility.WaitForMilliseconds(500);

            if (__instance.state == PDA.State.Opened)
            {
                Player.main.playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
                FreezeTime.Begin(PDAUtility.FreezeId, true);
            }
        }
    }

    [HarmonyPatch(typeof(PDA))]
    [HarmonyPatch(nameof(PDA.Close))]
    class PDAClosePatch
    {
        static void Postfix(PDA __instance)
        {
            if (Core.Settings.EnablePDAPause && !__instance.ui.introActive)
            {
                Player.main.playerAnimator.updateMode = AnimatorUpdateMode.Normal;
                FreezeTime.End(PDAUtility.FreezeId);
            }
        }
    }
}
#endif
