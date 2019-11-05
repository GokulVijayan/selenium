using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FrameworkSetup.Pages
{
    internal class LookupPage
    {
        private readonly IWebDriver driver;
        private int step = 0;
        private List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        private readonly JObject jObject;
        private static bool isReportVerified = false;
        private readonly string exportLookupBtn = "exportLookupBtn";

        public LookupPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("LookupPageSelector.json");
        }

        public List<TestReportSteps> ExportLookups()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("ExportLookupFieldsReport.json");

            CheckForToaster();
            // Click 'Export' button
            ReusableComponents.JEClick(driver, "XPath", jObject[exportLookupBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            Thread.Sleep(Constant.waitTimeoutForExport);

            //Verify exported details
            bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
            if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".xlsx"))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Export Lookup Report"));

            //Verify that exported data matches the previewed data
            var excelSheet = ReusableComponents.ReadExcelFile(ReusableComponents.RetrieveFileName());
            IEnumerable<LookupExcelDto> mappedSheet = excelSheet.ConvertSheetToObjects<LookupExcelDto>();
            var lookupApiResponse = GetLookups.GetLookupDetails();
            foreach (var sheet in mappedSheet.ToList())
            {
                foreach (var lookup in lookupApiResponse.data)
                {
                    if ((sheet.LookupKey == lookup.lookupKey) &&
                        (sheet.LookupName == lookup.lookupDetailValues.FirstOrDefault().lookupValue) &&
                        (sheet.ShortCode == lookup.valueShortCode))
                    {
                        isReportVerified = true;
                        break;
                    }
                }
            }
            if (isReportVerified)
                listOfReport[step++].SetActualResultFail("");

            return listOfReport;
        }

        /// <summary>
        /// Gets the lookup page screenshots.
        /// </summary>
        /// <returns></returns>
        public List<string> GetLookupPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }

        /// <summary>
        /// Check For Toaster
        /// </summary>
        private void CheckForToaster()
        {
            if (ReusableComponents.FindIfElementExists(driver, "XPath", Constant.toastSelector))
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            }
        }
    }
}