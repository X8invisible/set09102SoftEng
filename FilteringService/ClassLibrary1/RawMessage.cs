using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class RawMessage
    {
        private int id;

        public RawMessage(){ }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
