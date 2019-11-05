using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class Crash
    {
        public int id { get; set; }
        public string referenceNumber { get; set; }
        public DateTime crashDate { get; set; }
        public int crashTime { get; set; }
        public int numberOfVehicles { get; set; }
        public int numberOfCasualties { get; set; }
        public int validationStatusId { get; set; }
        public string imageUrl { get; set; }
        public string crashDateTime { get; set; }
        public int crashSeverityId { get; set; }
        public int rowVersion { get; set; }
    }
}
