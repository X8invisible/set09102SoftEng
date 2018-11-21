using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string rawMessage;
        private string message;

        public Email() { }
        public Email(int id, string sender, string type, string subject, string rawMessage)
        {
            Id = id;
            Sender = sender;
            Type = type;
            Subject = subject;
            RawMessage = rawMessage;
        }
        public Email(int id, string sender, string type, string subject, string rawMessage, string sortCode, string incident) : this(id, sender, type, subject, rawMessage)
        {
            SortCode = sortCode;
            Incident = incident;
        }
        public override string FullId
        {
            get { return ("E" + Id.ToString()); }
        }
        public string Sender
        {
            get { return sender; }
            set
            {
                if(!(new EmailAddressAttribute().IsValid(value)))
                {
                    throw new ArgumentException("Invalid e-mail address!");
                }
                sender = value;
            }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public string SortCode
        {
            get { return sortCode; }
            set
            {
                if(value !=null)
                    if (!Regex.IsMatch(value, @"^\d\d-\d\d-\d\d$"))
                        throw new ArgumentException("Sort code is invalid");
                sortCode = value;
            }
        }
        public string Incident
        {
            get { return incident; }
            set { incident = value; }
        }
        public string Subject
        {
            get { return subject; }
            set
            {
                if (value.Length > 20)
                    throw new ArgumentException("Subject must be less than 20 characters!");
                subject = value;
            }
        }
        public string RawMessage
        {
            get { return rawMessage; }
            set
            {
                if (value.Length > 1028)
                    throw new ArgumentException("Message must be less than 1028 characters!");
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
            string output = "Sender: "+ sender + " Subject: " + subject + " Message: " + message;
            return output;
        }
    }
}
