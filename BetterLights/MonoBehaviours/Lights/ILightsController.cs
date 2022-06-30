using UnityEngine;

namespace BetterLights.MonoBehaviours.Lights
{
    public interface ILightsController
    {
        Light[] Lights { get; }
        Color Color { get; set; }
        float IntensityOffset { get; set; }
        float RangeOffset { get; set; }
        float UpdateInterval { get; set; }

        void UpdateColor();
        void UpdateIntensity();
        void UpdateRange();
    }
}
