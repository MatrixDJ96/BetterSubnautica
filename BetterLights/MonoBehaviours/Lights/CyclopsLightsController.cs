#if SUBNAUTICA
using System.Collections.Generic;
using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public class CyclopsLightsController : AbstractLightsController<SubRoot>
    {
        private CyclopsLightingPanel additionalComponent;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                additionalComponent = component.gameObject.GetComponentInChildren<CyclopsLightingPanel>();

                if (!component.isCyclops || additionalComponent == null || additionalComponent.floodlightsHolder == null)
                {
                    Destroy(this);
                    return;
                }

                var cyclopsLights = new List<Light>();

                foreach (Transform item in additionalComponent.floodlightsHolder.transform)
                {
                    if (item.gameObject.GetComponent<Light>() is { } light)
                    {
                        cyclopsLights.Add(light);
                    }
                }

                lights = cyclopsLights.ToArray();
            }
        }

        protected override void UpdateSettings()
        {
            IntensityOffset = Core.CyclopsSettings.ExternalLightsIntensityOffset;
            RangeOffset = Core.CyclopsSettings.ExternalLightsRangeOffset;
        }
    }
}
#endif
