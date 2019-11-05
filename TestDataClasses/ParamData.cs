using System;
using System.Collections.Generic;
using System.Text;

namespace iMAAPTestAPI
{
    class ParamData
    {
        public IList<SeverityWeighting> severityWeightings { get; set; }
        public int numberOfRanges { get; set; }
        public string highColor { get; set; }
        public string lowColor { get; set; }
        public int isInflection { get; set; }
        public string inflectionColor { get; set; }
        public DateTime fromDateForQuery { get; set; }
        public DateTime toDateForQuery { get; set; }
        public int corridorLength { get; set; }
        public int incrementLength { get; set; }
        public int distance { get; set; }
        public IList<Rule> rules { get; set; }
        public string identifySites { get; set; }
        public int noOfSites { get; set; }
        public int score { get; set; }
        public int minCrashes { get; set; }
        public int identificationType { get; set; }
        public int corridorWidth { get; set; }
    }
}
