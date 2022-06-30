using BetterQuickSlots.Utility;
using BetterSubnautica.Utility;
using System;
using System.Reflection;
using UnityEngine;
#if SUBNAUTICA_STABLE
using Text = UnityEngine.UI.Text;
#else
using TMPro;
using Text = TMPro.TextMeshProUGUI;
#endif

namespace BetterQuickSlots.MonoBehaviours
{
    public class QuickSlotsController : MonoBehaviour
    {
        private Text[] labels = Array.Empty<Text>();

        private uGUI_QuickSlots quickSlots = null;
        private uGUI_QuickSlots QuickSlots
        {
            get
            {
                if (quickSlots == null)
                {
                    quickSlots = uGUI.main.quickSlots;
                }
                return quickSlots;
            }
        }

        private QuickSlots target = null;
        private QuickSlots Target
        {
            get
            {
                if (target == null && quickSlots != null)
                {
                    target = (QuickSlots)quickSlots.target;
                }
                return target;
            }
        }

        private int SlotCount
        {
            get
            {
                var slotCount = 0;
                if (target != null)
                {
                    slotCount = target.slotCount;
                }
                return slotCount;
            }
        }

        private int FontSize
        {
            get
            {
                var fontSize = 0;
                if (labels.Length > 0 && labels[0] != null)
                {
#if SUBNAUTICA_STABLE
                    fontSize = labels[0].fontSize;
#else
                    fontSize = (int)labels[0].fontSize;
#endif
                }
                return fontSize;
            }
        }

        private int TextOffsetY { get; set; } = 0;

        protected void Update()
        {
            if (QuickSlots != null && Target != null)
            {
                if (SlotCount != Core.Settings.SlotCount)
                {
                    UpdateSlotCount();
                    Reinitialize();
                    return;
                }
                if (FontSize != Core.Settings.TextFontSize || TextOffsetY != Core.Settings.TextOffsetY)
                {
                    UpdateSlotLabel();
                }
            }
        }

        protected void UpdateSlotCount()
        {
            var slotCount = Core.Settings.SlotCount;
            var newBinding = new InventoryItem[slotCount];
            var oldBinding = target.binding;

            for (int i = 0; i < oldBinding.Length; i++)
            {
                if (i < newBinding.Length)
                {
                    newBinding[i] = oldBinding[i];
                }
            }

            target.binding = newBinding;
            target.slotCount = slotCount;
        }

        protected void UpdateSlotLabel()
        {
#if SUBNAUTICA_STABLE
            var defaultText = HandReticle.main.interactPrimaryText;
#else
            var defaultText = HandReticle.main.compTextHand;
#endif

            TextOffsetY = Core.Settings.TextOffsetY;

            if (labels.Length != SlotCount)
            {
                labels = new Text[SlotCount];
            }

            for (int i = 0; i < quickSlots.icons.Length; i++)
            {
                if (labels[i] == null)
                {
                    labels[i] = Instantiate(defaultText, quickSlots.icons[i].transform, false);
                }

                labels[i].enabled = true;
                labels[i].gameObject.SetActive(true);

                labels[i].text = GetInputSlotName(i);
                labels[i].fontSize = Core.Settings.TextFontSize;

#if SUBNAUTICA_STABLE
                labels[i].alignment = TextAnchor.MiddleCenter;
                labels[i].verticalOverflow = VerticalWrapMode.Overflow;
                labels[i].horizontalOverflow = HorizontalWrapMode.Overflow;
#else
                labels[i].alignment = TextAlignmentOptions.Center;
                labels[i].overflowMode = TextOverflowModes.Overflow;
#endif

                labels[i].rectTransform.localScale = Vector3.one;
                labels[i].rectTransform.localPosition = Vector3.zero;
                labels[i].rectTransform.localRotation = Quaternion.identity;

                labels[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);
                labels[i].rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                labels[i].rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

                labels[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, labels[i].preferredHeight);
                labels[i].rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, labels[i].preferredWidth);

                labels[i].rectTransform.anchoredPosition = new Vector2(0f, quickSlots.icons[i].rectTransform.rect.y - TextOffsetY);
            }
        }

        public void Reinitialize()
        {
            quickSlots.Init(target);
        }

        private string GetInputSlotName(int slot)
        {
            if (slot < SlotCount)
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.Static;

                foreach (var property in typeof(SlotsUtility).GetProperties(bindingFlags))
                {
                    if (property.Name.ToLower() == ("slot" + (slot + 1).ToString()))
                    {
                        return KeyCodeUtility.GetName((KeyCode)property.GetValue(null));
                    }
                }
            }

            return "";
        }
    }
}
