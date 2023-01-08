using System;
using System.Collections.Generic;

namespace MapCreation
{
    public static class GridProperties
    {
        private static Dictionary<GridType, GridProperty> _propertyByGridType = new Dictionary<GridType, GridProperty>()
        {
            {GridType.HexagonalFlatTop, new GridProperty(0.86f, 1f, 0.5f, GridType.HexagonalFlatTop) },
            {GridType.SquareNONIMPLMENTED,new GridProperty(1f, 1f, 1f, GridType.SquareNONIMPLMENTED) }
        };

        public static GridProperty GetGridProperty(GridType gridType)
        {
            if (_propertyByGridType.TryGetValue(gridType, out GridProperty gridProperty) == false)
                new ArgumentNullException($"Not found {gridType} type of grid");

            return gridProperty;
        }
    }
}
