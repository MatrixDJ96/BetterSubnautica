#if SUBNAUTICA
namespace BetterLights.MonoBehaviours.Lights
{
    public class CyclopsCameraLightsController : AbstractLightsController<CyclopsExternalCams>
    {
        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                lights = [component.cameraLight];
            }
        }

        public override void UpdateColor() { }

        protected override void UpdateSettings()
        {
            IntensityOffset = Core.CyclopsSettings.CameraLightsIntensityOffset;
            RangeOffset = Core.CyclopsSettings.CameraLightsRangeOffset;
        }
    }
}
#endif
