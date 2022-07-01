using BetterSubnautica.Utility;
using HarmonyLib;
using UnityEngine;

namespace BetterGraphics.Patches
{
    [HarmonyPatch(typeof(GameSettings))]
    [HarmonyPatch(nameof(GameSettings.SerializeSettings))]
    class GameSettingsSerializeSettingsPatch
    {
        static void Postfix(GameSettings __instance, GameSettings.ISerializer serializer)
        {
            var resolutionWidth = Screen.width;
            var resolutionHeight = Screen.height;
            var fullScreenMode = Screen.fullScreenMode;
            var anisotropicFiltering = QualitySettings.anisotropicFiltering;
            var shadows = QualitySettings.shadows;
            var shadowDistance = QualitySettings.shadowDistance;
            var shadowResolution = QualitySettings.shadowResolution;

            Screen.SetResolution(Core.Settings.ResolutionWidth, Core.Settings.ResolutionHeight, Core.Settings.GetFixedFullScreenMode());

            QualitySettings.anisotropicFiltering = Core.Settings.AnisotropicFiltering;
            QualitySettings.shadows = Core.Settings.ShadowQuality;
            QualitySettings.shadowDistance = Core.Settings.ShadowDistance;
            QualitySettings.shadowResolution = Core.Settings.ShadowResolution;

            GraphicsUtility.OnQualityLevelChanged();

            if (resolutionWidth != Screen.width || resolutionHeight != Screen.height)
            {
                DebuggerUtility.ShowWarning("Resolution: " + resolutionWidth + "x" + resolutionHeight + " -> " + Screen.width + "x" + Screen.height);
            }

            if (resolutionWidth != Screen.width || resolutionHeight != Screen.height || fullScreenMode != Screen.fullScreenMode)
            {
                DebuggerUtility.ShowWarning("FullScreen Mode: " + fullScreenMode + " -> " + Screen.fullScreenMode);
            }

            if (anisotropicFiltering != QualitySettings.anisotropicFiltering)
            {
                DebuggerUtility.ShowWarning("Anisotropic Filtering: " + anisotropicFiltering + " -> " + QualitySettings.anisotropicFiltering);
            }

            if (shadows != QualitySettings.shadows)
            {
                DebuggerUtility.ShowWarning("Shadow Quality: " + shadows + " -> " + QualitySettings.shadows);
            }

            if (shadowDistance != QualitySettings.shadowDistance)
            {
                DebuggerUtility.ShowWarning("Shadow Distance: " + shadowDistance + " -> " + QualitySettings.shadowDistance);
            }

            if (shadowResolution != QualitySettings.shadowResolution)
            {
                DebuggerUtility.ShowWarning("Shadow Resolution: " + shadowResolution + " -> " + QualitySettings.shadowResolution);
            }

            Core.Settings.Save();
        }
    }
}
