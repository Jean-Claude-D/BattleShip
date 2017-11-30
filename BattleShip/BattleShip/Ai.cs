using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    abstract class Ai : Player
    {
        public abstract Square MakeMove(Board board);

        public abstract void PlaceShips(Board board);
    }
}
