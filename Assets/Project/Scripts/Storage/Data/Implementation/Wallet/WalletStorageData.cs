using System.Collections.Generic;
using Newtonsoft.Json;

namespace Storage.Wallet
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WalletStorageData : AbstractStorageData<WalletStorageData>
    {
        [JsonProperty] private List<WalletItem> _balance = new List<WalletItem>();

        public List<WalletItem> Balance => _balance;

        public WalletStorageData(string key) : base(key)
        {
        }

        public override void Load(WalletStorageData data)
        {
            _balance = data._balance;
        }

        public void AddCurrency(CurrencyType type, int value)
        {
            foreach (var walletItem in _balance)
            {
                if (walletItem.Type == type)
                {
                    walletItem.SetValue(walletItem.Value + value);
                    OnChanged();
                    return;
                }
            }

            _balance.Add(new WalletItem(type, value));
            OnChanged();
        }

        public bool CanPurchase(CurrencyType type, int value)
        {
            return  GetBalance(type)>= value;
        }

        public int GetBalance(CurrencyType currencyType)
        {
            foreach (var walletItem in _balance)
            {
                if (walletItem.Type == currencyType)
                {
                    return walletItem.Value;
                }
            }

            return 0;
        }
    }
}
