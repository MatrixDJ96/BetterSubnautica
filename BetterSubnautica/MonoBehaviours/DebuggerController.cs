using BetterSubnautica.Utility;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public class DebuggerController : MonoBehaviour
    {
        private static DebuggerController instance;
        public static DebuggerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(DebuggerController).Name).AddComponent<DebuggerController>();
                }
                return instance;
            }
        }

        public struct Message
        {
            public bool Prefix;
            public string Text;
        }

        private IDictionary<string, Message> dictionary;
        public IDictionary<string, Message> Dictionary
        {
            get
            {
                if (dictionary == null)
                {
                    dictionary = new SortedDictionary<string, Message>();
                }
                return dictionary;
            }
        }

        private IList<Message> list;
        public IList<Message> List
        {
            get
            {
                if (list == null)
                {
                    list = new List<Message>();
                }
                return list;
            }
        }

        public GUIStyle Style { get; set; }
        public Rect Position { get; set; }

        public string Text
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var item in Dictionary)
                {
                    sb.AppendLine(DebuggerUtility.GenerateString(item.Value.Text, item.Key, item.Value.Prefix));
                }
                if (Dictionary.Count > 0)
                {
                    sb.AppendLine();
                }
                foreach (var item in List)
                {
                    sb.AppendLine(DebuggerUtility.GenerateString(item.Text, null, item.Prefix));
                }
                return sb.ToString();
            }
        }

        public int MinimumFontSize { get; } = 9;

        protected void Awake()
        {
            Style = new GUIStyle
            {
                clipping = TextClipping.Overflow
            };

            Style.normal.textColor = Color.white;

            Position = new Rect(0, 0, 0, 0);
        }

        protected void Update()
        {
            Style.fontSize = (int)(MinimumFontSize * (Screen.currentResolution.width / 1920f) + MinimumFontSize * (Screen.currentResolution.height / 1080f));

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                ClearMessages();
            }
        }

        protected void OnGUI()
        {
            GUI.Label(Position, Text, Style);
        }

        public void ClearMessages()
        {
            Dictionary.Clear();
            List.Clear();
        }

        public void AddMessage(Message message, string key = null)
        {
            if (key != null)
            {
                Dictionary[key] = message;
            }
            else
            {
                List.Add(message);
            }
        }
        public void RemoveMessage(string key)
        {
            if (Dictionary.ContainsKey(key))
            {
                Dictionary.Remove(key);
            }
        }
    }
}
