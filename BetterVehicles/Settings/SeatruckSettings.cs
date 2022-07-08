#if BELOWZERO
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace BetterVehicles.Settings
{
    [Menu("Better Vehicles - Seatruck")]
    public class SeatruckSettings : ConfigFile
    {
        public SeatruckSettings() : base("config_seatruck") { }

        [Keybind("Direct Enter/Exit Button", Tooltip = "Extra button to press with default enter/exit action")]
        public KeyCode ForceAction { get; set; } = KeyCode.LeftControl;

        [Keybind("Detach Segments Button")]
        public KeyCode DetachSegments { get; set; } = KeyCode.V;
    }
}
#endif
