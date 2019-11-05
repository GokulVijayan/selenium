using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    public class FormFieldDto
    {
        public List<object> ResponseMessages { get; set; }
        public List<FormFieldAPIData> Data { get; set; }
        public int Status { get; set; }
    }
    public class FormFieldAPIData
    {
        public int Id { get; set; }
        public bool IsMandatory { get; set; }
        public int ChildFormFieldId { get; set; }
        public int ChildLookupMasterId { get; set; }
        public int FormId { get; set; }
        public int Order { get; set; }
        public int PropertyTypeId { get; set; }
        public int? Length { get; set; }
        public bool IsActive { get; set; }
        public bool IsMandatoryInUserRole { get; set; }
        public bool IsVisibleInARF { get; set; }
        public bool? IsVisibleInMobileARF { get; set; }
        public bool IsVisibleInUDR { get; set; }
        public bool IsVisibleInQB { get; set; }
        public bool IsVisibleInCT { get; set; }
        public bool IsVisibleInStickAnalysis { get; set; }
        public bool IsDynamic { get; set; }
        public int? LookupId { get; set; }
        public int? MaxLimit { get; set; }
        public int? MinLimit { get; set; }
        public object GroupId { get; set; }
        public string Index { get; set; }
        public string Icon { get; set; }
        public List<FieldName> FormFieldName { get; set; }
        public List<FieldDescription> FormFieldDescription { get; set; }
        public string DefaultValue { get; set; }
        public string IconExtension { get; set; }
        public int AccessType { get; set; }
        public string PropertyName { get; set; }
        public object ApiRoute { get; set; }
        public object ApiInputFields { get; set; }
    }

    public class FieldName
    {
        public int Id { get; set; }
        public int FormFieldId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }

    public class FieldDescription
    {
        public int Id { get; set; }
        public int FormFieldId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
    }
}
