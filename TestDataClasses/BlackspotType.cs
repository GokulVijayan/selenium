using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class BlackspotType
    {
        public int id { get; set; }
        public IList<BlackspotTypeName> blackspotTypeName { get; set; }
        public string remarks { get; set; }
        public bool isActive { get; set; }
    }
}
