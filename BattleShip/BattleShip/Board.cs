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
        private Square[] squares;

        /* ji = (j * 10) + i */
        public Board()
        {
            int numX = BoardLimit.Get().maxX() - BoardLimit.Get().minX() + 1;
            int numY = BoardLimit.Get().maxY() - BoardLimit.Get().minY() + 1;

            this.squares = new Square[numX * numY];
            for(int i = 0; i < numX; i++)
            {
                for(int j = 0; j < numY; j++)
                {
                    this.squares[(j * 10) + i] = new Square(i, j);
                }
            }
        }

        public void placeShip(Ship toPlace)
        {
            foreach(Square ShipS in toPlace.position)
            {
                for(int i = 0; i < this.squares.Length; i++)
                {
                    if(ShipS.Equals(this.squares[i]))
                    {
                        if(this.squares[i].isShip())
                        {
                            throw new ArgumentException("Cannot put Ship at :\n" + this.squares[i].ToString());
                        }
                        else
                        {
                            this.squares[i] = ShipS;
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
                    return s.shoot();
                }
            }

            throw new ArgumentException(square.ToString() + "\nIs not on board");
        }
    }
}
