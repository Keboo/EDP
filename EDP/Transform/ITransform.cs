namespace EDP.Transform
{
    public interface ITransform
    {
        void TransformData(DataDescription description, IDataObject item);
    }
}