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
        public int ResolutionWidth { get; set; } = Screen.width;
        [IgnoreMember]
        public int ResolutionHeight { get; set; } = Screen.height;

        [Choice("FullScreen Mode", new[] { "Exclusive FullScreen", "FullScreen Window", "Windowed" }), OnChange(nameof(FullScreenModeEvent))]
        public FullScreenModeEnum FullScreenMode { get; set; } = Screen.fullScreenMode;

        [Choice("Anisotropic Filtering", new[] { "Disabled", "Enabled", "Force Enabled" }), OnChange(nameof(AnisotropicFilteringEvent))]
        public AnisotropicFiltering AnisotropicFiltering { get; set; } = QualitySettings.anisotropicFiltering;

        [Choice("Shadow Quality", new[] { "Disable", "Hard Only", "All" }), OnChange(nameof(ShadowQualityEvent))]
        public ShadowQuality ShadowQuality { get; set; } = QualitySettings.shadows;

        [Slider("Shadow Distance", 0, 200, Step = 1), OnChange(nameof(ShadowDistanceEvent))]
        public float ShadowDistance { get; set; } = QualitySettings.shadowDistance;

        [Choice("Shadow Resolution", new[] { "Low", "Medium", "High", "Very High" }), OnChange(nameof(ShadowResolutionEvent))]
        public ShadowResolution ShadowResolution { get; set; } = QualitySettings.shadowResolution;

        public FullScreenModeEnum GetFixedFullScreenMode()
        {
            // Skip not supported fullscreen mode
            return FullScreenMode == FullScreenModeEnum.MaximizedWindow ? FullScreenModeEnum.Windowed : FullScreenMode;
        }

        private void FullScreenModeEvent(ChoiceChangedEventArgs e)
        {
            Screen.fullScreenMode = GetFixedFullScreenMode();
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void AnisotropicFilteringEvent(ChoiceChangedEventArgs e)
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering;
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void ShadowQualityEvent(ChoiceChangedEventArgs e)
        {
            QualitySettings.shadows = ShadowQuality;
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void ShadowDistanceEvent(SliderChangedEventArgs e)
        {
            QualitySettings.shadowDistance = ShadowDistance;
            GraphicsUtility.OnQualityLevelChanged();
        }

        private void ShadowResolutionEvent(ChoiceChangedEventArgs e)
        {
            QualitySettings.shadowResolution = ShadowResolution;
            GraphicsUtility.OnQualityLevelChanged();
        }
    }
}
