using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Email:RawMessage
    {
        private string sender;
        private string type;
        private string sortCode;
        private string incident;
        private string subject;
        private string message;

        public Email() { }
        public Email(int id, string sender, string type, string subject, string message)
        {
            Id = id;
            Sender = sender;
            Type = type;
            Subject = subject;
            Message = message;
        }
        public Email(int id, string sender, string type, string subject, string message, string sortCode, string incident) : this(id, sender, type, subject, message)
        {
            SortCode = sortCode;
            Incident = incident;
        }
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string SortCode
        {
            get { return sortCode; }
            set { sortCode = value; }
        }
        public string Incident
        {
            get { return incident; }
            set { incident = value; }
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
