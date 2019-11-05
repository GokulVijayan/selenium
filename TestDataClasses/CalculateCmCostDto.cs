using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class CalculateCmCostDto
    {
        public List<object> responseMessages { get; set; }
        public CmData data { get; set; }
        public int status { get; set; }

    }

    public class CbaActualCountermeasure
    {
        public int id { get; set; }
        public object actualConstructionCost { get; set; }
        public object actualMaintenanceCost { get; set; }
        public object actualYear { get; set; }
        public int interventionMeasureQuantity { get; set; }
        public int cbaCounterMeasureSchemeId { get; set; }
        public int cbaCounterMeasureId { get; set; }
        public int upgradeTypeId { get; set; }
    }

    

    public class CbaActualCountermeasureDetail
    {
        public CbaActualCountermeasure countermeasure { get; set; }
        public Countermeasure counterDetailmeasures { get; set; }
        public double totalEstimatedConstructionCost { get; set; }
        public double totalEstimatedMaintenanceCost { get; set; }
        public double totalEstimatedConstructionCostInBaseYear { get; set; }
        public double totalEstimatedMaintenanceCostInBaseYear { get; set; }
        public object actualConstructionCostInBaseYear { get; set; }
        public object actualMaintenanceCostInBaseYear { get; set; }
    }

    public class CmData
    {
        public List<CbaActualCountermeasureDetail> cbaActualCountermeasureDetails { get; set; }
        public object actualEffectiveness { get; set; }
        public double estimatedEffectiveness { get; set; }
        public double totalBenefits { get; set; }
        public bool resetActualCost { get; set; }
    }


}
