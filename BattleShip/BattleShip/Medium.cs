using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Medium : Ai
    {
        override public Square MakeMove(Board board)
        {
            Square[] available = board.AvailableMoves();
            Square[,] squares = board.AllSquares();
            //WAIT make method that returns position for
            //ships shot and not sunk in BOARD replace those bad methods

            //pick one beside
            for (int i = 0; i < squares.Length; i++)
            {
            }
            //then
            int random = new Random().Next(0, available.Length);
            return available[random];
        }
    

        override public void PlaceShips(Board board)
        {

        }

    }
}
