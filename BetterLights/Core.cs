using BetterLights.Settings;
using BetterSubnautica.Utility;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using System.Reflection;

namespace BetterLights
{
    [QModCore]
    public class Core
    {
        public static Harmony Harmony { get; } = new Harmony("BetterLights");

        public static FlashlightSettings FlashlightSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<FlashlightSettings>();
#if BELOWZERO
        public static FlashlightHelmetSettings FlashlightHelmetSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<FlashlightHelmetSettings>();
#endif
        public static SeaglideSettings SeaglideSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<SeaglideSettings>();
#if SUBNAUTICA
        public static SeamothSettings SeamothSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<SeamothSettings>();
#elif BELOWZERO
        public static SeatruckSettings SeatruckSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<SeatruckSettings>();
#endif
        public static ExosuitSettings ExosuitSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<ExosuitSettings>();
#if SUBNAUTICA
        public static CyclopsSettings CyclopsSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<CyclopsSettings>();
#endif
        public static MapRoomCameraSettings MapRoomCameraSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<MapRoomCameraSettings>();
        public static VehiclesSettings VehiclesSettings { get; } = OptionsPanelHandler.Main.RegisterModOptions<VehiclesSettings>();

        [QModPrePatch]
        public static void PrePatch() => HarmonyUtility.PrePatchAll(Harmony, Assembly.GetExecutingAssembly());

        [QModPatch]
        public static void Patch() => HarmonyUtility.PatchAll(Harmony, Assembly.GetExecutingAssembly());

        [QModPostPatch]
        public static void PostPatch() => HarmonyUtility.PostPatchAll(Harmony, Assembly.GetExecutingAssembly());
    }
}
