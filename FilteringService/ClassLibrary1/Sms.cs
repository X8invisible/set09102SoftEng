using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Sms
    {
        private int sender;
        private String message;

        public Sms()
        {
        }

        public Sms(int sender, string message)
        {
            this.Sender = sender;
            this.Message = message;
        }

        public int Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string Message
        {
            get { return Message; }
            set { message = value; }
        }
    }
}
