using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.WindowsLogic.PausePopup
{
    public class PausePopupView : AbstractWindowView
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restartButton;
        public Button BackButton => _backButton;
        public Button RestartButton => _backButton;

        public void SubscribeButton(UnityAction onCloseButtonClick, UnityAction onRestartButtonClick)
        {
            _backButton.onClick.AddListener(onCloseButtonClick);
            _restartButton.onClick.AddListener(onRestartButtonClick);
        }
    }
}
