using BetterSubnautica.Utility;
using BetterVehicles.Settings;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using System.Reflection;

namespace BetterVehicles
{
    [QModCore]
    public class Core
    {
        public static Harmony Harmony { get; } = new Harmony("BetterVehicles");

        public static GlobalSettings GlobalSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<GlobalSettings>();
#if SUBNAUTICA
        public static CyclopsSettings CyclopsSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<CyclopsSettings>();
#elif BELOWZERO
        public static SeatruckSettings SeatruckSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<SeatruckSettings>();
#endif

        [QModPrePatch]
        public static void PrePatch() => HarmonyUtility.PrePatchAll(Harmony, Assembly.GetExecutingAssembly());

        [QModPatch]
        public static void Patch() => HarmonyUtility.PatchAll(Harmony, Assembly.GetExecutingAssembly());

        [QModPostPatch]
        public static void PostPatch() => HarmonyUtility.PostPatchAll(Harmony, Assembly.GetExecutingAssembly());
    }
}
