using System;
using System.Collections;
using UnityEngine;

namespace BetterHUD.MonoBehaviours
{
    public class TimeDisplayController : MonoBehaviour
    {
        private GUIStyle style;
        public GUIStyle Style
        {
            get
            {
                if (style.fontSize != Core.Settings.TimeFontSize)
                {
                    style.fontSize = Core.Settings.TimeFontSize;
                }
                if (style.fontStyle != (FontStyle)Core.Settings.TimeFontStyle)
                {
                    style.fontStyle = (FontStyle)Core.Settings.TimeFontStyle;
                }
                return style;
            }
            private set => style = value;
        }

        private Rect position;
        public Rect Position
        {
            get
            {
                if (position.x != Core.Settings.TimePositionX)
                {
                    position.x = Core.Settings.TimePositionX;
                }
                if (position.y != Core.Settings.TimePositionY)
                {
                    position.y = Core.Settings.TimePositionY;
                }
                return position;
            }
            private set => position = value;
        }

        public string Text
        {
            get
            {
                float dayScalar = DayNightCycle.main.GetDayScalar();
                int hours = Mathf.FloorToInt(dayScalar * 24f);
                int minutes = Mathf.FloorToInt(Mathf.Repeat(dayScalar * 24f * 60f, 60f));

                return new TimeSpan(hours, minutes, 0).ToString(@"hh\:mm");
            }
        }

        private bool started = false;

        protected void Awake()
        {
            StartCoroutine(AsyncAwake());
        }

        protected IEnumerator AsyncAwake()
        {
            while (
                LightmappedPrefabs.main == null || LightmappedPrefabs.main.IsWaitingOnLoads() ||
                uGUI.main == null || uGUI.main.loading == null || uGUI.main.loading.IsLoading ||
                PAXTerrainController.main == null || PAXTerrainController.main.isWorking ||
                HandReticle.main == null
                )
            {
                yield return null;
            }

            Style = new GUIStyle
            {
#if SUBNAUTICA_STABLE
                font = HandReticle.main.interactPrimaryText.font,
#else
                font = HandReticle.main.compTextHand.font.sourceFontFile,
#endif
                fontSize = Core.Settings.TimeFontSize,
                fontStyle = (FontStyle)Core.Settings.TimeFontStyle,
                clipping = TextClipping.Overflow
            };

            Style.normal.textColor = Color.white;

            Position = new Rect(Core.Settings.TimePositionX, Core.Settings.TimePositionY, 0, 0);

            started = true;
        }

        public void OnGUI()
        {
            if (started)
            {
                GUI.Label(Position, Text, Style);
            }
        }
    }
}
