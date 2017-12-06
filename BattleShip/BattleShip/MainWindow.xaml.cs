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
        Board aiBoard;
        Board playerBoard;
        Player ai;

        int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
<<<<<<< HEAD
            goToStartPage();
=======

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button inGrid = new Button();
                    inGrid.Click += Button_Click;

                    battleGrid.Children.Add(inGrid);
                    Grid.SetRow(inGrid, i);
                    Grid.SetColumn(inGrid, j);
                }
            }

<<<<<<< HEAD
            timeSec++;

            return toReturn;
>>>>>>> Let me out!
=======
            aiBoard = new Board(battleGrid);
            playerBoard = new Board(aiGrid);
>>>>>>> Commiting rn mates
        }

        public void goToStartPage()
        {
            mainFrame.NavigationService.Navigate(new StartPage());
        }

        public void goToGame(object boardPlacement)
        {
            mainFrame.NavigationService.Navigate(new Game(boardPlacement));
        }

        public void goToScore(object playerListDB, object lastGame = null)
        {

        }
<<<<<<< HEAD
	}
=======

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Equals((Button)sender))
                    {
                        try
                        {
                            aiBoard.shoot(new Square(j, i));

                            ((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).IsEnabled = false;

                            aiBoard.updateGrid();

                            battleGrid.IsEnabled = false;
                            playerBoard.shoot(ai.MakeMove(playerBoard));
                            battleGrid.IsEnabled = true;
                        }
                        catch(Exception exc)
                        {
                            testLbl.Content = "Please try again";
                        }
                    }
                }
            }
        }

        private void simulateMove()
        {
            for (int i = 0; i < 99; i++) for (int j = 0; j < 99; j++) for (int k = 0; k < 99; k++) ;
        }

        private void gridBtn_Click(object sender, RoutedEventArgs e)
        {
            battleGrid.IsEnabled = !battleGrid.IsEnabled;
            if(battleGrid.IsEnabled)
            {
                gridBtn.Content = "Disable";
            }
            else
            {
                gridBtn.Content = "Enable";
            }
        }
    }
>>>>>>> Let me out!
}
