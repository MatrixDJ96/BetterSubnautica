using UnityEngine;

namespace BetterSavegames.MonoBehaviours
{
    public class WaitScreenController : MonoBehaviour
    {
        private int frameRate;
        private int vSyncCount;

        public void UnlockFramerate()
        {
            if (Core.Settings.MaximizeLoadingSpeed)
            {
                frameRate = Application.targetFrameRate;
                vSyncCount = QualitySettings.vSyncCount;

                Application.targetFrameRate = -1;
                QualitySettings.vSyncCount = 0;
            }
        }

        public void RestoreFramerate()
        {
            if (Core.Settings.MaximizeLoadingSpeed)
            {
                Application.targetFrameRate = frameRate;
                QualitySettings.vSyncCount = vSyncCount;
            }
        }
    }
}
