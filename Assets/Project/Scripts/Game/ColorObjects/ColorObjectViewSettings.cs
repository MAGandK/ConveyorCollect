using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ColorObjects
{
    [CreateAssetMenu(fileName = "ColorObjectViewSettings", menuName = "Game/ColorObjectViewSettings", order = 1)]
    public class ColorObjectViewSettings : ScriptableObject
    {
        [SerializeField] private List<ColorMaterialPair> _colorMaterialPair;
        public List<ColorMaterialPair> ColorMaterialPair => _colorMaterialPair;
    }

    [Serializable]
    public class ColorMaterialPair
    {
        public ColorType Color;
        public Material Material;
    }
}
