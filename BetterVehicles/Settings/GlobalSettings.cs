using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace BetterVehicles.Settings
{
    [Menu("Better Vehicles - Global")]
    public class GlobalSettings : ConfigFile
    {
        public GlobalSettings() : base("config_vehicles") { }

        [Keybind("Upgrade Modules Button")]
        public KeyCode UpgradeModules { get; set; } = KeyCode.U;

        [Keybind("Torpedo Storage Button")]
        public KeyCode TorpedoStorage { get; set; } = KeyCode.T;

        [Keybind("Vehicle Storage Button")]
        public KeyCode VehicleStorage { get; set; } = KeyCode.V;
    }
}
