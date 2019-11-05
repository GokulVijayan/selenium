using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class BlackspotManagement
    {
        public object userQuery { get; set; }
        public int blackspotId { get; set; }
        public string siteInformationInWKT { get; set; }
        public BlackspotInformation blackspotInformation { get; set; }
    }
}
