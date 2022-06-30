using BetterSubnautica.Utility;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;
using FullScreenModeEnum = UnityEngine.FullScreenMode;

namespace BetterGraphics
{
    [Menu("Better Graphics")]
    public class Settings : ConfigFile
    {
        [IgnoreMember]
        public int ResolutionWidth { get; set; } = Screen.currentResolution.width;
        [IgnoreMember]
        public int ResolutionHeight { get; set; } = Screen.currentResolution.height;

        [Choice("FullScreen Mode", new[] { "Exclusive FullScreen", "FullScreen Window", "Windowed" }), OnChange(nameof(FullScreenModeEvent))]
        public int FullScreenMode { get; set; } = (int)Screen.fullScreenMode;

        [Choice("Anisotropic Filtering", new[] { "Disabled", "Enabled", "Force Enabled" }), OnChange(nameof(AnisotropicFilteringEvent))]
        public int AnisotropicFiltering { get; set; } = (int)QualitySettings.anisotropicFiltering;

        [Choice("Shadow Quality", new[] { "Disable", "Hard Only", "All" }), OnChange(nameof(ShadowQualityEvent))]
        public int ShadowQuality { get; set; } = (int)QualitySettings.shadows;

        [Slider("Shadow Distance", 0, 200), OnChange(nameof(ShadowDistanceEvent))]
        public int ShadowDistance { get; set; } = (int)QualitySettings.shadowDistance;

        [Choice("Shadow Resolution", new[] { "Low", "Medium", "High", "Very High" }), OnChange(nameof(ShadowResolutionEvent))]
        public int ShadowResolution { get; set; } = (int)QualitySettings.shadowResolution;

        private void FullScreenModeEvent(ChoiceChangedEventArgs e)
        {
            // Skip not supported fullscreen mode
            Screen.fullScreenMode = (FullScreenMode == (int)FullScreenModeEnum.MaximizedWindow ? FullScreenModeEnum.Windowed : (FullScreenModeEnum)FullScreenMode);
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void AnisotropicFilteringEvent(ChoiceChangedEventArgs e)
        {
            QualitySettings.anisotropicFiltering = (AnisotropicFiltering)AnisotropicFiltering;
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void ShadowQualityEvent(SliderChangedEventArgs e)
        {
            QualitySettings.shadows = (ShadowQuality)ShadowQuality;
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void ShadowDistanceEvent(SliderChangedEventArgs e)
        {
            QualitySettings.shadowDistance = ShadowDistance;
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void ShadowResolutionEvent(ChoiceChangedEventArgs e)
        {
            QualitySettings.shadowResolution = (ShadowResolution)ShadowResolution;
            GraphicsUtility.OnQualityLevelChanged();
        }
    }
}
