using System;

namespace Storage
{
    public interface IStorageData
    {
        event Action<string> Changed;
        string Key { get; }
        void Load(IStorageData data);
    }

    public interface IStorageData<in T> : IStorageData
    {
        void IStorageData.Load(IStorageData data)
        {
            if (data is T tData)
            {
                Load(tData);
            }
        }

        void Load(T data);
    }
}