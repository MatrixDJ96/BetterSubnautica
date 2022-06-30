#if SUBNAUTICA_STABLE
using BetterSubnautica.Utility;
using HarmonyLib;
using UnityEngine;
using UWE;

namespace BetterSubnautica.Patches
{
    [HarmonyPatch(typeof(MainGameController))]
    [HarmonyPatch(nameof(MainGameController.Update))]
    class MainGameControllerUpdatePatch
    {
        static void Postfix(MainGameController __instance)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                // Force resume game
                FreezeTime.End(PDAUtility.FreezeId);
            }
        }
    }
}
#endif
