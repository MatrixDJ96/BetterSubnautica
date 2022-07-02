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
            var vSyncCount = QualitySettings.vSyncCount;
            var framerateLimit = Application.targetFrameRate;
            var anisotropicFiltering = QualitySettings.anisotropicFiltering;
            var shadows = QualitySettings.shadows;
            var shadowDistance = QualitySettings.shadowDistance;
            var shadowResolution = QualitySettings.shadowResolution;

            if (resolutionWidth != Core.Settings.ResolutionWidth || resolutionHeight != Core.Settings.ResolutionHeight || fullScreenMode != Core.Settings.GetFixedFullScreenMode())
            {
                Screen.SetResolution(Core.Settings.ResolutionWidth, Core.Settings.ResolutionHeight, Core.Settings.GetFixedFullScreenMode());
            }

            QualitySettings.vSyncCount = Core.Settings.GetFixedVSyncCount();
            Application.targetFrameRate = Core.Settings.GetFixedFramerateCount();
            QualitySettings.anisotropicFiltering = Core.Settings.AnisotropicFiltering;
            QualitySettings.shadows = Core.Settings.ShadowQuality;
            QualitySettings.shadowDistance = Core.Settings.ShadowDistance;
            QualitySettings.shadowResolution = Core.Settings.ShadowResolution;

            GraphicsUtility.OnQualityLevelChanged();

            if (resolutionWidth != Screen.width || resolutionHeight != Screen.height)
            {
                DebuggerUtility.ShowWarning("Resolution: " + resolutionWidth + "x" + resolutionHeight + " -> " + Screen.width + "x" + Screen.height);
            }

            if (fullScreenMode != Screen.fullScreenMode)
            {
                DebuggerUtility.ShowWarning("FullScreen Mode: " + fullScreenMode + " -> " + Screen.fullScreenMode);
            }

            if (vSyncCount != QualitySettings.vSyncCount)
            {
                DebuggerUtility.ShowWarning("VSync: " + vSyncCount + " -> " + QualitySettings.vSyncCount);
            }

            if (framerateLimit != Application.targetFrameRate)
            {
                DebuggerUtility.ShowWarning("Framerate: " + framerateLimit + " -> " + Application.targetFrameRate);
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
