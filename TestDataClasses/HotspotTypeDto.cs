using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class HotspotTypeDto
    {
        public IList<ResponseMessage> responseMessages { get; set; }
        public string data { get; set; }
        public int status { get; set; }
    }
}
