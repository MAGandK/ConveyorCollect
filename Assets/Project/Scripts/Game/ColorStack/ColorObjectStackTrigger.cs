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
        private MoveGroup _moveGroup;

        private void OnTriggerEnter(Collider other)
        {
            if (_objectStack.IsFull)
            {
                return;
            }

            if (!other.TryGetComponent(out MoveGroupPhysics moveGroupPhysics) || !moveGroupPhysics.MoveGroup.IsSetup)
            {
                return;
            }

            if (moveGroupPhysics.MoveGroup.ColorObjects.Count > 0)
            {
                _objectStack.Push(moveGroupPhysics.MoveGroup.ColorObjects);

                if (moveGroupPhysics.MoveGroup.ColorObjects.IsEmpty())
                {
                    _pathMover.Remove(moveGroupPhysics.MoveGroup);
                }
            }
        }
    }
}
