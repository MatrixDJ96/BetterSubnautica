﻿#if BELOWZERO
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace BetterLights.Settings
{
    [Menu("Better Lights - Seatruck")]
    public class SeatruckSettings : ConfigFile
    {
        public SeatruckSettings() : base("config_seatruck") { }

        [Keybind("Lights Button Toggle")]
        public KeyCode LightsButtonToggle { get; set; } = KeyCode.Mouse1;

        [Slider("Lights Consumption", 0f, 0.2f, Step = 0.001f, Format = "{0:F3}")]
        public float LightsConsumption { get; set; } = 0f;

        [Slider("Lights Range Offset", -100f, 100f, Step = 1f)]
        public float LightsRangeOffset { get; set; } = 0f;

        [Slider("Lights Intensity Offset", -5f, 5f, Format = "{0:F2}")]
        public float LightsIntensityOffset { get; set; } = -0.2f;

        [Slider("Vol. Lights Intensity Offset", -5f, 5f, Format = "{0:F2}")]
        public float VolumetricLightsIntensityOffset { get; set; } = -0.80f;
    }
}
#endif
