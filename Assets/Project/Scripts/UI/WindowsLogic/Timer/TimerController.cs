using System;
using Game.Managers;
using UnityEngine;

namespace UI.WindowsLogic.Timer
{
    public class TimerController: MonoBehaviour
    {
        public event Action TimerEnded;

        [SerializeField] private float _initialTime;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private GameManager _gameManager;

        private float _currentTime;
        private bool _isRunning;

        private void OnEnable()
        {
            _gameManager.GameStarted += StartTimer;
        }

        private void OnDisable()
        {
            _gameManager.GameStarted -= StartTimer;
        }

        public void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0f)
            {
                _currentTime = 0f;
                _isRunning = false;
                TimerEnded?.Invoke();
            }

            _timerView.UpdateTimerText(_currentTime);
        }

        public void StartTimer()
        {
            _currentTime = _initialTime;
            _isRunning = true;
            _timerView.UpdateTimerText(_currentTime);
        }

        public void PauseTimer()
        {
            _isRunning = false;
        }

        public void Continue()
        {
            _isRunning = true;
        }

        public void ResetTimer()
        {
            _currentTime = _initialTime;
            _timerView.UpdateTimerText(_currentTime);
        }
    }
}
