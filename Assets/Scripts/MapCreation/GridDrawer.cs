using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapCreation
{
    public class GridDrawer : MonoBehaviour
    {
        [SerializeField] private Cell _phantomCell;
        [SerializeField] private Transform _container;

        public List<DrawCell> Draw(int width, int height, GridType gridType, List<Vector3> excludePositions = null)
        {
            var positions = GetPositions(width, height, Vector3Int.zero, gridType);

            if (excludePositions != null)
                positions = ExcludePositions(positions, excludePositions);

            return Draw(positions);
        }

        public List<DrawCell> Draw(List<Vector3> positions)
        {
            List<DrawCell> cells = new List<DrawCell>();

            foreach (var position in positions)
            {
                DrawCell drawCell = Draw(position);
                cells.Add(drawCell);
            }

            return cells;
        }

        public DrawCell Draw(Vector3 position)
        {
            var cell = Instantiate(_phantomCell, _container);
            cell.transform.position = position;

            return new DrawCell(cell);
        }

        public static List<Vector3> GetPositions(float width, float height, Vector3 center, GridType gridType)
        {
            List<Vector3> positions = new List<Vector3>();
            Vector3 currentPosition = Vector3.zero;

            int halfWidth = Mathf.CeilToInt((width / 2));
            int halfHeight = Mathf.CeilToInt((height / 2));

            GridProperty gridProperty = GridProperties.GetGridProperty(gridType);

            for (int x = -halfWidth; x <= halfWidth; x++)
            {
                for (int z = -halfHeight; z <= halfHeight; z++)
                {
                    float zValue = z * gridProperty.ZSize * 2;
                    float xValue = x * gridProperty.XSize;

                    if (x % 2 == 0)
                        zValue = z + gridProperty.ZSize;

                    currentPosition.x = xValue;
                    currentPosition.z = zValue;
                    positions.Add(currentPosition);
                }
            }

            return positions;
        }

        private List<Vector3> ExcludePositions(List<Vector3> positions, List<Vector3> positionsToExclude)
        {
            List<Vector3> newPositions = new List<Vector3>();
            for (int i = 0; i < positions.Count; i++)
            {
                if (positionsToExclude.Any(posToExlcude => Vector3Equals(posToExlcude, positions[i])) == false)
                {
                    newPositions.Add(positions[i]);
                }
            }

            return newPositions;
        }

        private bool Vector3Equals(Vector3 a, Vector3 b)
        {
            return (Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.z, b.z));
        }
    }
}