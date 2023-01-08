using System;
using UnityEngine;

[Serializable]
public class CellView
{
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }

    public void SetColor(Color color)
    {
        var tempMaterial = new Material(MeshRenderer.sharedMaterial);
        tempMaterial.color = color;
        MeshRenderer.sharedMaterial = tempMaterial;
    }
}
