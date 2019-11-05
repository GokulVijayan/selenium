using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class SafetyCameraAnalysis
    {
        public int dateFrom { get; set; }
        public int datetTo { get; set; }
        public string length { get; set; }
        public string area { get; set; }
        public string areaTypeId { get; set; }
        public string ksiWeighting { get; set; }
        public string slightWeighting { get; set; }
        public List<int> crashIds { get; set; }
        public string siteName { get; set; }
        public int languageId { get; set; }
        public List<CameraCriteria> cameraCriteria { get; set; }
        public string createdOn { get; set; }
    }
    public class CameraCriteria2
    {
        public int id { get; set; }
        public int cameraTypeId { get; set; }
        public object minLength { get; set; }
        public int maxLength { get; set; }
        public string ksi { get; set; }
    }

    public class PicDetail
    {
        public int id { get; set; }
        public int cameraTypeId { get; set; }
        public int areaTypeId { get; set; }
        public string value { get; set; }
    }

    public class CameraCriteria
    {
        public CameraCriteria2 cameraCriteria { get; set; }
        public List<PicDetail> picDetails { get; set; }
        public int defaultPICValue { get; set; }
    }


}
