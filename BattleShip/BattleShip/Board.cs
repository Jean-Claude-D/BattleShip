using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    
    class Board
    {
        /* 2D array of all the squares */
        private Square[,] squares;

        /* ji = (j * 10) + i */
        /* j = (int)(ji / 10)*/
        /* i = ji - j*/
        public Board()
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
        }


		public void placeShip(Ship toPlace)
		{
			foreach (Square ShipS in toPlace.position)
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

		public Boolean isTaken(Square space)
		{
            return this.squares[space.getX(), space.getY()].isShip();
		}

        public bool shoot(Square square)
        {
            foreach(Square s in this.squares)
            {
                if(s.Equals(square))
                {
                    return s.shoot();
                }
            }

            throw new ArgumentException(square.ToString() + "\nIs not on board");
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
                    else if (ship && shot)
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
    }
}
