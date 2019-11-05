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
Test Case Automated 		:Add ARF, Edit ARF and Delete ARF functionality is working as expected
Test Objective              :To verify Add ARF, Edit ARF and Delete ARF functionality
Author 				        :Experion
Script Name 			    :TS001_Web_Verify the Add ARF, Edit ARF and Delete ARF functionality
Script Created on 		    :1/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class EditAndDeleteCrashTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile,editTestDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;

        public EditAndDeleteCrashTest()
        {
            VerifyEditAndDeleteCrashTest();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify Add ARF, Edit ARF and Delete ARF functionality";
            scriptName = "TS001_Web_Verify the Add ARF, Edit ARF and Delete ARF functionality ";
            testDataFile = "AddandDeleteCrashDetailsTest.json";
            editTestDataFile = "EditCrashDetailsTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        
        public void VerifyEditAndDeleteCrashTest()
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


                            //Edit crash details
                            report.AddRange(crashPage.EditCrashDetailsWithOptionalFields(testData, editTestDataFile));


                            //Navigate to vehicle tab
                            report.AddRange(vehiclePage.NavigateToVehicleTab());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Edit Vehicle Tab"));


                            //Edit vehicle details
                            report.AddRange(vehiclePage.EditOptionalVehicleDetails(testData, editTestDataFile, 1));


                            //Navigate to casualty tab
                            report.AddRange(casualtyPage.NavigateToCasualtyTab());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Edit Casualty Tab"));


                            //Edit casualty details
                            report.AddRange(casualtyPage.EditOptionalCasualtyDetails(testData, editTestDataFile, 1));


                            //Save Crash Record
                            report.AddRange(casualtyPage.SaveCrashRecord(testData));
                            foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Delete crash record
                            report.AddRange(crashPage.DeleteCrashRecord(testData, casualtyPage.GetCrashRefNo()));
                            foreach (string screenshot in crashPage.GetCrashPageScreenshots())
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