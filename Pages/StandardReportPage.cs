using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI.CrashRecords;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;


namespace FrameworkSetup.Pages
{
    class StandardReportPage
    {

        string message = string.Empty;
        bool crashValue = true;
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        List<TestReportSteps> listOfReport1;
        readonly JObject jObject;
        public string selector;
        public List<string> screenshotList = new List<string>();


        private string verifyReportPageTitle = "verifyReportPageTitle";
        private string filterOption = "filterOption";
        private string checkValidRecords = "checkValidRecords";
        private string fromDate = "fromDate";
        private string toDate = "toDate";
        private string applyButton = "applyButton";
        private string runButton = "runButton";
        private string reportPages = "reportPages";
        private string reportTable = "reportTable";
        private string column1 = "column1";
        private string column2 = "column2";
        private string tableTitle = "tableTitle";
        private string crashDetailsTitle = "crashDetailsTitle";
        private string crashIdTitleInCrashDetails = "crashIdTitleInCrashDetails";
        private string crashRefNoTitleInCrashDetails = "crashRefNoTitleInCrashDetails";
        private string vehicleDetailsTitle = "vehicleDetailsTitle";
        private string crashIdTitleInVehicleDetails = "crashIdTitleInVehicleDetails";
        private string casualtyDetailsTitle = "casualtyDetailsTitle";
        private string crashIdTitleInCasualtyDetails = "crashIdTitleInCasualtyDetails";
        private string casualtyDetails = "casualtyDetails";
        private string column3 = "column3";
        private string general = "general";
        private string monthOfYear = "monthOfYear";
        private string runBtn = "runBtn";
        private string month = "month";
        private string rowSelector = "rowSelector";
        private string colSelector = "colSelector";
        private string severity = "severity";
        private string total = "total";
        private string tableRowCount = "tableRowCount";
        private string columnCount = "columnCount";
        private string filterOptionAfterApplyingQuery = "filterOptionAfterApplyingQuery";
        private string noOfColInTableAfterQuery = "noOfColInTableAfterQuery";
        private string table = "table";
        private static string cellTimeText;
        private string rowStart = "rowStart";

        public StandardReportPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("StandardReportPageSelector.json");
        }
        /// <summary>
        /// Verify web application standard report page displayed
        /// </summary>
        public List<TestReportSteps> VerifyStandardReportPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyStandardReport.json");
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
        /// Apply filter and run standard print report
        /// </summary>
        public List<TestReportSteps> ApplyFilterAndRun(string reportName)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyFilterAndRunStandardReport.json");
            listOfReport[step].SetTestObjective("Verify that the user is able to run " + reportName + " after the filter applied.");
            try
            {
                //Click General option
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[general].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[general].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select corresponding report.
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                string xPath = "//li[contains(text(),'" + reportName + "')]";

                ReusableComponents.Click(driver, "XPath", xPath);
                listOfReport[step++].SetActualResultFail("");

                //Click filter option
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.Click(driver, "XPath", jObject[filterOption].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Check 'Valid Records'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.Click(driver, "XPath", jObject[checkValidRecords].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.AddDays(-10).ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Click 'Apply and Run' button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify standard report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Apply filter and run standard print report
        /// </summary>
        public List<TestReportSteps> ApplyFilterInTimeOfDayReport(string reportName)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyFilterAndRunStandardReport.json");
            listOfReport[step].SetTestObjective("Verify that the user is able to run " + reportName + " after the filter applied.");
            try
            {
                //Click General option
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[general].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[general].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select corresponding report.
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                string xPath = "//li[contains(text(),'" + reportName + "')]";

                ReusableComponents.Click(driver, "XPath", xPath);
                listOfReport[step++].SetActualResultFail("");

                //Click filter option
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.Click(driver, "XPath", jObject[filterOption].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Check 'Valid Records'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.Click(driver, "XPath", jObject[checkValidRecords].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Click 'Apply and Run' button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                //ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify standard report page.Exception caught : " + e);
            }
            return listOfReport;
        }




        /// <summary>
        /// Run standard print report without filter.
        /// </summary>
        public List<TestReportSteps> RunReportWithoutFilter(string menuItem)
        {
            step = 0;
            bool isFound = true;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RunStandardReportWithoutFilter.json");
            try
            {
                //Click General option
                ReusableComponents.Click(driver, "XPath", jObject[general].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Click selected general option
                string mainListSelector = "//div[@class='panel-collapse collapse in show']//ul//li";
                int count = ReusableComponents.ElementsCount(driver, "XPath", mainListSelector);
                for (int i = 1; i <= count; i++)
                {
                    string innerList = mainListSelector + "[" + i + "]";

                    //Retrieve and Compare text

                    isFound = ReusableComponents.RetrieveAndCompareText(driver, "XPath", innerList, menuItem);

                    if (isFound)
                    {
                        ReusableComponents.Click(driver, "XPath", innerList);
                        listOfReport[step].SetStepDescription(listOfReport[step].GetStepDescription() + menuItem);
                        listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + menuItem);
                        listOfReport[step].SetActualResultPass(listOfReport[step].GetActualResultPass() + menuItem);
                        listOfReport[step++].SetActualResultFail("");
                        break;
                    }

                }

                //Click Run button
                ReusableComponents.Click(driver, "XPath", jObject[runButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                //wait for page to load
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard report page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Apply filter and run standard print report
        /// </summary>
        public List<TestReportSteps> ApplyQueryAndRunReport()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyQueryAndRunReport.json");
            try
            {

                //Click filter option
                ReusableComponents.Click(driver, "XPath", jObject[filterOptionAfterApplyingQuery].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.AddDays(-20).ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

                //Click 'Apply and Run' button
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Run crash distribution for month of year report 
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> RunReportAfterQueryApplied(string reportName)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RunReportAfterQueryAppliedReport.json");
            listOfReport[step].SetTestObjective("Verify that the user is able to run " + reportName + " after the query applied.");
            try
            {
                //Click General option
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[general].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[general].ToString());
                listOfReport[step].SetActualResultFail("");

                //Select corresponding report.
                string xPath = "//li[contains(text(),'" + reportName + "')]";

                ReusableComponents.Click(driver, "XPath", xPath);
                listOfReport[++step].SetStepDescription(listOfReport[step].GetStepDescription() + reportName);
                listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + reportName);
                listOfReport[step].SetActualResultPass(listOfReport[step].GetActualResultPass() + reportName);

                listOfReport[step].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

                //Click filter option
                ReusableComponents.Click(driver, "XPath", jObject[filterOptionAfterApplyingQuery].ToString());
                listOfReport[++step].SetActualResultFail("");

                //Click 'Apply and Run' button
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                listOfReport[++step].SetActualResultFail("");

                //wait for page to load
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Retrieves standard report page screenshots
        /// </summary>
        /// <returns></returns>
        public List<string> GetStandardReportPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
        /// <summary>
        /// Verify generated standard report details 
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyGeneratedSummaryPrintReport(JToken inputjson, string CrashRefNo)
        {

            step = 0;
            screenshotList.Clear();
            int reportTableCount, reportPageCount, tableCounter, innerCounter = 0;
            bool isCrashRefNoFound = false, IsCrashTableVerified = false, IsVehicleTableVerified = false, isCasualtyTableVerified = false;
            bool isCrashTitleVerified = false, isVehicleTitleVerified = false, isCasualtyTitleVerified = false;
            string crashId = string.Empty, title1Selector, title2Selector;
            listOfReport = ConfigFile.GetReportFile("VerifyGeneratedSummaryPrintReport.json");
            try
            {
                Constant.waitTimeout = 5;
                reportPageCount = ReusableComponents.ElementsCount(driver, "XPath", jObject[reportPages].ToString());
                for (int pageCounter = 1; pageCounter <= reportPageCount; pageCounter++)
                {
                    string pageSelector = jObject[reportPages].ToString() + "[" + pageCounter + "]" + jObject[reportTable].ToString();
                    reportTableCount = ReusableComponents.ElementsCount(driver, "XPath", pageSelector);
                    tableCounter = 1;
                    innerCounter = 1;
                    if (!IsCrashTableVerified)
                    {
                        //Verify table 'Crash Details'
                        for (tableCounter = 1; tableCounter <= reportTableCount; tableCounter++)
                        {
                            selector = pageSelector + "[" + tableCounter + "]" + jObject[tableTitle].ToString();

                            if (!isCrashTitleVerified)
                            {
                                try
                                {
                                    if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selector, inputjson[crashDetailsTitle].ToString()))
                                    {
                                        Console.WriteLine("User was able to verify title 'Crash Details'");
                                        listOfReport[step++].SetActualResultFail("");
                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Crash Details"));

                                        tableCounter += 2;
                                        title1Selector = pageSelector + "[" + tableCounter + "]" + jObject[column1].ToString();
                                        title2Selector = pageSelector + "[" + tableCounter + "]" + jObject[column3].ToString();

                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title1Selector, inputjson[crashIdTitleInCrashDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash Id' in Crash Details");
                                            listOfReport[step++].SetActualResultFail("");

                                        }
                                        else
                                        {
                                            step++;
                                        }
                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title2Selector, inputjson[crashRefNoTitleInCrashDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash ref. No' in Crash Details");
                                            listOfReport[step++].SetActualResultFail("");
                                            isCrashTitleVerified = true;
                                        }
                                        else
                                        {

                                            step++;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("title not found in loop" + e);
                                }
                            }

                            if (isCrashTitleVerified)
                            {
                                //Verify crash ref no
                                for (innerCounter = tableCounter; innerCounter <= reportTableCount; innerCounter++)
                                {
                                    string selectorCrashRefNo = pageSelector + "[" + innerCounter + "]" + jObject[column3].ToString();
                                    try
                                    {
                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selectorCrashRefNo, CrashRefNo))
                                        {
                                            crashId = ReusableComponents.RetrieveText(driver, "XPath", pageSelector + "[" + innerCounter + "]" + jObject[column1].ToString());
                                            isCrashRefNoFound = true;

                                            ReusableComponents.ScrollToElement(driver, "XPath", pageSelector + "[" + innerCounter + "]" + jObject[column1].ToString());
                                            break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Crash ref no not verified" + e);
                                    }
                                }


                                if (isCrashRefNoFound)
                                {
                                    Console.WriteLine("User was able to verify 'Crash ref. No' in Crash Details");
                                    listOfReport[step++].SetActualResultFail("");

                                    IsCrashTableVerified = true;
                                    break;
                                }
                                else
                                {
                                    IsCrashTableVerified = false;
                                    break;
                                }
                            }

                        }
                    }

                    if (IsCrashTableVerified && (!IsVehicleTableVerified))
                    {
                        //Verify table 'Vehicle Details'
                        for (tableCounter = innerCounter; tableCounter <= reportTableCount; tableCounter++)
                        {
                            selector = pageSelector + "[" + tableCounter + "]" + jObject[tableTitle].ToString();
                            if (!isVehicleTitleVerified)
                            {
                                try
                                {
                                    if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selector, inputjson[vehicleDetailsTitle].ToString()))
                                    {
                                        Console.WriteLine("User was able to verify title 'Vehicle Details'");
                                        listOfReport[step++].SetActualResultFail("");

                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Details"));

                                        tableCounter += 2;
                                        title1Selector = pageSelector + "[" + tableCounter + "]" + jObject[column1].ToString();
                                        title2Selector = pageSelector + "[" + tableCounter + "]" + jObject[column2].ToString();

                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title1Selector, inputjson[crashIdTitleInVehicleDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash Id' in Vehicle Details");
                                            listOfReport[step++].SetActualResultFail("");
                                            isVehicleTitleVerified = true;
                                            IsVehicleTableVerified = true;

                                        }
                                        else
                                        {
                                            step++;
                                        }

                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Vehicle details not found" + e);
                                }
                            }



                        }
                    }

                    if (IsVehicleTableVerified && (!isCasualtyTableVerified))
                    {
                        ReusableComponents.ScrollToElement(driver, "XPath", jObject[casualtyDetails].ToString());
                        //Verify table 'Casualty Details'
                        for (tableCounter = innerCounter; tableCounter <= reportTableCount; tableCounter++)
                        {
                            selector = pageSelector + "[" + tableCounter + "]" + jObject[tableTitle].ToString();

                            if (!isCasualtyTitleVerified)
                            {
                                try
                                {
                                    if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selector, inputjson[casualtyDetailsTitle].ToString()))
                                    {
                                        Console.WriteLine("User was able to verify title 'Casualty Details'");
                                        listOfReport[step++].SetActualResultFail("");

                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Details"));

                                        tableCounter += 2;
                                        title1Selector = pageSelector + "[" + tableCounter + "]" + jObject[column1].ToString();
                                        title2Selector = pageSelector + "[" + tableCounter + "]" + jObject[column2].ToString();

                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title1Selector, inputjson[crashIdTitleInCasualtyDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash Id' in Casualty Details");
                                            listOfReport[step++].SetActualResultFail("");
                                            isCasualtyTitleVerified = true;
                                            isCasualtyTableVerified = true;
                                        }
                                        else
                                        {
                                            step++;
                                        }

                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Casualty title not verified in loop " + e);
                                }
                            }



                        }
                    }

                    if (IsCrashTableVerified && IsVehicleTableVerified && isCasualtyTableVerified)
                    {
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify summary print report generated.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Run crash distribution for month of year report 
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> RunMonthOfYearReport()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RunMonthOfYearReportWithoutFilterReport.json");
            try
            {
                //Click General option
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[general].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[general].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Crash Distribution for month of year option
                ReusableComponents.Click(driver, "XPath", jObject[monthOfYear].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;
        }

      

        /// <summary>
        /// read values in table
        /// </summary>
        /// <returns></returns>
        public string[,] ReadTable(int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            step = 0;
            int j = 0, i = 0;
            int row = 0, col = 0;
            screenshotList.Clear();
            string cellSelector = "";
            string[,] array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
            try
            {
                for (row = rowOfCrashDateMonth; row <= rowOfTotal; row++)
                {
                    for (col = 1; col <= colOfTotal; col++)
                    {
                        cellSelector = jObject[rowSelector].ToString() + "[" + row + "]" + "//td" + "[" + col + "]";
                        ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                        array[i, j] = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                        j++;
                    }
                    i++;
                    j = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return array;
        }

        /// <summary>
        /// Compare the values in the report table before after creating the crash record
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> CompareTableForMonthOfYearReport(JToken inputJson, CrashRootObject crashData, string[,] array, string[,] array1, int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            crashValue = true;
            message = string.Empty;
            step = 0;
            int j, i, rowOfMonthInArray = 0, rowOfTotalInArray = 0, colOfTotalInArray = 0, colOfSeverityInArray = 0, flag = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyTableInMonthOfYearReport.json");
            listOfReport1 = ConfigFile.GetReportFile("VerifyTableInMonthOfYearErrorReport.json");
            string date = crashData.crash.fieldValues[0].value;

            // Format our new DateTime object to start at the UNIX Epoch
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(Convert.ToDouble(date));
            string month = dateTime.ToString("MMMM");
            string severityValue = crashData.crash.fieldValues[36].value;

            var severity = ReusableComponents.GetCrashSeverity("CrashSeverity.json");
            var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<Severity>>(severity);
            for(int p=0;p< casualtyMasterDataSelectors.Count;p++)
            {
                if(casualtyMasterDataSelectors[p].severityId== severityValue)
                {
                    severityValue = casualtyMasterDataSelectors[p].severityType;
                    flag = 0;
                    break;
                }
                else
                {
                    flag = 1;
                }
            }
            if(flag == 1)
            {
                severityValue = "Blank";
            }
            
            if ((array.GetLength(0) == array1.GetLength(0)) && (array.GetLength(1) == array1.GetLength(1)))
            {
                try
                {
                    for (int x = 0; x <= (rowOfTotal - rowOfCrashDateMonth); x++)
                    {
                        string monthNameInTable = month;
                        string monthNameInArray = array[x, 0];
                        if (monthNameInArray.Contains(monthNameInTable))
                        {
                            rowOfMonthInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= (rowOfTotal - (rowOfCrashDateMonth - 1)); x++)
                    {
                        string totalInTableRow = inputJson[total].ToString();
                        string totalInArrayRow = array[x, 0];
                        if (totalInArrayRow.Contains(totalInTableRow))
                        {
                            rowOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        string totalInTableCol = inputJson[total].ToString();
                        string totalInArrayCol = array[0, x];
                        if (totalInArrayCol.Contains(totalInTableCol))
                        {
                            colOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        
                        string severityInArray = array[0, x];
                        if (severityInArray.Contains(severityValue))
                        {
                            colOfSeverityInArray = x;
                            break;
                        }
                    }

                    for (i = 0; i <= (rowOfTotal - rowOfCrashDateMonth); i++)
                    {
                        for (j = 0; j < colOfTotal; j++)
                        {
                            if (array[i, j] == array1[i, j])
                            {
                                Console.WriteLine(array[i, j]);
                                Console.WriteLine(array1[i, j]);
                            }
                            else
                            {
                                if (i == rowOfMonthInArray && j == colOfSeverityInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfMonthInArray && j == colOfTotalInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfSeverityInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfTotalInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {    
                                        string result = GetTableDetailsOnError(i, j, array1);
                                        message = message + " Values have changed at" + result + "<br />";
                                        crashValue = false;
                                    }
                                }
                            }
                        }
                    }
                    if(crashValue == true)
                    {
                        listOfReport[step++].SetActualResultFail("");
                    }
                    else
                    {
                        listOfReport[step].SetActualResultFail("");
                        listOfReport[step++].SetActualResultPass(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No able to verify Crash Distribution for month of year report page.Exception caught : " + e);
                }
                return listOfReport;
            }
            else
            {
                listOfReport1[step++].SetActualResultFail("");
                return listOfReport1;
            }
        }

        /// <summary>
        /// Compare the values in the report table before after creating the crash record
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> CompareTableAfterQuery(JToken inputJson, string[,] array1, string[,] array2, int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            crashValue = true;
            message = string.Empty;
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyQueryBuilderFunctionalityReport.json");
            listOfReport1 = ConfigFile.GetReportFile("VerifyQueryBuilderErrorReport.json");

            if (!((array1.GetLength(0) == array2.GetLength(0)) && (array1.GetLength(1) == array2.GetLength(1))))
            {
                try
                {
                    int colsInTableAfterQuery = Convert.ToInt32(inputJson[noOfColInTableAfterQuery].ToString());

                    if(array2.GetLength(1) == colsInTableAfterQuery)
                    {
                            if(array2[(rowOfTotal - rowOfCrashDateMonth), (colOfTotal - 1)] == "1")
                            {
                                listOfReport[step++].SetActualResultFail("");
                            }
                        else
                            {
                                listOfReport[step++].SetActualResultPass("");
                            }
                    }
                    else
                    {
                        listOfReport[step++].SetActualResultPass("");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No able to verify Crash Distribution for month of year report page.Exception caught : " + e);
                }
                return listOfReport;
            }
            else
            {
                listOfReport1[step++].SetActualResultFail("");
                return listOfReport1;
            }
        }



        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public string GetTableDetailsOnError(int i, int j, string[,] array1)
        {
            screenshotList.Clear();
            string result = "";
            try
            {
                string month = array1[i, 0];
                string cellText = array1[0, j];
                result = " Row:" + month + " and col: " + cellText;
                
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return result;
        }

        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public int GetTableRow(JToken inputJson)
        {
            step = 0;
            int rowOfTotal = 0;
            screenshotList.Clear();
            try
            {

                int count = ReusableComponents.GetRowsInTable(driver, jObject[tableRowCount].ToString());
                for (int i = 1; i <= count; i++)
                {

                    string c = jObject[rowSelector].ToString() + "[" + i + "]" + jObject[colSelector].ToString();
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", c);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", c);
                    string b = inputJson[total].ToString();
                    if (a.Contains(b))
                    {
                        rowOfTotal = i;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return rowOfTotal;
        }

        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public int GetTableCol(JToken inputJson, int rowOfCrashDateMonth)
        {
            step = 0;
            int colOfTotal = 0;
            screenshotList.Clear();
            try
            {
                string colCount = inputJson[columnCount].ToString();
                //Convert.ToInt32(colCount)
                for (int j = 1; j <= Convert.ToInt32(colCount); j++)
                {

                    string d = jObject[rowSelector].ToString() + "[" + rowOfCrashDateMonth + "]" + "//td" + "[" + j + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", d);
                    string e = ReusableComponents.RetrieveText(driver, "XPath", d);
                    string f = inputJson[total].ToString();
                    if (e.Contains(f))
                    {
                        colOfTotal = j;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return colOfTotal;
        }


        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public int GetFirstRow(JToken inputJson)
        {
            step = 0;
            int firstRowNumber = 0;
            screenshotList.Clear();
            try
            {
                int count = ReusableComponents.GetRowsInTable(driver, jObject[tableRowCount].ToString());
                for (int i = 1; i <= count; i++)
                {

                    string cellSelector = jObject[rowSelector].ToString() + "[" + i + "]" + jObject[colSelector].ToString();
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string textInCell = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string b = inputJson[rowStart].ToString();
                    if (textInCell.Contains(b))
                    {
                        firstRowNumber = i;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return firstRowNumber;
        }

        /// <summary>
        /// Click on run button 
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> ClickRunButton()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyRunStandardReport.json");
            try
            {
                //Click Run button
                ReusableComponents.Click(driver, "XPath", jObject[runBtn].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Generate report"));
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// To enter crash time after identifying the first crash time span 
        /// </summary>
        /// <returns></returns>
        public int GetTTimeToEnter()
        {
            step = 0;
            int td1 = 1, i = 5, timeToEnter = 0;

            int min = 30;
            screenshotList.Clear();
            try
            {
                int count = ReusableComponents.GetRowsInTable(driver, jObject[tableRowCount].ToString());


                string c = jObject[rowSelector].ToString() + "[" + i + "]" + "//td" + "[" + td1 + "]".ToString();
                cellTimeText = ReusableComponents.RetrieveText(driver, "XPath", c);
                int numb = int.Parse(cellTimeText.Split('-')[0]);
                int time = (numb * 60) + min;
                timeToEnter = time;
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return timeToEnter;
        }

        /// <summary>
        /// Compare the values in the report table before after creating the crash record
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> CompareTableOfCasualtyByYear(JToken inputJson, CrashRootObject crashData, string[,] array, string[,] array1, int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            crashValue = true;
            message = string.Empty;
            step = 0;
            int j = 0, i = 0,rowOfYearInArray = 0,rowOfTotalInArray = 0, colOfTotalInArray = 0, colOfSeverityInArray = 0;
            int flag = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyCasualtyByYearReport.json");
            listOfReport1 = ConfigFile.GetReportFile("VerifyTableCasualtyByYearErrorReport.json");

            //Current year.
            string year = DateTime.Parse(DateTime.Now.ToString()).Year.ToString();
            string severityValue = crashData.casualties[0].fieldValues[4].value;


            var severity = ReusableComponents.GetCrashSeverity("CasualtySeverity.json");
            var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<Severity>>(severity);
            for (int p = 0; p < casualtyMasterDataSelectors.Count; p++)
            {
                if (casualtyMasterDataSelectors[p].severityId == severityValue)
                {
                    severityValue = casualtyMasterDataSelectors[p].severityType;
                    flag = 0;
                    break;
                }
                else
                {
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                severityValue = "Blank";
            }


            if ((array.GetLength(0) == array1.GetLength(0)) && (array.GetLength(1) == array1.GetLength(1)))
            {
                try
                {
                    for (int x = 0; x <= (rowOfTotal - rowOfCrashDateMonth); x++)
                    {
                        string yearNameInTable = year;
                        string yearNameInArray = array[x, 0];
                        if (yearNameInArray.Contains(yearNameInTable))
                        {
                            rowOfYearInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= (rowOfTotal - (rowOfCrashDateMonth - 1)); x++)
                    {
                        string totalInTableRow = inputJson[total].ToString();
                        string totalInArrayRow = array[x, 0];
                        if (totalInArrayRow.Contains(totalInTableRow))
                        {
                            rowOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        string totalInTableCol = inputJson[total].ToString();
                        string totalInArrayCol = array[0, x];
                        if (totalInArrayCol.Contains(totalInTableCol))
                        {
                            colOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {

                        string severityInArray = array[0, x];
                        if (severityInArray.Contains(severityValue))
                        {
                            colOfSeverityInArray = x;
                            break;
                        }
                    }

                    for (i = 0; i <= (rowOfTotal - rowOfCrashDateMonth); i++)
                    {
                        for (j = 0; j < colOfTotal; j++)
                        {
                            if (array[i, j] == array1[i, j])
                            {
                                Console.WriteLine(array[i, j]);
                                Console.WriteLine(array1[i, j]);
                            }
                            else
                            {
                                if (i == rowOfYearInArray && j == colOfSeverityInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfYearInArray && j == colOfTotalInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfSeverityInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfTotalInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        string result = GetTableDetailsOnError(i, j, array1);
                                        message = message + " Values have changed at" + result + "<br />";
                                        crashValue = false;
                                    }

                                }

                            }
                        }
                    }
                    if (crashValue == true)
                    {
                        listOfReport[step++].SetActualResultFail("");
                    }
                    else
                    {
                        listOfReport[step++].SetActualResultFail("");
                        listOfReport[step++].SetActualResultPass(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
                }
                return listOfReport;
            }
            else
            {
                listOfReport1[step++].SetActualResultFail("");
                return listOfReport1;
            }
        }

        /// <summary>
        /// Compare the values in the report table before after creating the crash record
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> CompareTableDayOfWeek(JToken inputJson, CrashRootObject crashData, string[,] array, string[,] array1, int rowOfTotal, int colOfTotal, int rowOfDayOfWeek)
        {
            crashValue = true;
            message = string.Empty;
            step = 0;
            int j = 0, i = 0, rowOfDayInArray = 0, rowOfTotalInArray = 0, colOfTotalInArray = 0, colOfSeverityInArray = 0;
            int flag = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyTableInDayOfWeekReport.json");
            listOfReport1 = ConfigFile.GetReportFile("VerifyTableInDayOfWeekErrorReport.json");

            string date = crashData.crash.fieldValues[0].value;
            // Format our new DateTime object to start at the UNIX Epoch
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(Convert.ToDouble(date));
            string day = dateTime.ToString("dddd");
            string severityValue = crashData.crash.fieldValues[36].value;
            var severity = ReusableComponents.GetCrashSeverity("CrashSeverity.json");
            var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<Severity>>(severity);
            for (int p = 0; p < casualtyMasterDataSelectors.Count; p++)
            {
                if (casualtyMasterDataSelectors[p].severityId == severityValue)
                {
                    severityValue = casualtyMasterDataSelectors[p].severityType;
                    flag = 0;
                    break;
                }
                else
                {
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                severityValue = "Blank";
            }

            if ((array.GetLength(0) == array1.GetLength(0)) && (array.GetLength(1) == array1.GetLength(1)))
            {
                try
                {
                    for (int x = 0; x <= (rowOfTotal - rowOfDayOfWeek); x++)
                    {
                        string dayNameInTable = day;
                        string dayNameInArray = array[x, 0];
                        if (dayNameInArray.Contains(dayNameInTable))
                        {
                            rowOfDayInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= (rowOfTotal - (rowOfDayOfWeek - 1)); x++)
                    {
                        string totalInTableRow = inputJson[total].ToString();
                        string totalInArrayRow = array[x, 0];
                        if (totalInArrayRow.Contains(totalInTableRow))
                        {
                            rowOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        string totalInTableCol = inputJson[total].ToString();
                        string totalInArrayCol = array[0, x];
                        if (totalInArrayCol.Contains(totalInTableCol))
                        {
                            colOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {

                        string severityInArray = array[0, x];
                        if (severityInArray.Contains(severityValue))
                        {
                            colOfSeverityInArray = x;
                            break;
                        }
                    }

                    for (i = 0; i <= (rowOfTotal - rowOfDayOfWeek); i++)
                    {
                        for (j = 0; j < colOfTotal; j++)
                        {
                            if (array[i, j] == array1[i, j])
                            {
                                Console.WriteLine(array[i, j]);
                                Console.WriteLine(array1[i, j]);
                            }
                            else
                            {
                                if (i == rowOfDayInArray && j == colOfSeverityInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfDayInArray && j == colOfTotalInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfSeverityInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfTotalInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    if (num2 > num1)
                                    {
                                        string result = GetTableDetailsOnError(i, j, array1);
                                        message = message + " Values have changed at" + result + "<br />";
                                        crashValue = false;
                                    }

                                }

                            }
                        }
                    }
                    if (crashValue == true)
                    {
                        listOfReport[step++].SetActualResultFail("");
                    }
                    else
                    {
                        listOfReport[step].SetActualResultFail("");
                        listOfReport[step++].SetActualResultPass(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
                }
                return listOfReport;
            }
            else
            {
                listOfReport1[step++].SetActualResultFail("");
                return listOfReport1;
            }
        }

        /// <summary>
        /// Compare the values in the report table before after creating the crash record
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> CompareTable(JToken inputJson, string[,] array, string[,] array1, int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            step = 0;
            int j = 0, i = 0, rowOfMonthInArray = 0, rowOfTotalInArray = 0, colOfTotalInArray = 0, colOfSeverityInArray = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyTableInMonthOfYearReport.json");
            listOfReport1 = ConfigFile.GetReportFile("VerifyTableInMonthOfYearErrorReport.json");

            if ((array.GetLength(0) == array1.GetLength(0)) && (array.GetLength(1) == array1.GetLength(1)))
            {
                try
                {
                    for (int x = 0; x <= (rowOfTotal - rowOfCrashDateMonth); x++)
                    {
                        string monthNameInTable = inputJson[month].ToString();
                        string monthNameInArray = array[x, 0];
                        if (monthNameInArray.Contains(monthNameInTable))
                        {
                            rowOfMonthInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= (rowOfTotal - (rowOfCrashDateMonth - 1)); x++)
                    {
                        string totalInTableRow = inputJson[total].ToString();
                        string totalInArrayRow = array[x, 0];
                        if (totalInArrayRow.Contains(totalInTableRow))
                        {
                            rowOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        string totalInTableCol = inputJson[total].ToString();
                        string totalInArrayCol = array[0, x];
                        if (totalInArrayCol.Contains(totalInTableCol))
                        {
                            colOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        string severityInTable = inputJson[severity].ToString();
                        string severityInArray = array[0, x];
                        if (severityInArray.Contains(severityInTable))
                        {
                            colOfSeverityInArray = x;
                            break;
                        }
                    }

                    for (i = 0; i <= (rowOfTotal - rowOfCrashDateMonth); i++)
                    {
                        for (j = 0; j < colOfTotal; j++)
                        {
                            if (array[i, j] == array1[i, j])
                            {
                                Console.WriteLine(array[i, j]);
                                Console.WriteLine(array1[i, j]);
                            }
                            else
                            {
                                if (i == rowOfMonthInArray && j == colOfSeverityInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    num1 += 1;
                                    if (num2 == num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                }
                                else if (i == rowOfMonthInArray && j == colOfTotalInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    num1 += 1;
                                    if (num2 == num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfSeverityInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    num1 += 1;
                                    if (num2 == num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfTotalInArray)
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    num1 += 1;
                                    if (num2 == num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                }
                                else
                                {
                                    string a = array[i, j];
                                    string b = array1[i, j];
                                    int num1 = Convert.ToInt32(a);
                                    int num2 = Convert.ToInt32(b);
                                    num1 += 1;
                                    if (num2 == num1)
                                    {
                                        string result = GetTableDetailsOnError(i, j, array1);
                                        listOfReport[step++].SetActualResultFail("Value was changed at position" + result);
                                    }

                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
                }
                return listOfReport;
            }
            else
            {
                listOfReport1[step++].SetActualResultFail("");
                return listOfReport1;
            }
        }

        /// <summary>
        /// Compare the values in the report table before after creating the crash record
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> CompareTableForTimeOfDay(JToken inputJson, CrashRootObject crashData, string[,] array, string[,] array1, int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            step = 0;
            int j = 0, i = 0, rowOfMonthInArray = 0, rowOfTotalInArray = 0, colOfTotalInArray = 0, colOfSeverityInArray = 0, flag = 0; ;
            crashValue = true;
            message = string.Empty;

            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyTimeOfDayReport.json");
            listOfReport1 = ConfigFile.GetReportFile("VerifyTimeOfDayErrorReport.json");
            string severityValue = crashData.crash.fieldValues[36].value;

            var severity = ReusableComponents.GetCrashSeverity("CrashSeverity.json");
            var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<Severity>>(severity);

            for (int p = 0; p < casualtyMasterDataSelectors.Count; p++)
            {
                if (casualtyMasterDataSelectors[p].severityId == severityValue)
                {
                    severityValue = casualtyMasterDataSelectors[p].severityType;
                    flag = 0;
                    break;
                }
                else
                {
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                severityValue = "Blank";
            }



            if ((array.GetLength(0) == array1.GetLength(0)) && (array.GetLength(1) == array1.GetLength(1)))
            {
                try
                {
                    for (int x = 0; x <= (rowOfTotal - rowOfCrashDateMonth); x++)
                    {
                        string timeNameInTable = cellTimeText;
                        string monthNameInArray = array[x, 0];
                        if (monthNameInArray.Contains(timeNameInTable))
                        {
                            rowOfMonthInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= (rowOfTotal - (rowOfCrashDateMonth - 1)); x++)
                    {
                        string totalInTableRow = inputJson[total].ToString();
                        string totalInArrayRow = array[x, 0];
                        if (totalInArrayRow.Contains(totalInTableRow))
                        {
                            rowOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        string totalInTableCol = inputJson[total].ToString();
                        string totalInArrayCol = array[0, x];
                        if (totalInArrayCol.Contains(totalInTableCol))
                        {
                            colOfTotalInArray = x;
                            break;
                        }
                    }

                    for (int x = 0; x <= colOfTotal; x++)
                    {
                        //string severityInTable = inputJson[severity].ToString();
                        string severityInArray = array[0, x];
                        if (severityInArray.Contains(severityValue))
                        {
                            colOfSeverityInArray = x;
                            break;
                        }
                    }

                    for (i = 0; i <= (rowOfTotal - rowOfCrashDateMonth); i++)
                    {
                        for (j = 0; j < colOfTotal; j++)
                        {
                            if (array[i, j] == array1[i, j])
                            {
                                Console.WriteLine(array[i, j]);
                                Console.WriteLine(array1[i, j]);
                            }
                            else
                            {
                                if (i == rowOfMonthInArray && j == colOfSeverityInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfMonthInArray && j == colOfTotalInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfSeverityInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else if (i == rowOfTotalInArray && j == colOfTotalInArray)
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        listOfReport[step++].SetActualResultFail("");
                                    }
                                    else
                                    {
                                        listOfReport[step++].SetActualResultPass("");
                                    }
                                }
                                else
                                {
                                    int num1 = Convert.ToInt32(array[i, j]);
                                    int num2 = Convert.ToInt32(array1[i, j]);
                                    if (num2 > num1)
                                    {
                                        string result = GetTableDetailsOnError(i, j, array1);
                                        message = message + " Values have changed at" + result + "<br />";
                                        crashValue = false;
                                    }
                                }
                            }
                        }
                    }
                    if (crashValue == true)
                    {
                        listOfReport[step++].SetActualResultFail("");
                    }
                    else
                    {
                        listOfReport[step].SetActualResultFail("");
                        listOfReport[step++].SetActualResultPass(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("No able to verify Crash Distribution for month of year report page.Exception caught : " + e);
                }
                return listOfReport;
            }
            else
            {
                listOfReport1[step++].SetActualResultFail("");
                return listOfReport1;
            }
        }

        /// <summary>
        /// Apply filter and run standard report
        /// </summary>
        public List<TestReportSteps> ApplyFilterAndRunReport(string menuItem)
        {
            step = 0;
            bool isFound = true;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyFilterAndRunStandardReport.json");
            try
            {
                //Click General option
                ReusableComponents.Click(driver, "XPath", jObject[general].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Click selected general option
                string mainListSelector = "//div[@class='panel-collapse collapse in show']//ul//li";
                int count = ReusableComponents.ElementsCount(driver, "XPath", mainListSelector);
                for (int i = 1; i <= count; i++)
                {
                    string innerList = mainListSelector + "[" + i + "]";

                    //Retrieve and Compare text

                    isFound = ReusableComponents.RetrieveAndCompareText(driver, "XPath", innerList, menuItem);

                    if (isFound)
                    {
                        ReusableComponents.Click(driver, "XPath", innerList);
                        listOfReport[step].SetStepDescription(listOfReport[step].GetStepDescription() + menuItem);
                        listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + menuItem);
                        listOfReport[step].SetActualResultPass(listOfReport[step].GetActualResultPass() + menuItem);
                        listOfReport[step++].SetActualResultFail("");
                        break;
                    }
                }

                //Click filter option
                ReusableComponents.Click(driver, "XPath", jObject[filterOption].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.AddDays(-20).ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

                //Click 'Apply and Run' button
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                //wait for page to load
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard report page.Exception caught : " + e);
            }
            return listOfReport;
        }

    }
}


