using BetterHUD.MonoBehaviours;
using HarmonyLib;

namespace BetterHUD.Patches
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch(nameof(Player.Awake))]
    class PlayerAwakePatch
    {
        static void Postfix(Player __instance)
        {
            if (__instance.gameObject.GetComponent<TimeDisplayController>() == null)
            {
                __instance.gameObject.AddComponent<TimeDisplayController>();
            }
        }
    }
}
