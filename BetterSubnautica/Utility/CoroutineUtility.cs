using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace BetterSubnautica.Utility
{
    public static class CoroutineUtility
    {
        public static IEnumerator WaitUntil(Func<bool> predicate, Action action = null)
        {
            yield return new WaitUntil(predicate);

            if (action != null)
            {
                action.Invoke();
            }
        }

        public static IEnumerator WaitForSeconds(int seconds, Action action = null)
        {
            yield return new WaitForSeconds(seconds);

            if (action != null)
            {
                action.Invoke();
            }
        }

        public static IEnumerator WaitForMilliseconds(int milliseconds, Action action = null)
        {
            var stopwatch = Stopwatch.StartNew();

            yield return WaitUntil(() => stopwatch.Elapsed.Milliseconds >= milliseconds, action);
        }
    }
}
