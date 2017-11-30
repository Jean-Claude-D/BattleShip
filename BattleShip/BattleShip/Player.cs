using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    interface Player
    {
        Square MakeMove(Board board);
    }
}
