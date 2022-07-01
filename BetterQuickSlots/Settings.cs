using BetterQuickSlots.MonoBehaviours;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace BetterQuickSlots
{
    [Menu("Better Quick Slots")]
    public class Settings : ConfigFile
    {
        [Slider("Slot Count", 5f, 10f, Step = 1f)]
        public int SlotCount { get; set; } = 10;

        [Keybind("Slot 6 Toggle")]
        public KeyCode Slot6 { get; set; } = KeyCode.Alpha6;

        [Keybind("Slot 7 Toggle")]
        public KeyCode Slot7 { get; set; } = KeyCode.Alpha7;

        [Keybind("Slot 8 Toggle")]
        public KeyCode Slot8 { get; set; } = KeyCode.Alpha8;

        [Keybind("Slot 9 Toggle")]
        public KeyCode Slot9 { get; set; } = KeyCode.Alpha9;

        [Keybind("Slot 10 Toggle")]
        public KeyCode Slot10 { get; set; } = KeyCode.Alpha0;

        [Slider("Text Font Size", 0, 100), OnChange(nameof(TextFontSizeEvent))]
        public int TextFontSize { get; set; } = 12;

        [Slider("Text Offset Y", 0, 100), OnChange(nameof(TextOffsetEvent))]
        public int TextOffsetY { get; set; } = 8;

        private void TextFontSizeEvent(SliderChangedEventArgs e)
        {
            if (QuickSlotsController.Instance != null)
            {
                QuickSlotsController.Instance.ForceUpdate = true;
            }
        }

        private void TextOffsetEvent(SliderChangedEventArgs e)
        {
            if (QuickSlotsController.Instance != null)
            {
                QuickSlotsController.Instance.ForceUpdate = true;
            }
        }
    }
}
