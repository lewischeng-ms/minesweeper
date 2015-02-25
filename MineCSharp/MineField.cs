using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp
{
    // 游戏只需要一个雷区。
    static class MineField
    {
        // 雷区的后端数据。
        private static BlockInfo[,] blocks;

        // 随机数生成器
        private static Random random;

        static MineField()
        {
            blocks = new BlockInfo[GameData.MaxRowCount, GameData.MaxColCount];
            for (int i = 0; i < GameData.MaxRowCount; i++)
            {
                for (int j = 0; j < GameData.MaxColCount; j++)
                {
                    blocks[i, j] = new BlockInfo();
                    blocks[i, j].CurrentState = BlockInfo.BlockStates.Normal;
                    blocks[i, j].IsMine = false;
                    blocks[i, j].Number = 0;
                }
            }
            random = new Random();
        }

        public static void GenerateNewField()
        {
            // 复位所有方块数据。
            for (int i = 0; i < GameData.RowCount; i++)
            {
                for (int j = 0; j < GameData.ColCount; j++)
                {
                    blocks[i, j].CurrentState = BlockInfo.BlockStates.Normal;
                    blocks[i, j].IsMine = false;
                    blocks[i, j].Number = 0;
                }
            }
            // 随机产生地雷。
            int currentMineCount = 0;
            int rnd;
            int max = GameData.ColCount * GameData.RowCount;
            while (currentMineCount < GameData.MineCount)
            {
                rnd = random.Next(max);
                int rowId = rnd / GameData.ColCount;
                int colId = rnd % GameData.ColCount;
                if (blocks[rowId, colId].IsMine == true)
                    continue;
                else
                {
                    blocks[rowId, colId].IsMine = true;
                    // 更新方块周围数据。
                    // 上一行
                    if (rowId >= 1 && rowId < GameData.RowCount)
                    {
                        // 上一列
                        if (colId >= 1 && colId < GameData.ColCount)
                            blocks[rowId - 1, colId - 1].Number++;
                        // 本列
                        blocks[rowId - 1, colId].Number++;
                        // 下一列
                        if (colId >= 0 && colId < GameData.ColCount - 1)
                            blocks[rowId - 1, colId + 1].Number++;
                    }

                    // 本行
                    // 上一列
                    if (colId >= 1 && colId < GameData.ColCount)
                        blocks[rowId, colId - 1].Number++;
                    // 本行本列的number不用加。
                    // 下一列
                    if (colId >= 0 && colId < GameData.ColCount - 1)
                        blocks[rowId, colId + 1].Number++;

                    // 下一行
                    if (rowId >= 0 && rowId < GameData.RowCount - 1)
                    {
                        // 上一列
                        if (colId >= 1 && colId < GameData.ColCount)
                            blocks[rowId + 1, colId - 1].Number++;
                        // 本列
                        blocks[rowId + 1, colId].Number++;
                        // 下一列
                        if (colId >= 0 && colId < GameData.ColCount - 1)
                            blocks[rowId + 1, colId + 1].Number++;
                    }
                    currentMineCount++;
                }
            }

        }

        // 获取位于(rowId, colId)处方块的当前信息
        public static BlockInfo GetCurrentInfo(int rowId, int colId)
        {
            return blocks[rowId, colId];
        }
    }
}
