using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Joystick
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IJoystickController
    {
        private const float CLICK_DURATION = 0.5f;
        public event Action PointerUp;
        public event Action PointerDown;
        public event Action DoubleClick;

        private int _clickCount;
        private float _oldClickTime;

        public Vector2 Direction { get; private set; }

        public Vector2 Position { get; private set; }

        public void OnDrag(PointerEventData eventData)
        {
            Position = eventData.position;
            Direction = eventData.delta.normalized;
            _clickCount = 0;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Position = Vector2.zero;
            Direction = Vector2.zero;
            PointerUp?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Position = eventData.position;
            PointerDown?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _clickCount++;

            if (_clickCount > 1)
            {
                if (_oldClickTime + CLICK_DURATION >= Time.time)
                {
                    DoubleClick?.Invoke();
                }

                _clickCount = 0;
            }

            _oldClickTime = Time.time;
        }
    }
}
