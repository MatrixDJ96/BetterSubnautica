using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace BetterSubnautica.Utility
{
    public class CoroutineUtility
    {
        public static IEnumerator WaitForMilliseconds(int milliseconds)
        {
            var stopwatch = Stopwatch.StartNew();
            yield return new WaitUntil(() => stopwatch.Elapsed.Milliseconds >= milliseconds);
            stopwatch.Stop();
        }
    }
}
