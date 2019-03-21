using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Advent.UI
{
    public class EventLogs : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        public int maxLines = 10;
        private List<string> eventLog = new List<string>();
        private string guiLogText = "";

        public void AddEvent(string eventString)
        {
            eventLog.Add(eventString);

            if(eventLog.Count >= maxLines)
            {
                eventLog.RemoveAt(0);
            }

            guiLogText = "";

            foreach (string logEvent in eventLog)
            {
                guiLogText += logEvent;
                guiLogText += "\n";
            }
            textMesh.text = guiLogText;
        }
    }
}
