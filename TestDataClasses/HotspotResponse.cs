using FrameworkSetup.TestDataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace iMAAPTestAPI
{
    class HotspotResponse
    {
        public IList<ResponseMessage> responseMessages { get; set; }
        public HotspotReportData data { get; set; }
        public int status { get; set; }
    }
}
