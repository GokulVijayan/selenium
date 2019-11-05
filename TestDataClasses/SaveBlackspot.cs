using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class SaveBlackspot
    {
        public object userQuery { get; set; }
        public int blackspotId { get; set; }
        public string siteInformationInWKT { get; set; }
        public SaveBlackspotInformation blackspotInformation { get; set; }
    }
}
