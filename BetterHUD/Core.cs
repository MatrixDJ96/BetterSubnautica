using BetterSubnautica.Utility;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using System.Reflection;

namespace BetterHUD
{
    [QModCore]
    public class Core
    {
        public static Harmony Harmony { get; } = new Harmony("BetterHUD");

        public static Settings Settings { get; } = OptionsPanelHandler.Main.RegisterModOptions<Settings>();

        [QModPrePatch]
        public static void PrePatch() => HarmonyUtility.PrePatchAll(Harmony, Assembly.GetExecutingAssembly());

        [QModPatch]
        public static void Patch() => HarmonyUtility.PatchAll(Harmony, Assembly.GetExecutingAssembly());

        [QModPostPatch]
        public static void PostPatch() => HarmonyUtility.PostPatchAll(Harmony, Assembly.GetExecutingAssembly());
    }
}
