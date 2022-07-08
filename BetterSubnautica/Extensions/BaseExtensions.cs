using UnityEngine;

namespace BetterSubnautica.Extensions
{
    public static class BaseExtensions
    {
        public static int GetCellIndex(this Base __instance, Vector3 worldPosition)
        {
            return __instance.GetCellIndex(__instance.WorldToGrid(worldPosition));
        }
    }
}
