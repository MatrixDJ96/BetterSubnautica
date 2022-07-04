using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace BetterVehicles.Settings
{
    [Menu("Better Vehicles - Global")]
    public class GlobalSettings : ConfigFile
    {
        public GlobalSettings() : base("config_global") { }

        [Toggle("Linked Storage", Tooltip = "Link personal inventory with vehicle storage")]
        public bool LinkedStorage { get; set; } = false;

        [Keybind("Upgrade Modules Button")]
        public KeyCode UpgradeModules { get; set; } = KeyCode.U;

        [Keybind("Torpedo Storage Button")]
        public KeyCode TorpedoStorage { get; set; } = KeyCode.T;

        [Keybind("Vehicle Storage Button")]
        public KeyCode VehicleStorage { get; set; } = KeyCode.V;
    }
}
