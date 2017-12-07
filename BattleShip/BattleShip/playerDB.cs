using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public sealed class PlayerDB
    {
        private string dirPath;
        List<GamePageData> saves;

        private static readonly PlayerDB db = new PlayerDB(@"saves");

        public static PlayerDB getDB()
        {
            return PlayerDB.db;
        }

        /* Instantiates a playerDB that will communication with the file at filePath */
        private PlayerDB(string dirPath)
        {
            if(dirPath == null || dirPath.Trim().Length == 0)
            {
                throw new ArgumentException("dirPath must not be null or empty");
            }

            /* If the directory does not exist, create it */
            if(!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            this.dirPath = dirPath;

            /* Get a listing of all the files in the directory and deserialize each */
            string[] allFiles = Directory.GetFiles(this.dirPath);
            this.saves = new List<GamePageData>();
            foreach(string file in allFiles)
            {
                saves.Add(SerializeUtilities<GamePageData>.Deserialize(file));
            }
        }

        public bool isPlayerExist(string playerName)
        {
            /* Get a listing of all the files in the directory */
            string[] allFiles = Directory.GetFiles(this.dirPath);
            foreach (string file in allFiles)
            {
                /* Remember, files are saved as ('player name' + '.ser') */
                if((this.dirPath + "\\" + playerName + ".ser").Equals(file))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isPlayerSavedInList(string playerName)
        {
            foreach(GamePageData game in this.saves)
            {
                if(game.boardPlacementData.GetStartPageData().getPlayerName().Equals(playerName))
                {
                    return true;
                }
            }
            return false;
        }

        public GamePageData loadGame(string playerName)
        {
            /* Search within the List of GamePageData to find a game with an associated player name that matches playerName*/
            foreach (GamePageData save in this.saves)
            {
                if(save.boardPlacementData.GetStartPageData().getPlayerName().Equals(playerName))
                {
                    return save;
                }
            }

            throw new Exception(playerName + " has no saved game");
        }

        
        public void saveGame(GamePageData save)
        {
            /* If there is already a save, remove it*/
            if(this.isPlayerSavedInList(save.boardPlacementData.GetStartPageData().getPlayerName()))
            {
                this.saves.Remove(save);
            }
            this.saves.Add(save);
        }

        public void saveDB()
        {
            /* Serialize each GamePageData in this.saves */
            foreach (GamePageData save in this.saves)
            {
                /* Saves the GamePageData at save's player's named file */
                SerializeUtilities<GamePageData>.Serialize(save, this.dirPath + "\\" + save.boardPlacementData.GetStartPageData().getPlayerName() + ".ser");
            }
        }
    }
}
