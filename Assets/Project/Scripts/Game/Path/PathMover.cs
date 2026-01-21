using System.Collections.Generic;
using Game.ColorObjects.MoveGroup;
using PathCreation;
using UnityEngine;

namespace Game.Path
{
    public class PathMover : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreation;
        [SerializeField] private float _speed;

        private readonly List<MoveGroup> _groups = new();

        private void Update()
        {
            MoveObjects();
        }

        public void MoveObjects()
        {
            var path = _pathCreation.path;

            if (path == null)
            {
                return;
            }

            foreach (var group in _groups)
            {
                var position = path.GetPointAtDistance(group.Distance);

                group.Move(_speed, position, Quaternion.identity);
            }
        }

        public void AddObject(MoveGroup group)
        {
            _groups.Add(group);

            group.Distance = _pathCreation.path.GetClosestDistanceAlongPath(group.transform.position);
        }

        public void Remove(MoveGroup group)
        {
            _groups.Remove(group);
        }

        public Vector3 GetClosestPointOnPath(Vector3 transformPosition)
        {
            return _pathCreation.path.GetClosestPointOnPath(transformPosition);
        }
    }
}
