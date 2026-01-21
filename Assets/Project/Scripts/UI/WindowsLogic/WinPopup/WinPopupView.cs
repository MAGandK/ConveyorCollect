using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.WindowsLogic.WinPopup
{
    public class WinPopupView: AbstractWindowView
    {
        [SerializeField] private Button _nextButton;

        public void SubscribeButton(UnityAction onContinueButtonClick)
        {
            _nextButton.onClick.AddListener(onContinueButtonClick);
        }
    }
}
