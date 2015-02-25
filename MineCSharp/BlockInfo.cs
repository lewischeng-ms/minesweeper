using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp
{
    class BlockInfo
    {
        public enum BlockStates
        {
            Normal,
            Digged,
            Flagged,
            Questioned
        }

        // 方块的当前状态。
        public BlockStates CurrentState { get; set; }

        // 是否是雷。
        public bool IsMine { get; set; }

        // 方块上显示的数字，即周围8个方块中的雷数。
        public int Number { get; set; }
    }
}
