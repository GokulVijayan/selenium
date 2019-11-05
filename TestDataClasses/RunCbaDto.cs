using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class RunCbaDto
    {
        public List<object> responseMessages { get; set; }
        public CbaData data { get; set; }
        public int status { get; set; }
    }

    public class CbaLookupDetailValue
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public string lookupValue { get; set; }
        public int languageId { get; set; }
    }

    public class CrashStatistic
    {
        public List<CbaLookupDetailValue> lookupDetailValues { get; set; }
        public double count { get; set; }
        public double reducedCount { get; set; }
    }

    public class CbaCalculationSummaryDetail
    {
        public int year { get; set; }
        public double? constructionCost { get; set; }
        public double? maintenanceCost { get; set; }
        public double? savingCost { get; set; }
        public int? sumRecievedYear { get; set; }
        public double? discountedConstructionCost { get; set; }
        public double? discountedMaintenanceCost { get; set; }
        public double? discountedSavingCost { get; set; }
    }

    public class CbaData
    {
        public double noOfYearsForEvaluation { get; set; }
        public double discountPercentage { get; set; }
        public int analysisNoOfYears { get; set; }
        public double totalConstrcutionCost { get; set; }
        public double totalMaintenanceCost { get; set; }
        public double totalBenefit { get; set; }
        public double netPresentValue { get; set; }
        public double benefitToCostRatio { get; set; }
        public double firstYearRateOfReturn { get; set; }
        public List<CrashStatistic> crashStatistics { get; set; }
        public List<object> casualtyStatistics { get; set; }
        public List<CbaCalculationSummaryDetail> cbaCalculationSummaryDetails { get; set; }
    }

}
