using System;
using System.Diagnostics;

namespace KenCode
{
    public static class Log
    {
        // Add Timing to DebugLogs
        private static string WrapLogMessage(string message)
        {
            return $"[{DateTime.UtcNow}] {message}";
        }

        [Conditional("UNITY_EDITOR")]
        public static void DebugInfo(string message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.Log(WrapLogMessage(message), context);
        }

        [Conditional("UNITY_EDITOR")]
        public static void DebugWarning(string message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogWarning(WrapLogMessage(message), context);
        }

        [Conditional("UNITY_EDITOR")]
        public static void DebugError(string message, UnityEngine.Object context = null)
        {
            UnityEngine.Debug.LogError(WrapLogMessage(message), context);
        }
    }
}
