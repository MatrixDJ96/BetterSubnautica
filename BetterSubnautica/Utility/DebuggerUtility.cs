using BetterSubnautica.MonoBehaviours.Debug;
using System;

namespace BetterSubnautica.Utility
{
    public static class DebuggerUtility
    {
        public static string Prefix => Core.Harmony.Id;

        public static string GenerateString(string value, string key, bool prefix = true)
        {
            return !string.IsNullOrWhiteSpace(value) ? (prefix ? "[" + Prefix + "] " : "") + (key != null ? key + ": " : string.Empty) + value : string.Empty;
        }

        public static void ShowWarning(string text, string key = null, bool prefix = true)
        {
            ErrorMessage.AddWarning(GenerateString(text, key, prefix));
        }

        public static void ShowMessage(string text, string key = null, bool prefix = true)
        {
            DebuggerController.Instance.AddMessage(new DebuggerController.Message() { Text = text, Prefix = prefix }, key);
        }

        public static void WriteMessage(string text, string key = null, bool prefix = true)
        {
            Console.WriteLine(GenerateString(text, key, prefix));
        }

        public static void ClearMessages()
        {
            DebuggerController.Instance.ClearMessages();
        }
    }
}
