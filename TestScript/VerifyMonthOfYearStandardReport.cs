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
using FrameworkSetup.TestDataClasses;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Standard Report functionality of Crash Distribution for month of year is working as expected
Test Objective              :To verify that iMAAP web portal -  Standard Report functionality of Crash Distribution for month of year is working as expected
Author 				        :Experion
Script Name 			    :TS009_Web_Crash Distribution for MoD Standard Report
Script Created on 		    :09/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    class VerifyMonthOfYearStandardReport
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        ApiReusableComponents apiReusableComponents;
        StandardReportPage standardReportPage;
        SummaryPrintReportPage summaryPrintReportPage;
        QueryBuilderPage queryBuilderPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;
        List<CrashRootObject> crashData;
        List<QueryBuilder> queryBuilderData;
        string[,] array, array1, array2;
        public VerifyMonthOfYearStandardReport()
        {
            VerifySummaryPrintReport();
        }
        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            apiReusableComponents = new ApiReusableComponents();
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            standardReportPage = new StandardReportPage(driver);
            queryBuilderPage = new QueryBuilderPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Standard Report functionality of Crash distribution for month of year is working as expected";
            scriptName = "TS009_Web_Crash Distribution for MoD Standard Report";
            testDataFile = "StandardReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            crashData = RetrieveTestData.GetMultipleCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
            Constant.SetConfig("Configuration\\AppSettings.json");
        }
        public void VerifySummaryPrintReport()
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

                            //Verify standard report page
                            report.AddRange(standardReportPage.VerifyStandardReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Standard Report page"));

                            //Navigate to Casualty Severity by year page.
                            report.AddRange(standardReportPage.RunReportWithoutFilter("Crash Distribution for Month of Year"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            for (int i = 0; i < crashData.Count; i++)
                            {
                                //Get table dimensions
                                int rowOfCrashDateMonth = standardReportPage.GetFirstRow(testData);
                                int rowOfTotal = standardReportPage.GetTableRow(testData);
                                int colOfTotal = standardReportPage.GetTableCol(testData, rowOfCrashDateMonth);

                                array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
                                array = standardReportPage.ReadTable(rowOfTotal, colOfTotal, rowOfCrashDateMonth);

                                //Add crash using API
                                Int32 timeStampSample = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                crashData[i].crash.fieldValues[0].value = timeStampSample.ToString();
                                report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[i]));
                                foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                    screenshotList.Add(screenshot);

                                //Click on run button
                                standardReportPage.ClickRunButton();
                                
                                //Get table dimensions
                                int rowOfCrashDateMonthnew = standardReportPage.GetFirstRow(testData);
                                int rowOfTotalnew = standardReportPage.GetTableRow(testData);
                                int colOfTotalnew = standardReportPage.GetTableCol(testData, rowOfCrashDateMonth);

                                //Retrieve table value after adding crash
                                array1 = new string[(rowOfTotalnew - (rowOfCrashDateMonthnew - 1)), colOfTotalnew];
                                array1 = standardReportPage.ReadTable(rowOfTotalnew, colOfTotalnew, rowOfCrashDateMonthnew);

                                //compare table values before and after creating the crash record
                                report.AddRange(standardReportPage.CompareTableForMonthOfYearReport(testData, crashData[i], array, array1, rowOfTotal, colOfTotal, rowOfCrashDateMonth));

                            }
                            //Navigate to query builder
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select crash form
                            queryBuilderData[0].fieldValue = ApiReusableComponents.crashReferenceNumber;
                            string crashFormField = testData["crashReferenceNumber"].ToString();
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashFormField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Apply query builder
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Close Query Builder Page
                            report.AddRange(queryBuilderPage.CloseQueryBuilderPage());


                            //Apply filter and run
                            report.AddRange(standardReportPage.RunReportAfterQueryApplied("Crash Distribution for Month of Year"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Get table dimensions
                            int rowOfCrashDateMonthAfterQuery = standardReportPage.GetFirstRow(testData);
                            int rowOfTotalAfterQuery = standardReportPage.GetTableRow(testData);
                            int colOfTotalAfterQuery = standardReportPage.GetTableCol(testData, rowOfCrashDateMonthAfterQuery);

                            //Retrieve table value 
                            array2 = new string[(rowOfTotalAfterQuery - (rowOfCrashDateMonthAfterQuery - 1)), colOfTotalAfterQuery];
                            array2 = standardReportPage.ReadTable(rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfCrashDateMonthAfterQuery);

                            //Compare Table after applying the query
                            report.AddRange(standardReportPage.CompareTableAfterQuery(testData, array1, array2, rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfCrashDateMonthAfterQuery));

                            //Verify export feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifyReportExportToDocument("Crash Distribution by month of year report"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Clear query builder
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
