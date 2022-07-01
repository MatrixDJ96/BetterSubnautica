using System.Text;
using UnityEngine;

namespace BetterSubnautica.Utility
{
    public static class KeyCodeUtility
    {
        public static string GetName(KeyCode keyCode, bool withColor = true)
        {
            StringBuilder sb = new();

            foreach (var input in GameInput.inputs)
            {
                if (input.keyCode == keyCode)
                {
                    string text = GameInput.GetInputName(input.name);
                    if (text != null)
                    {
                        text = uGUI.GetDisplayTextForBinding(text);
                        sb.Append(withColor ? $"<color=#ADF8FFFF>{text}</color>" : text);
                    }
                }
            }

            if (sb.Length == 0)
            {
                var text = Language.main.Get("NoInputAssigned");
                sb.Append(withColor ? $"<color=#ADF8FFFF>{text}</color>" : text);
            }

            return sb.ToString();
        }

        public static KeyCode GetKeyCode(GameInput.Button button)
        {
            int numBindingSets = GameInput.GetNumBindingSets();

            for (int i = 0; i < numBindingSets; i++)
            {
                int bindingInternal = GameInput.GetBindingInternal(GameInput.GetPrimaryDevice(), button, (GameInput.BindingSet)i);

                if (bindingInternal != -1)
                {
                    return GameInput.inputs[bindingInternal].keyCode;
                }
            }

            return KeyCode.None;
        }
    }
}
