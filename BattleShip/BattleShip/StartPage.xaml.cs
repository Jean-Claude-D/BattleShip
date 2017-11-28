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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BattleShip
{
	/// <summary>
	/// Interaction logic for StartPage.xaml
	/// </summary>
	public partial class StartPage : Page
	{
		public StartPage()
		{
			InitializeComponent();
            Narrator.displayIntro(msgTxt);
        }

		public void goToBoatPlacement(object setting)
		{
			this.NavigationService.Navigate(new BoardPlacement(setting));
		}

		public void goToGame(object game)
		{
			this.NavigationService.Navigate(new Game(game));
		}

        /**
         * Button pressed after name is entered.
         * 
         * @author karina
         **/ 
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTxt.Text;
            //check database
            //if name in database && saved game
            Narrator.displayNameFoundSaved(msgTxt, name);
            //if name found no saved game
            Narrator.displayNameFoundSaved(msgTxt, name);
        }
    }
}
