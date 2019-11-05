using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI.CrashRecords
{
    class SearchCrashResponseDto
    {
        public List<ResponseMessage> responseMessages { get; set; }
        public List<Datum> data { get; set; }
        public int status { get; set; }
    }

    public class FieldName
    {
        public int id { get; set; }
        public int formFieldId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class FieldValue
    {
        public int formId { get; set; }
        public int formFieldId { get; set; }
        public int fieldTypeId { get; set; }
        public int fieldLookupId { get; set; }
        public string index { get; set; }
        public List<FieldName> fieldNames { get; set; }
        public string value { get; set; }
        public bool isMandatory { get; set; }
    }

    public class Crash
    {
        public int id { get; set; }
        public bool isRemoved { get; set; }
        public List<FieldValue> fieldValues { get; set; }
    }

    public class FieldName2
    {
        public int id { get; set; }
        public int formFieldId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class FieldValue2
    {
        public int formId { get; set; }
        public int formFieldId { get; set; }
        public int fieldTypeId { get; set; }
        public int fieldLookupId { get; set; }
        public string index { get; set; }
        public List<FieldName2> fieldNames { get; set; }
        public string value { get; set; }
        public bool isMandatory { get; set; }
    }

    public class Vehicle
    {
        public int id { get; set; }
        public bool isRemoved { get; set; }
        public List<FieldValue2> fieldValues { get; set; }
    }

    public class FieldName3
    {
        public int id { get; set; }
        public int formFieldId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class FieldValue3
    {
        public int formId { get; set; }
        public int formFieldId { get; set; }
        public int fieldTypeId { get; set; }
        public int fieldLookupId { get; set; }
        public string index { get; set; }
        public List<FieldName3> fieldNames { get; set; }
        public string value { get; set; }
        public bool isMandatory { get; set; }
    }

    public class Casualty
    {
        public int id { get; set; }
        public bool isRemoved { get; set; }
        public List<FieldValue3> fieldValues { get; set; }
    }

    public class Datum
    {
        public object isConfirmed { get; set; }
        public bool canValidateAll { get; set; }
        public int attachmentCount { get; set; }
        public int rowVersion { get; set; }
        public Crash crash { get; set; }
        public List<Vehicle> vehicles { get; set; }
        public List<Casualty> casualties { get; set; }
        public List<object> attachments { get; set; }
    }

    public class RootObject
    {
        public List<object> responseMessages { get; set; }
        public List<Datum> data { get; set; }
        public int status { get; set; }
    }
}
