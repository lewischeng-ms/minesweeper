using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineCSharp.Properties
{
    interface IGameController
    {
        void NewGame();
        void TerminateGame();
        void Cheat();
        void TimeTick();
        void ProcessMouseDown(int rowId, int colId, StaticData.MyMouseButtons buttons);
        void ProcessMouseUp(int rowId, int colId, StaticData.MyMouseButtons buttons);
        void ProcessMouseMove(int rowId, int colId);
        void ProcessMouseLeave(int rowId, int colId);
    }
}
