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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Board b = new Board();
            Console.Write(b.ToString());
            b.shoot(new Square(6, 7));
            Console.Write(b.ToString());
            Square[] a = { new Square(0, 0), new Square(0, 1)};
            b.placeShip(new Ship(a));
            Console.Write(b.ToString());
            b.shoot(new Square(0, 0));
            Console.Write(b.ToString());
            b.shoot(new Square(0, 1));
            Console.Write(b.ToString());
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
    }
}