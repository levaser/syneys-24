using System.Linq;
using UnityEngine;

namespace Array2DEditor
{
    public enum CellType
    {
        None,
        Wall,
        Enemy,
        Switcher,
        Trap
    }

    [System.Serializable]
    public class Array2DCellType : Array2D<CellType>
    {
        [SerializeField]
        CellRowCellType[] cells = new CellRowCellType[Consts.defaultGridSize];

        protected override CellRow<CellType> GetCellRow(int idx)
        {
            return cells[idx];
        }

        /// <inheritdoc cref="Array2D{T}.Clone"/>
        protected override Array2D<CellType> Clone(CellRow<CellType>[] cells)
        {
            return new Array2DCellType()
                {
                    cells = cells.Select(r => new CellRowCellType(r)).ToArray()
                };
        }
    }

    [System.Serializable]
    public class CellRowCellType : CellRow<CellType>
    {
        /// <inheritdoc/>
        [JetBrains.Annotations.UsedImplicitly]
        public CellRowCellType() { }

        /// <inheritdoc/>
        public CellRowCellType(CellRow<CellType> row)
            : base(row) { }
    }
}