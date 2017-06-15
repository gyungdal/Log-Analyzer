using LogAnalysis.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

namespace LogAnalysis
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            TextBox_LostFocus(SearchText, null);
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                //If nothing was entered, reset default text.
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Foreground = Brushes.Gray;
                    ((TextBox)sender).Text = "Search";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                //If nothing has been entered yet.
                if (((TextBox)sender).Foreground == Brushes.Gray)
                {
                    ((TextBox)sender).Text = "";
                    ((TextBox)sender).Foreground = Brushes.White;
                }
            }
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Storyboard)FindResource("WaitStoryboard")).Begin();
        }
    }
}
