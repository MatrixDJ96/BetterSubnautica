using UnityEngine;
using UWE;

namespace BetterSubnautica.Utility
{
    public static class PDAUtility
    {
        public static bool InPause { get; private set; } = false;

#if SUBNAUTICA_STABLE
        public static string FreezeId { get => "PDAPause"; }
#else
        public static FreezeTime.Id FreezeId { get => FreezeTime.Id.PDA; }
#endif

        private static void FreezeBegin(PDA __instance, bool bypassInPause)
        {
            if ((!InPause || bypassInPause) && !__instance.ui.introActive)
            {
                if (__instance.state == PDA.State.Opened)
                {
                    Player.main.playerAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;

#if SUBNAUTICA_STABLE
                    FreezeTime.Begin(FreezeId, true);
#else
                    FreezeTime.Begin(FreezeId);
#endif

                    InPause = true;
                }
            }
        }

        public static void FreezeBegin(PDA __instance)
        {
            FreezeBegin(__instance, false);
        }

        public static void FreezeEnd(PDA __instance)
        {
            if (!__instance.ui.introActive)
            {
                Player.main.playerAnimator.updateMode = AnimatorUpdateMode.Normal;

                FreezeTime.End(FreezeId);

                InPause = false;
            }
        }

        public static void FreezeBeginCoroutine(PDA __instance, int milliseconds)
        {
            if (!InPause && (InPause = true))
            {
                __instance.StartCoroutine(CoroutineUtility.WaitForMilliseconds(milliseconds, () => FreezeBegin(__instance, true)));
            }
        }

        public static void FreezeEndCoroutine(PDA __instance, int milliseconds)
        {
            __instance.StartCoroutine(CoroutineUtility.WaitForMilliseconds(milliseconds, () => FreezeEnd(__instance)));
        }
    }
}
