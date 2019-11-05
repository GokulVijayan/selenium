using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class DeleteCrashResponse
    {
        public List<ResponseMessage> responseMessages { get; set; }
        public object data { get; set; }
        public int status { get; set; }
    }
    
}
