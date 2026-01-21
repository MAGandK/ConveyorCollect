using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Storage
{
    public class StorageService : IStorageService
    {
        private readonly Dictionary<string, IStorageData> _dataMap;

        public StorageService(IEnumerable<IStorageData> data)
        {
            _dataMap = data.ToDictionary(x => x.Key, x => x);
        }

        public void Initialize()
        {
            foreach (var (_, data) in _dataMap)
            {
                data.Changed += StorageDataOnChanged;

                if (!PlayerPrefs.HasKey(data.Key))
                {
                    continue;
                }

                var json = PlayerPrefs.GetString(data.Key);
                var deserializeStorageData =
                    (IStorageData)JsonConvert.DeserializeObject(json, data.GetType());

                data.Load(deserializeStorageData);
            }
        }

        public T GetData<T>(string key) where T : class, IStorageData
        {
            _dataMap.TryGetValue(key, out var data);
            return data as T;
        }

        private void StorageDataOnChanged(string dataKey)
        {
            if (!_dataMap.TryGetValue(dataKey, out var data))
            {
                return;
            }

            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(dataKey, json);
        }
    }
}