using System;
using Level;
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
        [SerializeField] private LevelManager _levelManager;

        public event Action GameStarted;
        public event Action GameFinished;
        public event Action GameRestarted;
        public event Action GamePause;

        private IUIController _uiController;
        private WinPopupController _winPopup;
        private GameWindowController _gameWindow;
        private PausePopupController _pausePopup;
        private FailPopupController _failPopup;

        private bool _isLevelWon;

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

            _winPopup = _uiInitializer.WinPopupController;
            _gameWindow = _uiInitializer.GameWindowController;
            _pausePopup = _uiInitializer.PausePopupController;
            _failPopup = _uiInitializer.FailPopupController;

            _winPopup.Won += WinPopupOnWon;
            _timerController.TimerEnded += UITimerOnTimerEnded;
            _pausePopup.Restarted += RestartGame;
            _pausePopup.Closed += PausePopupOnClosed;
            _failPopup.RestartClicked += FailPopupButtonClicked;
            _gameWindow.PauseClicked += PauseGame;
            _gameWindow.RestartClicked += RestartGame;
        }

        private void OnDisable()
        {
            _winPopup.Won -= WinPopupOnWon;
            _timerController.TimerEnded -= UITimerOnTimerEnded;
            _pausePopup.Restarted -= RestartGame;
            _pausePopup.Closed -= PausePopupOnClosed;
            _failPopup.RestartClicked -= FailPopupButtonClicked;
            _gameWindow.PauseClicked -= PauseGame;
            _gameWindow.RestartClicked -= RestartGame;
        }

        private void PausePopupOnClosed()
        {
            _timerController.Continue();
        }

        public void OnLevelWon()
        {
            if (_isLevelWon)
            {
                return;
            }

            _isLevelWon = true;

            _timerController.PauseTimer();

            FinishGame();

            Time.timeScale = 0f;
        }

        private void WinPopupOnWon()
        {
            Time.timeScale = 1f;
            _isLevelWon = false;

            _levelManager.LoadNextLevel();

            _timerController.ResetTimer();
            _timerController.StartTimer();

            _uiController.ShowWindow<GameWindowController>();

            GameRestarted?.Invoke();
        }

        private void StartGame()
        {
            Time.timeScale = 1f;

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
            Time.timeScale = 1f;
            _isLevelWon = false;

            _levelManager.RestartCurrentLevel();

            _timerController.ResetTimer();
            _timerController.StartTimer();

            _uiController.ShowWindow<GameWindowController>();

            GameRestarted?.Invoke();
        }

        private void UITimerOnTimerEnded()
        {
            if (_isLevelWon)
            {
                return;
            }

            _timerController.PauseTimer();

            _uiController.ShowWindow<FailPopupController>();

            Time.timeScale = 0f;
        }

        private void FailPopupButtonClicked()
        {
            RestartGame();
        }
        
        public void PauseGame()
        {
            if (_isLevelWon)
            {
                return;
            }

            _timerController.PauseTimer();
            _uiController.ShowWindow<PausePopupController>();
            GamePause?.Invoke();
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