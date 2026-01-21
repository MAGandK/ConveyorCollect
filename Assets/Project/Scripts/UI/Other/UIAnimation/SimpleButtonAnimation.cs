using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.WindowsLogic.Window.StartWindow
{
    [RequireComponent(typeof(Button))]
    public class SimpleButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _scaleValue = 0.9f;
        [SerializeField] private float _duration = 0.1f;

        private Vector3 _transformLocalScale;
        private Tween _tweenerCore;

        private void Awake()
        {
            _transformLocalScale = transform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            KillCurrentAnimation();
            _tweenerCore = transform.DOScale(_transformLocalScale * _scaleValue, _duration);
            _tweenerCore.Play();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            KillCurrentAnimation();
            _tweenerCore = transform.DOScale(_transformLocalScale, _duration);
            _tweenerCore.Play();
        }

        private void OnDisable()
        {
            transform.localScale = _transformLocalScale;
            KillCurrentAnimation();
        }

        private void KillCurrentAnimation()
        {
            _tweenerCore.Kill();
            _tweenerCore = null;
        }
    }
}