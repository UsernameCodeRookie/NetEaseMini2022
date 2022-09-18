using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Debugger
{
    public static class TempDebug
    {
        [Conditional("ENABLE_DEBUG_LOG")]
        public static void Log(string content)
        {
            Debug.Log(content);
        }
    }
}
