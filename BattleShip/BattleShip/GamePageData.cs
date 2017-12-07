using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    [Serializable]
    /* Groups all data necessary to save&load a game of battleship */
    public sealed class GamePageData
    {
        public readonly BoardPlacementData boardPlacementData;

        private int currTimeSec; //With the DispatcherTimer
        private int currTimeMin; //With the DispatcherTimer
        private int currTimeHour; //With the DispatcherTimer

        private int turnCount;

        /*The Board the player shoots on*/
        public readonly Board playerBoard;
        /*The Board the ai shoots on*/
        public readonly Board aiBoard;

        public readonly Ai ai;

        public GamePageData(BoardPlacementData boardPlacementData, int currTimeSec, int currTimeMin, int currTimeHour, int turnCount, Board playerBoard, Board aiBoard, Ai ai)
        {
            /* Verify nullity and negativity of parameters */
            if(boardPlacementData == null)
            {
                throw new ArgumentException("boardPlacementData cannot be null");
            }
            else if(currTimeSec < 0 || currTimeMin < 0 || currTimeHour < 0)
            {
                throw new ArgumentException("One of the time counter is negative, they should all be positive");
            }
            else if(turnCount < 0)
            {
                throw new ArgumentException("the number of turns cannot be negative");
            }
            else if(playerBoard == null || aiBoard == null)
            {
                throw new ArgumentException("Both boards should not be null");
            }
            else if(ai == null)
            {
                throw new ArgumentException("the ai cannot be null");
            }

            this.boardPlacementData = boardPlacementData;
            this.currTimeHour = currTimeHour;
            this.currTimeMin = currTimeMin;
            this.currTimeSec = currTimeSec;
            this.turnCount = turnCount;
            this.playerBoard = playerBoard;
            this.aiBoard = aiBoard;
            this.ai = ai;
        }

        /* All getters and setters */

        public void setTime(int currTimeSec, int currTimeMin, int currTimeHour)
        {
            if (currTimeSec < 0 || currTimeMin < 0 || currTimeHour < 0)
            {
                throw new ArgumentException("One of the time counter is negative, they should all be positive");
            }

            this.currTimeSec = currTimeSec;
            this.currTimeMin = currTimeMin;
            this.currTimeHour = currTimeHour;
        }

        public int getTimeSec()
        {
            return this.currTimeSec;
        }

        public int getTimeMin()
        {
            return this.currTimeMin;
        }

        public int getTimeHour()
        {
            return this.currTimeHour;
        }

        public void setTurnCount(int turnCount)
        {
            if (turnCount < 0)
            {
                throw new ArgumentException("the number of turns cannot be negative");
            }

            this.turnCount = turnCount;
        }

        public int getTurnCount()
        {
            return this.turnCount;
        }
    }
}
