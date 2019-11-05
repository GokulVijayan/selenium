using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    public class LookupDetailValueDto
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public string lookupValue { get; set; }
        public int languageId { get; set; }
    }

    public class LookupDescriptionDto
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public int languageId { get; set; }
        public string description { get; set; }
    }

    public class LookupData
    {
        public int id { get; set; }
        public int lookupMasterId { get; set; }
        public object lookupMasterName { get; set; }
        public string lookupKey { get; set; }
        public string valueShortCode { get; set; }
        public string icon { get; set; }
        public string iconExtension { get; set; }
        public string valueColorCode { get; set; }
        public int sortOrder { get; set; }
        public object dependentLookupDetailId { get; set; }
        public object lookupGroupId { get; set; }
        public object internalValue { get; set; }
        public List<LookupDetailValueDto> lookupDetailValues { get; set; }
        public List<LookupDescriptionDto> lookupDescription { get; set; }
    }

    public class LookupDto
    {
        public List<object> responseMessages { get; set; }
        public List<LookupData> data { get; set; }
        public int status { get; set; }
    }
}