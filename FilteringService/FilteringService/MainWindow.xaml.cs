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
using Business;
using Data;

namespace FilteringService
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataHolderSingleton holder = DataHolderSingleton.Instance;
        private MessageFactory factory = MessageFactory.Instance;
        private List<RawMessage> fullList = new List<RawMessage>();

        public MainWindow()
        {
            InitializeComponent();
            holder.ReadData();
            holder.readAbbr();
            factory.UpdateNumbers();
            fullList = holder.FullList;
            gridTexts.ItemsSource = fullList;
        }

        private void btnCustom_Click(object sender, RoutedEventArgs e)
        {
            WindowInput input = new WindowInput();
            input.Show();
            this.Close();
        }

        private void btnMentions_Click(object sender, RoutedEventArgs e)
        {
            WindowMention w = new WindowMention();
            w.Show();
            this.Close();
        }

        private void BtnQuarantine_Click(object sender, RoutedEventArgs e)
        {
            WindowQ w = new WindowQ();
            w.Show();
            this.Close();
        }
    }
}
