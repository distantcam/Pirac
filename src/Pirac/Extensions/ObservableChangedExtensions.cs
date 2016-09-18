using System;
using System.Linq;
using System.Reactive.Linq;

namespace Pirac
{
    public static partial class PublicExtensions
    {
        public static IObservable<PropertyChangedData> WhenPropertyChanged(this IObservablePropertyChanged changed, string propertyName)
            => changed.Changed.Where(p => p.PropertyName == propertyName);

        public static IObservable<PropertyChangedData<TProperty>> WhenPropertyChanged<TProperty>(this IObservablePropertyChanged changed, string propertyName)
            => changed.Changed.Where(p => p.PropertyName == propertyName).Select(data => (PropertyChangedData<TProperty>)data);

        public static IObservable<PropertyChangedData> WhenPropertiesChanged(this IObservablePropertyChanged changed, params string[] propertyNames)
            => changed.Changed.Where(p => propertyNames.Contains(p.PropertyName));

        public static IObservable<PropertyChangedData<TProperty>> WhenPropertiesChanged<TProperty>(this IObservablePropertyChanged changed, params string[] propertyNames)
            => changed.Changed.Where(p => propertyNames.Contains(p.PropertyName)).Select(data => (PropertyChangedData<TProperty>)data);

        public static IObservable<PropertyChangingData> WhenPropertyChanging(this IObservablePropertyChanging changing, string propertyName)
            => changing.Changing.Where(p => p.PropertyName == propertyName);

        public static IObservable<PropertyChangingData<TProperty>> WhenPropertyChanging<TProperty>(this IObservablePropertyChanging changing, string propertyName)
            => changing.Changing.Where(p => p.PropertyName == propertyName).Select(data => (PropertyChangingData<TProperty>)data);

        public static IObservable<PropertyChangingData> WhenPropertiesChanging(this IObservablePropertyChanging changing, params string[] propertyNames)
            => changing.Changing.Where(p => propertyNames.Contains(p.PropertyName));

        public static IObservable<PropertyChangingData<TProperty>> WhenPropertiesChanging<TProperty>(this IObservablePropertyChanging changing, params string[] propertyNames)
            => changing.Changing.Where(p => propertyNames.Contains(p.PropertyName)).Select(data => (PropertyChangingData<TProperty>)data);

        public static IObservable<PropertyChangedData<TProperty>> CastPropertyType<TProperty>(this IObservable<PropertyChangedData> observable)
            => observable.Select(data => (PropertyChangedData<TProperty>)data);

        public static IObservable<PropertyChangingData<TProperty>> CastPropertyType<TProperty>(this IObservable<PropertyChangingData> observable)
            => observable.Select(data => (PropertyChangingData<TProperty>)data);
    }
}