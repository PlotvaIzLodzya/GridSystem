using System.Collections.Generic;
using UnityEngine;
using MapCreation;

public class GameGrid: MonoBehaviour
{
    [SerializeField] private MapCreation.GridLayout _gridLayout;

    public PathCreator PathCreator { get; private set; }
    public GridType GridType => _gridLayout.CurrentGridType;

    private List<Vector3Int> _gridPositions = new List<Vector3Int>();
    private Dictionary<Vector3Int, Cell> _cellGrid;

    public IReadOnlyDictionary<Vector3Int, Cell> CellGrid => _cellGrid;
    public IReadOnlyList<Vector3Int> GridPositions => _gridPositions;

    private void Awake()
    {
        _cellGrid = new Dictionary<Vector3Int, Cell>();

        foreach (var cell in _gridLayout.Cells)
        {
            _cellGrid.Add(cell.GridPosition, cell);
            _gridPositions.Add(cell.GridPosition);
        }

        PathCreator = new PathCreator(this);
    }

    public bool HasGridPosition(Vector3Int gridPosition)
    {
        return _cellGrid.ContainsKey(gridPosition);
    }

    public Vector3Int GetClosestCellPosition(Vector3 position)
    {
        Vector3Int closestPosition = Vector3Int.zero;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var cell in _cellGrid)
        {
            float dist = Vector3.Distance(cell.Value.transform.position, currentPosition);

            if (dist < minDistance)
            {
                closestPosition = cell.Key;
                minDistance = dist;
            }
        }

        return closestPosition;
    }

    public bool TryGetCell(Vector3Int gridPosition, out Cell cell)
    {
        return _cellGrid.TryGetValue(gridPosition, out cell);
    }

    public Cell GetCell(Vector3Int vector3Int)
    {
        return _cellGrid[vector3Int];
    }
}
