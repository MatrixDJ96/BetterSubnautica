#if BELOWZERO
using BetterSubnautica.Extensions;

namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public class SeatruckVolumetricLightsController : AbstractVolumetricLightsController<SeaTruckSegment>
    {
        private SeaTruckLights additionalComponent = null;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                additionalComponent = component.gameObject.GetComponent<SeaTruckLights>();

                if (!component.IsMainSegment() || additionalComponent == null || additionalComponent.dimFloodlightsOnEnter == null)
                {
                    Destroy(this);
                    return;
                }

                volumetricLights = additionalComponent.dimFloodlightsOnEnter;
            }
        }

        protected override void UpdateSettings()
        {
            IntensityOffset = Core.SeatruckSettings.VolumetricLightsIntensityOffset;
        }
    }
}
#endif
