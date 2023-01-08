using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static List<Vector3Int> DirectionsOffSetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1,0,-1),
        new Vector3Int(0,0,-2),
        new Vector3Int(1,0,-1),
        new Vector3Int(1,0,1),
        new Vector3Int(0,0,2),
        new Vector3Int(-1,0,1)
    };

    public static List<Vector3Int> DirectionOffSetEven = new List<Vector3Int>
    {
        new Vector3Int(0,0,-2),
        new Vector3Int(-1,0,-1),
        new Vector3Int(1,0,1),
        new Vector3Int(0,0,2),
        new Vector3Int(-1,0,1),
        new Vector3Int(1,0,-1)
    };

    public static List<Vector3Int> GetDirecationList(int zCoordinate) => zCoordinate % 2 == 0 ? DirectionOffSetEven : DirectionsOffSetOdd;
}
