using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class CalculateCmCost
    {
        public int id { get; set; }
        public object actualConstructionCost { get; set; }
        public object actualMaintenanceCost { get; set; }
        public object actualYear { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public int interventionMeasureQuantity { get; set; }
        public int cbaCounterMeasureSchemeId { get; set; }
        public int cbaCounterMeasureId { get; set; }
        public int upgradeTypeId { get; set; }
        public string actualConstructionCostInBaseYear { get; set; }
        public string actualMaintenanceCostInBaseYear { get; set; }

    }

    

}
