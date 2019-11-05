
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Summary Print Report Export functionality is working as expected
Test Objective              :To verify that iMAAP web portal - Summary Print Report Export functionality is working as expected
Author 				        :Experion
Script Name 			    :TS2-Export report to different formats 
Script Created on 		    :05/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class SummaryPrintReportExportTest
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
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;

        public SummaryPrintReportExportTest()
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
            testObjective = "To verify that iMAAP web portal - Summary Print Report Export functionality is working as expected";
            scriptName = "TS2-Export Summary Print report to different formats";
            testDataFile = "SummaryPrintReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
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

                            //Verify export to document feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportExportToDocument());
                            


                            //Verify export to pdf feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportExportToPDF());
                            

                            //Verify export to html feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportExportToHTML());
                            


                            //Verify export to word feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportExportToWord());
                            


                            //Verify export to excel feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportExportToExcel());
                            


                            //Verify export to data file feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifySummaryPrintReportExportToDataFile());
                           


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
    