using UnityEditor;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(Array2DCellType))]
    public class Array2DCellTypeDrawer : Array2DEnumDrawer<CellType> {}
}