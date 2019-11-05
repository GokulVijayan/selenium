
using Ex_haft.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    internal class LookupExcelDto
    {
        [Column(1)]
        public string LookupKey { get; set; }

        [Column(2)]
        public string ShortCode { get; set; }

        [Column(3)]
        public string LookupName { get; set; }

        [Column(4)]
        public string DependentLookup { get; set; }

        [Column(5)]
        public string LookupIcon { get; set; }
    }
}