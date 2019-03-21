using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Advent.UI
{
    public class EventLogs : MonoBehaviour
    {
        public Text logText;
        public int maxLines = 10;
        private List<string> eventLog = new List<string>();
        private string guiText = "";

        public void AddEvent(string eventString)
        {
            eventLog.Add(eventString);

            if(eventLog.Count >= maxLines)
            {
                eventLog.RemoveAt(0);
            }

            guiText = "";

            foreach (string logEvent in eventLog)
            {
                guiText += logEvent;
                guiText += "\n";
            }
            logText.text = guiText;
        }
    }
}
