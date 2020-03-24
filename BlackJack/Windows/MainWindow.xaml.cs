using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
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
        private MainWindow _mainWindow;

        private readonly List<Card> _cards = new List<Card>();
        private readonly List<Card> _playersHand = new List<Card>();
        private readonly List<Card> _banksHand = new List<Card>();

        private readonly Random _rand = new Random();

        private enum Hands
        {
            Bank,
            Player
        }


        public MainWindow()
        {
            InitializeComponent();

            // Fill the card list with cards
            FillCardList();

            for (var i = 0; i < 2; i++)
            {
                AddCardToHand(Hands.Player);
            }
        }


        /// <summary>
        /// When "R" is clicked restart game.
        /// When "Escape" is clicked return to start menu window and dispose game window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Escape))
            {

            }

            switch (e.Key)
            {
                case Key.Escape:
                    _startMenuWindow = new StartWindow();
                    _startMenuWindow.Show();
                    Close();
                    break;
                case Key.R:
                    _mainWindow = new MainWindow();
                    _mainWindow.Show();
                    Close();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e.Key), e.Key, null);
            }
        }

        /*
         * Todo: Check card hands
         * Todo: animate cards
         * Todo: find better background for main screen
         * Todo: Style end menu
         */

        /// <summary>
        /// Add card to specified hand
        /// </summary>
        /// <param name="hand"></param>
        private void AddCardToHand(Hands hand)
        {
            var random = _rand.Next(0, _cards.Count);

            switch (hand)
            {
                case Hands.Bank:
                    _banksHand.Add(_cards[random]);
                    BankHand.Children.Add(new Image {Source = new BitmapImage(_cards[random].FileLocation), Effect = new DropShadowEffect()});
                    break;
                case Hands.Player:
                    _playersHand.Add(_cards[random]);
                    PlayerHand.Children.Add(new Image { Source = new BitmapImage(_cards[random].FileLocation), Effect = new DropShadowEffect()});
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
            }

            CheckHand();
            PointsLabel.Content = $"Points: {_playersHand.Sum(x => x.Value)}";
            _cards.RemoveAt(random);
        }

        private void CheckHand()
        {
            foreach (var card in _playersHand.Where(x => x.IsAce))
            {
                card.Value = 11;

                if (_playersHand.Sum(x => x.Value) > 21)
                {
                    card.Value = 1;
                }
            }

            foreach (var card in _banksHand.Where(x => x.IsAce))
            {
                card.Value = 11;

                if (_banksHand.Sum(x => x.Value) > 21)
                {
                    card.Value = 1;
                }
            }

            var player = _playersHand.Sum(x => x.Value);
            var bank = _banksHand.Sum(x => x.Value);

            if (player > 21)
            {
                EndMenu.Visibility = Visibility.Visible;
                MenuTxt.Content = "Busted bank won!\nPress \"R\" to start over.";
                return;
            }

            if (player == 21)
            {
                EndMenu.Visibility = Visibility.Visible;
                MenuTxt.Content = "Blackjack!\nPress \"R\" to start over.";
                return;
            }


            if (_banksHand.Sum(x => x.Value) == 0) return;

            if (bank > 21)
            {
                EndMenu.Visibility = Visibility.Visible;
                MenuTxt.Content = "Bank busted you've won!\nPress \"R\" to start over.";
                return;
            }

            if (bank == 21)
            {
                EndMenu.Visibility = Visibility.Visible;
                MenuTxt.Content = "Bank has Blackjack!\nPress \"R\" to start over.";
                return;
            }

            if (player == bank)
            {
                EndMenu.Visibility = Visibility.Visible;
                MenuTxt.Content = "Draw!\nPress \"R\" to start over.";
                return;
            } 
            if (player < bank)
            {
                EndMenu.Visibility = Visibility.Visible;
                MenuTxt.Content = "Bank has won!\nPress \"R\" to start over.";
                return;
            }

            if (player <= bank) return;
            EndMenu.Visibility = Visibility.Visible;
            MenuTxt.Content = "Player has won!\nPress \"R\" to start over.";

        }

        private void HitBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCardToHand(Hands.Player);
        }

        private void StandBtn_Click(object sender, RoutedEventArgs e)
        {
            while (_banksHand.Sum(x => x.Value) < _playersHand.Sum(x => x.Value))
            {
                AddCardToHand(Hands.Bank);
            }
        }

        private void FillCardList()
        {
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/10C.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/10D.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/10H.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/10S.png", UriKind.Relative), IsAce = false, Value = 10 });

            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/2C.png", UriKind.Relative), IsAce = false, Value = 2 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/2D.png", UriKind.Relative), IsAce = false, Value = 2 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/2H.png", UriKind.Relative), IsAce = false, Value = 2 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/2S.png", UriKind.Relative), IsAce = false, Value = 2 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/3C.png", UriKind.Relative), IsAce = false, Value = 3 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/3D.png", UriKind.Relative), IsAce = false, Value = 3 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/3H.png", UriKind.Relative), IsAce = false, Value = 3 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/3S.png", UriKind.Relative), IsAce = false, Value = 3 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/4C.png", UriKind.Relative), IsAce = false, Value = 4 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/4D.png", UriKind.Relative), IsAce = false, Value = 4 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/4H.png", UriKind.Relative), IsAce = false, Value = 4 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/4S.png", UriKind.Relative), IsAce = false, Value = 4 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/5C.png", UriKind.Relative), IsAce = false, Value = 5 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/5D.png", UriKind.Relative), IsAce = false, Value = 5 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/5H.png", UriKind.Relative), IsAce = false, Value = 5 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/5S.png", UriKind.Relative), IsAce = false, Value = 5 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/6C.png", UriKind.Relative), IsAce = false, Value = 6 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/6D.png", UriKind.Relative), IsAce = false, Value = 6 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/6H.png", UriKind.Relative), IsAce = false, Value = 6 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/6S.png", UriKind.Relative), IsAce = false, Value = 6 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/7C.png", UriKind.Relative), IsAce = false, Value = 7 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/7D.png", UriKind.Relative), IsAce = false, Value = 7 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/7H.png", UriKind.Relative), IsAce = false, Value = 7 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/7S.png", UriKind.Relative), IsAce = false, Value = 7 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/8C.png", UriKind.Relative), IsAce = false, Value = 8 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/8D.png", UriKind.Relative), IsAce = false, Value = 8 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/8H.png", UriKind.Relative), IsAce = false, Value = 8 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/8S.png", UriKind.Relative), IsAce = false, Value = 8 });
            
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/9C.png", UriKind.Relative), IsAce = false, Value = 9 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/9D.png", UriKind.Relative), IsAce = false, Value = 9 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/9H.png", UriKind.Relative), IsAce = false, Value = 9 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/9S.png", UriKind.Relative), IsAce = false, Value = 9 });

            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/AC.png", UriKind.Relative), IsAce = true, Value = 1 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/AD.png", UriKind.Relative), IsAce = true, Value = 1 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/AH.png", UriKind.Relative), IsAce = true, Value = 1 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/AS.png", UriKind.Relative), IsAce = true, Value = 1 });

            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/JC.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/JD.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/JH.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/JS.png", UriKind.Relative), IsAce = false, Value = 10 });

            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/KC.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/KD.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/KH.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/KS.png", UriKind.Relative), IsAce = false, Value = 10 });

            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/QC.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/QD.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/QH.png", UriKind.Relative), IsAce = false, Value = 10 });
            _cards.Add(new Card { FileLocation = new Uri("../Resources/Cards/QS.png", UriKind.Relative), IsAce = false, Value = 10 });
        }

    }

}
