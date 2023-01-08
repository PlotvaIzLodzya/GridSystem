using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapCreation
{
    public class RoundBrush : Brush
    {
        public override Vector3Int[] GetPositions(GridLayout gridLayout, int size)
        {
            Vector3Int gridPos = Vector3Int.one * 1000;

            if (TryGetCell(Event.current.mousePosition, out Cell cell))
                gridPos = cell.GridPosition;

            var gridPositions = gridLayout.GetNeighbours(gridPos, size);

            return gridPositions.ToArray();
        }
    }
}
