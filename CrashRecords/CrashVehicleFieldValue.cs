using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    class CrashVehicleFieldValue
    {
        public int formId { get; set; }
        public int formFieldId { get; set; }
        public int fieldTypeId { get; set; }
        public int fieldLookupId { get; set; }
        public string index { get; set; }
        public List<VehicleFieldName> fieldNames { get; set; }
        public string value { get; set; }
        public bool isMandatory { get; set; }
    }
}
