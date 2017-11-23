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

        int posx[1];
        int posy[1];
        int moving = 1; //what ship you're moving
        public MainWindow()
        {

            InitializeComponent();

         

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            completeclean();
            draw(4, 1, 'X');
           


        }


        private void left(object sender, KeyEventArgs e)
        {

            if ((e.Key == Key.Right && posy+1<10)){
                clear(e.Key);
                posy++;
                draw(4,1, 'X');
            }
            else if ((e.Key == Key.Left && posy>0))
            {
                clear(e.Key);
                posy--;
                draw(4, 1, 'X');
            }
            else if ((e.Key == Key.Down && posx+4<10))
            {
                clear(e.Key);
                posx++;
                draw(4, 1, 'X');
            }
            else if ((e.Key == Key.Up && posx > 0))
            {
                clear(e.Key);
                posx--;
                draw(4, 1,'X');
            }

        }



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

        private void clear(Key direction)
        {

            draw(4, 1, ' ');
           
        }


      


        private void draw(int width, int length, char a)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)

                {
                    Label dynamicLabel = new Label();
                    battleGrid.Children.Add(dynamicLabel);
                    Grid.SetRow(dynamicLabel, i+posx);
                    Grid.SetColumn(dynamicLabel,j+posy);

                    ((Label)battleGrid.Children.Cast<UIElement>().First(f => Grid.GetRow(f) == i+posx && Grid.GetColumn(f) == j+posy)).Content =a;
              
                }
            }
        }

    }
}
