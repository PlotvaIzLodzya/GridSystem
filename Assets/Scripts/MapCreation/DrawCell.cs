using System;
using UnityEngine;

namespace MapCreation
{
    [Serializable]
    public class DrawCell
    {
        [SerializeField] public bool IsDrawn;

        [field: SerializeField] public Vector3Int GridPosition;
        [field: SerializeField] public Cell DrawnCell { get; private set; }
        [field: SerializeField] public Cell PhatomCell { get; private set; }

        [SerializeField] private Material _initialMaterial;
        [SerializeField] private Material _drawnCellInitialMaterial;

        public DrawCell(Cell cell)
        {
            GridPosition = cell.GridPosition;
            PhatomCell = cell;
            _initialMaterial = cell.View.MeshRenderer.sharedMaterial;
        }

        public void Highlight(Color color, bool highLightDrawnCell)
        {
            PhatomCell.View.SetColor(color * 0.4f);

            if (DrawnCell != null && highLightDrawnCell)
                DrawnCell.View.SetColor(color * 0.8f);
        }

        public void DeHighlight()
        {
            PhatomCell.View.MeshRenderer.sharedMaterial = _initialMaterial;

            if (DrawnCell != null)
                DrawnCell.View.MeshRenderer.sharedMaterial = _drawnCellInitialMaterial;
        }

        public void SetDrawn(Cell cell)
        {
            IsDrawn = true;
            DrawnCell = cell;
            _drawnCellInitialMaterial = cell.View.MeshRenderer.sharedMaterial;
        }

        public void Erase()
        {
            IsDrawn = false;
        }

        public void Destroy()
        {
            PhatomCell.DestroyFully();
        }
    }
}
