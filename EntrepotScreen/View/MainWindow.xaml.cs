using System;
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

namespace EntrepotScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MyWindow_Loaded;
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new PageStockMouvement());
        }

        private void btnStockMouvement_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new PageStockMouvement();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new PageProducts();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new PageAdmin();
        }
    }
}
