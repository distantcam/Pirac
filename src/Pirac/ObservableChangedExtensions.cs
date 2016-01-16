using System;
using System.Reactive.Linq;

namespace Pirac
{
    public static class ObservableChangedExtensions
    {
        public static IObservable<PropertyChangedData> ChangedProperty(this IObservablePropertyChanged changed, string propertyName)
        {
            return changed.Changed.Where(p => p.PropertyName == propertyName);
        }

        public static IObservable<PropertyChangedData<TProperty>> ChangedProperty<TProperty>(this IObservablePropertyChanged changed, string propertyName)
        {
            return changed.Changed.Where(p => p.PropertyName == propertyName).Select(data => (PropertyChangedData<TProperty>)data);
        }

        public static IObservable<PropertyChangingData> ChangingProperty(this IObservablePropertyChanging changing, string propertyName)
        {
            return changing.Changing.Where(p => p.PropertyName == propertyName);
        }

        public static IObservable<PropertyChangingData<TProperty>> ChangingProperty<TProperty>(this IObservablePropertyChanging changing, string propertyName)
        {
            return changing.Changing.Where(p => p.PropertyName == propertyName).Select(data => (PropertyChangingData<TProperty>)data);
        }

        public static IObservable<PropertyChangedData<TProperty>> CastPropertyType<TProperty>(this IObservable<PropertyChangedData> observable)
        {
            return observable.Select(data => (PropertyChangedData<TProperty>)data);
        }

        public static IObservable<PropertyChangingData<TProperty>> CastPropertyType<TProperty>(this IObservable<PropertyChangingData> observable)
        {
            return observable.Select(data => (PropertyChangingData<TProperty>)data);
        }
    }
}