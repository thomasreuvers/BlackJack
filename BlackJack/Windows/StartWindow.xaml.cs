using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlackJack.Windows
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly MainWindow _gameWindow = new MainWindow();

        public StartWindow()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            _gameWindow.Show();
            Close();
        }

        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        { 
            Close();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
