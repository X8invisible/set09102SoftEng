using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Tweet:RawMessage
    {
        private string sender;
        private string message;

        public Tweet()
        {
        }

        public Tweet(int id, string sender, string message)
        {
            Id = id;
            Sender = sender;
            Message = message;
        }
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public override string ToString()
        {
            string output = "Sender: " + sender + " Message: " + message;
            return output;
        }
    }
}
