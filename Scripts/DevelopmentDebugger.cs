using UnityEngine;

namespace ServiceLocatorPattern
{
    public class DevelopmentDebugger : IDebuggerService
    {
        public int messageCount;

        public void DebugMessage(string message)
        {
            Debug.Log(message);
            messageCount++;
        }
    }
}