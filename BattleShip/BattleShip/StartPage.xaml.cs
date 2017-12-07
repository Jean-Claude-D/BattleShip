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
			this.NavigationService.Navigate(new Game(PlayerDB.getDB().loadGame(playerFileName)));
		}

        /**
         * Button pressed after name is entered.
         * 
         * @author karina
         **/ 
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTxt.Text;

            nameTxt.Visibility = Visibility.Hidden;
            sendBtn.Visibility = Visibility.Hidden;
            newBtn.Visibility = Visibility.Visible;
            continueBtn.Visibility = Visibility.Hidden;

            if (PlayerDB.getDB().isPlayerExist(name))
            {
                Narrator.displayNameFoundSaved(msgTxt, name);
                continueBtn.Visibility = Visibility.Visible;
            }
            else
            {
                Narrator.newName(msgTxt, name);
            }
		}

		private void newBtn_Click(object sender, RoutedEventArgs e)
		{
			goToBoatPlacement(new StartPageData(nameTxt.Text));
			//new game code
		}
		private void continueBtn_Click(object sender, RoutedEventArgs e)
		{
			goToGame(nameTxt.Text);
			//load game code
		}
	}
}
