using System;
using System.Collections.Generic;
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

        private List<Card> _cards = new List<Card>();

        public MainWindow()
        {
            InitializeComponent();
            FillCardList();

            PlayerCardOne.Source = new BitmapImage(_cards[0].FileLocation);
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

        private void FillCardList()
        {
            _cards.Add(new Card{FileLocation = new Uri("../Resources/Cards/AceOfClub.png", UriKind.Relative), IsAce = true, Value = 0});
        }
    }

    
}
