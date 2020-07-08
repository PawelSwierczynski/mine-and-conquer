using UnityEngine;
using UnityEngine.UI;

namespace Assets.Utility
{
    public class DebugLogger : MonoBehaviour
    {
        public static DebugLogger Instance { get; private set; }

        public Text text;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void AddMessage(string log)
        {
            text.text += log + "\n";
        }
    }
}