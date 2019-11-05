using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class PlotCrashData
    {
        public int languageId { get; set; }
        public int mapProjId { get; set; }
        public DateTime toDateForQuery { get; set; }
        public DateTime fromDateForQuery { get; set; }
        public string crashSevIds { get; set; }
        public List<string> siteWkts { get; set; }
    }
}
