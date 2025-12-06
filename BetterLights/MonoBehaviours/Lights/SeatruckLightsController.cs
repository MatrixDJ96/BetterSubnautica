#if BELOWZERO
using System.Collections.Generic;
using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public class SeatruckLightsController : AbstractLightsController<SeaTruckSegment>
    {
        protected override void GetSettings()
        {
            Color = Core.SeatruckSettings.LightsColor;
            IntensityOffset = Core.SeatruckSettings.LightsIntensityOffset;
            RangeOffset = Core.SeatruckSettings.LightsRangeOffset;
        }
    }
}
#endif
