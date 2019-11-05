using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class FormFields
    {
        public List<ResponseMessage> responseMessages { get; set; }
        public List<Datum> data { get; set; }
        public int status { get; set; }
    }

    public class ResponseMessage
    {
        public int messageType { get; set; }
        public string messageKey { get; set; }
        public string messageText { get; set; }
        public int messageValueType { get; set; }
    }

    
    public class Datum
    {
        public int id { get; set; }
        public bool isMandatory { get; set; }
        public int childFormFieldId { get; set; }
        public int childLookupMasterId { get; set; }
        public int formId { get; set; }
        public int order { get; set; }
        public int propertyTypeId { get; set; }
        public int length { get; set; }
        public bool isActive { get; set; }
        public bool isMandatoryInUserRole { get; set; }
        public bool isVisibleInARF { get; set; }
        public bool isVisibleInMobileARF { get; set; }
        public bool isVisibleInUDR { get; set; }
        public bool isVisibleInQB { get; set; }
        public bool isVisibleInCT { get; set; }
        public bool isVisibleInStickAnalysis { get; set; }
        public bool isDynamic { get; set; }
        public int lookupId { get; set; }
        public int maxLimit { get; set; }
        public int minLimit { get; set; }
        public int groupId { get; set; }
        public string index { get; set; }
        public string icon { get; set; }
        public List<FormFieldName> formFieldName { get; set; }
        public List<FormFieldDescription> formFieldDescription { get; set; }
        public string defaultValue { get; set; }
        public string iconExtension { get; set; }
        public int accessType { get; set; }
        public string propertyName { get; set; }
        public string apiRoute { get; set; }
        public List<int> apiInputFields { get; set; }
    }
}
