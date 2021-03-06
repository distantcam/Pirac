﻿using System;
using System.Collections.ObjectModel;

namespace Pirac
{
    public static partial class PublicExtensions
    {
        public static ObservableCollection<T> ToCollection<T>(this IObservable<T> source)
        {
            var collection = new ObservableCollection<T>();
            source.Subscribe(t => collection.Add(t));
            return collection;
        }
    }
}