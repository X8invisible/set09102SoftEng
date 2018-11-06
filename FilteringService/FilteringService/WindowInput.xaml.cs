using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private MessageFactory factory = MessageFactory.Instance;
        private DataHolderSingleton holder = DataHolderSingleton.Instance;
        public WindowInput()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if(comboMsgType.SelectedIndex == -1)
            {
                MessageBox.Show("No message type selected!");
            }
            else
            {
                switch (comboMsgType.SelectedIndex)
                {
                    case 0 :
                        {
                            try
                            {
                                string send = txtSender.Text;
                                string message = txtMessage.Text;
                                Sms result = new Sms(factory.MessageID, send, message);
                                holder.AddSms(result);
                            }
                            catch (Exception ep)
                            {
                                MessageBox.Show(ep.Message);
                            }
                            break;
                        }
                    case 1 :
                        {
                            if (comboEmailType.SelectedIndex != -1)
                            {
                                switch (comboEmailType.SelectedIndex)
                                {
                                    case 0:
                                        {
                                            try
                                            {
                                                string send = txtSender.Text;
                                                string subj = txtSubject.Text;
                                                string message = txtMessage.Text;
                                                ComboBoxItem item = comboEmailType.Items[comboEmailType.SelectedIndex] as ComboBoxItem;
                                                string type = item.Content.ToString();
                                                Email result = new Email(factory.MessageID, send, type, subj, message);
                                                holder.AddEmail(result);
                                                MessageBox.Show(result.ToString());
                                            }
                                            catch (Exception ep)
                                            {
                                                MessageBox.Show(ep.Message);
                                            }
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (comboIncident.SelectedIndex != -1)
                                            {
                                                try
                                                {
                                                    string send = txtSender.Text;
                                                    string subj = txtSubject.Text;
                                                    string sort = txtSortCode.Text;
                                                    if (Regex.IsMatch(txtSortCode.Text, @"$1-$2-$3"))
                                                    {
                                                        sort = txtSortCode.Text;
                                                    }
                                                    else
                                                    {
                                                        sort = Regex.Replace(sort, @"^(..)(..)(..)$", "$1-$2-$3");
                                                    }
                                                    string message = txtMessage.Text;
                                                    ComboBoxItem item = comboEmailType.Items[comboEmailType.SelectedIndex] as ComboBoxItem;
                                                    string type = item.Content.ToString();
                                                    item = comboIncident.Items[comboIncident.SelectedIndex] as ComboBoxItem;
                                                    string incident = item.Content.ToString();
                                                    Email result = new Email(factory.MessageID, send, type, subj, message, sort, incident);
                                                    holder.AddEmail(result);
                                                }
                                                catch (Exception ep)
                                                {
                                                    MessageBox.Show(ep.Message);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("No incident type selected!");
                                            }
                                            break;
                                        }
                                }
                               
                            }
                            else
                            {
                                MessageBox.Show("No email type selected!");
                            }
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                string send = txtSender.Text;
                                string message = txtMessage.Text;
                                Tweet result = new Tweet(factory.MessageID, send, message);
                                holder.AddTweet(result);
                            }
                            catch (Exception ep)
                            {
                                MessageBox.Show(ep.Message);
                            }
                            break;
                        }
                }

                holder.ConvertAbbr();
                holder.ConvertUrl();
                holder.SaveData();
            }
           

        }

        private void comboMsgType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboMsgType.SelectedIndex == 1)
            {
                txtSubject.IsEnabled = true;
                gridEmailType.Visibility = Visibility.Visible;
            }
            else
            {
                txtSubject.Text = "";
                txtSubject.IsEnabled = false;
                gridEmailType.Visibility = Visibility.Hidden;
            }
        }

		private void btnArrowBack_Click(object sender, RoutedEventArgs e)
		{
			MainWindow back = new MainWindow();
			back.Show();
			this.Close();
		}

        private void comboEmailType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboEmailType.SelectedIndex == 1)
            {
                txtSortCode.IsEnabled = true;
                gridIncident.Visibility = Visibility.Visible;
            }
            else
            {
                txtSortCode.Text = "";
                txtSortCode.IsEnabled = false;
                gridIncident.Visibility = Visibility.Hidden;
            }
        }
    }
}
