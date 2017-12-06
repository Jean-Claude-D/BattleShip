using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Ship
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
            return positions + " Is " + ((this.isSunk)?(" sunk "):(" alive "));
        }

    }
}
