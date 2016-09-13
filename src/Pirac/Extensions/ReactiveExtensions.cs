using System;
using System.Reactive.Linq;

namespace Pirac
{
    public static class ReactiveExtensions
    {
        public static IObservable<TSource> ObserveOnPiracMain<TSource>(this IObservable<TSource> source)
            => source.ObserveOn(PiracRunner.MainScheduler);

        public static IObservable<TSource> ObserveOnPiracBackground<TSource>(this IObservable<TSource> source)
            => source.ObserveOn(PiracRunner.BackgroundScheduler);

        public static IObservable<TSource> SubscribeOnPiracMain<TSource>(this IObservable<TSource> source)
            => source.SubscribeOn(PiracRunner.MainScheduler);

        public static IObservable<TSource> SubscribeOnPiracBackground<TSource>(this IObservable<TSource> source)
            => source.SubscribeOn(PiracRunner.BackgroundScheduler);
    }
}