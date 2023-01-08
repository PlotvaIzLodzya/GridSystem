namespace MapCreation
{
    public class GridProperty
    {
        public GridType GridType { get; private set; }
        public float XSize { get; private set; }
        public float YSize { get; private set; }
        public float ZSize { get; private set; }

        public GridProperty(float xSize, float ySize, float zSize, GridType gridType)
        {
            XSize = xSize;
            YSize = ySize;
            ZSize = zSize;
            GridType = gridType;
        }
    }
}