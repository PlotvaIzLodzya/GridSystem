using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPositionsUtility
{
    public static Vector3Int[] GetNeighbours(Vector3Int hexCoordinates, IEnumerable<Vector3Int> gridPositions,  int radius, bool include = true)
    {
        List<Vector3Int> neighbourGridPositions = new List<Vector3Int>();

        if (include)
            neighbourGridPositions.Add(hexCoordinates);

        for (int i = 0; i < radius; i++)
        {
            List<Vector3Int> temPos = new List<Vector3Int>();

            for (int j = 0; j < neighbourGridPositions.Count; j++)
            {
                foreach (var neigborPos in GetNeighbours(neighbourGridPositions[j], gridPositions, true))
                {
                    if (neighbourGridPositions.Contains(neigborPos) == false)
                        temPos.Add(neigborPos);
                }
            }

            neighbourGridPositions.AddRange(temPos);
        }

        return neighbourGridPositions.ToArray();
    }

    public static Vector3Int[] GetNeighbours(Vector3Int hexCoordinates, IEnumerable<Vector3Int> gridPositions, bool include = true)
    {
        List<Vector3Int> neighboursGridPosition = new List<Vector3Int>();

        if (include)
            neighboursGridPosition.Add(hexCoordinates);

        foreach (var direcation in Direction.GetDirecationList(hexCoordinates.z))
        {
            Vector3Int neighborGridPosition = hexCoordinates + direcation;

            if (gridPositions.FirstOrDefault(gridPosition => gridPosition == neighborGridPosition) != default)
            {
                neighboursGridPosition.Add(neighborGridPosition);
            }
        }

        return neighboursGridPosition.ToArray();
    }
}
