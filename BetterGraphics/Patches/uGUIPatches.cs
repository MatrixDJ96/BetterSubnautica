using BetterSubnautica.Utility;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BetterGraphics.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    [HarmonyPatch(nameof(uGUI_OptionsPanel.OnResolutionChanged))]
    class uGUIPatchesPatch
    {
#if SUBNAUTICA
        static void Postfix(uGUI_OptionsPanel __instance, int currentIndex)
        {
            var applyIndex = currentIndex;
#elif BELOWZERO
        static void Postfix(uGUI_OptionsPanel __instance, int applyIndex)
        {
#endif
            Resolution resolution = __instance.resolutions[applyIndex];

            Core.Settings.ResolutionWidth = resolution.width;
            Core.Settings.ResolutionHeight = resolution.height;

            Core.Settings.Save();
        }
    }

    [HarmonyPatch(typeof(uGUI_TabbedControlsPanel))]
#if SUBNAUTICA
    // AddToggleOption(int tabIndex, string label, bool value, UnityAction<bool> callback = null)
    [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddToggleOption), new[] { typeof(int), typeof(string), typeof(bool), typeof(UnityAction<bool>) })]
    class uGUITabbedControlsPanelAddToggleOptionPatch
    {
        static void Postfix(Toggle __result, int tabIndex, string label, bool value, UnityAction<bool> callback = null)
        {
#elif BELOWZERO
    // AddToggleOption(int tabIndex, string label, bool value, UnityAction<bool> callback = null, string tooltip = null)
    [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddToggleOption), new[] { typeof(int), typeof(string), typeof(bool), typeof(UnityAction<bool>), typeof(string) })]
    class uGUITabbedControlsPanelAddToggleOptionPatch
    {
        static void Postfix(Toggle __result, int tabIndex, string label, bool value, UnityAction<bool> callback = null, string tooltip = null)
        {
#endif
            if ((label == "Fullscreen" || label == "Vsync") && tabIndex == uGUIUtility.GeneralTabIndex)
            {
                __result.transform.parent.gameObject.SetActive(false);
            }
        }
    }

#if BELOWZERO
    [HarmonyPatch(typeof(uGUI_TabbedControlsPanel))]
    [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddSliderOption), new[] { typeof(int), typeof(string), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(UnityAction<float>), typeof(SliderLabelMode), typeof(string) })]
    class uGUITabbedControlsPanelAddSliderOptionPatch
    {
        static void Postfix(GameObject __result, int tabIndex, string label, float value, float minValue, float maxValue, float defaultValue, float step, UnityAction<float> callback, SliderLabelMode labelMode, string floatFormat)
        {
            if (label == "FPSCap" && tabIndex == uGUIUtility.GeneralTabIndex)
            {
                __result.SetActive(false);
            }
        }
    }

    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    [HarmonyPatch(nameof(uGUI_OptionsPanel.OnVSyncChanged))]
    class uGUI_OptionsPanelOptionPatch
    {
        static void Postfix(uGUI_OptionsPanel __instance)
        {
            __instance.targetFrameRateOption.SetActive(false);
        }
    }
#endif
}
