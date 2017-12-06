﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public enum AiLevel
    {
        EASY, MEDIUM, HARD
    }

    abstract class Ai : Player
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
            return null;
        }


    }

    class Easy : Ai
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


    class Medium : Ai
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

    class Hard : Ai
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
