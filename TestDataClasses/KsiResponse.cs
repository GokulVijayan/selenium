
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class KsiResponse
    {
        public IList<ResponseMessage> responseMessages { get; set; }
        public bool data { get; set; }
        public int status { get; set; }
    }
}
