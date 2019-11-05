using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FrameworkSetup.Pages
{
    class FormFieldPage
    {
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        readonly JObject jObject;

        private static bool isReportVerified = false;

        private string exportButton = "exportButton";

        public FormFieldPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("FormFieldPageSelector.json");
        }

        public List<TestReportSteps> ExportFormFields()
        {
            screenshotList.Clear();
            step = 0;

            listOfReport = ConfigFile.GetReportFile("ExportFormFieldReport.json");

            //Wait for toaster to disappear
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            Thread.Sleep(Constant.waitTimeoutForExport);

            //Click Export button
            ReusableComponents.JEClick(driver, "XPath", jObject[exportButton].ToString());
            listOfReport[step++].SetActualResultFail("");

            Thread.Sleep(Constant.waitTimeoutForExport);

            //Verify exported details
            bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
            if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".xlsx"))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Verify that exported data matches the previewed data
            var excelSheet = ReusableComponents.ReadExcelFile(ReusableComponents.RetrieveFileName());
            IEnumerable<FormFieldExcelDto> mappedSheet = excelSheet.ConvertSheetToObjects<FormFieldExcelDto>();
            var formFieldsApiResponse = GetFormFields.GetFormFieldDetails();
            foreach (var sheet in mappedSheet.ToList())
            {
                foreach (var formField in formFieldsApiResponse.Data)
                {
                    if ((sheet.Form == GetForm(formField.FormId))
                        &&
                        (sheet.Type == GetType(formField.PropertyTypeId))
                        &&
                        (sheet.Name == formField.FormFieldName.FirstOrDefault().Name)
                        &&
                        (!formField.Length.HasValue || sheet.Length == formField.Length.Value.ToString())
                        &&
                        (!formField.Length.HasValue && (sheet.Length == null || sheet.Length.Equals(string.Empty)))
                        &&
                        (sheet.Order == formField.Order.ToString()))
                    {
                        isReportVerified = true;
                        break;
                    }
                }
            }
            if (isReportVerified)
                listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Export Form Field"));

            return listOfReport;
        }

        private string GetType(int propertyTypeId)
        {
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            return property[propertyTypeId.ToString()].ToString();
        }

        private string GetForm(int formId)
        {
            JObject form = ConfigFile.RetrieveProperty("FormData.json");
            return form[formId.ToString()].ToString();
        }

        /// <summary>
        /// Returns form field page screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetFormFieldScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
