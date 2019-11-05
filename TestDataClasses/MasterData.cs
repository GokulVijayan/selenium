using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class MasterData
    {
        public List<object> responseMessages { get; set; }
        public Data data { get; set; }
        public int status { get; set; }
    }
    public class LookupDetailValue
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public string lookupValue { get; set; }
        public int languageId { get; set; }
    }

    public class LookupDescription
    {
        public int id { get; set; }
        public int lookupDetailId { get; set; }
        public int languageId { get; set; }
        public string description { get; set; }
    }

    public class Lookup
    {
        public int id { get; set; }
        public int lookupMasterId { get; set; }
        public string lookupMasterName { get; set; }
        public string lookupKey { get; set; }
        public string valueShortCode { get; set; }
        public string icon { get; set; }
        public string iconExtension { get; set; }
        public string valueColorCode { get; set; }
        public int sortOrder { get; set; }
        public int dependentLookupDetailId { get; set; }
        public int lookupGroupId { get; set; }
        public string internalValue { get; set; }
        public List<LookupDetailValue> lookupDetailValues { get; set; }
        public List<LookupDescription> lookupDescription { get; set; }
    }

    public class LookupMasterNameModel
    {
        public int id { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class LookupMaster
    {
        public int dependentLookupId { get; set; }
        public int id { get; set; }
        public bool isGroupable { get; set; }
        public string internalName { get; set; }
        public List<LookupMasterNameModel> lookupMasterNameModel { get; set; }
    }

    public class FormFieldName
    {
        public int id { get; set; }
        public int formFieldId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class FormFieldDescription
    {
        public int id { get; set; }
        public int formFieldId { get; set; }
        public int languageId { get; set; }
        public string description { get; set; }
    }

    public class Field
    {
        public int id { get; set; }
        public bool isMandatory { get; set; }
        public int childFormFieldId { get; set; }
        public int childLookupMasterId { get; set; }
        public int formId { get; set; }
        public int order { get; set; }
        public int propertyTypeId { get; set; }
        public int length { get; set; }
        public bool isActive { get; set; }
        public bool isMandatoryInUserRole { get; set; }
        public bool isVisibleInARF { get; set; }
        public bool isVisibleInMobileARF { get; set; }
        public bool isVisibleInUDR { get; set; }
        public bool isVisibleInQB { get; set; }
        public bool isVisibleInCT { get; set; }
        public bool isVisibleInStickAnalysis { get; set; }
        public bool isDynamic { get; set; }
        public int lookupId { get; set; }
        public int maxLimit { get; set; }
        public int minLimit { get; set; }
        public int groupId { get; set; }
        public string index { get; set; }
        public string icon { get; set; }
        public List<FormFieldName> formFieldName { get; set; }
        public List<FormFieldDescription> formFieldDescription { get; set; }
        public string defaultValue { get; set; }
        public string iconExtension { get; set; }
        public int accessType { get; set; }
        public string propertyName { get; set; }
        public string apiRoute { get; set; }
        public List<int> apiInputFields { get; set; }
    }

    public class FieldTypeName
    {
        public int id { get; set; }
        public int fieldTypeId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class FieldType
    {
        public int id { get; set; }
        public string internalName { get; set; }
        public string type { get; set; }
        public List<FieldTypeName> fieldTypeName { get; set; }
    }

    public class FormNameModel
    {
        public int id { get; set; }
        public int formId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Form
    {
        public int id { get; set; }
        public string internalName { get; set; }
        public bool isActive { get; set; }
        public int order { get; set; }
        public int minLimit { get; set; }
        public int maxLimit { get; set; }
        public List<FormNameModel> formNameModel { get; set; }
    }

    public class FieldGroupName
    {
        public int id { get; set; }
        public int groupId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class FieldGroup
    {
        public int id { get; set; }
        public string internalName { get; set; }
        public int order { get; set; }
        public List<FieldGroupName> fieldGroupNames { get; set; }
    }

    public class FormFieldName2
    {
        public int id { get; set; }
        public int formFieldId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class SearchField
    {
        public int formFieldId { get; set; }
        public int fieldTypeId { get; set; }
        public int fieldLookupId { get; set; }
        public int queryConditionTypeId { get; set; }
        public List<FormFieldName2> formFieldNames { get; set; }
    }

    public class SearchListPlaceholder
    {
        public string position { get; set; }
        public string formField { get; set; }
    }

    public class IdentifyCrashPlaceholder
    {
        public string position { get; set; }
        public string formField { get; set; }
    }

    public class QbPlaceholder
    {
        public string position { get; set; }
        public string formField { get; set; }
    }

    public class QueryConditionTypeName
    {
        public int id { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
        public int queryConditionTypeId { get; set; }
    }

    public class QueryConditionType
    {
        public int id { get; set; }
        public string key { get; set; }
        public int fieldTypeId { get; set; }
        public bool isDefault { get; set; }
        public string @operator { get; set; }
        public int order { get; set; }
        public List<QueryConditionTypeName> queryConditionTypeNames { get; set; }
    }

    public class BlackspotAreaTypeName
    {
        public int id { get; set; }
        public int blackspotAreaTypeId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class BlackspotAreaType
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public string internalName { get; set; }
        public List<BlackspotAreaTypeName> blackspotAreaTypeNames { get; set; }
    }

    public class BlackspotStageName
    {
        public int id { get; set; }
        public int blackspotStageId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class BlackspotStage
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public string interalName { get; set; }
        public string colorCode { get; set; }
        public List<BlackspotStageName> blackspotStageNames { get; set; }
    }

    public class CountermeasureStatusName
    {
        public int id { get; set; }
        public int countermeasureStatusId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class CountermeasureStatu
    {
        public int id { get; set; }
        public string internalName { get; set; }
        public bool isActive { get; set; }
        public List<CountermeasureStatusName> countermeasureStatusNames { get; set; }
    }

    public class BlackspotRuleName
    {
        public int id { get; set; }
        public int blackspotRuleId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class BlackspotRule
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isDefault { get; set; }
        public string interalName { get; set; }
        public int defaultValue { get; set; }
        public List<BlackspotRuleName> blackspotRuleNames { get; set; }
    }

    public class MonitoringOptionName
    {
        public int id { get; set; }
        public int monitoringOptionId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class BlackspotMonitoringOption
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public string interalName { get; set; }
        public List<MonitoringOptionName> monitoringOptionNames { get; set; }
    }

    public class DropdownDetailValue
    {
        public int id { get; set; }
        public int dropdownDetailId { get; set; }
        public int languageId { get; set; }
        public string text { get; set; }
    }

    public class ApplicableTreatmentType
    {
        public int id { get; set; }
        public string key { get; set; }
        public int dropdownMasterId { get; set; }
        public List<DropdownDetailValue> dropdownDetailValues { get; set; }
    }

    public class DropdownDetailValue2
    {
        public int id { get; set; }
        public int dropdownDetailId { get; set; }
        public int languageId { get; set; }
        public string text { get; set; }
    }

    public class ApplicableAreaType
    {
        public int id { get; set; }
        public string key { get; set; }
        public int dropdownMasterId { get; set; }
        public List<DropdownDetailValue2> dropdownDetailValues { get; set; }
    }

    public class DropdownDetailValue3
    {
        public int id { get; set; }
        public int dropdownDetailId { get; set; }
        public int languageId { get; set; }
        public string text { get; set; }
    }

    public class ApplicableControlType
    {
        public int id { get; set; }
        public string key { get; set; }
        public int dropdownMasterId { get; set; }
        public List<DropdownDetailValue3> dropdownDetailValues { get; set; }
    }

    public class DropdownDetailValue4
    {
        public int id { get; set; }
        public int dropdownDetailId { get; set; }
        public int languageId { get; set; }
        public string text { get; set; }
    }

    public class Source
    {
        public int id { get; set; }
        public string key { get; set; }
        public int dropdownMasterId { get; set; }
        public List<DropdownDetailValue4> dropdownDetailValues { get; set; }
    }

    public class DropdownDetailValue5
    {
        public int id { get; set; }
        public int dropdownDetailId { get; set; }
        public int languageId { get; set; }
        public string text { get; set; }
    }

    public class UnitOfMeasure
    {
        public int id { get; set; }
        public string key { get; set; }
        public int dropdownMasterId { get; set; }
        public List<DropdownDetailValue5> dropdownDetailValues { get; set; }
    }

    public class CbaUpgradeType2
    {
        public int id { get; set; }
        public string key { get; set; }
        public int percentage { get; set; }
    }

    public class CbaUpgradeTypeValue
    {
        public int id { get; set; }
        public int cbaUpgradeTypeId { get; set; }
        public int languageId { get; set; }
        public string text { get; set; }
    }

    public class CbaUpgradeType
    {
        public CbaUpgradeType2 cbaUpgradeType { get; set; }
        public List<CbaUpgradeTypeValue> cbaUpgradeTypeValues { get; set; }
    }

    public class DependentFormFieldDetail
    {
        public int parentFormFieldId { get; set; }
        public int childFormFieldId { get; set; }
        public int lookupDetailid { get; set; }
        public bool isVisible { get; set; }
    }

    public class Data
    {
        public List<Lookup> lookups { get; set; }
        public List<LookupMaster> lookupMasters { get; set; }
        public List<Field> fields { get; set; }
        public List<FieldType> fieldTypes { get; set; }
        public List<Form> forms { get; set; }
        public List<FieldGroup> fieldGroups { get; set; }
        public List<SearchField> searchFields { get; set; }
        public List<SearchListPlaceholder> searchListPlaceholder { get; set; }
        public List<IdentifyCrashPlaceholder> identifyCrashPlaceholder { get; set; }
        public List<QbPlaceholder> qbPlaceholder { get; set; }
        public List<QueryConditionType> queryConditionTypes { get; set; }
        public List<BlackspotAreaType> blackspotAreaTypes { get; set; }
        public List<BlackspotStage> blackspotStages { get; set; }
        public List<CountermeasureStatu> countermeasureStatus { get; set; }
        public List<BlackspotRule> blackspotRules { get; set; }
        public List<BlackspotMonitoringOption> blackspotMonitoringOptions { get; set; }
        public List<ApplicableTreatmentType> applicableTreatmentType { get; set; }
        public List<ApplicableAreaType> applicableAreaType { get; set; }
        public List<ApplicableControlType> applicableControlType { get; set; }
        public List<Source> source { get; set; }
        public List<UnitOfMeasure> unitOfMeasure { get; set; }
        public List<CbaUpgradeType> cbaUpgradeTypes { get; set; }
        public List<DependentFormFieldDetail> dependentFormFieldDetails { get; set; }
    }

    
}
