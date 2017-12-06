using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Ship
    {
        public Square[] position;
        private bool isSunk;

        public Ship(Square[] position)
        {
            if(position.Length < 2)
            {
                throw new ArgumentException("Ship must have at least 2 Squares");
            }

            this.position = new Square[position.Length];
            for(int i = 0; i < this.position.Length; i++)
            {
                this.position[i] = position[i].placeShipOnto(this);
            }

            this.isSunk = false;
        }

        /* Deep copies a Ship, used in BoardPlacementData to make sure that Ship placed on BoardPlacement Page are not the same as in Game*/
        public Ship(Ship toCopy)
        {
            /* Array copying */
            this.position = new Square[toCopy.position.Length];
            for(int i = 0; i < this.position.Length; i++)
            {
                this.position[i] = toCopy.position[i];
            }
        }

        public bool getShot(Square square)
        {
            if(this.isSunk)
            {
                throw new NotSupportedException("Cannot shoot a sunk ship");
            }

            for(int i = 0; i < this.position.Length; i++)
            {
                if(this.position[i].Equals(square))
                {
                    this.updateIsSunk();
                    return this.isSunk;
                }
            }

            throw new ArgumentException("This Ship is not present on square parameter");
        }

        private void updateIsSunk()
        {
            for(int i = 0; i < this.position.Length; i++)
            {
                if(!this.position[i].isShot())
                {
                    return;
                }
            }

            this.isSunk = true;
        }

        public bool getIsSunk()
        {
            return this.isSunk;
        }

        public override string ToString()
        {
            String positions = "";
            foreach(Square s in this.position)
            {
                positions += s.ToString() + "\n";
            }
            return positions + "Is " + ((this.isSunk)?("sunk"):("alive"));
        }

    }
}
