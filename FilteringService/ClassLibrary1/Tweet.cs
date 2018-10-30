﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    class Tweet
    {
        private String sender;
        private String message;

        public Tweet()
        {
        }

        public Tweet(string sender, string message)
        {
            this.sender = sender;
            this.message = message;
        }

        public String Sender
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