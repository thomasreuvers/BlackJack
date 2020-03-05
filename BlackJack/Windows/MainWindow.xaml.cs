using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace BlackJack.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartWindow _startMenuWindow;

        private readonly List<Card> _cards = new List<Card>();
        private readonly List<Card> _playersHand = new List<Card>();

        private readonly Random _rand = new Random();

        public MainWindow()
        {
            InitializeComponent();

            // Fill the card list with cards
            FillCardList();

            InitPlayersHand();
            InitPlayersHand();
        }


        /// <summary>
        /// When "Escape" is clicked return to start menu window and dispose game window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Escape)) return;
            _startMenuWindow = new StartWindow();
            _startMenuWindow.Show();
            Close();
        }

        /// <summary>
        /// Get a random int and use it to retrieve a card from the cards list
        /// Than add it to the player hand and remove it from the cards list
        /// So the player cannot get that card again
        /// </summary>
        /// <returns></returns>
        private void InitPlayersHand()
        {
             var random = _rand.Next(0, _cards.Count);

             PlayerHand.Children.Add(new Image { Source = new BitmapImage(_cards[random].FileLocation) });

            _playersHand.Add(_cards[random]);
            _cards.RemoveAt(random);
        }

        private void FillCardList()
        {
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/10C.png", UriKind.Relative), IsAce = true, Value = 0 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/10D.png", UriKind.Relative), IsAce = false, Value = 10 });
        }
    }

    
}
