using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    public class RuleValue
    {
        public int formFieldId { get; set; }
        public string formId { get; set; }
        public int operatorId { get; set; }
        public int valueTypeId { get; set; }
        public List<string> value { get; set; }
    }

    public class RuleMessage
    {
        public int id { get; set; }
        public int ruleId { get; set; }
        public int languageId { get; set; }
        public string message { get; set; }
    }

    public class ValidationRuleData
    {
        public bool isConfirmed { get; set; }
        public int id { get; set; }
        public int formFieldId { get; set; }
        public List<object> ruleCondition { get; set; }
        public RuleValue ruleValue { get; set; }
        public object ruleWhereCondition { get; set; }
        public string referenceNumber { get; set; }
        public bool validateWhileSave { get; set; }
        public object ruleDescription { get; set; }
        public List<RuleMessage> ruleMessages { get; set; }
    }

    public class ValidationRuleDto
    {
        public List<object> responseMessages { get; set; }
        public List<ValidationRuleData> data { get; set; }
        public int status { get; set; }
    }
}