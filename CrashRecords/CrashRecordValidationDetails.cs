using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    class CrashRecordValidationDetails
    {
        public CrashValidationResult crashValidationResult { get; set; }
        public List<VehicleValidationResult> vehicleValidationResults { get; set; }
        public List<VehicleValidationResult> casualtyValidationResults { get; set; }
        public bool isValid { get; set; }
    }
}
