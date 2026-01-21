using UnityEngine;

namespace Game.ColorStack
{
    [ExecuteAlways]
    public class ColorObjectStackEditor : MonoBehaviour
    {
        [SerializeField] private ColorObjectStack _colorObjectStack;

        private void Update()
        {
            if (Application.isPlaying)
            {
                return;
            }

            var startObjects = _colorObjectStack.StartObjects;

            for (var i = 0; i < startObjects.Count; i++)
            {
                startObjects[i].transform.localPosition = Vector3.up * (_colorObjectStack.DistanceBetween * i);
            }
        }
    }
}
