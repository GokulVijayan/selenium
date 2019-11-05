using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class BlackspotInformation
    {
        public int id { get; set; }
        public string siteWKT { get; set; }
        public BlackspotAttributes attributes { get; set; }
    }
}
