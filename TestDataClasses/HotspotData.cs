using System;
using System.Collections.Generic;
using System.Text;

namespace iMAAPTestAPI
{
    class HotspotData
    {
        public string remarks { get; set; }
        public IList<Blackspot> blackspots { get; set; }
        public object reportId { get; set; }
    }
}
