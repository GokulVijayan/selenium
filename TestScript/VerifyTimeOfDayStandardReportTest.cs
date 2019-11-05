using FrameworkSetup.APITestScript;
using FrameworkSetup.Pages;
using FrameworkSetup.TestDataClasses;
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using iMAAPTestAPI;
using iMAAPTestAPI.CrashRecords;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Standard Report functionality is working as expected
Test Objective              :To verify that iMAAP web portal - Crash Distribution for Time of Day Standard Report functionality is working as expected
Author 				        :Experion
Script Name 			    :TS010_Web_Crash Distribution for ToD Standard Report
Script Created on 		    :28/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class VerifyTimeOfDayStandardReportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        ApiReusableComponents apiReusableComponents;
        SummaryPrintReportPage summaryPrintReportPage;
        StandardReportPage standardReportPage;
        LogoutPage logoutPage;
        QueryBuilderPage queryBuilderPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<QueryBuilder> queryBuilderData;
        List<TestReportSteps> report;
        int testCheck = 0;
        List<CrashRootObject> crashData;
        static string[,] arrayNew;
        public VerifyTimeOfDayStandardReportTest()
        {
            VerifyTimeOfDayStandardReport();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            Constant.SetConfig("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            standardReportPage = new StandardReportPage(driver);
            queryBuilderPage = new QueryBuilderPage(driver);
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            logoutPage = new LogoutPage(driver);
            apiReusableComponents = new ApiReusableComponents();
            testObjective = "To verify that iMAAP web portal - Crash Distribution for Time of Day Standard Report functionality is working as expected";
            scriptName = "TS010_Web_Crash Distribution for ToD Standard Report";
            testDataFile = "VerifyTimeOfDayStandardReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            crashData = RetrieveTestData.GetMultipleCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
        }


        public void VerifyTimeOfDayStandardReport()
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

                            for (int i = 0; i < crashData.Count; i++)
                            {
                                crashData[i].crash.fieldValues[58].value = "50";
                                crashData[i].crash.fieldValues[0].value = ReusableComponents.ToUnixTimeStamp(DateTime.Today).ToString();
                                apiReusableComponents.AddMultipleCrashUsingAPI(crashData[i]);

                            }

                           


                            //Apply filter and run time of day Standard Report
                            report.AddRange(standardReportPage.ApplyFilterInTimeOfDayReport("Crash Distribution for Time of Day"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            for (int i = 0; i < crashData.Count; i++)
                            {

                                //Get table dimensions
                                Thread.Sleep(Constant.waitTimeoutForExport);
                                int rowOfCrashDateTime = standardReportPage.GetFirstRow(testData);
                                int rowOfTotal = standardReportPage.GetTableRow(testData);
                                int colOfTotal = standardReportPage.GetTableCol(testData, rowOfCrashDateTime);

                                //Store the table dimension in the array
                                string[,] array = new string[(rowOfTotal - (rowOfCrashDateTime - 1)), colOfTotal];
                                array = standardReportPage.ReadTable(rowOfTotal, colOfTotal, rowOfCrashDateTime);

                                //Add crash record using API
                                int Time = standardReportPage.GetTTimeToEnter();
                                string timeStamp = apiReusableComponents.GetTimestampToEnter();
                                crashData[i].crash.fieldValues[58].value = Time.ToString();
                                crashData[i].crash.fieldValues[0].value = ReusableComponents.ToUnixTimeStamp(DateTime.Today).ToString();
                                report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[i]));

                                Thread.Sleep(Constant.waitTimeout);

                                //Click on run button
                                report.AddRange(standardReportPage.ClickRunButton());
                                foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                    screenshotList.Add(screenshot);

                                //Get table dimensions
                                int rowOfCrashDateTimeNew = standardReportPage.GetFirstRow(testData);
                                int rowOfTotalNew = standardReportPage.GetTableRow(testData);
                                int colOfTotalNew = standardReportPage.GetTableCol(testData, rowOfCrashDateTimeNew);

                                //Store the table dimension in the arrayNew
                                arrayNew = new string[(rowOfTotal - (rowOfCrashDateTimeNew - 1)), colOfTotal];
                                arrayNew = standardReportPage.ReadTable(rowOfTotal, colOfTotal, rowOfCrashDateTimeNew);

                                //compare table values before and after creating the crash record
                                report.AddRange(standardReportPage.CompareTableForTimeOfDay(testData, crashData[i], array, arrayNew, rowOfTotal, colOfTotal, rowOfCrashDateTime));

                            }

                            //Verify export feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifyReportExportToDocument("Casualty Severity by Time"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Navigate to the Query builder screen
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select crash form fields
                            string crashFormField = testData["crashReferenceNumber"].ToString();
                            queryBuilderData[0].fieldValue = ApiReusableComponents.crashReferenceNumber;
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashFormField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Apply query
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Close Query Builder Page
                            report.AddRange(queryBuilderPage.CloseQueryBuilderPage());


                            //Apply filter and run time of day Standard Report
                            report.AddRange(standardReportPage.RunReportAfterQueryApplied("Crash Distribution for Time of Day"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Get table dimensions
                            int rowOfCrashDateTimeQuery = standardReportPage.GetFirstRow(testData);
                            int rowOfTotalAfterQuery = standardReportPage.GetTableRow(testData);
                            int colOfTotalAfterQuery = standardReportPage.GetTableCol(testData, rowOfCrashDateTimeQuery);

                            //Retrieve table value 
                            string[,] array3 = new string[(rowOfTotalAfterQuery - (rowOfCrashDateTimeQuery - 1)), colOfTotalAfterQuery];
                            array3 = standardReportPage.ReadTable(rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfCrashDateTimeQuery);

                            //Compare the table after query
                            report.AddRange(standardReportPage.CompareTableAfterQuery(testData, arrayNew, array3, rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfCrashDateTimeQuery));

                            //Clear the query applied
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