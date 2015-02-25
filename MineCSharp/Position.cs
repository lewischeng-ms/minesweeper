using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp
{
    class Position
    {
        // 行号。
        public int RowId;

        // 列号。
        public int ColId;

        public Position(int rowId, int colId)
        {
            RowId = rowId;
            ColId = colId;
        }
    }
}
