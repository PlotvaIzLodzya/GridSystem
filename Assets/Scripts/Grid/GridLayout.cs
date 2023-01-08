using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MapCreation
{
    public class GridLayout : MonoBehaviour
    {
        [SerializeField] private GridDrawer _gridDrawer;
        [SerializeField] private int _height;
        [SerializeField] private int _width;

        [HideInInspector][SerializeField] private List<Cell> _cells = new List<Cell>();
        [HideInInspector][SerializeField] private List<DrawCell> _drawCells = new List<DrawCell>();

        public GridType CurrentGridType { get; private set; } = GridType.HexagonalFlatTop;
        public IReadOnlyList<Cell> Cells => _cells;

        public List<DrawCell> GetDrawCellsByPositions(Vector3Int[] gridPositions)
        {
            return _drawCells.Where(cell => gridPositions.Any(gridPos => gridPos == cell.GridPosition)).ToList();
        }

        public bool TryGetDrawCell(Vector3Int gridPosition, out DrawCell drawCell)
        {
            drawCell = _drawCells.FirstOrDefault(cell => cell.GridPosition == gridPosition);

            return drawCell != null;
        }

        public void TryAddCell(Cell cell)
        {
            if (_cells.Any(cachedCell => cachedCell.GridPosition == cell.GridPosition))
                return;

            _cells.Add(cell);
        }

        public void DeleteCells(Vector3Int[] gridPositions)
        {
            var cells = _cells.Where(cell => gridPositions.Any(gridPos => gridPos == cell.GridPosition));
            _cells = _cells.Except(cells).ToList();
            foreach (var cell in cells)
            {
                cell.DestroyFully();
            }

            var drawCells = GetDrawCellsByPositions(gridPositions);

            foreach (var drawCell in drawCells)
            {
                drawCell.Erase();
            }
        }

        public List<Vector3Int> GetNeighbours(Vector3Int gridPosition, int size)
        {
            var gridPositions = _drawCells.Select(drawCell => drawCell.GridPosition);

            return GridPositionsUtility.GetNeighbours(gridPosition, gridPositions, size).ToList();
        }

        public void Repaint()
        {
            foreach (var drawCell in _drawCells)
            {
                drawCell.DeHighlight();
            }
        }

        public void RemoveDrawnCell(DrawCell drawnCell)
        {
            _cells.Remove(drawnCell.DrawnCell);
            drawnCell.DrawnCell.DestroyFully();
            drawnCell.Erase();
        }

        //[ContextMenu(nameof(ClearAll))]
        public void ClearAll()
        {
            _cells.Clear();
            _drawCells.Clear();
            var cells = GetComponentsInChildren<Cell>();
            foreach (var cell in cells)
            {
                cell.DestroyFully();
            }
        }

        [ContextMenu(nameof(ClearPhantomCells))]
        public void ClearPhantomCells()
        {
            foreach (var drawCell in _drawCells)
            {
                drawCell.Destroy();
            }

            _drawCells.Clear();
        }

        [ContextMenu(nameof(DrawGrid))]
        public void DrawGrid()
        {
            foreach (var cell in _cells)
            {
                if (_drawCells.FirstOrDefault(drawCell => drawCell.GridPosition == cell.GridPosition) == default)
                {
                    DrawCell drawCell = _gridDrawer.Draw(cell.transform.position);
                    drawCell.SetDrawn(cell);
                    _drawCells.Add(drawCell);
                }
            }

            var positionsToExclude = _drawCells.Select(cell => cell.PhatomCell.transform.position).ToList();

            _drawCells.AddRange(_gridDrawer.Draw(_width, _height, GridType.HexagonalFlatTop, positionsToExclude));
        }
    }
}
