using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class ResponseMessage
    {
        public int messageType { get; set; }
        public string messageKey { get; set; }
        public string messageText { get; set; }
        public int messageValueType { get; set; }
    }
}
