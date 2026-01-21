using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.ColorObjects
{
    public class ColorObjectEditor : MonoBehaviour
    {
        [SerializeField] private ColorObject _colorObject;
        [SerializeField] private ColorType _colorType;
        [SerializeField] private ColorObjectViewSettings _settings;
        private void OnValidate()
        {
            var colorMaterialPair = _settings.ColorMaterialPair.FirstOrDefault(x => x.Color == _colorType);

            if (colorMaterialPair != null)
            {
                _colorObject.EditorSetColor(_colorType, colorMaterialPair.Material);
                EditorUtility.SetDirty(_colorObject);
            }
        }
    }
}
