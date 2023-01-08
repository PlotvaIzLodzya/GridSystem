using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CellVariants", menuName = "ScriptableObjects/CellVariants", order = 1)]
public class CellVariants : ScriptableObject
{
    [SerializeField] private List<Cell> _cells;

    public bool TryGetCell(CellDifficulty cellDifficulty, out Cell cell)
    {
        cell = _cells.FirstOrDefault(cell => cell.CellDifficulty == cellDifficulty);

        if (cell == null)
            new NullReferenceException($"Not found {cellDifficulty} variant");

        return cell != null;
    }
}
