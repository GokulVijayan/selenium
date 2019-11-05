using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    class CrashResponseDto
    {
        public CrashRootObject crashDetails { get; set; }
        public int crashId { get; set; }
        public List<long> vehicles { get; set; }
        public List<long> casualties { get; set; }
        public CrashRecordValidationDetails crashRecordValidationDetails { get; set; }
    }
}
