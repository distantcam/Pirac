using System.Reactive.Concurrency;

namespace Pirac
{
    public static class SchedulerProvider
    {
        public static IScheduler UIScheduler => PiracRunner.UIScheduler;
        public static IScheduler BackgroundScheduler => PiracRunner.BackgroundScheduler;
    }
}