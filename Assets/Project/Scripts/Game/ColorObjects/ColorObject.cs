using System;
using DG.Tweening;
using UnityEngine;

namespace Game.ColorObjects
{
    public class ColorObject : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private ColorType _color;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _jumpPower = 1f;
        [SerializeField] private float _jumpDuration = 0.5f;
        [SerializeField] private float _betweenJumpDelay = 0.1f;

        public float BetweenJumpDelay => _betweenJumpDelay;

        public float JumpDuration => _jumpDuration;

        private bool _isJumping;
        public bool IsJumping => _isJumping;
        public ColorType Color => _color;

        public void JumpTo(Vector3 targetPosition, float delay,Action callBack = null)
        {
            _isJumping = true;
            var originalRotation = transform.rotation;

            transform.DOJump(targetPosition, _jumpPower, 1, _jumpDuration)
                .SetDelay(delay)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transform.rotation = originalRotation;
                    _isJumping = false;
                    callBack?.Invoke();
                });
        }

        public void EditorSetColor(ColorType colorType, Material material)
        {
            _color = colorType;
            _meshRenderer.material = material;
        }
    }
}
