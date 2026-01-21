using UnityEngine;

namespace Game.ColorObjects.MoveGroup
{
    public class MoveGroupPhysics : MonoBehaviour
    {
        [SerializeField] private MoveGroup _moveGroup;
        public MoveGroup MoveGroup => _moveGroup;
    }
}
