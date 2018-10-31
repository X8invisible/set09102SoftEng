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

        public RawMessage ProcessMessage(string header, string body)
        {
            RawMessage output;
            switch(header[0])
            {
                case 'S':
                    {
                        int numberIndex = body.IndexOf(' ');
                        string sender = body.Substring(0, numberIndex);
                        string msg = body.Substring(numberIndex +1);
                        output = new Sms(sender, msg);
                        break;
                    }
                case 'E':
                    {
                        int senderIndex = body.IndexOf(' ');
                        string sender = body.Substring(0, senderIndex);
                        int subjectIndex = body.IndexOf(' ',senderIndex +1);
                        string subject = body.Substring(senderIndex, subjectIndex - senderIndex);
                        string msg = body.Substring(subjectIndex+1);
                        output = new Email(sender,subject,msg);
                        break;
                    }
                case 'T':
                    {
                        int numberIndex = body.IndexOf(' ');
                        string sender = body.Substring(0, numberIndex);
                        string msg = body.Substring(numberIndex+1);
                        output = new Tweet(sender,msg);
                        break;
                    }
                default:
                    {
                        throw new ArgumentNullException("Invalid header");
                    }
                    

            }
            return output;
        }
    }
}
