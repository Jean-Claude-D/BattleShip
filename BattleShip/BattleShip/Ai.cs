using System;
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

        /**
         * 
         **/ 
        public Square findShip(Board board)
		{
			Square[,] squares = board.AllSquares();
			for (int i = 0; i < squares.GetLength(0); i++)
			{
				for (int j = 0; j < squares.GetLength(1); j++)
				{
					if (board.IsShipShotNotSunk(i, j)) return squares[i, j];
				}
			}
			return null;
		}

        /**
         * 
         **/
        /*   public Square finishShip(Board board)
			   {
				   Square[,] squares = board.AllSquares();

				   //SIMPLIFY
				   for (int i = 0; i < squares.GetLength(0); i++)
				   {
					   //Console.Write(squares[i, i].ToString());
					   for (int j = 0; j < squares.GetLength(1); j++)
					   {
						   //Console.Write(squares[i, j].ToString());
						   if (board.IsShipShotNotSunk(i, j))
						   {
							   int indexi = i;
							   int indexj = j;
							   //Console.Write(squares[i, j].ToString());

							   if (i + 1 < squares.GetLength(0) && board.IsShipShotNotSunk(i + 1, j))
							   {
								   indexi = i + 1;
								   while (indexi + 1 < squares.GetLength(0) && board.IsShipShotNotSunk(indexi - 1, j))
								   {
									   Console.Write(squares[indexi, j].ToString());
									   indexi++;
								   }                           
								   return squares[indexi, j];
							   }
							   if (indexi != i || indexj != j)
							   {
								   return squares[indexi, indexj];
							   }

							   if (i - 1 >= 0 && board.IsShipShotNotSunk(i - 1, j))
							   {
								   indexi = i - 1;
								   while (indexi - 1 >= 0 && board.IsShipShotNotSunk(indexi - 1, j))
								   {
									   indexi--;
								   }
								   return squares[indexi, j];
							   }

							   if (indexi != i || indexj != j)
								   {
									   return squares[indexi, indexj];
								   }

							   if (j + 1 < squares.GetLength(1) && board.IsShipShotNotSunk(i, j + 1))

							   {
								   indexj = j + 1;

								   while (indexj + 1 < squares.GetLength(1) && board.IsShipShotNotSunk(i, indexj + 1))
								   {

									   indexj++;

								   }

								   return squares[i, indexj];
							   }


							   if (indexi != i || indexj != j)
									   {
										   return squares[indexi, indexj];
									   }

							   if (j - 1 >= 0 && board.IsShipShotNotSunk(i, j - 1))

							   {

								   indexj = j - 1;

								   while (indexj - 1 >= 0 && board.IsShipShotNotSunk(i, indexj - 1))

								   {

									   indexj--;

								   }

								   return squares[i, indexj];

							   }
							   if (indexi != i || indexj != j)
										   {
											   return squares[indexi, indexj];
										   }

						   }

					   }

				   }         

			   return null;
			   }
			   */
        public Square finishShip(Board board)
        {
            Square ship = findShip(board);
            if (ship == null) return ship;

            int i = ship.getX();
            int j = ship.getY();

            Square[,] squares = board.AllSquares();


            Square square = isShotNext(board, i, j, 1, 0);
     
            if (square == null)
            {
                square = isShotNext(board, i, j, -1, 0);
                
                if (square == null)
                {
                    square = isShotNext(board, i, j, 0, 1);
                    
                    if (square == null)
                    {
                        square = isShotNext(board, i, j, 0, -1);

                        if (square == null)
                        {
                            square = NoShipNext(board, i, j);
                        }
                        else if (square.Equals(squares[i, j]))
                        {
                            square = helper(board, i, j, 0, -1);
                        }
                    }
                    else if (square.Equals(squares[i, j]))
                    {
                        square = helper(board, i, j, 0, -1);
                    }
                }
                else if (square.Equals(squares[i, j]))
                {
                    square = helper(board, i, j, 0, 1);
                }
            }
            else if (square.Equals(squares[i, j]))
            {
                square = helper(board, i, j, -1, 0);
            }
            return square;        
        }

       // helper(board, i + addi, j + addj, addi, addj);
        /**
         * 
         **/
        public Square isShotNext(Board board, int i, int j, int addi, int addj)
        {
            Square[,] squares = board.AllSquares();
            if (board.IsShipShotNotSunk(i, j)
                   && (i + addi < 10) && (j + addj < 10)
                   && (i + addi >= 0) && (j + addj >= 0)

                   && board.IsShipShotNotSunk(i + addi, j + addj))
            {
                return squares[i,j];
            }
            return null;
        }
        public Square helper(Board board, int i, int j, int addi, int addj)
        {
            Square[,] squares = board.AllSquares();
            while (board.IsShipShotNotSunk(i, j)
				   && (i + addi < 10) && (j + addj < 10)
				   && (i + addi >= 0) && (j + addj >= 0)
				   && !(squares[i + addi, j + addj].isShot()))
			{
				i = i + addi;
				j = j + addj;
            }
            //if (!squares[i, j].isShot()) return null;

            return squares[i, j];
        }

        public Square NoShipNext(Board board, int i, int j)
        {
            Square[,] squares = board.AllSquares();
            Square square;
            if ((i + 1) >= squares.GetLength(0))
            {
                square = helper(board, i, j, -1, 0);
                if (square != null) return square;
            }
            if ((j + 1) >= squares.GetLength(1))
            {
                square = helper(board, i, j, 0, -1);
                if (square != null) return square;
            }
            if ((i - 1) < 0)
            {
                square = helper(board, i, j, 1, 0);
                if (square != null) return square;
            }
            if ((j - 1) < 0)
            {
                square = helper(board, i, j, 0, 1);
                if (square != null) return square;
            }
            return helper(board, i, j, 0, 1);
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


