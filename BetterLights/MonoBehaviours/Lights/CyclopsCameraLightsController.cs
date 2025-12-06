#if SUBNAUTICA
using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public class CyclopsCameraLightsController : AbstractLightsController<CyclopsExternalCams>
    {
        protected override void GetLights()
        {
            
        }

        protected override void GetSettings()
        {
            IntensityOffset = Core.CyclopsSettings.CameraLightsIntensityOffset;
            RangeOffset = Core.CyclopsSettings.CameraLightsRangeOffset;
        }
    }
}
#endif
