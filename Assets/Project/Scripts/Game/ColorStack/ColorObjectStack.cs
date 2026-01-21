using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extensions;
using Game.ColorObjects;
using Game.ColorObjects.MoveGroup;
using Game.Path;
using UnityEngine;

namespace Game.ColorStack
{
    public class ColorObjectStack : MonoBehaviour
    {
        public event Action FilledStack;

        [SerializeField] private int _capacity;
        [SerializeField] private Transform _startObjectPosition;
        [SerializeField] private float _distanceBetween;
        [SerializeField] private float _drawRadius;
        [SerializeField] private PathMover _pathMover;
        [SerializeField] private MoveGroup _moveGroupPrefab;
        [SerializeField] private List<ColorObject> _startObjects;
        [SerializeField] private ColorObjectViewSettings _viewSettings;
        [SerializeField] private Renderer _rendererStackPlatform;

        private float _canInteractTime;
        private Vector3[] _objectPositions;
        private readonly Stack<ColorObject> _colorObjects = new();
        private Material _initMaterialStackPlatform;

        public List<ColorObject> StartObjects => _startObjects;
        public float DistanceBetween => _distanceBetween;
        public bool CanInteract => Time.time >= _canInteractTime;
        public bool IsFull => _colorObjects.Count >= _capacity;

        private void Awake()
        {
            _initMaterialStackPlatform = _rendererStackPlatform.material;

            foreach (var startObject in _startObjects)
            {
                _colorObjects.Push(startObject);
            }

            _objectPositions = new Vector3[_capacity];

            for (var i = 0; i < _objectPositions.Length; i++)
            {
                var position = _startObjectPosition.position;
                position = position.ChangeY(position.y + i * _distanceBetween);

                _objectPositions[i] = position;
            }

            if (_startObjects.Count > 0)
            {
                SetPlatformColor(_startObjects[0].Color);
            }
        }

        public void Push(Stack<ColorObject> colorObjects)
        {
            var allDelay = 0f;
            var duration = colorObjects.Peek().JumpDuration;

            while (colorObjects.Count > 0 && !IsFull)
            {
                var colorObject = colorObjects.Pop();

                var color = colorObject.Color;

                SetPlatformColor(color);

                allDelay += colorObject.BetweenJumpDelay;

                var objectPosition = _objectPositions[_colorObjects.Count];
                colorObject.JumpTo(objectPosition, allDelay, () => { colorObject.transform.SetParent(_startObjectPosition); });

                _colorObjects.Push(colorObject);
            }

            var jumpDuration = duration + allDelay;
            _canInteractTime = jumpDuration + Time.time;

            CheckWinState();
        }

        private void SetPlatformColor(ColorType colorType)
        {
            var material = GetMaterialByColor(colorType);
            if (material != null)
            {
                _rendererStackPlatform.material = material;
            }
        }

        public void Pop()
        {
            if (_colorObjects.Count == 0)
            {
                return;
            }

            var targetPosition = _pathMover.GetClosestPointOnPath(transform.position);
            ColorObject temp = null;

            var tempList = new List<ColorObject>();
            var moveGroup = Instantiate(_moveGroupPrefab, targetPosition, Quaternion.identity);

            float allDelay = 0f;

            do
            {
                temp = _colorObjects.Pop();
                allDelay += temp.BetweenJumpDelay;
                tempList.Add(temp);

                temp.JumpTo(targetPosition, allDelay);
                targetPosition = targetPosition.ChangeY(targetPosition.y + _distanceBetween);
            } while (_colorObjects.Count > 0 && temp.Color == _colorObjects.Peek().Color);

            _rendererStackPlatform.material = _initMaterialStackPlatform;

            var allJumpDuration = tempList[0].JumpDuration + allDelay;

            _canInteractTime = Time.time + allJumpDuration ;

            DOVirtual.DelayedCall(allJumpDuration,
                () =>
                {
                    moveGroup.Setup(tempList, _pathMover);
                    _pathMover.AddObject(moveGroup);
                });
        }

        private void OnDrawGizmos()
        {
            if (_startObjectPosition == null)
            {
                return;
            }

            Gizmos.color = Color.red;

            for (int i = 0; i < _capacity; i++)
            {
                var position = _startObjectPosition.position;
                position = position.ChangeY(position.y + i * _distanceBetween);

                Gizmos.DrawWireSphere(position, _drawRadius);
            }
        }

        private Material GetMaterialByColor(ColorType colorType)
        {
            var pair = _viewSettings.ColorMaterialPair.FirstOrDefault(p => p.Color == colorType);
            if (pair == null || pair.Material == null)
            {
                Debug.LogWarning($"Material not found for color {colorType}");
                return null;
            }

            return pair.Material;
        }

        public bool CheckWinState()
        {
            if (!IsFull)
            {
                return false;
            }

            var color = _colorObjects.Peek().Color;

            foreach (var colorObject in _colorObjects)
            {
                if (colorObject.Color != color)
                {
                    return false;
                }
            }

            FilledStack?.Invoke();
            Debug.Log($"собрали цвет {color}");

            return true;
        }
    }
}
