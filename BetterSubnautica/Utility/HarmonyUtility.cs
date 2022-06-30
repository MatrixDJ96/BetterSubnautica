using BetterSubnautica.Attributes;
using BetterSubnautica.Extensions;
using HarmonyLib;
using System.Reflection;

namespace BetterSubnautica.Utility
{
    public static class HarmonyUtility
    {
        public static void PrePatchAll(Harmony harmony, Assembly assembly)
        {
            AccessTools.GetTypesFromAssembly(assembly).Do(type => harmony.CreateClassProcessor(type).PatchWithAttribute<PrePatchAttribute>());
        }

        public static void PatchAll(Harmony harmony, Assembly assembly)
        {
            AccessTools.GetTypesFromAssembly(assembly).Do(type => harmony.CreateClassProcessor(type).PatchWithoutAttributes(new[] { typeof(PrePatchAttribute), typeof(PostPatchAttribute) }));
        }

        public static void PostPatchAll(Harmony harmony, Assembly assembly)
        {
            AccessTools.GetTypesFromAssembly(assembly).Do(type => harmony.CreateClassProcessor(type).PatchWithAttribute<PostPatchAttribute>());
        }
    }
}
