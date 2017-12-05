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

       int [] posy = new int[5];
        int [] posx = new int[5];
        int [] length = new int[5];
        int [] width = new int[5];
        int moving = 0; //what ship you're moving
		Board battleboard;


		public MainWindow()
        {

            InitializeComponent();
             battleboard= new Board();

          for (int i=0;i<5;i++)
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
            randomize();

        }

		/*Event for testing purposes
		 *Summary: Draws
		 * 
		 * 
		 */
      

        /*Temporary event things
         * 
         * 
         */
		 private void Button_Click1(object sender, RoutedEventArgs e)
         {
           moving=0;
         }
		
		private void Button_Click2(object sender, RoutedEventArgs e)
         {
           moving=1;
         }

        /*Event: Click
         *Summary: A lot of code stoled that find the row and column 
         * of a grid space when it's clicked
         * 
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
            rotate(findShip(row,col));

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            completeClean();
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
                    clear();
                    posx[moving]++;
                    draw(moving, 'X');
                }
                else if (e.Key == Key.Left)
                {
                    clear();
                    posx[moving]--;
                    draw(moving, 'X');
                }
                else if (e.Key == Key.Down)
                {
                    clear();
                    posy[moving]++;
                    draw(moving, 'X');
                }
                else if (e.Key == Key.Up)
                {
                    clear();
                    posy[moving]--;
                    draw(moving, 'X');
                }

            }
        }

        /*Rotation event
		 * Triggered by:  Button being pressed
		 * Summary: Saves original length and width,
         * then modifies them to what it would be if the rotation was successful
         * Checks if it's a valid rotation,
		 * if it is, it draws itself in the new position
         * if not it undoes changes to length and width
		 * 
		 */
        private void Rotation_Click(object sender, RoutedEventArgs e)
        {
            clear();
            //Original mesurements
            int templength = length[moving];
            int tempwidth = width[moving];

            length[moving] = tempwidth;
            width[moving] = templength;


            if (tempwidth < templength)
            {
                if (isInside(moving))
                {
                    draw(moving, 'X');
                }
                else
                {

                    length[moving] = templength;
                    width[moving] = tempwidth;
                    draw(moving, 'X');
                }


            }
            else if (tempwidth > templength)
            {
                if (isInside(moving))
                {
                    draw(moving, 'X');
                }
                else
                {

                    length[moving] = templength;
                    width[moving] = tempwidth;
                    draw(moving, 'X');
                }
            }



        }

        /*Randomize()
         * Summary: Randomly places every ship
         * Uses isInside method to check if
         * the new x,y coordinates are valid
         * 
         * 
         */

        public void randomize()
        {
			
			battleboard = new Board();
			Ship[] Ships = new Ship[width.Length];
			Random rndx = new Random();
			Random rndy = new Random();
			for (int i=0; i < width.Length; i++)
            {
                moving = i;
              
                int randomx = 0;
                int randomy = 0;
				Boolean unique=false;

                while (!unique)
                {
					unique = true;

					randomx = rndx.Next(0,10);
					randomy = rndx.Next(0,10);
					while (randomx > 10 || randomy + length[i] > 10)
					{
						
						randomx = rndx.Next(0, 10);
						randomy = rndx.Next(0, 10);
					}
					Square[] boats = new Square[width[i] + length[i] - 1];

					
						for (int j = 0; j < length[i]; j++)
						{
								
								boats[j] = new Square(randomx , randomy+j );


						       //  MessageBox.Show(boats[j].ToString());




					    }
					//}
					posy[i] = randomy;
					posx[i] = randomx;
					
					Ship boat = new Ship(boats);
					MessageBox.Show(boat.ToString());


						if ( (	!battleboard.placeShip(boat)))
						{
							unique = false;
						}

					

                }

				posy[i] = randomy;
				posx[i] = randomx;

			}

			for (int i = 0; i < width.Length; i++)
            {
			//	MessageBox.Show("" + posx[i] + posy[i]);
                draw(i, 'X');
            }
			Console.Write((battleboard.ToString()));
		}

		

		/*Void, no params
		 *Summary: Cleans the grid, replacing any label by ""
		 * 
		 */
        private void completeClean()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)

                {
                    Label dynamicLabel = new Label();

                    battleGrid.Children.Add(dynamicLabel);
                    Grid.SetRow(dynamicLabel, i);
                    Grid.SetColumn(dynamicLabel, j);

                    ((Label)battleGrid.Children.Cast<UIElement>().First(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j)).Content = ("");

                }
            }
        }
		
		/*void no params
		 *Summary: Cleans the place where the boat was previously on
		 * 
		 */
        private void clear()
        {

            draw(moving, ' ');

        }

        public int findShip(int x, int y)
        {
            return 0;
        }

        public void rotate(int boat)
        {
            clear();
            //Original mesurements
            int templength = length[moving];
            int tempwidth = width[moving];

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
		 *		  char a: char we are going to draw inside the label
		 *Summary: Draws a ship of the width and length indicated in the Grid
		 * 
		 */
        private void draw(int boat, char a)
        {
            for (int i = 0; i < length[boat]; i++)
            {
                for (int j = 0; j < width[boat]; j++)

                {
                    Label dynamicLabel = new Label();
                    battleGrid.Children.Add(dynamicLabel);
                    Grid.SetRow(dynamicLabel, i+posy[boat]);
                    Grid.SetColumn(dynamicLabel,j+posx[boat]);

                    ((Label)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i+posy[boat] && Grid.GetColumn(f) == j+posx[boat])).Content =a;
              
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
						if (posy[moving] >= posy[i] && posy[moving] < posy[i] + length[i] || posy[i] >= posy[moving] && posy[i] < posy[moving]+length[moving])
							return false;
					}
				}



			}

			else if (key == Key.Left )
			{
				if (posx[moving] <= 0) return false; //If it's at the left edge, then it's invalid

				for (int i = 0; i < posx.Length; i++)
				{
					if (i != moving && posx[moving] == posx[i]+width[i])//Checks if it's directly to the right of a ship
					{
                        //Test if it's in the vertical range of the ship next to it
                        if (posy[moving] >= posy[i] && posy[moving] < posy[i] + length[i] || posy[i] >= posy[moving] && posy[i] < posy[moving] + length[moving]) 

                            return false;

					}
				}
			}

			else if (key == Key.Down)
			{
				if (posy[moving] +length[moving] >= 10) return false; //If it's at the bottom, then it's invalid

				for (int i = 0; i < posx.Length; i++)
				{
					if (i != moving && posy[moving] + length[moving] == posy[i])//Checks if it's directly above a ship
					{
                       
                        if (posx[moving] + width[moving]  >= posx[i]+1 && posx[moving] + 1 <= posx[i] + width[i] ) //Test if it's in the range of the ship below
						return false;
                      
					}
				}
			}
            

			else if (key== Key.Up )
			{
				if (posy[moving] <= 0) return false;//If it's at the top, then it's invalid

				for (int i = 0; i < posx.Length; i++)
				{
					if (i != moving &&  posy[moving] == posy[i]+length[i])//Checks if it's directly below a ship
					{
                       
                        if (posx[moving] + width[moving]  >= posx[i]+1 && posx[moving] + 1 <= posx[i] + width[i] ) //Test if it's in the range of the ship above
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
            

            if (posx[boat] + width[boat] > 10 || posy[boat] + length[boat] > 10 )
                return false;  //If it's at the right edge, then it's invalid

				for (int i = 0; i < posy.Length; i++)
				{
					if ( posy[boat] +length[boat] <= posy[i] || posy[boat] >= posy[i] + length[i] ) return true;

                    for (int j=0; j< width[boat];j++)
                    {
						//MessageBox.Show(j +" + " + posx[moving]+ " >= " + posx[i]+" + " + 1 + " && "+ j +" + " + posx[moving] + " <= " + posx[i] +" + " + width[i] );
                        if (j + posx[boat] >= posx[i] && j + posx[boat] < posx[i] + width[i] && i != boat) 
                        return false;
                    }



                }
                

				for (int i = 0; i < posx.Length; i++)
				{
                if (posx[boat] + width[boat] <= posx[i] || posx[boat] >= posx[i] + width[i]) return true;
				
                    for (int j=0; j< length[boat];j++)
                    {

						//MessageBox.Show(j +" + " + posy[moving]+ " >= " + posy[i]+" + " + " && "+ j +" + " + posy[moving] + " <= " + posy[i] +" + " + length[i] );
                        if (j + posy[boat] >= posy[i] && j + posy[boat] < posy[i] + length[i] && i != boat) 
                        return false;
                    }



                }

            return true; //if all tests fail, return true
        }

       
    }
}
