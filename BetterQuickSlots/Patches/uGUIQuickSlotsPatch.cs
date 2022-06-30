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
            if (uGUI.main == __instance)
            {
                if (__instance.gameObject.GetComponent<QuickSlotsController>() == null)
                {
                    __instance.gameObject.AddComponent<QuickSlotsController>();
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
            if (Player.main.GetCanItemBeUsed() && !IntroVignette.isIntroActive && __instance.target != null)
            {
                var traverse = Traverse.Create(typeof(SlotsUtility));

                for (int i = Player.quickSlotButtonsCount; i < Core.Settings.SlotCount; i++)
                {
                    var keyCode = traverse.Property($"Slot{i + 1}").GetValue<KeyCode>();

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
