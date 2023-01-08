using Unity.Collections;
using UnityEngine;

public class Cell: MonoBehaviour
{
    [field: SerializeField] public CellDifficulty CellDifficulty { get; private set; }
    [field: SerializeField] public GridType GridType { get; private set; }
    [field: SerializeField] public CellView View { get; private set; } = new CellView();

    public MoveCost MoveCost { get; private set; } = new MoveCost();
    public Vector3Int GridPosition => PositionConverter.Wrap(transform.position, GridType);

    public void DestroyFully()
    {
#if(UNITY_EDITOR)
        DestroyImmediate(gameObject);
#endif
    }
}
