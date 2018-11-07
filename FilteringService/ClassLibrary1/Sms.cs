using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business
{

    public class Sms:RawMessage
    {
        private string sender;
        private string rawMessage;
        private string message;

        public Sms()
        {
        }
        public Sms(int id, string sender, string rawMessage)
        {
            Id = id;
            Sender = sender;
            RawMessage = rawMessage;
        }
        public override string FullId
        {
            get { return ("S" + Id.ToString()); }
        }
        public string Sender
        {
            get { return sender; }
            set
            {
                if (!Regex.IsMatch(value, @"^(\+?\d{3,20})$"))
                    throw new ArgumentException("Invalid phone number!");
                sender = value;
            }
        }
        public string RawMessage
        {
            get { return rawMessage; }
            set
            {
                if (value.Length > 140)
                    throw new ArgumentException("Message must be less than 140 characters!");
                rawMessage = value;
            }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public override string ToString()
        {
            string output = "Sender: "+ sender + " Message: " + message;
            return output;
        }
    }
}
