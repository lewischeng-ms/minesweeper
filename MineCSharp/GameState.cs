using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp
{
    // 游戏动态数据（游戏状态）。
    static class GameState
    {
        // 是否结束游戏？
        public static bool IsGameEnd;

        // 挖开的方块数。
        public static int DiggedCount;

        // 剩余雷数。
        public static int RemainCount;

        // 已用时间。
        public static int ElapsedTime;

        // 判断同时点击
        public static bool IsLeftDown;
        public static bool IsRightDown;

        public static void ClearState()
        {
            GameState.DiggedCount = 0;
            GameState.RemainCount = GameData.MineCount;
            GameState.ElapsedTime = 0;
            GameState.IsLeftDown = false;
            GameState.IsRightDown = false;
            GameState.IsGameEnd = false;
        }

        public static void IncTime()
        {
            ElapsedTime++;
        }

        public static void IncRemain()
        {
            RemainCount++;
        }

        public static void DecRemain()
        {
            RemainCount--;
        }

        public static void IncDigged()
        {
            DiggedCount++;
        }
    }
}
