using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BattleShip
{
    
    class Board
    {
        /* 2D array of all the squares */
        private Square[,] squares;

        int available = 100;
        public Grid grid;
        /* ji = (j * 10) + i */
        /* j = (int)(ji / 10)*/
        /* i = ji - j*/
        public Board(Grid grid)
        {
            int numX = BoardLimit.Get().maxX() - BoardLimit.Get().minX() + 1;
            int numY = BoardLimit.Get().maxY() - BoardLimit.Get().minY() + 1;

            this.squares = new Square[numX , numY];
            for(int i = 0; i < numX; i++)
            {
                for(int j = 0; j < numY; j++)
                {
                    this.squares[i, j] = new Square(i, j);
                }
            }

            this.grid = grid;
        }

        /* Returns true if all the Square in this Board's squares are either empty or contain a Ship that is completely sunk 
                   false otherwise*/
        public bool isAllShipSunk()
        {
            foreach(Square boardSquare in this.squares)
            {
                if()
            }
        }

        public void placeShip(Ship toPlace)
        {
            foreach(Square ShipS in toPlace.position)
            {
                for (int i = 0; i < this.squares.GetLength(0); i++)
                {
                    for (int j = 0; j < this.squares.GetLength(1); j++)
                    {
                        if (ShipS.Equals(this.squares[j, i]))
                        {
                            if (this.squares[j, i].isShip())
                            {
                                throw new ArgumentException("Cannot put Ship at :\n" + this.squares[j, i].ToString());
                            }
                            else
                            {
                                this.squares[j, i] = ShipS;
                            }
                        }
                    }
                }
            }
        }

        public bool shoot(Square square)
        {
            foreach(Square s in this.squares)
            {
                if(s.Equals(square))
                {
                    bool temp = s.shoot();
                    available--;
                    return temp;
                }
            }

            throw new ArgumentException(square.ToString() + "\nIs not on board");
        }

        public void updateGrid()
        {
            for (int i = 0; i < this.squares.GetLength(0); i++)
            {
                for (int j = 0; j < this.squares.GetLength(1); j++)
                {
                    bool ship = this.squares[j, i].isShip();
                    bool shot = this.squares[j, i].isShot();

                    if (!ship && !shot)
                    {
                        ((Button)this.grid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Content = "";
                    }
                    else if (!ship && shot)
                    {
                        ((Button)this.grid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Content = "X";
                    }
                    else if (ship && !shot)
                    {
                        ((Button)this.grid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Content = "O";
                    }
                    else
                    {
                        if (this.squares[j, i].hasShipSunk())
                        {
                            ((Button)this.grid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Content = "-";
                        }
                        else
                        {
                            ((Button)this.grid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Content = "Q";
                        }
                    }
                }
            }
        }

        public override String ToString()
        {
            /*[ ] = empty, [X] = shot, [O] = boat, [Q] = boat shot, [-] = boat sunk*/

            String toReturn = "------------------------------\n";

            for (int i = 0; i < this.squares.GetLength(0); i++)
            {
                for (int j = 0; j < this.squares.GetLength(1); j++)
                {
                    bool ship = this.squares[j, i].isShip();
                    bool shot = this.squares[j, i].isShot();

                    if(!ship && !shot)
                    {
                        toReturn += "[ ]";
                    }
                    else if(!ship && shot)
                    {
                        toReturn += "[X]";
                    }
                    else if(ship && !shot)
                    {
                        toReturn += "[O]";
                    }
                    else
                    {
                        if(this.squares[j, i].hasShipSunk())
                        {
                            toReturn += "[-]";
                        }
                        else
                        {
                            toReturn += "[Q]";
                        }
                    }
                }
                toReturn += '\n';
            }
            return toReturn + "------------------------------\n";
        }
        /**
         * Get's available moves for the AI
         * WARNING!! NOT DEEP COPY, BE CAREFUL
         * 
         * @author Karina
         * @returns Square[] available moves
         **/
        public Square[] AvailableMoves()
        {
            Square[] avail = new Square[available];
            int k = 0;

            for (int i = 0; i < this.squares.GetLength(0); i++)
            {
                for (int j = 0; j < this.squares.GetLength(1); j++)
                {
                    if (!this.squares[i, j].isShot())
                    {
                        avail[k] = this.squares[i, j];
                        k++;
                    }
                }
            }
            return avail;
        }

        /**
         * Get's every second available move for the hard AI
         * WARNING!! NOT DEEP COPY, BE CAREFUL
         * 
         * @author Karina
         * @returns Square[] every second available moves
         **/
        public Square[] SecondAvailableMoves()
        {
            Square[] avail = new Square[available / 2];
            int k = 0;

            for (int i = 0; i < this.squares.Length; i++)
            {
                for (int j = i % 2 == 0 ? 0 : 1; j < this.squares.Length; j+=2)
                {
                    if (!this.squares[i, j].isShot())
                    {
                        avail[k] = this.squares[i, j];
                        k++;
                    }
                }
            }
            return avail;
        }

        /**
         * Get's all squares for the AI
         * WARNING!! NOT DEEP COPY, BE CAREFUL
         * 
         * @author Karina
         * @returns Square[]
         **/
        public Square[,] AllSquares()
        {
            return this.squares;
        }

        /**
         * Helper method for AI to know where to shoot
         * Checks if a square has been shot, if there's a ship there 
         * and if it's sunk
         * 
         * @author Karina
         * @return boolean
         **/
        public bool IsShipShotNotSunk(int i, int j)
        {
            return squares[i, j].isShot() && squares[i, j].isShip() && !squares[i, j].hasShipSunk();
        }
    }
}
