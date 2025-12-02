using System.Reflection;
using BetterQuickSlots.Utility;
using BetterSubnautica.Utility;
using HarmonyLib;

namespace BetterQuickSlots.Patches
{
    [HarmonyPatch(typeof(GameInputSystem))]
    [HarmonyPatch(nameof(GameInputSystem.PopulateBindingSettings))]
    class GameInputSystemPopulateBindingSettingsPatch
    {
        static void Postfix(uGUI_OptionsPanel panel, int tabIndex, GameInput.Device device)
        {
            if (tabIndex == uGUIUtility.KeyboardTabIndex && device == GameInput.Device.Keyboard)
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.Static;

                for (int i = 0; i < Inventory.main.quickSlots.slotCount; i++)
                {
                    if (typeof(SlotsUtility).GetProperty($"Slot{i + 1}", bindingFlags) is { } property)
                    {
                        if ("Option" + property.Name == label)
                        {
                            __result.transform.parent.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
