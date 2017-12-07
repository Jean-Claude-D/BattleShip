using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class ScoreRecord : IComparable
    {
        public string playerName;
        public int score;

        public ScoreRecord(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }

        public override string ToString()
        {
            return "[" + playerName + "] : " + score + ((this.score > 1) ? (" pts") : (" pt"));
        }

        public int CompareTo(object obj)
        {
            if(this.score > ((ScoreRecord)obj).score)
            {
                return 1;
            }
            else if(this.score == ((ScoreRecord)obj).score)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }

    public sealed class ScoreDB
    {
        private string fileName;
        public List<ScoreRecord> records;

        private static readonly ScoreDB db = new ScoreDB(@"scores.ser");

        public static ScoreDB getDB()
        {
            return ScoreDB.db;
        }

        /* Instantiates a ScoreDB that will communication with the file at filePath */
        private ScoreDB(string fileName)
        {
            if (fileName == null || fileName.Trim().Length == 0)
            {
                throw new ArgumentException("fileName must not be null or empty");
            }

            this.fileName = fileName;

            if(File.Exists(fileName))
            {
                records = SerializeUtilities<List<ScoreRecord>>.Deserialize(fileName);
            }
            else
            {
                records = new List<ScoreRecord>();
            }
        }

        public void saveDB()
        {
            SerializeUtilities<List<ScoreRecord>>.Serialize(this.records, this.fileName);
        }

    }
}