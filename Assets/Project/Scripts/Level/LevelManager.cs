using System.Collections.Generic;
using Game.Path;
using UnityEngine;

namespace Level
{
    public sealed class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _levelPrefabs;
        [SerializeField] private Transform _spawnPoint;
        private GameObject _currentLevel;
        private int _currentIndex;

        private void Start()
        {
            LoadLevel(_currentIndex);
        }

        private void LoadLevel(int index)
        {
            _currentLevel = Instantiate(_levelPrefabs[index], _spawnPoint.position, Quaternion.identity);
        }

        public void LoadNextLevel()
        {
            if (_currentLevel != null)
            {
                Destroy(_currentLevel);
            }

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