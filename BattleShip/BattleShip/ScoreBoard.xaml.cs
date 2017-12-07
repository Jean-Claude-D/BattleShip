using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for ScoreBoard.xaml
    /// </summary>
    public partial class ScoreBoard : Page
    {
        ScoreDB db;
        /*Author: Jean-Clause
         *Class that takes all user information that needs to be saved in the leaderboard
         *and saves it.
         * 
         * 
         */ 
        public ScoreBoard(GamePageData previousGame, int score)
        {
            InitializeComponent();

            if(score == 0)
            {
                Narrator.youLost(narrTxt, previousGame.boardPlacementData.GetStartPageData().getPlayerName());
            }
            else
            {
                Narrator.youWin(narrTxt, previousGame.boardPlacementData.GetStartPageData().getPlayerName());
            }

            db = ScoreDB.getDB();

            //Add the previous game to the scores
            db.records.Add(new ScoreRecord(previousGame.boardPlacementData.GetStartPageData().getPlayerName(), score));
            db.records.Sort();

            string scoreTable = "";
            for(int i = 0; i < 10 && i < db.records.Count; i++)
            {
                scoreTable += (i + 1) + ". " + db.records.ElementAt(i).ToString() + "\n";
            }

            msgTxt.Text = scoreTable;
        }

        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            goToStart();
        }

        private void goToStart()
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void quitgame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.exit();
        }

    }
}
