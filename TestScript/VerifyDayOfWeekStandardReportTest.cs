using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using iMAAPTestAPI.CrashRecords;
using iMAAPTestAPI;
using System.Threading;
using FrameworkSetup.TestDataClasses;


/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Standard Report functionality of Crash Distribution for Day of Week is working as expected
Test Objective              :To verify that iMAAP web portal -  Standard Report functionality of Crash Distribution for Day of Week is working as expected
Author 				        :Experion
Script Name 			    :TS008_Web_Crash Distribution for DoW Standard Report
Script Created on 		    :09/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    class VerifyDayOfWeekStandardReportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        ApiReusableComponents apiReusableComponents;
        StandardReportPage standardReportPage;
        SummaryPrintReportPage summaryPrintReportPage;
        LogoutPage logoutPage;
        QueryBuilderPage queryBuilderPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;
        List<CrashRootObject> crashData;
        List<QueryBuilder> queryBuilderData;
        string[,] array1;

        public VerifyDayOfWeekStandardReportTest()
        {
            VerifyDayOfWeekReport();
        }
        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");          
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            apiReusableComponents = new ApiReusableComponents();
            standardReportPage = new StandardReportPage(driver);
            summaryPrintReportPage=new SummaryPrintReportPage(driver);
            queryBuilderPage=new QueryBuilderPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Standard Report functionality of Crash distribution for Day of Week is working as expected";
            scriptName = "TS008_Web_Crash Distribution for DoW Standard Report";
            testDataFile = "VerifyDayOfWeekTestData.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
            crashData = RetrieveTestData.GetMultipleCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
            Constant.SetConfig("Configuration\\AppSettings.json");
        }
        public void VerifyDayOfWeekReport()
        {
            Init();
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
                {
                    try
                    {
                        //Login to application
                        report = loginPage.LoginToApplication(testData);
                        foreach (string screenshot in loginPage.GetLoginPageScreenshots())
                            screenshotList.Add(screenshot);

                        testCheck = CheckTestFailure.CheckReportForFailure(report, testCheck);

                        if (testCheck == 0)
                        {
                            //Verify home page
                            report.AddRange(homePage.VerifyHomePage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Home page"));

                            //Navigate to standard reports screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Standard Report"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Run Standard Report
                            report.AddRange(standardReportPage.ApplyFilterAndRunReport("Crash Distribution for Day of Week"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            for (int i = 0; i < crashData.Count; i++)
                            {

                                //Get table dimensions
                                int rowOfDayOfWeek = standardReportPage.GetFirstRow(testData);
                                int rowOfTotal = standardReportPage.GetTableRow(testData);
                                int colOfTotal = standardReportPage.GetTableCol(testData, rowOfDayOfWeek);

                                string timeStamp = ReusableComponents.ToUnixTimeStamp(DateTime.Today).ToString();
                                crashData[i].crash.fieldValues[0].value = timeStamp.ToString();

                                string[,] array = new string[(rowOfTotal - (rowOfDayOfWeek - 1)), colOfTotal];
                                array = standardReportPage.ReadTable(rowOfTotal, colOfTotal, rowOfDayOfWeek);

                                //Add crash using API
                                report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[i]));
                                foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                    screenshotList.Add(screenshot);

                                Thread.Sleep(Constant.waitTimeoutForExport);

                                //Click on run button
                                standardReportPage.ClickRunButton();

                                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                                //Get table dimensions
                                int rowOfDayOfWeeknew = standardReportPage.GetFirstRow(testData);
                                int rowOfTotalnew = standardReportPage.GetTableRow(testData);
                                int colOfTotalnew = standardReportPage.GetTableCol(testData, rowOfDayOfWeeknew);

                                //Retrieve table value after adding crash
                                array1 = new string[(rowOfTotalnew - (rowOfDayOfWeeknew - 1)), colOfTotalnew];
                                array1 = standardReportPage.ReadTable(rowOfTotalnew, colOfTotalnew, rowOfDayOfWeeknew);

                                //compare table values before and after creating the crash record
                                report.AddRange(standardReportPage.CompareTableDayOfWeek(testData, crashData[i], array, array1, rowOfTotal, colOfTotal, rowOfDayOfWeek));
                                //Capture screenshot
                                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Standard Report After Adding Crash"));

                            }
                            //Verify export feature in Standard Report
                            report.AddRange(summaryPrintReportPage.VerifyReportExportToDocument("Crash Distribution for Day of Week report"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);
 
                            //Navigate to query builder
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);
                          
                            //Select fields from crash form
                            queryBuilderData[0].fieldValue = ApiReusableComponents.crashReferenceNumber;
                            string crashFormField = testData["crashReferenceNumber"].ToString();
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashFormField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);
 
                            //Apply the query based on values selected
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Close Query Builder Page
                            report.AddRange(queryBuilderPage.CloseQueryBuilderPage());

                            //Run Standard Report
                            report.AddRange(standardReportPage.ApplyFilterAndRunReport("Crash Distribution for Day of Week"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Get table dimensions
                            int rowOfCrashDateMonthAfterQuery = standardReportPage.GetFirstRow(testData);
                            int rowOfTotalAfterQuery = standardReportPage.GetTableRow(testData);
                            int colOfTotalAfterQuery = standardReportPage.GetTableCol(testData, rowOfCrashDateMonthAfterQuery);

                            //Retrieve table value 
                            string[,] array2 = new string[(rowOfTotalAfterQuery - (rowOfCrashDateMonthAfterQuery - 1)), colOfTotalAfterQuery];
                            array2 = standardReportPage.ReadTable(rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfCrashDateMonthAfterQuery);

                            //Compare Table after applying the query
                            report.AddRange(standardReportPage.CompareTableAfterQuery(testData, array1, array2, rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfCrashDateMonthAfterQuery));

                           // //clear the query builder
                           report.AddRange(queryBuilderPage.ClearQueryBuilder(testData));
                           foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                           screenshotList.Add(screenshot);    
 
                            //Logout from application
                            report.AddRange(logoutPage.LogoutFromApplication(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));
                            Exit();
                        }
                    }
                    catch (Exception e)
                    {
                        Exit();
                        Console.WriteLine(e);
                    }
                }
            }
        }

        

        public void Exit()
        {
            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
            driver.Quit();
        }
    }
}
