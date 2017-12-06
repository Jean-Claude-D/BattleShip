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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        /*The Board the player shoots on*/
        Board playerBoard;
        Board aiBoard;
        Player ai;

        public Game(Object obj)
        {
            InitializeComponent();

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

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button inGrid = new Button();

                    battleGrid_Copy.Children.Add(inGrid);
                    Grid.SetRow(inGrid, i);
                    Grid.SetColumn(inGrid, j);
                }
            }

            ai = new Easy();
            aiBoard = new Board(battleGrid_Copy);
            playerBoard = new Board(battleGrid);

            aiBoard.placeShip(new Ship(new Square[] { new Square(0, 1), new Square(0, 2) }));
            aiBoard.placeShip(new Ship(new Square[] { new Square(5, 6), new Square(6, 6), new Square(7, 6) }));
            aiBoard.placeShip(new Ship(new Square[] { new Square(9, 9), new Square(8, 9) , new Square(7, 9) , new Square(6, 9) }));
            aiBoard.placeShip(new Ship(new Square[] { new Square(3, 4), new Square(3, 3) , new Square(3, 2) , new Square(3, 1) }));
            aiBoard.placeShip(new Ship(new Square[] { new Square(1, 8), new Square(1, 7), new Square(1, 6), new Square(1, 5) , new Square(1, 4) }));

            playerBoard.placeShip(new Ship(new Square[] { new Square(0, 1), new Square(0, 2) }));
            playerBoard.placeShip(new Ship(new Square[] { new Square(5, 6), new Square(6, 6), new Square(7, 6) }));
            playerBoard.placeShip(new Ship(new Square[] { new Square(9, 9), new Square(8, 9), new Square(7, 9), new Square(6, 9) }));
            playerBoard.placeShip(new Ship(new Square[] { new Square(3, 4), new Square(3, 3), new Square(3, 2), new Square(3, 1) }));
            playerBoard.placeShip(new Ship(new Square[] { new Square(1, 8), new Square(1, 7), new Square(1, 6), new Square(1, 5), new Square(1, 4) }));


            battleGrid_Copy.IsEnabled = false;

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Equals((Button)sender))
                    {
                        playerBoard.shoot(new Square(j, i));

                        ((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).IsEnabled = false;

                        playerBoard.updateGrid();

                        battleGrid.IsEnabled = false;
                        aiBoard.shoot(ai.MakeMove(aiBoard));
                        aiBoard.updateGrid();
                        battleGrid.IsEnabled = true;
                    }
                }
            }
        }

        private void goToStart()
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void goToBoatPlacement(object setting)
        {
            this.NavigationService.Navigate(new BoardPlacement(setting));
        }

        private void reset(object game)
        {
            this.NavigationService.Navigate(new Game(game));
        }

        private void goToScore(object playerListDB, object game = null)
        {
            this.NavigationService.Navigate(new ScoreBoard(playerListDB, game));
        }
    }
}
