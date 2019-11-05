using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{
    class StickAnalysisPage
    {
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public bool status = false;
        int crashCount = 0;
        public List<string> screenshotList = new List<string>();

        private string crashTab = "crashTab";
        private string stickAnalysisHeader = "stickAnalysisHeader";
        private string crashFieldSelector = "crashFieldSelector";
        private string runButton = "runButton";
        private string crashList = "crashList";

        public StickAnalysisPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("StickAnalysisPageSelector.json");
        }

        /// <summary>
        /// Verify web application stick analysis page is displayed
        /// </summary>
        public List<TestReportSteps> VerifyStickAnalysisPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyStickAnalysisPageReport.json");
            try
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[stickAnalysisHeader].ToString());
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify stick analysis page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Select fields from stick analysis page
        /// </summary>
        public List<TestReportSteps> SelectFieldsFromStickAnalysisPage(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SelectFieldsFromStickAnalysisReport.json");
            try
            {
                //Click Crash Tab
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashTab].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Crash Form Field
                ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", inputjson[crashFieldSelector].ToString(), jObject[crashFieldSelector].ToString());
                listOfReport[step++].SetActualResultFail("");

                status = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[crashFieldSelector].ToString(), inputjson[crashFieldSelector].ToString());
                if(status==true)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Stick Analysis Fields"));

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to select fields from stick analysis page.Exception caught : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Verify stick analysis 
        /// </summary>
        public List<TestReportSteps> RunStickAnalysisReport()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RunStickAnalysisReport.json");
            try
            {
                //Click on run button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[runButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[runButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to run stick analysis report.Exception caught : " + e);
            }
            return listOfReport;
        }





        /// <summary>
        /// Verify web application stick analysis report
        /// </summary>
        public List<TestReportSteps> VerifyStickAnalysisReport(int count)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyStickAnalysisReport.json");
            try
            {
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[stickAnalysisHeader].ToString());
                listOfReport[step++].SetActualResultFail("");


                //Compare Crash Count
                int crashCountInReport = ReusableComponents.ElementsCount(driver, "XPath", jObject[crashList].ToString());
                for (int i=1;i<= (crashCountInReport- (crashCountInReport/2)); i++)
                {
                    for(int j=0;j< count; j++)
                    {
                       if(ReusableComponents.RetrieveText(driver,"XPath", jObject[crashList].ToString()+"["+i+"]")==ApiReusableComponents.crashReferenceNumberList[j])
                        {
                            status = true;
                            crashCount++;
                        }
                    }
                }

                if (status == true && crashCount == ApiReusableComponents.crashReferenceNumberList.Count)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Stick Analysis"));

            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify crash count.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Retrieves stick analysis page screenshots
        /// </summary>
        /// <returns></returns>
        public List<string> GetStickAnalysisPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
