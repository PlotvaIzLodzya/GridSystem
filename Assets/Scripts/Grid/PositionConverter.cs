using UnityEngine;

public static class PositionConverter
{
    public static Vector3Int Wrap(Vector3 position, GridType gridType)
    {
        var gridPorperty = MapCreation.GridProperties.GetGridProperty(gridType);

        int x = Mathf.RoundToInt(position.x / gridPorperty.XSize);
        int y = Mathf.RoundToInt(position.y / gridPorperty.YSize);
        int z = Mathf.RoundToInt(position.z / gridPorperty.ZSize);

        return new Vector3Int(x, y, z);
    }

    public static Vector3 Unwrap(Vector3Int position, GridType gridType)
    {
        var gridProperty = MapCreation.GridProperties.GetGridProperty(gridType);

        float x = position.x * gridProperty.XSize;
        float y = position.y * gridProperty.YSize;
        float z = position.z * gridProperty.ZSize;

        return new Vector3(x, y, z);
    }
}