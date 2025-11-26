using BetterLights.MonoBehaviours.Lights;
using BetterLights.MonoBehaviours.ToggleLights;
using HarmonyLib;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(PlayerTool))]
    [HarmonyPatch(nameof(PlayerTool.Awake))]
    class SeaglideAwakePatch
    {
        static void Postfix(PlayerTool __instance)
        {
            if (__instance is Seaglide seaglide)
            {
                if (__instance.gameObject.GetComponent<SeaglideLightsController>() == null)
                {
                    __instance.gameObject.AddComponent<SeaglideLightsController>();
                }

                if (__instance.gameObject.GetComponent<SeaglideToggleLightsController>() == null)
                {
                    //__instance.gameObject.AddComponent<SeaglideToggleLightsController>();
                }
            }
        }
    }
}
