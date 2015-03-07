using System.Diagnostics;

namespace Pirac.Internal
{
    internal class Logger : ILogger
    {
        public void Error(string message)
        {
            Trace.WriteLine("[ERROR] " + message);
        }

        public void Warn(string message)
        {
            Trace.WriteLine("[WARN ] " + message);
        }

        public void Info(string message)
        {
            Trace.WriteLine("[INFO ] " + message);
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine("[DEBUG] " + message);
        }
    }
}