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
    internal class PasswordPolicyPage
    {
        private readonly IWebDriver driver;
        private int step = 0;
        private List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        private readonly JObject jObject;
        private static bool isReportVerified = false;
        private readonly string exportPasswordPoliciesBtn = "exportPasswordPoliciesBtn";

        public PasswordPolicyPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("PasswordPolicyPageSelector.json");
        }

        /// <summary>
        /// Exports the password policies.
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> ExportPasswordPolicies()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("ExportPasswordPoliciesReport.json");

            CheckForToaster();

            // Click 'Export' button
            ReusableComponents.JEClick(driver, "XPath", jObject[exportPasswordPoliciesBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            Thread.Sleep(Constant.waitTimeoutForExport);

            //Verify exported details
            bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
            if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".xlsx"))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Export Password Policy Report"));

            //Verify that exported data matches the previewed data
            var excelSheet = ReusableComponents.ReadExcelFile(ReusableComponents.RetrieveFileName());
            IEnumerable<PasswordPolicyExcelDto> mappedSheet = excelSheet.ConvertSheetToObjects<PasswordPolicyExcelDto>();
            var passwordPolicyApiResponse = GetPasswordPolicy.GetPasswordPolicies();
            foreach (var sheet in mappedSheet.ToList())
            {
                foreach (var passwordPolicy in passwordPolicyApiResponse.data)
                {
                    if ((sheet.Key == passwordPolicy.key) &&
                        (sheet.Value == passwordPolicy.value))
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