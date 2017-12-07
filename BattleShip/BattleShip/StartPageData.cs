using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    /* Holds the data collected by StartPage, will be passed onto BoardPlacement*/
    public sealed class StartPageData
    {
        private readonly string playerName;

        public StartPageData(string playerName)
        {
            /* A null, or empty or whitespaces filled string is rejected */
            if (playerName == null || playerName.Trim().Length == 0 || playerName.Length > 20)
            {
                this.playerName = "Mr. No Name";

            }
            else
            {
                this.playerName = playerName;
            }

        }

        /* Getter for playerName */
        public string getPlayerName()
        {
            return this.playerName;
        }
    }
}
