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
        readonly BoardPlacementData boardPlacementData;
        private int currIdleTimeLeft;
        private int currTime; //With the DispatcherTimer
        private int turnCount; //incremented in aiMove, because AI will always play

        /*The Board the player shoots on*/
        Board playerBoard;
        /*The Board the ai shoots on*/
        Board aiBoard;

        Ai ai;

        public Game(GamePageData gamePageData)
        {
            InitializeComponent();
        }

        public Game(BoardPlacementData boardPlacementData)
        {
            this.boardPlacementData = boardPlacementData;

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

            switch(boardPlacementData.getLevel())
            {
                case AiLevel.EASY:
                    ai = new Easy();
                    break;
                case AiLevel.MEDIUM:
                    ai = new Medium();
                    break;
                case AiLevel.HARD:
                    ai = new Hard();
                    break;
                default:
                    throw new NotSupportedException("Ai unknown");
            }

            aiBoard = new Board(battleGrid_Copy);
            battleGrid_Copy.IsEnabled = false;
            for (int i = 0; i < boardPlacementData.getAiShip().Length; i++)
            {
                aiBoard.placeShip(boardPlacementData.getAiShip()[i]);
            }

            playerBoard = new Board(battleGrid);
            for (int i = 0; i < boardPlacementData.getPlayerShip().Length; i++)
            {
                playerBoard.placeShip(boardPlacementData.getPlayerShip()[i]);
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            checkWin();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).Equals((Button)sender))
                    {
                        playerBoard.shoot(new Square(j, i));

                        ((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i && Grid.GetColumn(f) == j)).IsEnabled = false;

                        playerBoard.updateGrid();

                        aiMove();
                    }
                }
            }
            checkWin();
        }

        /* The ai plays a move can be called by Button_Click and when the user's idle time is over */
        private void aiMove()
        {
            battleGrid.IsEnabled = false;
            aiBoard.shoot(ai.MakeMove(aiBoard));
            aiBoard.updateGrid();
            turnCount++;
            battleGrid.IsEnabled = true;
        }

        /* Checks the winning condition for each Board */
        private void checkWin()
        {
            if(aiBoard.isAllShipSunk() || playerBoard.isAllShipSunk())
            {
                /* This is temp, will need to go to score page */
                MessageBox.Show("Win");
            }
        }

        private void Quit()
        {
            GamePageData gamePageData = new GamePageData();
        }

        private void goToStart()
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void goToBoatPlacement()
        {
            this.NavigationService.Navigate(new BoardPlacement(this.boardPlacementData.GetStartPageData()));
        }

        private void reset()
        {
            this.NavigationService.Navigate(new Game(this.boardPlacementData));
        }

        private void goToScore(object playerListDB, object game = null)
        {
            this.NavigationService.Navigate(new ScoreBoard(playerListDB, game));
        }
    }
}
