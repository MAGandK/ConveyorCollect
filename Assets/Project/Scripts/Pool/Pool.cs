using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pool
{
    public class Pool : IPool
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<PoolData, List<IPoolObject>> _poolObjects = new();
        private readonly Dictionary<PoolData, Transform> _poolObjectsParents = new();
        private readonly Transform _poolParent;

        public Pool(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _poolParent = _diContainer.CreateEmptyGameObject("Pool").transform;
        }

        public T PreparePool<T>(PoolData key, int count) where T : class, IPoolObject
        {
            var poolObjects = new List<IPoolObject>();

            SetParents(key);

            for (int i = 0; i < count; i++)
            {
                CreateNewObject<T>(key, poolObjects);
            }

            if (!_poolObjects.TryAdd(key, poolObjects))
            {
                _poolObjects[key].AddRange(poolObjects);
            }

            return GetFreeObjectFromPool<T>(poolObjects);
        }

        public List<IPoolObject> GetPool()
        {
            var allObject = new List<IPoolObject>();

            foreach (var pool in _poolObjects.Values)
            {
                allObject.AddRange(pool);
            }

            return allObject;
        }

        public T Get<T>(PoolData key) where T : class, IPoolObject
        {
            if (_poolObjects.TryGetValue(key, out var poolObjects))
            {
                var freeObject = GetFreeObjectFromPool<T>(poolObjects);

                if (freeObject != null)
                {
                    return freeObject;
                }

                return CreateNewObject<T>(key, poolObjects);
            }

            SetParents(key);

            var parent = _poolObjectsParents[key];
            var firstObject = _diContainer.InstantiatePrefabForComponent<T>(key.Obj, parent.transform);
            var objects = new List<IPoolObject>();

            objects.Add(firstObject);

            _poolObjects.Add(key, objects);

            firstObject.SetIsFree(false);

            return firstObject;
        }


        public void FreeAllPool()
        {
            foreach (var ( key, poolObjects) in _poolObjects)
            {
                foreach (var poolObject in poolObjects)
                {
                    poolObject.SetIsFree(true);
                }
            }
        }

        public void FreePool(PoolData data)
        {
            if (_poolObjects.ContainsKey(data))
            {
                _poolObjects[data].Clear();
                _poolObjectsParents.Remove(data);
            }
        }

        private T CreateNewObject<T>(PoolData key, List<IPoolObject> poolObjects) where T : class, IPoolObject
        {
            var newObject = _diContainer.InstantiatePrefabForComponent<T>(key.Obj, _poolObjectsParents[key]);
            poolObjects.Add(newObject);
            newObject.SetIsFree(false);
            return newObject;
        }

        private T GetFreeObjectFromPool<T>(List<IPoolObject> poolObjects) where T : class, IPoolObject
        {
            foreach (var poolObject in poolObjects)
            {
                if (poolObject.IsFree)
                {
                    poolObject.SetIsFree(false);
                    return (T)poolObject;
                }
            }

            return null;
        }

        private void SetParents(PoolData key)
        {
            if (!_poolObjectsParents.ContainsKey(key))
            {
                var poolObjectParent = _diContainer.CreateEmptyGameObject(key.Key).transform;
                poolObjectParent.transform.SetParent(_poolParent);
                _poolObjectsParents.Add(key, poolObjectParent.transform);
            }
        }
    }
}
