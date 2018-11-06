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
using Data;

namespace FilteringService
{
    /// <summary>
    /// Interaction logic for WindowLists.xaml
    /// </summary>
    public partial class WindowMention : Window
    {
        private DataHolderSingleton holder = DataHolderSingleton.Instance;
        public WindowMention()
        {
            InitializeComponent();
            dgridTrend.ItemsSource = holder.TrendList;
        }

        private void btnArrowBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Close();
        }
    }
}
