using BetterSubnautica.Utility;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;

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
    [HarmonyPatch(nameof(uGUI_TabbedControlsPanel.AddToggleOption))]
    class uGUI_TabbedControlsPanelAddToggleOptionPatch
    {
        static bool Prefix(int tabIndex, string label, bool value, UnityAction<bool> callback = null)
        {
            return !(label == "Fullscreen" && tabIndex == uGUIUtility.GeneralTabIndex);
        }
    }
}
