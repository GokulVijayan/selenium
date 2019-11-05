using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class RunCba
    {
        public CbaActualCountersmeasure cbaActualCountermeasure { get; set; }
        public CbaCrashCostsRequest cbaCrashCostRequest { get; set; }
        public object constructionDate { get; set; }
        public object openingDate { get; set; }
    }

    

    public class CbaActualCountermeasuresDetail
    {
        public CbaActualCountermeasure cbaActualCountermeasure { get; set; }
        public Countermeasure countermeasure { get; set; }
        public int totalEstimatedConstructionCost { get; set; }
        public double totalEstimatedMaintenanceCost { get; set; }
        public double totalEstimatedConstructionCostInBaseYear { get; set; }
        public double totalEstimatedMaintenanceCostInBaseYear { get; set; }
        public object actualConstructionCostInBaseYear { get; set; }
        public object actualMaintenanceCostInBaseYear { get; set; }
    }

    public class CbaActualCountersmeasure
    {
        public List<CbaActualCountermeasuresDetail> cbaActualCountermeasureDetails { get; set; }
        public int actualEffectiveness { get; set; }
        public int estimatedEffectiveness { get; set; }
        public double totalBenefits { get; set; }
        public bool resetActualCost { get; set; }
    }

    public class CbaCrashCostsRequest
    {
        public string selectedAreaGeoJson { get; set; }
        public int fromDate { get; set; }
        public int toDate { get; set; }
    }

}
