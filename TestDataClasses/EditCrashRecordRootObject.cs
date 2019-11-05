using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMAAPTestAPI.CrashRecords;


namespace iMAAPTestAPI
{
    public class EditCrashRecordRootObject
    {
        public List<ResponseMessage> responseMessages { get; set; }
        public CrashRootObject data { get; set; }
        public int status { get; set; }
    }
}