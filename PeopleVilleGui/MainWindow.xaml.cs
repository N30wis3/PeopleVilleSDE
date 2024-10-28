using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PeopleVilleEngine;

namespace PeopleVilleGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility of the LocationsPanel
            if (LocationsPanel.Visibility == Visibility.Collapsed)
            {
                LocationsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                LocationsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void Populations_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility of the populationPanel
            if (population.Visibility == Visibility.Collapsed)
            {
                population.Visibility = Visibility.Visible;
            }
            else
            {
                population.Visibility = Visibility.Collapsed;
            }
        }

        private void OnClikMin(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnClikClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}