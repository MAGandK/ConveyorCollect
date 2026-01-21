using UnityEngine;

namespace Pool
{
    public struct PoolData
    {
        public readonly MonoBehaviour Obj;
        public readonly string Key;

        public PoolData(MonoBehaviour obj, string key)
        {
            Obj = obj;
            Key = key;
        }

        public override bool Equals(object objT)
        {
            if (objT is PoolData otherData)
            {
                return Obj == otherData.Obj && Key == otherData.Key;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (obj: Obj, key: Key).GetHashCode();
        }
    }
}
