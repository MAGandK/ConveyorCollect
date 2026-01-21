using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.WindowsLogic.GameWindow
{
    public class GameWindowView: AbstractWindowView
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _restartButton;

        public void SubscribeButton(UnityAction onPauseButtonClick, UnityAction onRestartButtonClick)
        {
            _pauseButton.onClick.AddListener(onPauseButtonClick);
            _restartButton.onClick.AddListener(onRestartButtonClick);
        }
    }
}
