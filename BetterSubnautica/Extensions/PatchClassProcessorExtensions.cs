using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BetterSubnautica.Extensions
{
    public static class PatchClassProcessorExtensions
    {
        public static Type GetContainerType(this PatchClassProcessor __instance)
        {
            return (Type)Traverse.Create(__instance).Field("containerType").GetValue();
        }

        public static bool HasAttribute<T>(this PatchClassProcessor __instance) where T : Attribute
        {
            return __instance.GetContainerType().GetCustomAttribute<T>() != null;
        }

        public static bool HasAttribute(this PatchClassProcessor __instance, Type attribute)
        {
            if (attribute != null && attribute.IsSubclassOf(typeof(Attribute)))
            {
                return __instance.GetContainerType().GetCustomAttribute(attribute) != null;
            }

            return false;
        }

        public static bool HasAttributes(this PatchClassProcessor __instance, Type[] attributes)
        {
            var result = attributes != null;

            if (result)
            {
                foreach (var attribute in attributes)
                {
                    if (attribute == null || !__instance.HasAttribute(attribute))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        public static List<MethodInfo> PatchWithAttribute<T>(this PatchClassProcessor __instance) where T : Attribute
        {
            return __instance.HasAttribute<T>() ? __instance.Patch() : null;
        }

        public static List<MethodInfo> PatchWithoutAttribute<T>(this PatchClassProcessor __instance) where T : Attribute
        {
            return __instance.HasAttribute<T>() ? null : __instance.Patch();
        }

        public static List<MethodInfo> PatchWithAttributes(this PatchClassProcessor __instance, Type[] attributes)
        {
            return __instance.HasAttributes(attributes) ? __instance.Patch() : null;
        }

        public static List<MethodInfo> PatchWithoutAttributes(this PatchClassProcessor __instance, Type[] attributes)
        {
            return __instance.HasAttributes(attributes) ? null : __instance.Patch();
        }
    }
}
