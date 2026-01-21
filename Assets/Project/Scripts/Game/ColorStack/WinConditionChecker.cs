using System.Linq;
using UnityEngine;

namespace Game.ColorStack
{
    public class WinConditionChecker : MonoBehaviour
    {
        [SerializeField] private ColorObjectStack[] _colorObjectStacks;

        private void Awake()
        {
            foreach (var colorObjectStack in _colorObjectStacks)
            {
                colorObjectStack.FilledStack += ColorObjectStackOnFilledStack;
            }
        }

        private void ColorObjectStackOnFilledStack()
        {
            if (_colorObjectStacks.All(x => x.CheckWinState()))
            {
                Debug.Log("WIN");
            }
        }
    }
}
