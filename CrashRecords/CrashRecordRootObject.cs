using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    class CrashRecordRootObject
    {
        public List<ResponseMessage> responseMessages { get; set; }
        public CrashResponseDto data { get; set; }
        public int status { get; set; }
    }
}
