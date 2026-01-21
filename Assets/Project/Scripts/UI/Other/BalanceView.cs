using System.Collections.Generic;
using Constants;
using Storage;
using Storage.Wallet;
using UnityEngine;
using Zenject;

namespace UI.Other
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private List<CurrencyView> _views;
        private WalletStorageData _walletStorageData;

        [Inject]
        private void Construct(IStorageService storageService)
        {
            _walletStorageData = storageService.GetData<WalletStorageData>(StorageDataNames.WALLET_STORAGE_DATA_KEY);
            _walletStorageData.Changed += WalletStorageDataOnChanged;
        }

        public void Setup(params CurrencyType[] types)
        {
            foreach (var currencyView in _views)
            {
                currencyView.SetActive(false);

                foreach (var currencyType in types)
                {
                    if (currencyView.Type != currencyType)
                    {
                        continue;
                    }

                    currencyView.SetActive(true);
                    currencyView.Refresh(_walletStorageData.GetBalance(currencyView.Type));
                }
            }
        }

        private void Refresh()
        {
            foreach (var currencyView in _views)
            {
                if (currencyView.IsActive)
                {
                    currencyView.Refresh(_walletStorageData.GetBalance(currencyView.Type));
                }
            }
        }

        private void WalletStorageDataOnChanged(string key)
        {
            Refresh();
        }
    }
}
