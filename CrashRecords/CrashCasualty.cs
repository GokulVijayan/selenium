using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    public class CrashCasualty
    {
        public int id { get; set; }
        public bool isRemoved { get; set; }
        public List<CrashFieldValue> fieldValues { get; set; }
    }
}
