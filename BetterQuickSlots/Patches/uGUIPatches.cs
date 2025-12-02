using System.Reflection;
using BetterQuickSlots.MonoBehaviours;
using BetterQuickSlots.Utility;
using HarmonyLib;
using UnityEngine;

namespace BetterQuickSlots.Patches
{
    [HarmonyPatch(typeof(uGUI))]
    [HarmonyPatch(nameof(uGUI.Awake))]
    class uGUIAwakePatch
    {
        static void Postfix(uGUI __instance)
        {
            if (uGUI.main == __instance && __instance.quickSlots is { } quickSlots)
            {
                if (quickSlots.gameObject.GetComponent<QuickSlotsController>() == null)
                {
                    quickSlots.gameObject.AddComponent<QuickSlotsController>();
                }
            }
        }
    }

    [HarmonyPatch(typeof(uGUI_QuickSlots))]
    [HarmonyPatch(nameof(uGUI_QuickSlots.HandleInput))]
    class uGUIQuickSlotsHandleInputPatch
    {
        static void Postfix(uGUI_QuickSlots __instance)
        {
            if (Player.main.GetCanItemBeUsed() && !uGUI.isIntro && !IntroLifepodDirector.IsActive && __instance.target != null)
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.Static;

                for (int i = Inventory.main.quickSlots.slotCount; i < Core.Settings.SlotCount; i++)
                {
                    if (typeof(SlotsUtility).GetProperty($"Slot{i + 1}", bindingFlags) is { } property)
                    {
                        var keyCode = (KeyCode)property.GetValue(null);

                        if (Input.GetKeyDown(keyCode))
                        {
                            __instance.target.SlotKeyDown(i);
                        }

                        if (Input.GetKeyUp(keyCode))
                        {
                            __instance.target.SlotKeyUp(i);
                        }
                    }
                }
            }
        }
    }

}
