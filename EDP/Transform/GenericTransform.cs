using System;

namespace EDP.Transform
{
    public class GenericTransform : ITransform
    {
        private readonly Action<DataDescription, IDataObject> _transformDelegate;

        public GenericTransform(Action<DataDescription, IDataObject> transformDelegate)
        {
            if (transformDelegate == null) throw new ArgumentNullException(nameof(transformDelegate));
            _transformDelegate = transformDelegate;
        }

        public void TransformData(DataDescription description, IDataObject item)
        {
            _transformDelegate(description, item);
        }
    }
}