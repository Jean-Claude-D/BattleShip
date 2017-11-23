﻿using System;
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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public Game(Object obj)
        {
            InitializeComponent();
        }

        private void goToStart()
        {
            this.NavigationService.Navigate(new StartPage());
        }

        private void goToBoatPlacement(object setting)
        {
            this.NavigationService.Navigate(new BoardPlacement(setting));
        }

        private void reset(object game)
        {
            this.NavigationService.Navigate(new Game(game));
        }

        private void goToScore(object playerListDB, object game = null)
        {
            this.NavigationService.Navigate(new ScoreBoard(playerListDB, game));
        }
    }
}
