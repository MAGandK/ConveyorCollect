using System;
using Newtonsoft.Json;

namespace Storage
{
    public abstract class AbstractStorageData<T> : IStorageData<T>
    {
        public event Action<string> Changed;
       [JsonProperty] public string Key { get; }

        protected AbstractStorageData(string key)
        {
            Key = key;
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(Key);
        }

        public abstract void Load(T data);
    }
}