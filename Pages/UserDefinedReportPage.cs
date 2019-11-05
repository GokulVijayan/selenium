using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.Pages
{
    class UserDefinedReportPage
    {

        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public List<string> screenshotList = new List<string>();

        private string verifyReportPageTitle = "verifyReportPageTitle";
        private string filterOption = "filterOption";
        private string checkValidRecords = "checkValidRecords";
        private string fromDate = "fromDate";
        private string toDate = "toDate";
        private string applyButton = "applyButton";
        private string crashTab = "crashTab";
        private string noOfVehicles = "noOfVehicles";
        private string noOfCasualties = "noOfCasualties";
        private string rows = "rows";
        private string columns = "columns";
        private string runButton = "runButton";

       

        public UserDefinedReportPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("UserDefinedReportPageSelector.json");
        }

        /// <summary>
        /// Verify web application user defined report page displayed
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyUserDefinedReport.json");
            try
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportPageTitle].ToString());
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Apply filter in user defined report
        /// </summary>
        public List<TestReportSteps> ApplyFilterInUserDefinedReport()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyFilterInUserDefinedReport.json");
            try
            {
                //Click filter option
                ReusableComponents.Click(driver, "XPath", jObject[filterOption].ToString());
                Console.WriteLine("User was able to click on filter option");
                listOfReport[step++].SetActualResultFail("");

                //Check 'Valid Records'
                ReusableComponents.Click(driver, "XPath", jObject[checkValidRecords].ToString());
                Console.WriteLine("User was able to click on valid records option");
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                Console.WriteLine("User was able to select from date from calender");
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                Console.WriteLine("User was able to select to date from calender");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

                //Click 'Apply' button
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                Console.WriteLine("User was able to click on apply button");
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to apply filter in user defined report.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Retrieve crash count from user defined report
        /// </summary>
        public List<TestReportSteps> RetrieveCrashCountFromReport(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RetrieveCrashCountFromUserDefinedReport.json");
            try
            {
                //Click on crash tab
                ReusableComponents.Click(driver,"XPath", jObject[crashTab].ToString());
                Console.WriteLine("User was able to click on crash tab");
                listOfReport[step++].SetActualResultFail("");

                //Drag and drop no of vehicles field to row section
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[noOfVehicles].ToString(), jObject[rows].ToString());
                Console.WriteLine("User was able to drag and drop no of vehicles to row section");
                listOfReport[step++].SetActualResultFail("");

                //Drag and drop no of casualties field to column section
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[noOfCasualties].ToString(), jObject[columns].ToString());
                Console.WriteLine("User was able to drag and drop no of casualties to row section");
                listOfReport[step++].SetActualResultFail("");

                //Click on run button
                ReusableComponents.Click(driver, "XPath", jObject[runButton].ToString());
                Console.WriteLine("User was able to click on run button");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Run Summary Report"));
            }
            catch (Exception e)
            {
                Console.WriteLine("User was not able to retrieve crash count from user defined report"+e);
            }
            return listOfReport;
        }

                /// <summary>
                /// Retrieves user defined report page screenshots
                /// </summary>
                /// <returns></returns>
                public List<string> GetUserDefinedReportPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
