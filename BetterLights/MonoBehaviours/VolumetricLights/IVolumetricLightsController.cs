namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public interface IVolumetricLightsController
    {
        VFXVolumetricLight[] VolumetricLights { get; }
        float IntensityOffset { get; set; }
        float UpdateInterval { get; set; }

        void UpdateMaterial(VFXVolumetricLight volumetricLight, bool forceUpdate);
    }
}
