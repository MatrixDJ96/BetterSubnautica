using BetterSubnautica.Utility;
using HarmonyLib;
using SMLHelper.V2.Options.Attributes;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BetterQuickSlots.Utility
{
    public class SlotsUtility
    {
        public static KeyCode Slot1 => KeyCodeUtility.GetKeyCode(GameInput.Button.Slot1);
        public static KeyCode Slot2 => KeyCodeUtility.GetKeyCode(GameInput.Button.Slot2);
        public static KeyCode Slot3 => KeyCodeUtility.GetKeyCode(GameInput.Button.Slot3);
        public static KeyCode Slot4 => KeyCodeUtility.GetKeyCode(GameInput.Button.Slot4);
        public static KeyCode Slot5 => KeyCodeUtility.GetKeyCode(GameInput.Button.Slot5);
        public static KeyCode Slot6 => Core.Settings.Slot6;
        public static KeyCode Slot7 => Core.Settings.Slot7;
        public static KeyCode Slot8 => Core.Settings.Slot8;
        public static KeyCode Slot9 => Core.Settings.Slot9;
        public static KeyCode Slot10 => Core.Settings.Slot10;

        private static string[] CreateSlotNames()
        {
            if (Core.Settings.GetType().GetProperty(nameof(Core.Settings.SlotCount)) is PropertyInfo property)
            {
                var attribute = property.GetCustomAttribute<SliderAttribute>();

                if (attribute != null)
                {
                    var slotCount = (int)attribute.Max;
                    var slotNames = new string[slotCount + 1];

                    for (int i = 0; i < slotNames.Length; i++)
                    {
                        slotNames[i] = "QuickSlot" + i;
                    }

                    return slotNames;
                }
            }

            return QuickSlots.slotNames;
        }

        public static string[] SlotNames = CreateSlotNames();

        public static string GetInputSlotName(int slot, bool withColor = true)
        {
            if (slot < SlotNames.Length)
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.Static;

                foreach (var property in typeof(SlotsUtility).GetProperties(bindingFlags))
                {
                    if (property.Name.ToLower() == ("slot" + (slot + 1).ToString()))
                    {
                        return KeyCodeUtility.GetName((KeyCode)property.GetValue(null), withColor);
                    }
                }
            }

            return "";
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = new List<CodeInstruction>(instructions);

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].operand is FieldInfo operand && operand == AccessTools.Field(typeof(QuickSlots), nameof(QuickSlots.slotNames)))
                {
                    list[i].operand = AccessTools.Field(typeof(SlotsUtility), nameof(SlotNames));
                }
            }

            return list;
        }
    }
}
