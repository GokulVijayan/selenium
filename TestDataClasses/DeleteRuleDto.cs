using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    public class DeleteRuleResponseMessage
    {
        public int messageType { get; set; }
        public string messageKey { get; set; }
        public string messageText { get; set; }
        public int messageValueType { get; set; }
    }

    public class DeleteRuleDto
    {
        public List<DeleteRuleResponseMessage> responseMessages { get; set; }
        public bool data { get; set; }
        public int status { get; set; }
    }
}