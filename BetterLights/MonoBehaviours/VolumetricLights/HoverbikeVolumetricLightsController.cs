#if BELOWZERO
using BetterSubnautica.Extensions;

namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public class HoverbikeVolumetricLightsController : AbstractVolumetricLightsController<Hoverbike>
    {
        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                if (component.GetToggleLights() is global::ToggleLights toggleLights && toggleLights.lightsParent != null)
                {
                    volumetricLights = toggleLights.lightsParent.GetComponentsInChildren<VFXVolumetricLight>();
                }
            }
        }

        protected override void UpdateSettings()
        {
            IntensityOffset = Core.HoverbikeSettings.VolumetricLightsIntensityOffset;
        }
    }
}
#endif
