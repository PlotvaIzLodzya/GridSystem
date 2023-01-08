using UnityEditor;
using UnityEngine;

namespace MapCreation
{
    public abstract class Brush
    {
        public abstract Vector3Int[] GetPositions(GridLayout gridLayout, int size);

        public bool TryGetCell(Vector3 screenPosition, out Cell cell)
        {
            cell = null;
            Ray ray = HandleUtility.GUIPointToWorldRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50))
            {
                if (hit.collider.TryGetComponent(out cell))
                {
                }
            }

            return cell != null;
        }
    }
}
