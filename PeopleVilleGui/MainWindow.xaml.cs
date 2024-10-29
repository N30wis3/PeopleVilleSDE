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


        private void Populations_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the population panel and hide LocationsPanel if active
            if (population.Visibility == Visibility.Collapsed)
            {
                population.Visibility = Visibility.Visible;
                LocationsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                population.Visibility = Visibility.Collapsed;
            }

            // Ensure other panels (houses, work, store) are collapsed if necessary
            houses.Visibility = Visibility.Collapsed;
            work.Visibility = Visibility.Collapsed;
            store.Visibility = Visibility.Collapsed;
        }

        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            // Show LocationsPanel, hide other panels if visible
            LocationsPanel.Visibility = Visibility.Visible;
            population.Visibility = Visibility.Collapsed;
            houses.Visibility = Visibility.Collapsed;
            work.Visibility = Visibility.Collapsed;
            store.Visibility = Visibility.Collapsed;
        }

        private void House_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility for houses and hide other panels if houses is being shown
            if (houses.Visibility == Visibility.Collapsed)
            {
                houses.Visibility = Visibility.Visible;
                LocationsPanel.Visibility = Visibility.Collapsed;
                population.Visibility = Visibility.Collapsed;
                work.Visibility = Visibility.Collapsed;
                store.Visibility = Visibility.Collapsed;
            }
            else
            {
                houses.Visibility = Visibility.Collapsed;
            }
        }

        private void Work_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility for work and hide other panels if work is being shown
            if (work.Visibility == Visibility.Collapsed)
            {
                work.Visibility = Visibility.Visible;
                LocationsPanel.Visibility = Visibility.Collapsed;
                population.Visibility = Visibility.Collapsed;
                houses.Visibility = Visibility.Collapsed;
                store.Visibility = Visibility.Collapsed;
            }
            else
            {
                work.Visibility = Visibility.Collapsed;
            }
        }

        private void Stores_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility for store and hide other panels if store is being shown
            if (store.Visibility == Visibility.Collapsed)
            {
                store.Visibility = Visibility.Visible;
                LocationsPanel.Visibility = Visibility.Collapsed;
                population.Visibility = Visibility.Collapsed;
                houses.Visibility = Visibility.Collapsed;
                work.Visibility = Visibility.Collapsed;
            }
            else
            {
                store.Visibility = Visibility.Collapsed;
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