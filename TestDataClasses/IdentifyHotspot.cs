using System;
using System.Collections.Generic;
using System.Text;

namespace iMAAPTestAPI
{
    class IdentifyHotspot
    {
        public ParamData paramData { get; set; }
        public IList<string> geometries { get; set; }
        public string Action { get; set; }
        public int mapProjId { get; set; }
    }
}
