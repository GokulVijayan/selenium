
using Ex_haft.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    internal class PasswordPolicyExcelDto
    {
        [Column(1)]
        public string Key { get; set; }

        [Column(2)]
        public string Value { get; set; }
    }
}