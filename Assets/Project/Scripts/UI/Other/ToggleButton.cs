using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Other
{
    [RequireComponent(typeof(Image))]
    public class ToggleButton : Button
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _disableSprite;

        private bool _isActiveState;

        public bool IsActiveState => _isActiveState;

        public void SetActiveState(bool isActive)
        {
            _isActiveState = isActive;
            _image.sprite = _isActiveState ? _activeSprite : _disableSprite;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            SetActiveState(!_isActiveState);

            base.OnPointerClick(eventData);
        }
    }
}