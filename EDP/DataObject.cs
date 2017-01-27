using System;
using System.Collections.Generic;

namespace EDP
{
    public class DataObject : IDataObject
    {
        private readonly Dictionary<string, IProperty> _properties = new Dictionary<string, IProperty>();

        public bool TryGet<T>(string propertyName, out T value)
        {
            value = default(T);
            IProperty property;
            if (_properties.TryGetValue(propertyName, out property))
            {
                value = property.GetValue<T>();
                return true;
            }
            return false;
        }

        public T Get<T>(string propertyName)
        {
            IProperty property = _properties[propertyName];
            return property.GetValue<T>();
        }

        public void Set<T>(string propertyName, T value)
        {
            _properties[propertyName] = new Property<T>(value);
        }

        private interface IProperty
        {
            T GetValue<T>();
        }

        private class Property<T> : IProperty
        {
            private readonly T _value;

            public Property(T value)
            {
                _value = value;
            }

            public TValue GetValue<TValue>()
            {
                if (_value is TValue)
                {
                    //TODO: Boxing.... yuck
                    return (TValue)(object)_value;
                }
                return (TValue)Convert.ChangeType(_value, typeof(TValue));
            }
        }
    }
}
