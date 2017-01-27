using System;

namespace EDP
{
    public static class DataMixins
    {
        public static void Transform<TIn, TOut>(this IDataObject dataObject, string propertyName, Func<TIn, TOut> transform)
        {
            if (dataObject == null) throw new ArgumentNullException(nameof(dataObject));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (transform == null) throw new ArgumentNullException(nameof(transform));

            dataObject.Set(propertyName, transform(dataObject.Get<TIn>(propertyName)));
        }
    }
}