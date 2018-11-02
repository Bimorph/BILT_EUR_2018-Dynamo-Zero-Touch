using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroTouchNodes
{
    public class HelloWorld
    {
        //HelloWorld field
        private string _message;

        //HelloWorld property
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        //HelloWorld constructor
        public HelloWorld(string message)
        {
            _message = message;
        }

        //A HelloWorld method
        public bool Contains(string subString)
        {
            bool contains = Message.Contains(subString);

            return contains;
        }
    }
}
