using System;
using System.Collections.Generic;
using System.Text;

namespace iMAAPTestAPI
{
    class Blackspot
    {
        public int weightage { get; set; }
        public IList<HotspotAnalysisCrash> crashes { get; set; }
        public string matchingRule { get; set; }
        public Geometry geometry { get; set; }
        public string color { get; set; }
        public string id { get; set; }
    }
}
