using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class CbaDto
    {
        public List<object> responseMessages { get; set; }
        public List<CbaDatum> data { get; set; }
        public int status { get; set; }

    }

    public class Countermeasure
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool applicableForCBA { get; set; }
        public int constructionYear { get; set; }
        public double estimatedConstructionCost { get; set; }
        public object unitOfMeasure { get; set; }
        public double knownMaintananceCost { get; set; }
        public int treatmentLifeTime { get; set; }
        public int? applicableAreaType { get; set; }
        public object applicableRoadType { get; set; }
        public int counterMeasureCategoryId { get; set; }
        public int? applicableControllType { get; set; }
        public object applicableTreatmentType { get; set; }
        public int source { get; set; }
        public string sourceUrl { get; set; }
        public double estimatedEffectiveness { get; set; }
        public string impactedAccidentSeverity { get; set; }
        public string benefits { get; set; }
        public string implementationIssues { get; set; }
    }

    public class LookupDetailsValue
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public string lookupValue { get; set; }
        public int languageId { get; set; }
    }

    public class LookupsDescription
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public int languageId { get; set; }
        public object description { get; set; }
    }

    public class CollisionType
    {
        public int id { get; set; }
        public int lookupMasterId { get; set; }
        public object lookupMasterName { get; set; }
        public string lookupKey { get; set; }
        public string valueShortCode { get; set; }
        public object icon { get; set; }
        public string iconExtension { get; set; }
        public string valueColorCode { get; set; }
        public int sortOrder { get; set; }
        public object dependentLookupDetailId { get; set; }
        public object lookupGroupId { get; set; }
        public string internalValue { get; set; }
        public List<LookupDetailsValue> lookupDetailValues { get; set; }
        public List<LookupsDescription> lookupDescription { get; set; }
    }

    public class CbaDatum
    {
        public Countermeasure countermeasure { get; set; }
        public List<CollisionType> collisionTypes { get; set; }
    }


}
