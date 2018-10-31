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
                        string msg = body.Substring(numberIndex, body.Length);
                        output = new Sms(sender, msg);
                        break;
                    }
                case 'E':
                    {
                        output = new Email();
                        break;
                    }
                case 'T':
                    {
                        output = new Tweet();
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
