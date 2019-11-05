using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class CalculateCrashCostDto
    {
        public List<object> responseMessages { get; set; }
        public List<Datums> data { get; set; }
        public int status { get; set; }

    }

    public class LookupDetailValues
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public string lookupValue { get; set; }
        public int languageId { get; set; }
    }

    public class Datums
    {
        public List<LookupDetailValues> lookupDetailValues { get; set; }
        public int numberOfCrashes { get; set; }
        public double averageNumberOfCrashes { get; set; }
        public double costPerCrash { get; set; }
        public double cost { get; set; }
        public bool isDamageOnly { get; set; }
    }


}
