using System;
using System.Reactive.Linq;

namespace Pirac
{
    public static class ReactiveExtensions
    {
        public static IObservable<TSource> ObserveOnUI<TSource>(this IObservable<TSource> source) => source.ObserveOn(PiracRunner.UIScheduler);

        public static IObservable<TSource> ObserveOnBackground<TSource>(this IObservable<TSource> source) => source.ObserveOn(PiracRunner.BackgroundScheduler);

        public static IObservable<TSource> SubscribeOnUI<TSource>(this IObservable<TSource> source) => source.SubscribeOn(PiracRunner.UIScheduler);

        public static IObservable<TSource> SubscribeOnBackground<TSource>(this IObservable<TSource> source) => source.SubscribeOn(PiracRunner.BackgroundScheduler);
    }
}