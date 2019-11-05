using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    public class Request
    {
        public int year { get; set; }
        public IList<int> blackspotTypeIds { get; set; }
        public IList<int> blackspotStageIds { get; set; }
    }
}
