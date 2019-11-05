using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		:Selenium
Application Function Automated 	:
Test Case Automated 		:Summary Print Report functionality is working as expected
Test Objective              :To verify that iMAAP web portal - Summary Print Report functionality is working as expected
Author 				        :Experion
Script Name 			    :TS003_iMAAP_Web_Summary Print Report verification
Script Created on 		    :11/08/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class SummaryPrintReportTest
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
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;

        public SummaryPrintReportTest()
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
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Summary Print Report functionality is working as expected";
            scriptName = "TS003_iMAAP_Web_Summary Print Report verification";
            jsonArray = ConfigFile.RetrieveInputTestData("SummaryPrintReportTest.json");
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        
        public void VerifySummaryPrintReport()
        {
            Init();
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
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

                        //Add new crash record
                        report.AddRange(crashPage.AddCrashDetailsForSummaryPrintReport(testData));
                        foreach (string screenshot in crashPage.GetCrashPageScreenshots())
                            screenshotList.Add(screenshot);

                        //Add new vehicle record
                        report.AddRange(vehiclePage.AddVehicleDetailsForSummaryPrintReport(testData));
                        foreach (string screenshot in vehiclePage.GetVehiclePageScreenshots())
                            screenshotList.Add(screenshot);

                        //Add new casualty record
                        report.AddRange(casualtyPage.AddCasualtyDetailsForSummaryPrintReport(testData));
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
                        

                        //Verify Generated Summary Print Report
                        report.AddRange(summaryPrintReportPage.VerifyGeneratedSummaryPrintReport(testData, casualtyPage.GetCrashRefNo()));
                        foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                            screenshotList.Add(screenshot);

                        

                        //Verify Add and Delete Template in Summary Print Report
                        report.AddRange(summaryPrintReportPage.AddAndDeleteTemplateInSummaryPrintPage(testData));
                        foreach (string screenshot in summaryPrintReportPage.GetSummaryPrintReportPageScreenshots())
                            screenshotList.Add(screenshot);


                        //Logout from application
                        report.AddRange(logoutPage.LogoutFromApplication(testData));
                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));

                        Exit();
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