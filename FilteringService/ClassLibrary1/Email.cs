using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Email:RawMessage
    {
        private String sender;
        private String subject;
        private String message;

        public String Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public String Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Message
        {
            get { return Message; }
            set { message = value; }
        }
    }
}
