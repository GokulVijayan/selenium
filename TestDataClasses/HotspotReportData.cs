using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class HotspotReportData
    {

        public Request request { get; set; }
        public int currentDate { get; set; }
        public IList<PotentialHotspot> potentialBlackspots { get; set; }
        public IList<object> activeBlackspots { get; set; }
        public IList<object> treatedBlackspots { get; set; }
    }
}
