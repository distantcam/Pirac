using System;
using System.Linq;
using System.Reactive.Linq;

namespace Pirac
{
    public static class ObservableChangedExtensions
    {
        public static IObservable<PropertyChangedData> ChangedProperty(this IObservablePropertyChanged changed, string propertyName)
            => changed.Changed.Where(p => p.PropertyName == propertyName);

        public static IObservable<PropertyChangedData<TProperty>> ChangedProperty<TProperty>(this IObservablePropertyChanged changed, string propertyName)
            => changed.Changed.Where(p => p.PropertyName == propertyName).Select(data => (PropertyChangedData<TProperty>)data);

        public static IObservable<PropertyChangedData> ChangedProperties(this IObservablePropertyChanged changed, params string[] propertyNames)
            => changed.Changed.Where(p => propertyNames.Contains(p.PropertyName));

        public static IObservable<PropertyChangedData<TProperty>> ChangedProperties<TProperty>(this IObservablePropertyChanged changed, params string[] propertyNames)
            => changed.Changed.Where(p => propertyNames.Contains(p.PropertyName)).Select(data => (PropertyChangedData<TProperty>)data);

        public static IObservable<PropertyChangingData> ChangingProperty(this IObservablePropertyChanging changing, string propertyName)
            => changing.Changing.Where(p => p.PropertyName == propertyName);

        public static IObservable<PropertyChangingData<TProperty>> ChangingProperty<TProperty>(this IObservablePropertyChanging changing, string propertyName)
            => changing.Changing.Where(p => p.PropertyName == propertyName).Select(data => (PropertyChangingData<TProperty>)data);

        public static IObservable<PropertyChangingData> ChangingProperties(this IObservablePropertyChanging changing, params string[] propertyNames)
            => changing.Changing.Where(p => propertyNames.Contains(p.PropertyName));

        public static IObservable<PropertyChangingData<TProperty>> ChangingProperties<TProperty>(this IObservablePropertyChanging changing, params string[] propertyNames)
            => changing.Changing.Where(p => propertyNames.Contains(p.PropertyName)).Select(data => (PropertyChangingData<TProperty>)data);

        public static IObservable<PropertyChangedData<TProperty>> CastPropertyType<TProperty>(this IObservable<PropertyChangedData> observable)
            => observable.Select(data => (PropertyChangedData<TProperty>)data);

        public static IObservable<PropertyChangingData<TProperty>> CastPropertyType<TProperty>(this IObservable<PropertyChangingData> observable)
            => observable.Select(data => (PropertyChangingData<TProperty>)data);
    }
}