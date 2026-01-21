using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Other.UIAnimation
{
    public class ShowPopupAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _contentBack;
        [SerializeField] private float _scaleDownValue = 0.9f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private Image _back;

        public void Play()
        {
            if (_contentBack != null)
            {
                PlayBackScaleAnimation();
            }
            
            if (_back != null)
            {
                PlayFadeAnimation();
            }
        }

        private void PlayBackScaleAnimation()
        {
            var startLocalScale = _contentBack.localScale;

            var sequence = DOTween.Sequence();

            var scaleDuration = _scaleDuration / 3;
            var tween = _contentBack.DOScale(startLocalScale * _scaleDownValue, scaleDuration);
            sequence.Append(tween);

            tween = _contentBack.DOScale(startLocalScale, scaleDuration * 2);
            sequence.Append(tween);

            sequence.Play();
        }

        private void PlayFadeAnimation()
        {
            var startAlfa = _back.color.a;
            _back.color = new Color(_back.color.r, _back.color.g, _back.color.b, 0);

            _back.DOFade(startAlfa, _scaleDuration).Play();
        }
    }
}