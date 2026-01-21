using Newtonsoft.Json;

namespace Storage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LevelProgressStorageData : AbstractStorageData<LevelProgressStorageData>
    {
        [JsonProperty("levelIndex")] private int _levelIndex = 0;

        public int LevelIndex => _levelIndex;

        public LevelProgressStorageData(string key) : base(key)
        {
        }

        public override void Load(LevelProgressStorageData data)
        {
            _levelIndex = data._levelIndex;
        }

        public void IncrementLevelIndex()
        {
            _levelIndex++;

            OnChanged();
        }

        public void DecrementLevelIndex()
        {
            _levelIndex--;

            OnChanged();
        }
    }
}
