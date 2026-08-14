using UnityEngine;

namespace Level
{
    [CreateAssetMenu(menuName = "Game/LevelConfig", fileName = "LevelConfig", order = 0)]
    public sealed class LevelConfig : ScriptableObject
    {
        public GameObject LevelPrefab;
    }
}