namespace Pirac
{
    public class PropertyChangedData
    {
        public PropertyChangedData(string propertyName, object before, object after)
        {
            PropertyName = propertyName;
            Before = before;
            After = after;
        }

        public string PropertyName { get; }
        public object Before { get; }
        public object After { get; }
    }

    public class PropertyChangedData<TProperty>
    {
        public PropertyChangedData(string propertyName, TProperty before, TProperty after)
        {
            PropertyName = propertyName;
            Before = before;
            After = after;
        }

        public string PropertyName { get; }
        public TProperty Before { get; }
        public TProperty After { get; }

        public static implicit operator PropertyChangedData(PropertyChangedData<TProperty> data)
            => new PropertyChangedData(data.PropertyName, data.Before, data.After);

        public static explicit operator PropertyChangedData<TProperty>(PropertyChangedData data)
            => new PropertyChangedData<TProperty>(data.PropertyName, (TProperty)data.Before, (TProperty)data.After);
    }
}