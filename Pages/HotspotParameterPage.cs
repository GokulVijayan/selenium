using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FrameworkSetup.Pages
{
    class HotspotParameterPage
    {
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public List<string> screenshotList = new List<string>();

        private string dateRange = "dateRange";
        private string resolutionDistance = "resolutionDistance";
        private string corridorLength = "corridorLength";
        private string incrementalLength = "incrementalLength";
        private string maxCrashType = "maxCrashType";
        private string minCrashType = "minCrashType";
        private string qualificationScore = "qualificationScore";
        private string minCrashForCrashType = "minCrashForCrashType";
        private string minCrashForCluster = "minCrashForCluster";
        private string accessControls = "accessControls";
        private string accessControlOptions1 = "accessControlOptions1";
        private string accessControlOptions2 = "accessControlOptions2";
        private string accessControlOptions3 = "accessControlOptions3";
        private string accessControlOptions4 = "accessControlOptions4";
        private string accessControlOptions5 = "accessControlOptions5";
        private string accessControl1 = "accessControl1";
        private string accessControl2 = "accessControl2";
        private string accessControl3 = "accessControl3";
        private string accessControl4 = "accessControl4";
        private string accessControl5 = "accessControl5";
        private string generalParameters = "generalParameters";
        private string verifyDateRange = "verifyDateRange";
        private string resolutionLength = "resolutionLength";
        private string verifyCorridorLength = "verifyCorridorLength";
        private string verifyIncrementalLength = "verifyIncrementalLength";
        private string attributeName = "attributeName";
        private string saveBtn = "saveBtn";
        private string toastMsg = "toastMsg";

        public HotspotParameterPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("HotspotParameterPageSelector.json");
        }

        /// <summary>
        /// Verify access controls displayed
        /// </summary>
        public List<TestReportSteps> VerifyAccessControls(JToken inputjson)
        {
            step = 0;
            bool verifyOption1, verifyOption2, verifyOption3, verifyOption4, verifyOption5;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyAccessControlsReport.json");
            try
            {
                //Tap 'ACCESS CONTROLS'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[accessControls].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[accessControls].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Verify access control options displayed
                verifyOption1 = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[accessControlOptions1].ToString(), inputjson[accessControl1].ToString());
                verifyOption2 = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[accessControlOptions2].ToString(), inputjson[accessControl2].ToString());
                verifyOption3 = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[accessControlOptions3].ToString(), inputjson[accessControl3].ToString());
                verifyOption4 = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[accessControlOptions4].ToString(), inputjson[accessControl4].ToString());
                verifyOption5 = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[accessControlOptions5].ToString(), inputjson[accessControl5].ToString());

                if(verifyOption1 && verifyOption2 && verifyOption3 && verifyOption4 && verifyOption5)
                    listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Hotspot Parameter - Access Controls"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify hotspot parameters.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Save hotspot parameters
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SaveHotspotParameters(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("AddHotspotParametersReport.json");
            try
            {
                //Tap 'GENERAL PARAMETERS'
                ReusableComponents.Click(driver, "XPath", jObject[generalParameters].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
                listOfReport[step++].SetActualResultFail("");

                //Select 'Default Date Range'
                ReusableComponents.SelectFromSelectDropdown(driver,"XPath",jObject[dateRange].ToString(),inputjson[dateRange].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Default Resolution Distance - in meters'
                ReusableComponents.Clear(driver, "XPath", jObject[resolutionDistance].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[resolutionDistance].ToString(), inputjson[resolutionDistance].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Default Corridor Length - in meters'
                ReusableComponents.Clear(driver, "XPath", jObject[corridorLength].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[corridorLength].ToString(), inputjson[corridorLength].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Default Incremental Length - in meters'
                ReusableComponents.Clear(driver, "XPath", jObject[incrementalLength].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[incrementalLength].ToString(), inputjson[incrementalLength].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Maximum Number of Crash Type Pattern for Consideration'
                ReusableComponents.Clear(driver, "XPath", jObject[maxCrashType].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[maxCrashType].ToString(), inputjson[maxCrashType].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Minimum Crash Type Pattern for Consideration - %'
                ReusableComponents.Clear(driver, "XPath", jObject[minCrashType].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[minCrashType].ToString(), inputjson[minCrashType].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Qualification Score'
                ReusableComponents.Clear(driver, "XPath", jObject[qualificationScore].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[qualificationScore].ToString(), inputjson[qualificationScore].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Minimum Number of Crashes Required for Crash Type Pattern'
                ReusableComponents.Clear(driver, "XPath", jObject[minCrashForCrashType].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[minCrashForCrashType].ToString(), inputjson[minCrashForCrashType].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter 'Minimum Number of Crashes Required for Cluster'
                ReusableComponents.Clear(driver, "XPath", jObject[minCrashForCluster].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[minCrashForCluster].ToString(), inputjson[minCrashForCluster].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Click 'Save' button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
                ReusableComponents.Click(driver, "XPath", jObject[saveBtn].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Verify toast message 'The record was saved successfully.'
                if(ReusableComponents.RetrieveAndCompareText(driver,"XPath",Constant.toastSelector,inputjson[toastMsg].ToString()))
                    listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Save Hotspot Parameter"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify hotspot parameters.Exception caught : " + e);
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Error - Save Hotspot Parameter"));
            }
            return listOfReport;
        }

        /// <summary>
        /// Verify hotspot parameters displayed in hotspot identification screen
        /// </summary>
        public List<TestReportSteps> VerifyHotspotParametersDisplayed(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyHotspotParametersReport.json");
            try
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[resolutionLength].ToString());

                string retrieveDateRange = ReusableComponents.RetrieveValueFromSelectBox(driver, "XPath", jObject[verifyDateRange].ToString());
                retrieveDateRange = Regex.Replace(retrieveDateRange, @"\s+", string.Empty);

                string retrieveResolutionLength = ReusableComponents.RetrieveAttributeValue(driver, "XPath", jObject[resolutionLength].ToString(), jObject[attributeName].ToString());
                string retrieveCorridorLength = ReusableComponents.RetrieveAttributeValue(driver, "XPath", jObject[verifyCorridorLength].ToString(), jObject[attributeName].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Hotspot Parameter - 1"));

                IWebElement element = driver.FindElement(By.XPath(jObject[verifyIncrementalLength].ToString()));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);

                string retrieveIncrementalLength = ReusableComponents.RetrieveAttributeValue(driver, "XPath", jObject[verifyIncrementalLength].ToString(), jObject[attributeName].ToString());

                //Verify 'Default Date Range' displayed
                string inputDate = Regex.Replace(inputjson[dateRange].ToString(), @"\s+", string.Empty);

                if (ReusableComponents.ContainsText(inputDate, retrieveDateRange))
                    listOfReport[step++].SetActualResultFail("");
                else
                    step++;

                //Verify 'Default Resolution Distance - in meters' displayed
                if (ReusableComponents.CompareText(inputjson[resolutionDistance].ToString(), retrieveResolutionLength))
                    listOfReport[step++].SetActualResultFail("");
                else
                    step++;

                //Verify 'Default Corridor Length - in meters' displayed
                if (ReusableComponents.CompareText(inputjson[corridorLength].ToString(), retrieveCorridorLength))
                    listOfReport[step++].SetActualResultFail("");
                else
                    step++;

                //Verify 'Default Incremental Length - in meters' displayed
                if (ReusableComponents.CompareText(inputjson[incrementalLength].ToString(), retrieveIncrementalLength))
                    listOfReport[step++].SetActualResultFail("");
                else
                    step++;

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Hotspot Parameter - 2"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify hotspot parameters.Exception caught : " + e);
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Error - Verify Hotspot Parameter"));
            }
            return listOfReport;
        }

        /// <summary>
        /// Returns hotspot parameter page screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetHotspotParameterScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }

}

