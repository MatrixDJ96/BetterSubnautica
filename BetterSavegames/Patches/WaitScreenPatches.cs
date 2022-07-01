using BetterSavegames.MonoBehaviours;
using HarmonyLib;

namespace BetterSavegames.Patches
{
    [HarmonyPatch(typeof(WaitScreen))]
    [HarmonyPatch(nameof(WaitScreen.Awake))]
    class WaitScreenAwakePatch
    {
        static void Postfix(WaitScreen __instance)
        {
            if (__instance.gameObject.GetComponent<WaitScreenController>() == null)
            {
                __instance.gameObject.AddComponent<WaitScreenController>();
            }
        }
    }

    [HarmonyPatch(typeof(WaitScreen))]
    [HarmonyPatch(nameof(WaitScreen.Show))]
    class WaitScreenShowPatch
    {
        static void Prefix(WaitScreen __instance)
        {
            if (__instance.gameObject.GetComponent<WaitScreenController>() is WaitScreenController controller)
            {
                controller.UnlockFramerate();
            }
        }
    }

    [HarmonyPatch(typeof(WaitScreen))]
    [HarmonyPatch(nameof(WaitScreen.Hide))]
    class WaitScreenHidePatch
    {
        static void Postfix(WaitScreen __instance)
        {
            if (__instance.gameObject.GetComponent<WaitScreenController>() is WaitScreenController controller)
            {
                controller.RestoreFramerate();
            }
        }
    }
}
