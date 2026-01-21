using Game.ColorStack;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick.Joystick _joystick;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _interactableLayerMask;
        [SerializeField] private float _maxDistance = 100;
        private void OnEnable()
        {
            _joystick.PointerDown += TryToRay;
        }

        private void OnDisable()
        {
            _joystick.PointerDown -= TryToRay;
        }

        private void TryToRay()
        {
            var screenPointToRay = _camera.ScreenPointToRay(_joystick.Position);

            if (Physics.Raycast(screenPointToRay, out var hitInfo, _maxDistance, _interactableLayerMask))
            {
                if (hitInfo.collider.TryGetComponent(out ColorObjectStack colorObjectStack) && colorObjectStack.CanInteract)
                {
                    colorObjectStack.Pop();
                }
            }
        }
    }
}
