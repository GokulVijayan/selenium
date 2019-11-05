using System.Collections.Generic;

namespace FrameworkSetup.TestDataClasses
{
    public class ExportStatusData
    {
        public int numberOfVehicles { get; set; }
        public int numberOfCrashes { get; set; }
        public int numberOfCasualties { get; set; }
        public double expectedTimeInSeconds { get; set; }
    }

    public class ExportStatusDto
    {
        public List<object> responseMessages { get; set; }
        public ExportStatusData data { get; set; }
        public int status { get; set; }
    }

    public class ExportStatus
    {
        public int id { get; set; }
        public List<int> fields { get; set; }
        public int lookupExportOptions { get; set; }
        public int exportOption { get; set; }
        public int exportType { get; set; }
        public object fromDate { get; set; }
        public object toDate { get; set; }
        public bool canApplyActiveQuery { get; set; }
        public bool showOnlyValidRecords { get; set; }
    }
}