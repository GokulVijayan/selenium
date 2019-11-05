using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    public class CrashFieldValue
    {
        public int formFieldId { get; set; }
        public string value { get; set; }
        public object originalFieldValue { get; set; }
    }
}
