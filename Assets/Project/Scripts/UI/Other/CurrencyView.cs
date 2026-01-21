using Storage.Wallet;
using TMPro;
using UnityEngine;

namespace UI.Other
{
    public class CurrencyView : MonoBehaviour
    {
        [SerializeField] private CurrencyType _type;
        [SerializeField] private TMP_Text _value;

        public bool IsActive { get; private set; }
        public CurrencyType Type => _type;

        public void Refresh(int value)
        {
            _value.SetText(value.ToString());
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            gameObject.SetActive(isActive);
        }
    }
}
