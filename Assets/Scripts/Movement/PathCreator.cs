using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PathCreator
{
    private GameGrid _grid;

    public PathCreator(GameGrid grid)
    {
        _grid = grid;
    }

    public List<Vector3Int> GetPath(Vector3Int startPosition, Vector3Int targetPostion)
    {
        if (_grid.HasGridPosition(startPosition) == false)
            startPosition = _grid.GetClosestCellPosition(startPosition);

        BFSResult bFSResult = GraphSearch.BFSGetRange(_grid, startPosition, 10);
        
        return bFSResult.GetPathTo(targetPostion);
    }
}


