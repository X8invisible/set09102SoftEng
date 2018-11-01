using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Serializable]
    public class Sms:RawMessage
    {
        private int id;
        private string sender;
        private string message;

        public Sms()
        {
        }
        public Sms(int id, string sender, string message)
        {
            this.id = id;
            this.Sender = sender;
            this.Message = message;
        }

        public int Id
        {
            get { return id; }
        }
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string Message
        {
            get { return Message; }
            set { message = value; }
        }
        public override string ToString()
        {
            string output = "Sender: "+ sender + " Message: " + message;
            return output;
        }
    }
}
