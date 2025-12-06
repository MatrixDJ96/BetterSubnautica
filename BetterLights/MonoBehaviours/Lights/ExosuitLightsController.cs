using BetterSubnautica.Extensions;
using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public class ExosuitLightsController : AbstractLightsController<Exosuit>
    {
        protected override void GetSettings()
        {
            Color = Core.ExosuitSettings.LightsColor;
            IntensityOffset = Core.ExosuitSettings.LightsIntensityOffset;
            RangeOffset = Core.ExosuitSettings.LightsRangeOffset;
        }
    }
}
