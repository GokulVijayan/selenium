
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:User Defined Report Export functionality is working as expected
Test Objective              :To verify that iMAAP web portal - User Defined Report Export functionality is working as expected
Author 				        :Experion
Script Name 			    :TS2-Export report to different formats
Script Created on 		    :09/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class VerifyUserDefinedReportExportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        UserDefinedReportPage userDefinedReportPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;

        public VerifyUserDefinedReportExportTest()
        {
            VerifyUserDefinedReport();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            userDefinedReportPage = new UserDefinedReportPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - User Defined Report Export functionality is working as expected";
            scriptName = "TS2-Export report to different formats";
            testDataFile = "UserDefinedReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        
        public void VerifyUserDefinedReport()
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
                         
                            //Save Crash Record
                            report.AddRange(casualtyPage.SaveCrashRecord(testData));
                            foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                                screenshotList.Add(screenshot);

                           

                            //Navigate to user defined report screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("User Defined Reports"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify user defined report page
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "User Defined Report page"));


                            //Apply filter in user defined Report
                            report.AddRange(userDefinedReportPage.ApplyFilterInUserDefinedReport());
                            foreach (string screenshot in userDefinedReportPage.GetUserDefinedReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Select Form Fields in user defined Report
                            report.AddRange(userDefinedReportPage.RunUserDefinedReport(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "UserDefinedReport"));

                            //Verify export to document feature in User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportExportToDocument());



                            //Verify export to pdf feature in User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportExportToPDF());


                            //Verify export to html feature in User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportExportToHTML());



                            //Verify export to word feature in User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportExportToWord());



                            //Verify export to excel feature in User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportExportToExcel());



                            //Verify export to data file feature in User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportExportToDataFile());




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