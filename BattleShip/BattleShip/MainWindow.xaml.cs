using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timeSec = 50;
        private int timeMin = 59;
        private int timeHour = 0;

        bool played = false;
        int result = 0;

        public MainWindow()
        {
            InitializeComponent();
<<<<<<< HEAD
            goToStartPage();
=======

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            

        }

        public void Function()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += TimeConsumingFunction;
            var frame = new DispatcherFrame();
            worker.RunWorkerCompleted += (sender, args) =>
            {
                frame.Continue = false;
                testLbl.Content = (string)args.Result;
                Console.WriteLine((string)args.Result);
            };
            worker.RunWorkerAsync();
            Dispatcher.PushFrame(frame);
        }

        private void TimeConsumingFunction(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Entering");
            while(!played)
            {
                Thread.Sleep(1000);
            }
            played = false;
            e.Result = "I played";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            testLbl.Content = "Start playing";
            for(int i = 0; i < 5; i++)
            {
                Function();
                Thread.Sleep(9000);
                testLbl.Content = "It played";
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timeLbl.Content = getTime();
        }

        private string getTime()
        {
            if(timeSec >= 60)
            {
                timeSec = 0;
                timeMin++;
            }
            if(timeMin >= 60)
            {
                timeMin = 0;
                timeHour++;
            }
            string toReturn = ((timeHour != 0)?(timeHour + " h "):("")) +  ((timeMin != 0)?(timeMin + " min "):("")) + ((timeSec != 0)?(timeSec + " sec"):(""));

            timeSec++;

            return toReturn;
>>>>>>> Let me out!
        }

        public void goToStartPage()
        {
            mainFrame.NavigationService.Navigate(new StartPage());
        }

        public void goToGame(object boardPlacement)
        {
            mainFrame.NavigationService.Navigate(new Game(boardPlacement));
        }

        public void goToScore(object playerListDB, object lastGame = null)
        {

        }
<<<<<<< HEAD
	}
=======

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked me!");
        }

        private void noNameBtn_Click(object sender, RoutedEventArgs e)
        {
            played = true;
        }
    }
>>>>>>> Let me out!
}
