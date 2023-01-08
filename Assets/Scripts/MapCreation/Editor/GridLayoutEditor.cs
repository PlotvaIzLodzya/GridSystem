using UnityEditor;
using UnityEngine;

namespace MapCreation
{
    [CustomEditor(typeof(GridLayout))]
    public class GridLayoutEditor: Editor
    {
        private GridLayout _grid;

        private void Awake()
        {
            _grid = (GridLayout)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button(nameof(_grid.DrawGrid)))
            {
                _grid.DrawGrid();
            }

            if (GUILayout.Button(nameof(_grid.ClearPhantomCells)))
            {
                _grid.ClearPhantomCells();
            }

            if (GUILayout.Button(nameof(_grid.ClearAll)))
            {
                _grid.ClearAll();
            }
        }
    }
}
