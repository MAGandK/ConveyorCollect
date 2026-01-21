using Newtonsoft.Json;

namespace Storage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AudioStorageData : AbstractStorageData<AudioStorageData>
    {
        [JsonProperty("isSoundMute")] private bool _isSoundMute;
        [JsonProperty("isMusicMute")] private bool _isMusicMute;

        public bool IsSoundMuted => _isSoundMute;
        public bool IsMusicMuted => _isMusicMute;

        public AudioStorageData(string key) : base(key)
        {
        }

        public override void Load(AudioStorageData data)
        {
            _isMusicMute = data._isMusicMute;
            _isSoundMute = data._isSoundMute;
        }

        public void SetIsSoundMute(bool isMute)
        {
            _isSoundMute = isMute;

            OnChanged();
        }

        public void SetIsMusicMute(bool isMute)
        {
            _isMusicMute = isMute;

            OnChanged();
        }
    }
}
