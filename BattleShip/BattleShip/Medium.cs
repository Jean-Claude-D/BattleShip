using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Medium : Ai
    {
        /**
         * Medium level AI checks every square to see if an ships are hit but not sunk, 
         * and picks a square around it  
         * 
         * @author karina
         **/ 
        override public Square MakeMove(Board board)
        {
            Square[] available = board.AvailableMoves();
            Square[,] squares = board.AllSquares();
            
            //SIMPLIFY
            for (int i = 0; i < squares.Length; i++)
            {
                for (int j = 0; j < squares.Length; j++)
                {
                    if (board.IsShipShotNotSunk(i, j))
                    {                        
                        if (board.IsShipShotNotSunk(i + 1, j))
                        {
                            int index = i + 2;
                            while (board.IsShipShotNotSunk(index, j))
                            {
                                index++;
                            }
                            return squares[index, j];
                        }

                        if (board.IsShipShotNotSunk(i - 1, j))
                        {
                            int index = i - 2;
                            while (board.IsShipShotNotSunk(index, j))
                            {
                                index--;
                            }
                            return squares[index, j];
                        }

                        if (board.IsShipShotNotSunk(i, j + 1))
                        {
                            int index = j + 2;
                            while (board.IsShipShotNotSunk(i, index))
                            {
                                index++;
                            }
                            return squares[i, index];
                        }
                        if (board.IsShipShotNotSunk(i, j - 1))
                        {
                            int index = j - 2;
                            while (board.IsShipShotNotSunk(i, index))
                            {
                                index--;
                            }
                            return squares[i, index];
                        }
                    }
                }
            }
            int random = new Random().Next(0, available.Length);
            return available[random];
        }

        override public void PlaceShips(Board board)
        {

        }
    }
}
