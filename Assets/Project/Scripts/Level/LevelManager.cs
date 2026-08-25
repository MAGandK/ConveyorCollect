using System.Collections.Generic;
using Game.ColorObjects;
using Game.ColorStack;
using Game.Path;
using UnityEngine;

namespace Level
{
    public sealed class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _levelPrefabs;
        [SerializeField] private Transform _spawnPoint;
        private GameObject _currentLevel;
        public int _currentIndex;
        public int CurrentLevel => _currentIndex + 1;

        private void Start()
        {
            LoadLevel(_currentIndex);
        }

        private void LoadLevel(int index)
        {
            _currentLevel = Instantiate(_levelPrefabs[index], _spawnPoint.position, Quaternion.identity);
        }

        public void StopActiveTweens()
        {
            if (_currentLevel == null)
            {
                return;
            }

            foreach (var colorObject in _currentLevel.GetComponentsInChildren<ColorObject>(true))
            {
                colorObject.StopJump();
            }

            foreach (var stack in _currentLevel.GetComponentsInChildren<ColorObjectStack>(true))
            {
                stack.KillPendingTweens();
            }

            var pathMover = _currentLevel.GetComponentInChildren<PathMover>(true);
            pathMover?.StopJumps();
        }

        public void LoadNextLevel()
        {
            DestroyCurrentLevel();

            _currentIndex++;
            if (_currentIndex >= _levelPrefabs.Count)
            {
                _currentIndex = 0;
            }
            
            LoadLevel(_currentIndex);
        }
        
        public void RestartCurrentLevel()
        {
            DestroyCurrentLevel();
            LoadLevel(_currentIndex);
        }
        
        private void DestroyCurrentLevel()
        {
            if (_currentLevel == null)
            {
                return;
            }

            StopActiveTweens();

            var pathMover = _currentLevel.GetComponentInChildren<PathMover>();

            if (pathMover != null)
            {
                pathMover.Reset();
            }

            Destroy(_currentLevel);
            _currentLevel = null;
        }
    }
}