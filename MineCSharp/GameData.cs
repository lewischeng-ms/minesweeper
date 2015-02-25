using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp
{
    // 游戏相对静态数据。
    static class GameData
    {
        public const int BlockWidth = 30;
        public const int BlockHeight = 30;

        public const int FieldLeft = 20;
        public const int FieldTop = 40;
        public const int FieldRight = 25;
        public const int FieldBottom = 45;

        public const int MaxRowCount = 16;
        public const int MaxColCount = 30;

        public static int RowCount = 16;
        public static int ColCount = 16;
        public static int MineCount = 40;
        public static int SafeCount = RowCount * ColCount - MineCount;
    }
}
