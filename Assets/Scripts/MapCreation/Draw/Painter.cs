using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace MapCreation
{
    public partial class Painter : MonoBehaviour
    {
        [SerializeField] private bool IsEnabled;
        [SerializeField, Range(0, 5)] private int BrushSize;
        [SerializeField] private CellVariants _cellVariants;
        [SerializeField] private CellDifficulty _currentCellDifficulty;
        [SerializeField] private Transform _cellContainer;
        [SerializeField] private GridType _gridType;
        [SerializeField] private PaintStateHandler _paintStateHandler =new PaintStateHandler();
        [SerializeField] private GridLayout _grid;
    
        [HideInInspector][SerializeField] private List<DrawCell> _targetedCells = new List<DrawCell>();

        protected PaintStateHandler PaintStateHandler => _paintStateHandler;
        private Brush _brush = new RoundBrush();
        private Inputs _inputs = new Inputs();  

        public void Paint()
        {
            if (IsEnabled == false)
                return;

            Repaint();

            var positions = _brush.GetPositions(_grid, BrushSize);

            _targetedCells = _grid.GetDrawCellsByPositions(positions);

            HighlightTargetedCells();

            switch (_paintStateHandler.CurrentState)
            {
                case PaintState.Paint:
                    PlaceCells();
                    break;
                case PaintState.Erase:
                    Erase(positions);
                    break;
                default:
                    break;
            };
        }

        public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(screenPosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            plane.Raycast(ray, out float distance);
            return ray.GetPoint(distance);
        }

        private void PlaceCells()
        {
            if (Inputs.LeftMouseButton.IsPressed == false)
                return;

            foreach (var targetCell in _targetedCells)
            {
                if (targetCell.IsDrawn && targetCell.DrawnCell.CellDifficulty == _currentCellDifficulty)
                    continue;

                if (_cellVariants.TryGetCell(_currentCellDifficulty, out Cell cellPrefab))
                {
                    if (targetCell.IsDrawn && targetCell.DrawnCell.CellDifficulty != _currentCellDifficulty)
                        _grid.RemoveDrawnCell(targetCell);

                    Cell cell = Instantiate(cellPrefab, _cellContainer);
                    cell.transform.position = targetCell.PhatomCell.transform.position;
                    targetCell.SetDrawn(cell);
                    _grid.TryAddCell(cell);
                }
            }
        }

        private void Erase(Vector3Int[] positions)
        {
            if (Inputs.LeftMouseButton.IsPressed)
            {
                _grid.DeleteCells(positions);
            }
        }

        private Color GetCellPrefabColor()
        {
            Color  color = Color.white;

            if (_cellVariants.TryGetCell(_currentCellDifficulty, out Cell cell))
                color = cell.View.MeshRenderer.sharedMaterial.color;

            return color;
        }

        private void HighlightTargetedCells()
        {
            Color color = Color.white;
            if (_paintStateHandler.CurrentState == PaintState.Paint)
                color = GetCellPrefabColor();
            else if (_paintStateHandler.CurrentState == PaintState.Erase)
                color = Color.red * 0.7f;

            foreach (var targetCell in _targetedCells)
            {
                targetCell.Highlight(color, true);
            }
        }

        private void Repaint()
        {
            _inputs.Update();
            _grid.Repaint();
            _targetedCells.Clear();
        }
    }
}
