using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    [Serializable]
    public enum AiLevel
    {
        EASY, MEDIUM, HARD
    }

    [Serializable]
    public abstract class Ai : Player
    {
        public abstract Square MakeMove(Board board);

        public Square randomMove(Board board)
        {
            Square[] available = board.AvailableMoves();
            int random = new Random().Next(0, available.Length);
            return available[random];
        }
    
        public Square smartRandomMove(Board board)
        {
            Square[] available = board.SecondAvailableMoves();
            int random = new Random().Next(0, available.Length);
            return available[random];
        }

        public Square finishShip(Board board)
        {
            Square[,] squares = board.AllSquares();
            //SIMPLIFY
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    if (board.IsShipShotNotSunk(i, j))
                    {
                        if (i + 1 < squares.GetLength(0) && board.IsShipShotNotSunk(i + 1, j))
                        {
                            int index = i + 2;
                            while (index < squares.Length && board.IsShipShotNotSunk(index, j))
                            {
                                index++;
                            }
                            return squares[index, j];
                        }

                        if (i - 1 > 0 && board.IsShipShotNotSunk(i - 1, j))
                        {
                            int index = i - 2;
                            while (index > 0 && board.IsShipShotNotSunk(index, j))
                            {
                                index--;
                            }
                            return squares[index, j];
                        }

                        if (j + 1 < squares.GetLength(1) && board.IsShipShotNotSunk(i, j + 1))
                        {
                            int index = j + 2;
                            while (index < squares.Length && board.IsShipShotNotSunk(i, index))
                            {
                                index++;
                            }
                            return squares[i, index];
                        }
                        if (j - 1 > 0 && board.IsShipShotNotSunk(i, j - 1))
                        {
                            int index = j - 2;
                            while (index > 0 && board.IsShipShotNotSunk(i, index))
                            {
                                index--;
                            }
                            return squares[i, index];
                        }
                    }
                }
            }
            return null;
        }


    }

    [Serializable]
    public class Easy : Ai
    {
        /**
         * The easy AI pick a move randomly
         * 
         * @author Karina
         **/
        override public Square MakeMove(Board board)
        {
            return randomMove(board);
        }
    }

    [Serializable]
    public class Medium : Ai
    {
        /**
         * Medium level AI checks every square to see if an ships are hit but not sunk, 
         * and picks a square around it  
         * 
         * If there's no ships to sink it shoots randomly
         * 
         * @author karina
         **/
        override public Square MakeMove(Board board)
        {
            Square temp = finishShip(board);
            if (temp == null)
            {
                return randomMove(board);
            } 
            return temp;
        }
    }

    [Serializable]
    public class Hard : Ai
    {
        /**
         * Hard level AI checks every square to see if an ships are hit but not sunk, 
         * and picks a square around it. 
         * 
         * If there's no ships to sink it randomly shoots only "white" or "black" square
         * if we pretend the board is a checkerboard.
         * 
         * @author karina
         **/
        override public Square MakeMove(Board board)
        {
            Square temp = finishShip(board);
            if (temp == null)
            {
                return smartRandomMove(board);
            }
            return temp;
        }
    }
}

