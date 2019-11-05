
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Summary Print Report functionality is working as expected
Test Objective              :To verify that iMAAP web portal - Summary Print Report functionality is working as expected
Author 				        :Experion
Script Name 			    :TS003_iMAAP_Web_Summary Print Report verification
Script Created on 		    :11/08/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class VerifySummaryPrintReportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        SummaryPrintReportPage summaryPrintReportPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        QueryBuilderPage queryBuilderPage;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<QueryBuilder> queryBuilderData;
        List<TestReportSteps> report;
        int testCheck = 0;

        public VerifySummaryPrintReportTest()
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
            queryBuilderPage = new QueryBuilderPage(driver);
            casualtyPage = new CasualtyPage(driver);
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Summary Print Report functionality is working as expected";
            scriptName = "TS003_iMAAP_Web_Summary Print Report verification";
            testDataFile = "SummaryPrintReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
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

                            //Navigate to crash records screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Crash Records"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Navigate to ARF
                            report.AddRange(homePage.NavigateToARF());
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Enter mandatory and optional fields in crash form
                            report.AddRange(crashPage.AddCrashDetailsWithOptionalFields(testData, testDataFile));


                            //Navigate to vehicle tab
                            report.AddRange(vehiclePage.NavigateToVehicleTab());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab"));


                            //Enter mandatory and optional fields in vehicle form
                            report.AddRange(vehiclePage.EnterOptionalVehicleDetails(testData, testDataFile, 1));


                            //Navigate to casualty tab
                            report.AddRange(casualtyPage.NavigateToCasualtyTab());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab"));


                            //Click on add new button
                            report.AddRange(casualtyPage.ClickOnAddNewButton());

                            //Enter mandatory and optional fields in casualty form
                            report.AddRange(casualtyPage.EnterOptionalCasualtyDetails(testData, testDataFile, 1));


                            //Save Crash Record
                            report.AddRange(casualtyPage.SaveCrashRecord(testData));
                            foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                                screenshotList.Add(screenshot);



                            //Navigate to report screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Summary Print Report"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify summary print report page
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Summary Print Report page"));


                            //Apply filter and run Summary Print Report
                            report.AddRange(summaryPrintReportPage.ApplyFilterAndRunReport());
                            foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Select Form Fields in Summary Print Report
                            report.AddRange(summaryPrintReportPage.SelectFieldsFromSummaryPrintReport(testDataFile));

                            //Run summary print report
                            report.AddRange(summaryPrintReportPage.RunSummaryPrintReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Run Summary Print Report"));


                            //Verify Generated Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifyGeneratedSummaryPrintReport(testData, casualtyPage.GetCrashRefNo()));
                            foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                                screenshotList.Add(screenshot);



                            //Verify Add and Delete Template in Summary Print Report
                            report.AddRange(summaryPrintReportPage.AddAndDeleteTemplateInSummaryPrintPage(testData));
                            foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Navigate to Query Builder Page
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select crash form fields
                            string crashField = testData["crashReferenceNumber"].ToString();
                            queryBuilderData[0].fieldValue = casualtyPage.GetCrashRefNo();
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Apply Query
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Close Query Builder Page
                            report.AddRange(queryBuilderPage.CloseQueryBuilderPage());

                            //Navigate to report screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Summary Print Report"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify summary print report page
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Summary Print Report page"));


                            //Apply filter and run Summary Print Report
                            report.AddRange(summaryPrintReportPage.ApplyFilterAndRunReport());
                            foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Select Form Fields in Summary Print Report
                            report.AddRange(summaryPrintReportPage.SelectFieldsFromSummaryPrintReport(testDataFile));

                            //Run summary print report
                            report.AddRange(summaryPrintReportPage.RunSummaryPrintReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Run Summary Print Report"));



                            //Verify Generated Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifyGeneratedSummaryPrintReport(testData, casualtyPage.GetCrashRefNo()));
                            foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Logout from application
                            report.AddRange(logoutPage.LogoutFromApplication(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));

                            Exit();
                        }
                    }
                    catch(Exception e)
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