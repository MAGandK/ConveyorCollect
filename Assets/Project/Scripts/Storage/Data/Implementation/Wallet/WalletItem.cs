using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Storage.Wallet
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletItem
    {
        [SerializeField] [JsonProperty("type")]
        private CurrencyType _type;

        [SerializeField] [JsonProperty("value")]
        private int _value;

        public CurrencyType Type => _type;
        public int Value => _value;

        public WalletItem(CurrencyType type, int value)
        {
            _type = type;
            _value = value;
        }

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}
