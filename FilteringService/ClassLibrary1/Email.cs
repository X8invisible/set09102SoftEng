using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Email:RawMessage
    {
        private int id;
        private string sender;
        private string subject;
        private string message;

        public Email() { }
        public Email(int id, string sender, string subject, string message)
        {
            this.id = id;
            Sender = sender;
            Subject = subject;
            Message = message;
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
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public override string ToString()
        {
            string output = "Sender: "+ sender + " Subject: " + subject + " Message: " + message;
            return output;
        }
    }
}
