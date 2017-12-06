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
using System.Windows.Threading;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        readonly BoardPlacementData boardPlacementData;
        DispatcherTimer idleTimeDispatcher;
        private int currIdleTimeLeft;

        private int currTimeSec; //With the DispatcherTimer
        private int currTimeMin; //With the DispatcherTimer
        private int currTimeHour; //With the DispatcherTimer

        private int turnCount; //incremented in aiMove, because AI will always play

        /*The Board the player shoots on*/
        Board playerBoard;
        /*The Board the ai shoots on*/
        Board aiBoard;

        Ai ai;

        public Game(GamePageData gamePageData)
        {
            InitializeComponent();

            /* get time from gamePageData */
            startMainTimer(0, 0, 50);
            initPlayerTicker(2);
            playerBoard = new Board(new Grid());
            aiBoard = new Board(new Grid());
            ai = new Easy();

            /* Attach both board to the correct Grid objects */
            /* Because after serialization, Grid are not shallow copied */
            playerBoard.grid = battleGrid;
            aiBoard.grid = battleGrid_Copy;
        }

        public Game(BoardPlacementData boardPlacementData)
        {
            this.boardPlacementData = boardPlacementData;

            InitializeComponent();

            startMainTimer(0, 0, 0);
            initPlayerTicker(boardPlacementData.getIdleTime());

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
            idleTimeDispatcher.Start();
        }

        private void initPlayerTicker(int LeftidleTime)
        {
            idleTimeDispatcher = new System.Windows.Threading.DispatcherTimer();
            idleTimeDispatcher.Tick += new EventHandler(playerTurn_Tick);
            /* Increment timer each second */
            idleTimeDispatcher.Interval = new TimeSpan(0, 0, 1);

            /* Initializes the idleTime counter*/
            this.currIdleTimeLeft = LeftidleTime;
        }

        private void playerTurn_Tick(object sender, EventArgs e)
        {
            /* If the user runs out of time... */
            if(--this.currIdleTimeLeft <= 0)
            {
                /* ... Let the AI play*/
                aiMove();
                /* Reset the idleTime counter */
                this.currIdleTimeLeft = boardPlacementData.getIdleTime();
            }
        }

        /* Starts the main game timer with the specified value */
        private void startMainTimer(int initTimeSec, int initTimeMin, int initTimeHour)
        {
            this.currTimeSec = initTimeSec;
            this.currTimeMin = initTimeMin;
            this.currTimeHour = initTimeHour;

            /* Start another thread to keep track of time without blocking the UI */
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            /* Increment timer each second */
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer.Content = getTime();
        }

        private string getTime()
        {
            if (currTimeSec >= 60)
            {
                currTimeSec = 0;
                currTimeMin++;
            }
            if (currTimeMin >= 60)
            {
                currTimeMin = 0;
                currTimeHour++;
            }
            string toReturn = ((currTimeHour != 0) ? (currTimeHour + " h ") : ("")) +
                ((currTimeMin != 0) ? (currTimeMin + " min ") : ("")) +
                ((currTimeSec != 0) ? (currTimeSec + " sec") : (""));

            currTimeSec++;

            return toReturn;
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
            idleTimeDispatcher.Stop();
            battleGrid.IsEnabled = false;
            aiBoard.shoot(ai.MakeMove(aiBoard));
            aiBoard.updateGrid();
            turnCount++;
            battleGrid.IsEnabled = true;
            idleTimeDispatcher.Start();
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
