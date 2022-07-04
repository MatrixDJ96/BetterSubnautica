using UnityEngine;

namespace BetterSubnautica.Utility.Debug
{
    public class ComponentDebugUtility
    {
        public static void WriteComponents(Component __instance)
        {
            WriteComponents(__instance.gameObject);
        }

        public static void WriteComponents(GameObject __instance)
        {
            foreach (var component in __instance.GetComponentsInParent<Component>())
            {
                DebuggerUtility.WriteMessage(__instance.name + ".GetComponentsInParent.Name: " + component.name);
                DebuggerUtility.WriteMessage(__instance.name + ".GetComponentsInParent.Type: " + component.GetType());
                DebuggerUtility.WriteMessage("");
            }

            foreach (var component in __instance.GetComponents<Component>())
            {
                DebuggerUtility.WriteMessage(__instance.name + ".GetComponents.Name: " + component.name);
                DebuggerUtility.WriteMessage(__instance.name + ".GetComponents.Type: " + component.GetType());
                DebuggerUtility.WriteMessage("");
            }

            foreach (var component in __instance.GetComponentsInChildren<Component>())
            {
                DebuggerUtility.WriteMessage(__instance.name + ".GetComponentsInChildren.Name: " + component.name);
                DebuggerUtility.WriteMessage(__instance.name + ".GetComponentsInChildren.Type: " + component.GetType());
                DebuggerUtility.WriteMessage("");
            }
        }
    }
}
