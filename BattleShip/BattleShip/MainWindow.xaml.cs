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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void goToStartPage()
        {
            NavigationService.Navigate(new StartPage());
        }

        public void goToBoatPlacement(object setting)
        {

        }

        public void goToGame(object boardPlacement)
        {

        }

        public void goToScore(object playerListDB, object lastGame = null)
        {

        }
    }
}
