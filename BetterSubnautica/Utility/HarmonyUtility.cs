using System.Reflection;
using BepInEx.Logging;
using BetterSubnautica.Attributes;
using HarmonyLib;

namespace BetterSubnautica.Utility
{
    public static class HarmonyUtility
    {
        public static void PrePatchAll(Harmony harmony, Assembly assembly, ManualLogSource logger)
        {
            AccessTools.GetTypesFromAssembly(assembly).Do(type =>
            {
                var prePatchAttribute = type.GetCustomAttribute<PrePatchAttribute>();
                var postPatchAttribute = type.GetCustomAttribute<PostPatchAttribute>();

                if (prePatchAttribute != null)
                {
                    var methodInfos = harmony.CreateClassProcessor(type).Patch();

                    foreach (var methodInfo in methodInfos ?? [])
                    {
                        logger.LogInfo($" - Patched {methodInfo.Name} method");
                    }
                }
            });
        }

        public static void PatchAll(Harmony harmony, Assembly assembly, ManualLogSource logger)
        {
            AccessTools.GetTypesFromAssembly(assembly).Do(type =>
            {
                var prePatchAttribute = type.GetCustomAttribute<PrePatchAttribute>();
                var postPatchAttribute = type.GetCustomAttribute<PostPatchAttribute>();

                if (prePatchAttribute == null && postPatchAttribute == null)
                {
                    var methodInfos = harmony.CreateClassProcessor(type).Patch();

                    foreach (var methodInfo in methodInfos ?? [])
                    {
                        logger.LogInfo($" - Patched {methodInfo.Name} method");
                    }
                }
            });
        }

        public static void PostPatchAll(Harmony harmony, Assembly assembly, ManualLogSource logger)
        {
            AccessTools.GetTypesFromAssembly(assembly).Do(type =>
            {
                var prePatchAttribute = type.GetCustomAttribute<PrePatchAttribute>();
                var postPatchAttribute = type.GetCustomAttribute<PostPatchAttribute>();

                if (postPatchAttribute != null)
                {
                    var methodInfos = harmony.CreateClassProcessor(type).Patch();

                    foreach (var methodInfo in methodInfos ?? [])
                    {
                        logger.LogInfo($" - Patched {methodInfo.Name} method");
                    }
                }
            });
        }
    }
}
