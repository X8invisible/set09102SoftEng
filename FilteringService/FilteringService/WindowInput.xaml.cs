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
        private MessageFactory factory = MessageFactory.Instance;
        private DataHolderSingleton holder = DataHolderSingleton.Instance;
        public WindowInput()
        {
            InitializeComponent();
            holder.ReadData();
            holder.readAbbr();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                                MessageBox.Show(result.ToString());
                            }
                            catch (Exception ep)
                            {
                                MessageBox.Show(ep.Message);
                            }
                            break;
                        }
                    case 1 :
                        {
                            try
                            {
                                string send = txtSender.Text;
                                string subj = txtSubject.Text;
                                string message = txtMessage.Text;
                                Email result = new Email(factory.MessageID, send, subj, message);
                                holder.AddEmail(result);
                                MessageBox.Show(result.ToString());
                            }
                            catch (Exception ep)
                            {
                                MessageBox.Show(ep.Message);
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
                                MessageBox.Show(result.ToString());
                            }
                            catch (Exception ep)
                            {
                                MessageBox.Show(ep.Message);
                            }
                            break;
                        }
                }
            }
            //string header = txtHeader.Text;
            //string message = txtMessage.Text;
            //try
            //{
            //    RawMessage result = instance.ProcessMessage(header, message);
            //    MessageBox.Show(result.ToString());
            //}
            //catch(Exception ep)
            //{
            //    MessageBox.Show(ep.Message);
            //}
           
        }

        private void comboMsgType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboMsgType.SelectedIndex == 1)
            {
                txtSubject.IsEnabled = true;
            }
            else
            {
                txtSubject.Text = "";
                txtSubject.IsEnabled = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            holder.ConvertAbbr();
            holder.SaveData();
        }
    }
}
