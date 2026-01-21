using System;
using UI.Other;
using UI.UIControllers;
using UI.WindowsLogic.FailPopup;
using UI.WindowsLogic.GameWindow;
using UI.WindowsLogic.PausePopup;
using UI.WindowsLogic.Timer;
using UI.WindowsLogic.WinPopup;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TimerController _timerController;
        [SerializeField] private UiInitializer _uiInitializer;
        public event Action GameStarted;
        public event Action GameFinished;
        public event Action GameRestarted;

        public event Action GamePause;

        private IUIController _uiController;
        private WinPopupController _winPopup;
        private GameWindowController _gameWindow;
        private PausePopupController _pausePopup;
        private FailPopupController _failPopup;

        private void Awake()
        {
            _uiController = _uiInitializer.UIController;
        }
        private void Start()
        {
            StartGame();
        }

        private void OnEnable()
        {
            _uiController = _uiInitializer.UIController;

            _winPopup    = _uiInitializer.WinPopupController;
            _gameWindow   = _uiInitializer.GameWindowController;
            _pausePopup   = _uiInitializer.PausePopupController;
            _failPopup   = _uiInitializer.FailPopupController;

            _winPopup.Won += WinPopupOnWon;
            _timerController.TimerEnded += UITimerOnTimerEnded;
            _pausePopup.Restarted += RestartGame;
            _pausePopup.Closed += PausePopupOnClosed;
            _failPopup.RestartClicked += FailPopupButtonClicked;
        }

        private void OnDisable()
        {
            _winPopup.Won -= WinPopupOnWon;
            _timerController.TimerEnded -= UITimerOnTimerEnded;
            _pausePopup.Restarted -= RestartGame;
            _pausePopup.Closed -= PausePopupOnClosed;
            _failPopup.RestartClicked -= FailPopupButtonClicked;
        }

        private void PausePopupOnClosed()
        {

        }

        private void WinPopupOnWon()
        {
            FinishGame();
        }

        private void StartGame()
        {
            _uiController.ShowWindow<GameWindowController>();
            GameStarted?.Invoke();
        }

        private void FinishGame()
        {
            _uiController.ShowWindow<WinPopupController>();
            GameFinished?.Invoke();
        }

        private void RestartGame()
        {
            _timerController.ResetTimer();
            _timerController.StartTimer();
            _uiController.ShowWindow<GameWindowController>();
            GameRestarted?.Invoke();
            StartGame();
            Time.timeScale = 1f;
        }

        private void UITimerOnTimerEnded()
        {
            _uiController.ShowWindow<FailPopupController>();
            Time.timeScale = 0f;
        }

        private void FailPopupButtonClicked()
        {
            RestartGame();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FinishGame();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                RestartGame();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                StartGame();
            }
        }
#endif
    }
}
