using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    public class PasswordPolicyMessage
    {
        public int id { get; set; }
        public string message { get; set; }
        public int languageId { get; set; }
    }

    public class PasswordPolicyData
    {
        public int id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
        public string valueValidationExpression { get; set; }
        public string description { get; set; }
        public List<PasswordPolicyMessage> passwordPolicyMessages { get; set; }
    }

    public class PasswordPolicyDto
    {
        public object responseMessages { get; set; }
        public List<PasswordPolicyData> data { get; set; }
        public int status { get; set; }
    }
}