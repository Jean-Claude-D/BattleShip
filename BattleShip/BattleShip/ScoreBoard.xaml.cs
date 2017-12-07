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
        readonly GamePageData gamePageData;
        int score;
        public ScoreBoard(GamePageData previousGame, int score)
        {
            InitializeComponent();
            gamePageData = previousGame;
            this.score = score;
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
