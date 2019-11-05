using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class Cba
    {
        public List<object> collisionTypeIds { get; set; }
        public CbaCrashCostRequest cbaCrashCostRequest { get; set; }
        public string name { get; set; }
        public List<object> constructionYears { get; set; }
        public List<object> applicableAreaTypes { get; set; }

    }

    public class CbaCrashCostRequest
    {
        public string selectedAreaGeoJson { get; set; }
        public int fromDate { get; set; }
        public int toDate { get; set; }
    }


}
