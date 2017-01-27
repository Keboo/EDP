namespace EDP
{
    public interface IDataObject
    {
        bool TryGet<T>(string propertyName, out T value);
        T Get<T>(string propertyName);
        void Set<T>(string propertyName, T value);
    }
}