using Game.ColorObjects.MoveGroup;
using Game.Path;
using ModestTree;
using UnityEngine;

namespace Game.ColorStack
{
    public class ColorObjectStackTrigger : MonoBehaviour
    {
        [SerializeField] private ColorObjectStack _objectStack;
        [SerializeField] private PathMover _pathMover;

        private void Awake()
        {
            if (_objectStack == null)
            {
                _objectStack = GetComponentInParent<ColorObjectStack>();
            }

            if (_pathMover == null)
            {
                _pathMover = transform.root.GetComponentInChildren<PathMover>(true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_objectStack == null || _objectStack.IsFull)
            {
                return;
            }

            if (!other.TryGetComponent(out MoveGroupPhysics moveGroupPhysics) || !moveGroupPhysics.MoveGroup.IsSetup)
            {
                return;
            }

            var colorObjects = moveGroupPhysics.MoveGroup.ColorObjects;
            if (colorObjects == null || colorObjects.Count == 0)
            {
                return;
            }

            _objectStack.Push(colorObjects);

            if (_pathMover != null && colorObjects.IsEmpty())
            {
                _pathMover.Remove(moveGroupPhysics.MoveGroup);
            }
        }
    }
}
