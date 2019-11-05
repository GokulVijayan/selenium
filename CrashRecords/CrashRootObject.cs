using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    public class CrashRootObject
    {
        public bool canValidateAll { get; set; }
        public bool? isConfirmed { get; set; }
        public int rowVersion { get; set; }
        public CrashData crash { get; set; }
        public List<CrashVehicle> vehicles { get; set; }
        public List<CrashCasualty> casualties { get; set; }
    }
}
