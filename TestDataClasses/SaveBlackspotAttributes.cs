using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class SaveBlackspotAttributes
    {

        public string name { get; set; }
        public int year { get; set; }
        public string blackspotTypeId { get; set; }
        public string location { get; set; }
        public int ksiCasualties { get; set; }
        public int ksiCrashes { get; set; }
        public int crashSeverityScore { get; set; }
        public int collisionTypePatternScore { get; set; }
        public object remarks { get; set; }
        public int ruleId { get; set; }
        public int mpf { get; set; }
        public int blackspotAreaTypeId { get; set; }
        public int blackspotStageId { get; set; }
        public int fatalCrashes { get; set; }
        public int injuryCrashes { get; set; }
        public bool isDeleted { get; set; }
        public int ruleValue { get; set; }
        public object treatedDate { get; set; }
        public DateTime identificationFrom { get; set; }
        public DateTime identificationTo { get; set; }
        public string saveConfirm { get; set; }
    }
}
