using UnityEngine;

namespace BetterSubnautica.Utility
{
    public class ComponentUtility
    {
        public static void WriteComponents(Component __instance)
        {
            WriteComponents(__instance.gameObject);
        }

        public static void WriteComponents(GameObject __instance)
        {
            foreach (var component in __instance.GetComponentsInParent<Component>())
            {
                DebuggerUtility.WriteMessage(__instance.GetType() + ".GetComponentsInParent.Name: " + component.name);
                DebuggerUtility.WriteMessage(__instance.GetType() + ".GetComponentsInParent.Type: " + component.GetType());
                DebuggerUtility.WriteMessage("");
            }

            foreach (var component in __instance.gameObject.GetComponents<Component>())
            {
                DebuggerUtility.WriteMessage(__instance.GetType() + ".GetComponents.Name: " + component.name);
                DebuggerUtility.WriteMessage(__instance.GetType() + ".GetComponents.Type: " + component.GetType());
                DebuggerUtility.WriteMessage("");
            }

            foreach (var component in __instance.gameObject.GetComponentsInChildren<Component>())
            {
                DebuggerUtility.WriteMessage(__instance.GetType() + ".GetComponentsInChildren.Name: " + component.name);
                DebuggerUtility.WriteMessage(__instance.GetType() + ".GetComponentsInChildren.Type: " + component.GetType());
                DebuggerUtility.WriteMessage("");
            }
        }
    }
}
