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
    internal class ExportCrashPage
    {
        private readonly IWebDriver driver;
        private int step = 0;
        private List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        private readonly JObject jObject;


        private readonly string crashAccordion = "crashAccordion",
                                getNumofRecBtn = "getNumofRecBtn",
                                startExportBtn = "startExportBtn",
                                selectAllCrash = "selectAllCrash",
                                progressBar = "progressBar",
                                exportFileBtn = "exportFileBtn";


        public ExportCrashPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("ExportCrashPageSelector.json");
        }

        /// <summary>
        /// Exports the password policies.
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> ExportCrash()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("ExportCrashReport.json");

            //Wait for loader to close
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.webLoader);

            //Wait for toast to disappear
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            // Click 'Crash' accordion
            ReusableComponents.JEClick(driver, "XPath", jObject[crashAccordion].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Select all fields
            ReusableComponents.JEClick(driver, "XPath", jObject[selectAllCrash].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Click Get no of records button
            ReusableComponents.JEClick(driver, "XPath", jObject[getNumofRecBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Wait for loader to close
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.webLoader);

            //Wait for toast to disappear
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            CheckForToaster();

            //Click start export button
            ReusableComponents.JEClick(driver, "XPath", jObject[startExportBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Wait untill 100%
            while (true)
            {
                var status = ReusableComponents.RetrieveText(driver, "XPath", jObject[progressBar].ToString());
                if (status == "Progress: 100%")
                {
                    break;
                }
            }

            //Click export file
            ReusableComponents.JEClick(driver, "XPath", jObject[exportFileBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            Thread.Sleep(Constant.waitTimeoutForExport);

            //Verify exported details
            bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
            if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".xlsx"))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Capture screenshots
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Export Crash Report"));

            ////Verify that exported data matches the previewed data
            var excelSheet = ReusableComponents.ReadExcelFile(ReusableComponents.RetrieveFileName());
            IEnumerable<ExportCrashExcelDto> mappedSheet = excelSheet.ConvertSheetToObjects<ExportCrashExcelDto>();
            int crash = 0, vehicles = 0, casualites = 0;
            foreach (var sheet in mappedSheet.ToList())
            {
                ++crash;
                vehicles = vehicles + Convert.ToInt32(sheet.NumberOfVehicles);
                casualites = casualites + Convert.ToInt32(sheet.NumberOfCasualities);
            }
            var exportStatusResponse = GetExportStatus.GetExportStatusMethod();
            if ((crash == exportStatusResponse.data.numberOfCrashes) &&
                (vehicles == exportStatusResponse.data.numberOfVehicles) &&
                (casualites == exportStatusResponse.data.numberOfCasualties))
                listOfReport[step++].SetActualResultFail("");

            return listOfReport;
        }

        /// <summary>
        /// Gets the password policy page screenshots.
        /// </summary>
        /// <returns></returns>
        public List<string> GetPasswordPolicyPageScreenshots()
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