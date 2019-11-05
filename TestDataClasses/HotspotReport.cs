using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class HotspotReport
    {
        public IList<object> responseMessages { get; set; }
        public HotspotReportData data { get; set; }
        public int status { get; set; }
    }
}
