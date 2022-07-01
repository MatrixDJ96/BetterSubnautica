using UnityEngine;

namespace BetterSavegames.MonoBehaviours
{
    public class WaitScreenController : MonoBehaviour
    {
        private int frameRate;
        private int vSyncCount;

        protected void Awake()
        {
            frameRate = Application.targetFrameRate;
            vSyncCount = QualitySettings.vSyncCount;
        }

        public void UnlockFramerate()
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
        }

        public void RestoreFramerate()
        {
            Application.targetFrameRate = frameRate;
            QualitySettings.vSyncCount = vSyncCount;
        }
    }
}
