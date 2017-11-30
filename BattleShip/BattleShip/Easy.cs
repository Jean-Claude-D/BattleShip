using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Easy : Ai
    {
        /**
         * The easy AI pick a move randomly
         * 
         * @author Karina
         **/ 
        override public Square MakeMove(Board board)
        {
            Square[] available = board.AvailableMoves();
            int random = new Random().Next(0, available.Length);
            return available[random];
        }

        override public void PlaceShips(Board board)
        {

        }
    }
}
