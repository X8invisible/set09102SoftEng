using Data;
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
using System.Windows.Shapes;

namespace FilteringService
{
    /// <summary>
    /// Interaction logic for WindowTrending.xaml
    /// </summary>
    public partial class WindowQ : Window
    {
        private DataHolderSingleton holder = DataHolderSingleton.Instance;
        public WindowQ()
        {
            InitializeComponent();
            dgridQuarantine.ItemsSource = holder.QuarantineUrl;
        }

        private void btnArrowBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Close();
        }
    }
}
