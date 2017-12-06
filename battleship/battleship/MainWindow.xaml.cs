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
        int[] posx = new int[5];
        int[] posy = new int[5];
        int[] length = new int[5];
        int[] width = new int[5];
        int moving = 0; //what ship you're moving
        Board battleboard;
        Ship[] battleships;

        public MainWindow()
        {
            InitializeComponent();
            completeClean();
            battleboard = new Board();
            battleships = new Ship[5];
         
            randomize();
        }
		
     
        /*
        Event: Click
         *Summary:Finds the row and column of a grid space when it's clicked
         * Calls rotation if it's a double click
         */ 
        
        private void click(object sender, MouseButtonEventArgs e)
        {
            var point = Mouse.GetPosition(battleGrid);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in battleGrid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in battleGrid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            if (e.ClickCount == 1) // for double-click, remove this condition if only want single click
            {
                int temp = (findShip(col, row));
                if (temp > -1) moving = temp;
            }
           else if (e.ClickCount == 2) // for double-click, remove this condition if only want single click
           {
                int temp = (findShip(col, row));
				if (temp > -1) rotate(temp);
           }

        }
      
       
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            
            randomize();
        }

        /*Movement event 
		 * Triggered by:  Any key being pressed
		 * Summary: Checks if it's a valid movement,
		 * then erases where it currently is
		 * then draws itself in the next position 
		 * 
		 */
        private void movement(object sender, KeyEventArgs e)
        {
            if (isvalid(e.Key))
            {
                if (e.Key == Key.Right)
                {
                    clear(moving);
                    posx[moving]++;
                    draw(moving, 'X');
                }
                else if (e.Key == Key.Left)
                {
                    clear(moving);
                    posx[moving]--;
                    draw(moving, 'X');
                }
                else if (e.Key == Key.Down)
                {
                    clear(moving);
                    posy[moving]++;
                    draw(moving, 'X');
                }
                else if (e.Key == Key.Up)
                {
                    clear(moving);
                    posy[moving]--;
                    draw(moving, 'X');
                }
            }
        }

		private void Create_ships_Click(object sender, RoutedEventArgs e)
		{

			battleboard = new Board();
			battleships = new Ship[5];

			for (int i = 0; i < width.Length; i++)
			{
				Square[] boats = new Square[width[i] + length[i] - 1];
				for (int j = 0; j < length[i]; j++)
				{
					for (int k = 0; k < width[i]; k++)
					{
						boats[j + k] = new Square(posx[i]+k,+posy[i]+j);

					}
				}

				battleships[i] = new Ship(boats);
				battleboard.placeShip(battleships[i]);
			}
			Console.Write(battleboard);
		}


		public void resets()
		{
			for (int i = 0; i < 5; i++)
			{
				width[i] = 1;
				posy[i] = 0;
				posx[i] = 0;
			}
			length[0] = 5;
			length[1] = 4;
			length[2] = 3;
			length[3] = 3;
			length[4] = 2;
			completeClean();
		}

        /*Randomize()
         * Summary: Randomly places every ship
         * Uses the Board's isTaken method to check if
         * the new x,y coordinates are already ships
         */

        public void randomize()
        {
			resets();
            battleboard = new Board();
            Random rndx = new Random();
            for (int i = 0; i < width.Length; i++)
            {
                int randomx = 0;
                int randomy = 0;
                Boolean unique = false;

                while (!unique)
                {
                    unique = true;
                    randomx = rndx.Next(0, 10);
                    randomy = rndx.Next(0, 10);

                    while (randomy + length[i] > 10 || randomx+width[i]>10)
                    {

						randomx = rndx.Next(0, 10);
						randomy = rndx.Next(0, 10);
                    }
					
					Square[] boats = new Square[width[i] + length[i] - 1];



                    for (int j = 0; j < length[i]; j++)
                    {
						for (int k = 0; k < width[i]; k++)
						{
							boats[j+k] = new Square(randomx+k, randomy + j);
							Console.Write(j);
							if (battleboard.isTaken(boats[j+k]))
							{
								unique = false;
							}
						}
                    }

                    if (unique)
                    {
                        battleships[i] = new Ship(boats);
                        battleboard.placeShip(battleships[i]);
                    }

                }
                posy[i] = randomy;
                posx[i] = randomx;

            }

			for (int i = 0; i<18; i++)
			{
				Random randomship = new Random();
				rotate(rndx.Next(0, 5));
			}


            for (int i = 0; i < width.Length; i++)
            { 
                draw(i, 'X');
            }


        }

        /*Void, no params
		 *Summary: Cleans the grid, replacing any Button by ""
		 * 
		 */
        private void completeClean()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                   Button dynamicButton = new Button();

                    battleGrid.Children.Add(dynamicButton);
                    Grid.SetRow(dynamicButton, i);
                    Grid.SetColumn(dynamicButton, j);
                   
                    ((Button)battleGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j)).Content = ("");
                }
            }
        }

        /*void no params
		 *Summary: Cleans the place where the boat was previously on
		 * 
		 */
        private void clear(int boat)
        {
            draw(boat, ' ');
        }

		/*int findShip(int x, int y)
		 * Summary: finds the ship that the (x,y) belongs to
		 * belongs -1 if not found
		 * 
		 * 
		 */
		public int findShip(int x, int y)
		{

			for (int i = 0; i < posy.Length; i++)
			{

				for (int j = 0; j < width[i]; j++)
				{
					for (int k = 0; k < length[i]; k++)
					{
						if (x == posx[i]+j && y == posy[i]+k)
							return i;
					}
				}

			}
			return -11; //if all tests fail, return true
		}

        public void rotate(int boat)
        {
            clear(boat);
            //Original mesurements
            int templength = length[boat];
            int tempwidth = width[boat];

            length[boat] = tempwidth;
            width[boat] = templength;

            if (tempwidth < templength)
            {
                if (isInside(boat))
                {
                    draw(boat, 'X');
                }
                else
                {
                    length[boat] = templength;
                    width[boat] = tempwidth;
                    draw(boat, 'X');
                }
            }
            else if (tempwidth > templength)
            {
                if (isInside(boat))
                {
                    draw(boat, 'X');
                }
                else
                {
                    length[boat] = templength;
                    width[boat] = tempwidth;
                    draw(boat, 'X');
                }
            }
        }



        /*Params: int width: Width of the boat we're drawing
		 *		  int length: Length of the boat we're drawing
		 *		  char a: char we are going to draw inside the Button
		 *Summary: Draws a ship of the width and length indicated in the Grid
		 * 
		 */
        private void draw(int boat, char a)
        {
            for (int i = 0; i < length[boat]; i++)
            {
                for (int j = 0; j < width[boat]; j++)
                {
                    Button dynamicButton = new Button();
                    battleGrid.Children.Add(dynamicButton);
                    Grid.SetRow(dynamicButton, i + posy[boat]);
                    Grid.SetColumn(dynamicButton, j + posx[boat]);
                    dynamicButton.Content = a;
                    ((Button)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i + posy[boat] && Grid.GetColumn(f) == j + posx[boat])).Content = a;

                }
            }
        }


        /*Movement event
		 * Triggered by:  Any key being pressed
		 * Summary: Checks if it's a valid movement,
		 * then erases where it currently is
		 * then draws itself in the next position 
		 * 
		 */
        private Boolean isvalid(Key key)
        {
            if (key == Key.Right)
            {
                if (posx[moving] + width[moving] >= 10) return false;  //If it's at the right edge, then it's invalid
                for (int i = 0; i < posx.Length; i++)
                {
                    //Checks if it's directly to the left of a ship
                    if (i != moving && posx[moving] + width[moving] == posx[i])
                    {
                        //Test if it's in the vertical range of the ship next to it
                        if (posy[moving] >= posy[i] && posy[moving] < posy[i] + length[i] || posy[i] >= posy[moving] && posy[i] < posy[moving] + length[moving])
                            return false;
                    }
                }
            }

            else if (key == Key.Left)
            {
                if (posx[moving] <= 0) return false; //If it's at the left edge, then it's invalid

                for (int i = 0; i < posx.Length; i++)
                {
                    if (i != moving && posx[moving] == posx[i] + width[i])//Checks if it's directly to the right of a ship
                    {
                        //Test if it's in the vertical range of the ship next to it
                        if (posy[moving] >= posy[i] && posy[moving] < posy[i] + length[i] || posy[i] >= posy[moving] && posy[i] < posy[moving] + length[moving])

                            return false;
                    }
                }
            }

            else if (key == Key.Down)
            {
                if (posy[moving] + length[moving] >= 10) return false; //If it's at the bottom, then it's invalid

                for (int i = 0; i < posx.Length; i++)
                {
                    if (i != moving && posy[moving] + length[moving] == posy[i])//Checks if it's directly above a ship
                    {
                        if (posx[moving] + width[moving] >= posx[i] + 1 && posx[moving] + 1 <= posx[i] + width[i]) //Test if it's in the range of the ship below
                            return false;
                    }
                }
            }


            else if (key == Key.Up)
            {
                if (posy[moving] <= 0) return false;//If it's at the top, then it's invalid

                for (int i = 0; i < posx.Length; i++)
                {
                    if (i != moving && posy[moving] == posy[i] + length[i])//Checks if it's directly below a ship
                    {
                        if (posx[moving] + width[moving] >= posx[i] + 1 && posx[moving] + 1 <= posx[i] + width[i]) //Test if it's in the range of the ship above
                            return false;
                    }
                }
            }
            return true;
        }

        /*IsInside method
         * args: Key key: the movement is "Right" when rotation 90* towards the left
         * and "Down" when rotating towards the right
		 * Summary: Checks if the new rotated ship will be inside another ship
         * First checks the border, then checks ship collision
		 * 
		 */
        public Boolean isInside(int boat)
        {
            if (posx[boat] + width[boat] > 10 || posy[boat] + length[boat] > 10)
                return false;  //If it's at the right edge, then it's invalid

            for (int i = 0; i < posy.Length; i++)
            {
                if (posy[boat] + length[boat] <= posy[i] || posy[boat] >= posy[i] + length[i]) continue;

                for (int j = 0; j < width[boat]; j++)
                {
                    if (j + posx[boat] >= posx[i] && j + posx[boat] < posx[i] + width[i] && i != boat)
                        return false;
                }
            }

            for (int i = 0; i < posx.Length; i++)
            {
                if (posx[boat] + width[boat] <= posx[i] || posx[boat] >= posx[i] + width[i]) continue;

                for (int j = 0; j < length[boat]; j++)
                {
                     if (j + posy[boat] >= posy[i] && j + posy[boat] < posy[i] + length[i] && i != boat)
                        return false;
                }
            }
            return true; //if all tests fail, return true
        }

		
	}
}
