using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp
{
    // 游戏相关静态数据，仅可能在更改级别时修改。
    static class StaticData
    {
        public enum MyMouseButtons
        {
            None,
            Left,
            Right,
            Both
        }

        public enum Levels
        {
            Beginner,
            Intermediate,
            Advanced,
            RP,
            SuperRP
        }

        public const int MaxRowCount = 16;
        public const int MaxColCount = 30;

        private static Levels level;
        private static int rowCount;
        private static int colCount;
        private static int mineCount;
        private static int safeCount;

        public static Levels Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
                switch (level)
                {
                    case Levels.Beginner:
                        rowCount = 9;
                        colCount = 9;
                        mineCount = 10;
                        break;
                    case Levels.Intermediate:
                        rowCount = 16;
                        colCount = 16;
                        mineCount = 40;
                        break;
                    case Levels.Advanced:
                        rowCount = 16;
                        colCount = 30;
                        mineCount = 99;
                        break;
                    case Levels.RP:
                        rowCount = 16;
                        colCount = 30;
                        mineCount = 240;
                        break;
                    case Levels.SuperRP:
                        rowCount = 16;
                        colCount = 30;
                        mineCount = 476;
                        break;
                }
                safeCount = rowCount * colCount - mineCount;
            }
        }

        public static int RowCount
        {
            get
            {
                return rowCount;
            }
        }

        public static int ColCount
        {
            get
            {
                return colCount;
            }
        }

        public static int MineCount
        {
            get
            {
                return mineCount;
            }
        }

        public static int SafeCount
        {
            get
            {
                return safeCount;
            }
        }
    }
}
