using UnityEngine;

public class ConveyorArrows : MonoBehaviour
{
    [SerializeField] private Material _conveyorMaterial;
    [SerializeField] private float _scrollSpeed = 0.5f;
    private static readonly int MainTexId = Shader.PropertyToID("_MainTex");

    void Start()
    {
        if (_conveyorMaterial == null)
        {
            enabled = false;
            return;
        }

        if (!_conveyorMaterial.HasProperty(MainTexId))
        {
            enabled = false;
        }
    }

    private void Update()
    {
        var offset = _conveyorMaterial.GetTextureOffset(MainTexId);
        offset.y += _scrollSpeed * Time.deltaTime;
        _conveyorMaterial.SetTextureOffset(MainTexId, offset);
    }
}