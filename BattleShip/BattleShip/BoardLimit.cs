using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    /*Author: Jean Claude
     *Summary: Sealed Class that contains limits of board
     * 
     * 
     */
    sealed class BoardLimit
    {
        private int MAX_X_COORDINATE;
        private int MAX_Y_COORDINATE;
        private int MIN_X_COORDINATE;
        private int MIN_Y_COORDINATE;

        private static readonly BoardLimit limit = new BoardLimit(9, 9, 0, 0);

        private BoardLimit(int maxXCoordinate, int maxYCoordinate, int minXCoordinate, int minYCoordinate)
        {
            this.MAX_X_COORDINATE = maxXCoordinate;
            this.MAX_Y_COORDINATE = maxYCoordinate;
            this.MIN_X_COORDINATE = minXCoordinate;
            this.MIN_Y_COORDINATE = minYCoordinate;
        }

        public static BoardLimit Get()
        {
            return BoardLimit.limit;
        }

        public int maxX()
        {
            return this.MAX_X_COORDINATE;
        }

        public int maxY()
        {
            return this.MAX_Y_COORDINATE;
        }

        public int minX()
        {
            return this.MIN_X_COORDINATE;
        }

        public int minY()
        {
            return this.MIN_Y_COORDINATE;
        }
    }
}
