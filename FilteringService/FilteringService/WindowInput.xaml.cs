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
using Business;
using Data;

namespace FilteringService
{
    /// <summary>
    /// Interaction logic for WindowInput.xaml
    /// </summary>
    public partial class WindowInput : Window
    {
        MessageFactory instance = MessageFactory.Instance;
        public WindowInput()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string header = txtHeader.Text;
            string message = txtMessage.Text;
            try
            {
                RawMessage result = instance.ProcessMessage(header, message);
                MessageBox.Show(result.ToString());
            }
            catch(Exception ep)
            {
                MessageBox.Show(ep.Message);
            }
           
        }
    }
}
