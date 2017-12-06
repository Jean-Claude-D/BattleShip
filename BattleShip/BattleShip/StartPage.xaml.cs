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
			continueBtn.Visibility = Visibility.Hidden;
			newBtn.Visibility = Visibility.Hidden;
		}

		private void goToBoatPlacement(StartPageData data)
		{
			this.NavigationService.Navigate(new BoardPlacement(data));
		}

		private void goToGame(string playerFileName)
		{
            //Load and deserialize file at playerFileName
			this.NavigationService.Navigate(new Game(new GamePageData()));
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
			nameTxt.Visibility = Visibility.Hidden;
			sendBtn.Visibility = Visibility.Hidden;
			continueBtn.Visibility = Visibility.Visible;
			newBtn.Visibility = Visibility.Visible;

			//if name found no saved game
			Narrator.displayNameFound(msgTxt, name);
			nameTxt.Visibility = Visibility.Hidden;
			sendBtn.Visibility = Visibility.Hidden;
			newBtn.Visibility = Visibility.Visible;

			//if new name
			Narrator.newName(msgTxt, name);
			nameTxt.Visibility = Visibility.Hidden;
			sendBtn.Visibility = Visibility.Hidden;
			newBtn.Visibility = Visibility.Visible;
	
		}

		private void newBtn_Click(object sender, RoutedEventArgs e)
		{
			goToBoatPlacement(new object());
			//new game code
		}
		private void continueBtn_Click(object sender, RoutedEventArgs e)
		{
			goToGame(new object());
			//load game code
		}
	}
}
