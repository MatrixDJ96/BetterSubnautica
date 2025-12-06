#if SUBNAUTICA
using System.Collections.Generic;
using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public class CyclopsLightsController : AbstractLightsController<SubRoot>
    {
        protected override void GetLights()
        {
            
        }
        
        protected override void GetSettings()
        {
            IntensityOffset = Core.CyclopsSettings.ExternalLightsIntensityOffset;
            RangeOffset = Core.CyclopsSettings.ExternalLightsRangeOffset;
        }
    }
}
#endif
