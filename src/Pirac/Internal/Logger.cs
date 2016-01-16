using System.Diagnostics;

namespace Pirac.Internal
{
    public class Logger : ILogger
    {
        private string name;

        public Logger(string name)
        {
            this.name = name;
        }

        public void Error(string message)
        {
            Trace.WriteLine($"[ERROR] ({name}) {message}");
        }

        public void Warn(string message)
        {
            Trace.WriteLine($"[WARN ] ({name}) {message}");
        }

        public void Info(string message)
        {
            Trace.WriteLine($"[INFO ] ({name}) {message}");
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[DEBUG] ({name}) {message}");
        }
    }
}