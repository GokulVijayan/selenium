using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class SaveBlackspotInformation
    {
        public int id { get; set; }
        public string siteWKT { get; set; }
        public SaveBlackspotAttributes attributes { get; set; }
        public int mapProjId { get; set; }
    }
}
