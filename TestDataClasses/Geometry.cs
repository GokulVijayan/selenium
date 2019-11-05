using System;
using System.Collections.Generic;
using System.Text;

namespace iMAAPTestAPI
{
    class Geometry
    {
        public string type { get; set; }
        public IList<IList<IList<double>>> coordinates { get; set; }
    }
}
