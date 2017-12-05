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
    /// Interaction logic for BoardPlacement.xaml
    /// </summary>
    public partial class BoardPlacement : Page
    {
        public BoardPlacement(Object obj)
        {
            InitializeComponent();
        }

        public void goToGame(object game)
        {
            this.NavigationService.Navigate(new Game(game));
        }

        private void goBtn_Click(object sender, RoutedEventArgs e)
        {
            goToGame(new object());
        }
        
        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            goToStart();
        }

        private void goToStart()
        {
            this.NavigationService.Navigate(new StartPage());
        }
    }
}
