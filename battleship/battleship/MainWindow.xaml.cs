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

namespace battleship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       int [] posx = new int[2];
        int [] posy = new int[2];
        int [] length = new int[2];
        int [] width = new int[2];
        int moving = 0; //what ship you're moving
        public MainWindow()
        {

            InitializeComponent();
            width[0] = 1;
            width[1] = 1;
            length[0] = 3;
            length[1] = 2;
        }

		/*Event for testing purposes
		 *Summary: Draws
		 * 
		 * 
		 */
        private void Button_Click(object sender, RoutedEventArgs e)
        {

          
            draw(length[moving], width[moving], 'X');
        }

		 private void Button_Click1(object sender, RoutedEventArgs e)
         {
           moving=0;
         }
		
		private void Button_Click2(object sender, RoutedEventArgs e)
         {
           moving=1;
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
						posy[moving]++;
                        draw(length[moving], width[moving], 'X');
                     }
                       else if (e.Key == Key.Left)
					   {
						clear();
						posy[moving]--;
                        draw(length[moving], width[moving], 'X');
                       }
					else if (e.Key == Key.Down)
					{
						clear();
						posx[moving]++;
                         draw(length[moving], width[moving], 'X');
                    }
					else if (e.Key == Key.Up)
					{
						clear();
						posx[moving]--;
                        draw(length[moving], width[moving], 'X');
                    }
				
			}
        }


		/*Void, no params
		 *Summary: Cleans the grid, replacing any label by ""
		 * 
		 * 
		 * 
		 */
        private void completeclean()
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

            draw(length[moving], width[moving], ' ');

        }


      

		/*Params: int width: Width of the boat we're drawing
		 *		  int length: Length of the boat we're drawing
		 *		  char a: char we are going to draw inside the label
		 *Summary: Cleans the place where the boat was previously on
		 * 
		 */
        private void draw(int width, int length, char a)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)

                {
                    Label dynamicLabel = new Label();
                    battleGrid.Children.Add(dynamicLabel);
                    Grid.SetRow(dynamicLabel, i+posx[moving]);
                    Grid.SetColumn(dynamicLabel,j+posy[moving]);

                    ((Label)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i+posx[moving] && Grid.GetColumn(f) == j+posy[moving])).Content =a;
              
                }
            }
        }



		private Boolean isvalid(Key key)
		{
			
			if (key == Key.Right)
			{
				if (posy[moving] + width[moving] >= 10) return false;  //If it's at the right edge, then it's invalid
				for (int i = 0; i < posy.Length; i++)
				{
                    //Checks if it's directly to the left of a ship
					if (i != moving && posy[moving] + width[moving] == posy[i])
					{
                          //Test if it's in the vertical range of the ship next to it
						if (posx[moving] >= posx[i] && posx[moving] < posx[i] + length[i] || posx[i] >= posx[moving] && posx[i] < posx[moving]+length[moving])
							return false;
					}
				}



			}

			else if (key == Key.Left )
			{
				if (posy[moving] <= 0) return false; //If it's at the left edge, then it's invalid

				for (int i = 0; i < posy.Length; i++)
				{
					if (i != moving && posy[moving] == posy[i]+width[i])//Checks if it's directly to the right of a ship
					{
                        //Test if it's in the vertical range of the ship next to it
                        if (posx[moving] >= posx[i] && posx[moving] < posx[i] + length[i] || posx[i] >= posx[moving] && posx[i] < posx[moving] + length[moving]) 

                            return false;

					}
				}
			}

			else if (key == Key.Down)
			{
				if (posx[moving] +length[moving] >= 10) return false; //If it's at the bottom, then it's invalid

				for (int i = 0; i < posy.Length; i++)
				{
					if (i != moving && posx[moving] + length[moving] == posx[i])//Checks if it's directly above a ship
					{
                       
                        if (posy[moving] + width[moving]  >= posy[i]+1 && posy[moving] + 1 <= posy[i] + width[i] ) //Test if it's in the range of the ship below
						return false;
                      
					}
				}
			}
            

			else if (key== Key.Up )
			{
				if (posx[moving] <= 0) return false;//If it's at the top, then it's invalid

				for (int i = 0; i < posy.Length; i++)
				{
					if (i != moving &&  posx[moving] == posx[i]+length[i])//Checks if it's directly below a ship
					{
                       
                        if (posy[moving] + width[moving]  >= posy[i]+1 && posy[moving] + 1 <= posy[i] + width[i] ) //Test if it's in the range of the ship above
						return false;
                      
					}

				}

			}

			return true;
			
		}

        private void Rotation_Click(object sender, RoutedEventArgs e)
        {
            clear();
            //Original mesurements
            int templength = length[moving]; 
            int tempwidth = width[moving];

           length[moving] = tempwidth;
            width[moving] = templength;

             
           if (tempwidth<templength)
           {
                if (isInside(Key.Right))
                {
                  draw(length[moving], width[moving], 'X');
                }
                else
                {
                
                length[moving] = templength;
                  width[moving] = tempwidth;
                    draw(length[moving], width[moving], 'X');
                }


           } 
           else if (tempwidth>templength)
           {
               if (isInside(Key.Down))
               {
                draw(length[moving], width[moving], 'X');
               }
               else 
               {
                
                length[moving] = templength;
                  width[moving] = tempwidth;
                    draw(length[moving], width[moving], 'X');
               }
           }
            
          

        }

        public Boolean isInside(Key key)
        {
            if (key == Key.Right)
            {

            if (posy[moving] + width[moving] >= 10) return false;  //If it's at the right edge, then it's invalid

				for (int i = 0; i < posy.Length; i++)
				{
					if ( posx[moving]+length[moving] <= posx[i] || posx[moving] >= posx[i] + length[i]) return true;

                    for (int j=0; j< width[moving];j++)
                    {
						//MessageBox.Show(j +" + " + posy[moving]+ " >= " + posy[i]+" + " + 1 + " && "+ j +" + " + posy[moving] + " <= " + posy[i] +" + " + width[i] );
                        if (j + posy[moving]>= posy[i] && j + posy[moving] <= posy[i] + width[i] && i != moving) 
                        return false;
                    }



                }

            }
			else if (key == Key.Down)
				{
				if (posy[moving] + width[moving] >= 10) return false;  //If it's at the right edge, then it's invalid

				for (int i = 0; i < posy.Length; i++)
				{
					if ( posy[moving]+width[moving] <= posy[i] || posy[moving] >= posy[i] + width[i]) return true;

                    for (int j=0; j< length[moving];j++)
                    {

						//MessageBox.Show(j +" + " + posx[moving]+ " >= " + posx[i]+" + " + " && "+ j +" + " + posx[moving] + " <= " + posx[i] +" + " + length[i] );
                        if (j + posx[moving]>= posx[i] && j + posx[moving] <= posx[i] + length[i] && i != moving) 
                        return false;
                    }



                }

            }
          
			return true;
        }      



    }
}
