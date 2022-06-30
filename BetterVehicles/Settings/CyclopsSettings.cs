#if SUBNAUTICA
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace BetterVehicles.Settings
{
    [Menu("Better Vehicles - Cyclops")]
    public class CyclopsSettings : ConfigFile
    {
        public CyclopsSettings() : base("config_cyclops") { }

        [Slider("Camera Rotation Speed Damper", 0f, 5f, Step = 0.1f, Format = "{0:F2}")]
        public float CameraRotationSpeedDamper { get; set; } = 1f;
    }
}
#endif