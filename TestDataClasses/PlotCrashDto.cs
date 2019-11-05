using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class PlotCrashDto
    {
        public List<ResponseMessageApi> responseMessages { get; set; }
        public OutputData data { get; set; }
        public int status { get; set; }
    }
    public class ResponseMessageApi
    {
        public int messageType { get; set; }
        public string messageKey { get; set; }
        public string messageText { get; set; }
        public int messageValueType { get; set; }
    }

    public class GeometryData
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public int NumberOfCasualties { get; set; }
        public string CrashSeverity { get; set; }
        public int ValidationStatusId { get; set; }
        public string ReferenceNumber { get; set; }
        public int CrashSeverityId { get; set; }
        public string CollisionType { get; set; }
        public int NumberOfVehicles { get; set; }
        public int? CollisionTypeId { get; set; }
        public object CollisionTypeAngle { get; set; }
        public string CrashDate { get; set; }
        public string ValueColorCode { get; set; }
        public int? CrashTime { get; set; }
        public int Id { get; set; }
    }

    public class Feature
    {
        public GeometryData geometry { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
        public int id { get; set; }
    }

    public class OutputData
    {
        public int totalFeatures { get; set; }
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }
}
