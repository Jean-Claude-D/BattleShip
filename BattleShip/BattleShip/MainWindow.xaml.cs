using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            goToStartPage();
        }

        public static void exit()
        {
            PlayerDB.getDB().saveDB();
            System.Windows.Application.Current.Shutdown();
        }

        public void goToStartPage()
        {
            mainFrame.NavigationService.Navigate(new StartPage());
        }

        public void goToGame(BoardPlacementData boardPlacement)
        {
            mainFrame.NavigationService.Navigate(new Game(boardPlacement));
        }

        public void goToScore(object playerListDB, object lastGame = null)
        {

        }
    }
}
