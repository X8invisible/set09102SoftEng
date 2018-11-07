using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MessageFactory
    {
        private static MessageFactory instance;
        private static int messageID = 100000000;


        private MessageFactory() { }

        public static MessageFactory Instance
        {
            get
            {
                //if the class hasn't been yet initialised, the constructor is called
                if (instance == null)
                    instance = new MessageFactory();
                return instance;
            }
        }

        public int MessageID
        {
            get { return messageID++; }
        }
        public void IDGoBack()
        {
            messageID--;
        }

        public void UpdateNumbers()
        {
            DataHolderSingleton instance = DataHolderSingleton.Instance;
            if(instance.SmsList.Count != 0)
            {
                if (instance.SmsList.Last().Id >= messageID)
                {
                    messageID = instance.SmsList.Last().Id +1;
                }
            }
            if (instance.EmailList.Count != 0)
            {
                if (instance.EmailList.Last().Id >= messageID)
                {
                    messageID = instance.EmailList.Last().Id + 1;
                }
            }
            if (instance.TweetList.Count != 0)
            {
                if (instance.TweetList.Last().Id >= messageID)
                {
                    messageID = instance.TweetList.Last().Id + 1;
                }
            }
        }
        //public RawMessage ProcessMessage(string header, string body)
        //{
        //    RawMessage output;
        //    switch(header[0])
        //    {
        //        case 'S':
        //            {
        //                int numberIndex = body.IndexOf(' ');
        //                string sender = body.Substring(0, numberIndex);
        //                string msg = body.Substring(numberIndex +1);
        //                output = new Sms(sender, msg);
        //                break;
        //            }
        //        case 'E':
        //            {
        //                int senderIndex = body.IndexOf(' ');
        //                string sender = body.Substring(0, senderIndex);
        //                int subjectIndex = body.IndexOf(' ',senderIndex +1);
        //                string subject = body.Substring(senderIndex, subjectIndex - senderIndex);
        //                string msg = body.Substring(subjectIndex+1);
        //                output = new Email(sender,subject,msg);
        //                break;
        //            }
        //        case 'T':
        //            {
        //                int numberIndex = body.IndexOf(' ');
        //                string sender = body.Substring(0, numberIndex);
        //                string msg = body.Substring(numberIndex+1);
        //                output = new Tweet(sender,msg);
        //                break;
        //            }
        //        default:
        //            {
        //                throw new ArgumentNullException("Invalid header");
        //            }
                    

        //    }
        //    return output;
        //}
    }
}
