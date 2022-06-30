#if BELOWZERO
using BetterSubnautica.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public class SeatruckLightsController : AbstractLightsController<SeaTruckSegment>
    {
        private SeaTruckLights additionalComponent = null;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                additionalComponent = component.gameObject.GetComponent<SeaTruckLights>();

                if (!component.IsMainSegment() || additionalComponent == null || additionalComponent.floodLight == null)
                {
                    Destroy(this);
                    return;
                }

                var seatruckLights = new List<Light>();

                foreach (Transform item in additionalComponent.floodLight.transform)
                {
                    if (item.gameObject.GetComponent<Light>() is Light light)
                    {
                        seatruckLights.Add(light);
                    }
                }

                lights = seatruckLights.ToArray();
            }
        }

        protected override void UpdateSettings()
        {
            IntensityOffset = Core.SeatruckSettings.LightsIntensityOffset;
            RangeOffset = Core.SeatruckSettings.LightsRangeOffset;
        }
    }
}
#endif
