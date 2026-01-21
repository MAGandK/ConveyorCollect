using System.Collections.Generic;
using Game.Path;
using UnityEngine;

namespace Game.ColorObjects.MoveGroup
{
    public class MoveGroup: MonoBehaviour
    {
        private Stack<ColorObject> _colorObjects;
        private PathMover _pathMover;
        private bool _isSetup;
        public bool IsSetup => _isSetup;

        public float Distance { get; set; }

        public Stack<ColorObject> ColorObjects => _colorObjects;

        public void Setup(IEnumerable<ColorObject> colorObjects, PathMover pathMover)
        {
            _colorObjects = new Stack<ColorObject>(colorObjects);
            _pathMover = pathMover;

            foreach (var colorObject in _colorObjects)
            {
                colorObject.transform.SetParent(transform);
            }

            _isSetup = true;
        }

        public void Move(float speed, Vector3 position, Quaternion rotation)
        {
            Distance += speed * Time.deltaTime;

            transform.position = position;
        }
    }
}
