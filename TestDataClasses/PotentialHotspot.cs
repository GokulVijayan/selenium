using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class PotentialHotspot
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public int blackspotTypeId { get; set; }
        public string location { get; set; }
        public int ksiCasualties { get; set; }
        public int ksiCrashes { get; set; }
        public double crashSeverityScore { get; set; }
        public double collisionTypePatternScore { get; set; }
        public string remarks { get; set; }
        public int ruleId { get; set; }
        public int ruleValue { get; set; }
        public int mpf { get; set; }
        public int blackspotAreaTypeId { get; set; }
        public int rank { get; set; }
        public int qualificationScore { get; set; }
        public int identificationFrom { get; set; }
        public int identificationTo { get; set; }
        public int fatalCrashes { get; set; }
        public int injuryCrashes { get; set; }
    }
}
