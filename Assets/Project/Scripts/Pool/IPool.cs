using System.Collections.Generic;

namespace Pool
{
    public interface IPool
    {
        public T PreparePool<T>(PoolData poolData, int count) where T : class, IPoolObject;
        public List<IPoolObject> GetPool();
        public T Get<T>(PoolData key) where T : class, IPoolObject;
        public void FreeAllPool();
        public void FreePool(PoolData data);
    }
}
