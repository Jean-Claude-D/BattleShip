using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    [Serializable]
    public sealed class BoardPlacementData
    {
        private readonly StartPageData startPageData;

        private readonly Ship[] playerShips;
        private readonly Ship[] aiShips;

        private readonly AiLevel level;

        private readonly int idleTime;

        /* Wraps around a StartPageData object (adding additional information) */
        public BoardPlacementData(StartPageData startPageData, Ship[] playerShips, Ship[] aiShips, AiLevel level, int idleTime)
        {
            /* Checking nullity and idleTime */
            if(startPageData == null)
            {
                throw new ArgumentException("startPageData cannot be null");
            }
            else if(startPageData == null || startPageData == null)
            {
                throw new ArgumentException("both Ship[] cannot be null");
            }
            else if(idleTime < 1)
            {
                throw new ArgumentException("idleTime should be at least 1");
            }

            /* Verify if both arrays are filled to capacity with Ship objects and deep-copy each Ship at the same time */
            this.playerShips = new Ship[playerShips.Length];
            for(int i = 0; i < playerShips.Length; i++)
            {
                if(playerShips[i] == null)
                {
                    throw new ArgumentException("Ship at " + i + " cannot be null in playerShips");
                }
                /* Deep-copy */
                this.playerShips[i] = new Ship(playerShips[i]);
            }
            this.aiShips = new Ship[aiShips.Length];
            for (int i = 0; i < aiShips.Length; i++)
            {
                if (aiShips[i] == null)
                {
                    throw new ArgumentException("Ship at " + i + " cannot be null in aiShips");
                }
                /* Deep-copy */
                this.aiShips[i] = new Ship(aiShips[i]);
            }

            this.startPageData = startPageData;
            this.level = level;
            this.idleTime = idleTime;
        }

        public StartPageData GetStartPageData()
        {
            return this.startPageData;
        }

        /* Because we want to be able to restart with original Ship positions, the Ships in BoardPlacementData msut not change at all*/
        public Ship[] getPlayerShip()
        {
            Ship[] toReturn = new Ship[this.playerShips.Length];
            for (int i = 0; i < this.playerShips.Length; i++)
            {
                /* Deep-copy to prevent any change to playerShips */
                toReturn[i] = new Ship(this.playerShips[i]);
            }
            return toReturn;
        }

        /* Because we want to be able to restart with original Ship positions, the Ships in BoardPlacementData msut not change at all*/
        public Ship[] getAiShip()
        {
            Ship[] toReturn = new Ship[this.aiShips.Length];
            for (int i = 0; i < this.aiShips.Length; i++)
            {
                /* Deep-copy to prevent any change to aiShips */
                toReturn[i] = new Ship(this.aiShips[i]);
            }
            return toReturn;
        }

        public AiLevel getLevel()
        {
            return this.level;
        }

        public int getIdleTime()
        {
            return this.idleTime;
        }

    }
}
