using System.Text;
using UnityEngine;

namespace BetterSubnautica.Utility
{
    public static class KeyCodeUtility
    {
        public static string GetName(KeyCode keyCode)
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
                        sb.AppendFormat("<color=#ADF8FFFF>{0}</color>", text);
                    }
                }
            }

            if (sb.Length == 0)
            {
                sb.AppendFormat("<color=#ADF8FFFF>{0}</color>", Language.main.Get("NoInputAssigned"));
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
