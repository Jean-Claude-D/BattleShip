using System;
using System.Windows;
using System.Windows.Controls;

namespace BattleShip
{
    /// <summary>
    /// Interaction logic for BoardPlacement.xaml
    /// </summary>
    public partial class BoardPlacement : Page
    {
        readonly StartPageData startPageData;

        /* TO REPLACE WITH SIMON'S SHIP PLACEMENT METHODS */
        Ship[] aiShips;
        Ship[] playerShips;

        public BoardPlacement(StartPageData startPageData)
        {
            /* Saving startPageData from previous StartPage while the user is choosing his ships and settings*/
            this.startPageData = startPageData;

            InitializeComponent();

            /* TO REPLACE WITH SIMON'S SHIP PLACEMENT METHODS */
            aiShips = new Ship[5];
            aiShips[0] = new Ship(new Square[] { new Square(0, 1), new Square(0, 2) });
            aiShips[1] = new Ship(new Square[] { new Square(5, 6), new Square(6, 6), new Square(7, 6) });
            aiShips[2] = new Ship(new Square[] { new Square(9, 9), new Square(8, 9), new Square(7, 9), new Square(6, 9) });
            aiShips[3] = new Ship(new Square[] { new Square(3, 4), new Square(3, 3), new Square(3, 2), new Square(3, 1) });
            aiShips[4] = new Ship(new Square[] { new Square(1, 8), new Square(1, 7), new Square(1, 6), new Square(1, 5), new Square(1, 4) });

            /* TO REPLACE WITH SIMON'S SHIP PLACEMENT METHODS */
            playerShips = new Ship[5];
            playerShips[0] = new Ship(new Square[] { new Square(0, 1), new Square(0, 2) });
            playerShips[1] = new Ship(new Square[] { new Square(5, 6), new Square(6, 6), new Square(7, 6) });
            playerShips[2] = new Ship(new Square[] { new Square(9, 9), new Square(8, 9), new Square(7, 9), new Square(6, 9) });
            playerShips[3] = new Ship(new Square[] { new Square(3, 4), new Square(3, 3), new Square(3, 2), new Square(3, 1) });
            playerShips[4] = new Ship(new Square[] { new Square(1, 8), new Square(1, 7), new Square(1, 6), new Square(1, 5), new Square(1, 4) });
        }

        public void goToGame(BoardPlacementData boardPlacementData)
        {
            this.NavigationService.Navigate(new Game(boardPlacementData));
        }

        private void goBtn_Click(object sender, RoutedEventArgs e)
        {
            AiLevel currSelectionLevel = (AiLevel)Enum.Parse(typeof(AiLevel), ((string)((ComboBoxItem)levelCBox.SelectedItem).Content).ToUpper());
            int currSelectionTime = int.Parse((string)((ComboBoxItem)timerCBox.SelectedItem).Content));

            goToGame(new BoardPlacementData(this.startPageData, this.playerShips, this.aiShips, currSelectionLevel, currSelectionTime));
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
