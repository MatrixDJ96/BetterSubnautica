using HarmonyLib;

#if !SUBNAUTICA_STABLE
using BetterSubnautica.Utility;
using UnityEngine.Events;
#endif

namespace BetterPDA.Patches
{
#if SUBNAUTICA_STABLE
    [HarmonyPatch(typeof(uGUI_FoodBar))]
    [HarmonyPatch(nameof(uGUI_FoodBar.Awake))]
    class uGUI_FoodBarAwakePatch
    {
        static void Postfix(uGUI_FoodBar __instance)
        {
            __instance.pulseTween.ignoreTimeScale = true;
            __instance.punchTween.ignoreTimeScale = true;
        }
    }

    [HarmonyPatch(typeof(uGUI_HealthBar))]
    [HarmonyPatch(nameof(uGUI_HealthBar.Awake))]
    class uGUI_HealthBarAwakePatch
    {
        static void Postfix(uGUI_HealthBar __instance)
        {
            __instance.pulseTween.ignoreTimeScale = true;
            __instance.punchTween.ignoreTimeScale = true;
        }
    }

    [HarmonyPatch(typeof(uGUI_WaterBar))]
    [HarmonyPatch(nameof(uGUI_WaterBar.Awake))]
    class uGUI_WaterBarAwakePatch
    {
        static void Postfix(uGUI_WaterBar __instance)
        {
            __instance.pulseTween.ignoreTimeScale = true;
            __instance.punchTween.ignoreTimeScale = true;
        }
    }
#else
    [HarmonyPatch(typeof(uGUI_TabbedControlsPanel))]
    [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddToggleOption))]
    class uGUI_TabbedControlsPanelAddToggleOptionPatch
    {
        static bool Prefix(int tabIndex, string label, bool value, UnityAction<bool> callback = null)
        {
            return !(label == "PDAPause" && tabIndex == uGUIUtility.AccessibilityTabIndex);
        }
    }
#endif
}
