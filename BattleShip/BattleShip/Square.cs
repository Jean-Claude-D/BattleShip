using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Square
    {
        private readonly int xCoordinate;
        private readonly int yCoordinate;
        private Ship shipThere;
        private bool hasBeenShot = false;

        public Square(int xCoordinate, int yCoordinate) : this(xCoordinate, yCoordinate, null) { }

        public Square(int xCoordinate, int yCoordinate, Ship shipThere)
        {
            if(xCoordinate > BoardLimit.Get().maxX() || xCoordinate < BoardLimit.Get().minX())
            {
                throw new ArgumentException("The xCoordinate parameter (" + xCoordinate + ") must be inclusively between "
                    + BoardLimit.Get().minX() + " and " + BoardLimit.Get().maxX());
            }
            else if (yCoordinate > BoardLimit.Get().maxY() || yCoordinate < BoardLimit.Get().minY())
            {
                throw new ArgumentException("The yCoordinate parameter (" + yCoordinate + ") must be inclusively between "
                    + BoardLimit.Get().minY() + " and " + BoardLimit.Get().maxY());
            }
            else
            {
                this.xCoordinate = xCoordinate;
                this.yCoordinate = yCoordinate;
                this.hasBeenShot = false;
                this.shipThere = shipThere;
            }
        }

        public Square(Square square)
        {
            this.xCoordinate = square.xCoordinate;
            this.yCoordinate = square.yCoordinate;
            this.shipThere = square.shipThere;
            this.hasBeenShot = square.hasBeenShot;
        }

        public Square placeShipOnto(Ship ship)
        {
            Square toReturn = new Square(this);
            toReturn.shipThere = ship;
            return toReturn;
        }

        public int getX()
        {
            return this.xCoordinate;
        }

        public int getY()
        {
            return this.yCoordinate;
        }

        public bool Equals(Square obj)
        {
            return this.xCoordinate == obj.xCoordinate && this.yCoordinate == obj.yCoordinate;
        }

        public bool shoot()
        {
            if(hasBeenShot)
            {
                throw new NotSupportedException("Cannot shoot an already shot Square");
            }

            this.hasBeenShot = true;

            if (isShip())
            {
                return this.shipThere.getShot(this);
            }
            else
            {
                return false;
            }
        }

        public bool isShot()
        {
			return this.hasBeenShot;
        }

        public bool isShip()
        {
            return !(this.shipThere == null);
        }

        public override String ToString()
        {
            return "\n\n" +this.xCoordinate + "," + this.yCoordinate + " :\n"
                + ((this.isShip())?(""):("No ")) + "Ship there\n"
                + "Has " + ((this.hasBeenShot)?(""):("not ")) + "been shot";
        }

        public bool hasShipSunk()
        {
            if(!this.isShip())
            {
                throw new NotSupportedException("There is no ship there");
            }
            return this.shipThere.getIsSunk();
        }
    }
}
