using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RawMessage
    {
        private String header;
        private String sender;
        private String message;

        public RawMessage()
        {
        }

        //public String Sender
        //{
        //    get { return sender; }
        //    set { sender = value; }
        //}
        public String Header
        {
            get { return header; }
            set { header = value; }
        }
        //public string Message
        //{
        //    get { return Message; }
        //    set { message = value; }
        //}
    }
}
