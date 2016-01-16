namespace Pirac
{
    public class PropertyChangingData
    {
        public PropertyChangingData(string propertyName, object before)
        {
            PropertyName = propertyName;
            Before = before;
        }

        public string PropertyName { get; }
        public object Before { get; }
    }

    public class PropertyChangingData<TProperty>
    {
        public PropertyChangingData(string propertyName, TProperty before)
        {
            PropertyName = propertyName;
            Before = before;
        }

        public string PropertyName { get; }
        public TProperty Before { get; }

        public static implicit operator PropertyChangingData(PropertyChangingData<TProperty> data) => new PropertyChangingData(data.PropertyName, data.Before);

        public static explicit operator PropertyChangingData<TProperty>(PropertyChangingData data) => new PropertyChangingData<TProperty>(data.PropertyName, (TProperty)data.Before);
    }
}