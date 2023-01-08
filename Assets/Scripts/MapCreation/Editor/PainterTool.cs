using UnityEditor;
using MapCreation;

[CustomEditor(typeof(Painter))]
public class PainterTool : Editor
{
    private Painter _painter;

    private void Awake()
    {
        _painter = (Painter)target;
    }

    private void OnSceneGUI()
    {
        _painter.Paint();
    }
}