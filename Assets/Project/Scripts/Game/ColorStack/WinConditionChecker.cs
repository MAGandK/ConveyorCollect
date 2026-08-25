using System.Collections;
using System.Linq;
using Game.Managers;
using UnityEngine;

namespace Game.ColorStack
{
    public class WinConditionChecker : MonoBehaviour
    {
        [SerializeField] private ColorObjectStack[] _colorObjectStacks;
        [SerializeField] private GameManager _gameManager;

        private bool _hasWon;
        private Coroutine _winRoutine;

        private void Awake()
        {
            if (_gameManager == null)
            {
                _gameManager = FindFirstObjectByType<GameManager>();
            }

            if (_colorObjectStacks == null || _colorObjectStacks.Length == 0)
            {
                _colorObjectStacks = GetComponentsInChildren<ColorObjectStack>(true)
                    .Where(stack => stack.GetComponent<ColorObjectStackTrigger>() != null)
                    .ToArray();
            }

            _colorObjectStacks = _colorObjectStacks
                .Where(stack => stack != null)
                .Distinct()
                .ToArray();

            foreach (var colorObjectStack in _colorObjectStacks)
            {
                colorObjectStack.FilledStack += ColorObjectStackOnFilledStack;
            }

            if (_gameManager != null)
            {
                _gameManager.GameRestarted += ResetWinState;
            }
        }

        private void OnDestroy()
        {
            foreach (var colorObjectStack in _colorObjectStacks)
            {
                if (colorObjectStack != null)
                {
                    colorObjectStack.FilledStack -= ColorObjectStackOnFilledStack;
                }
            }

            if (_gameManager != null)
            {
                _gameManager.GameRestarted -= ResetWinState;
            }
        }

        private void ResetWinState()
        {
            _hasWon = false;

            if (_winRoutine != null)
            {
                StopCoroutine(_winRoutine);
                _winRoutine = null;
            }

            foreach (var colorObjectStack in _colorObjectStacks)
            {
                colorObjectStack?.ResetWinProgress();
            }
        }

        private void ColorObjectStackOnFilledStack(ColorObjectStack _)
        {
            if (_hasWon || _winRoutine != null || _gameManager == null || _colorObjectStacks.Length == 0)
            {
                return;
            }

            if (!_colorObjectStacks.All(x => x.WasFilledThisSession && x.IsWinningStack()))
            {
                return;
            }

            _winRoutine = StartCoroutine(CompleteWinAfterJumps());
        }

        private IEnumerator CompleteWinAfterJumps()
        {
            yield return new WaitUntil(AreAllJumpsFinished);

            _winRoutine = null;

            if (_hasWon || _gameManager == null)
            {
                yield break;
            }

            if (!_colorObjectStacks.All(x => x.WasFilledThisSession && x.IsWinningStack()))
            {
                yield break;
            }

            _hasWon = true;
            _gameManager.OnLevelWon();
        }

        private bool AreAllJumpsFinished()
        {
            return _colorObjectStacks.All(stack => stack != null && !stack.HasJumpingObjects);
        }
    }
}
