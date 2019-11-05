
using Ex_haft.Configuration;
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
Test Case Automated 		:Validate a crash record successfully
Test Objective              :To Validate a crash record successfully
Author 				        :Experion
Script Name 			    :TS005_Web_Validate a crash record successfully
Script Created on 		    :2/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    [TestFixture]
    class ValidateAndSaveCrashTest
    {
        private IWebDriver driver;
        CrashPage crashPage;
        LoginPage loginPage;
        LogoutPage logoutPage;
        HomePage homePage;
        VehiclePage vehicleDetailsPage;
        CasualtyPage casualtyDetailsPage;
        JArray jsonArray1, jsonArray2;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        string testDataFile1, testDataFile2;

        [Obsolete]
        public ValidateAndSaveCrashTest()
        {

            AddAndSearchCrash();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            crashPage = new CrashPage(driver);
            logoutPage = new LogoutPage(driver);
            homePage = new HomePage(driver);
            vehicleDetailsPage = new VehiclePage(driver);
            casualtyDetailsPage = new CasualtyPage(driver);
            scriptName = "TS005_Web_Validate a crash record successfully";
            testObjective = "To verify that user is able to validate and save a crash record successfully";      
            testDataFile1 = "ValidateAndSaveCrashNegativeTest.json";
            testDataFile2 = "ValidateAndSaveCrashSuccessTest.json";
            jsonArray1 = ConfigFile.RetrieveInputTestData(testDataFile1);
            jsonArray2 = ConfigFile.RetrieveInputTestData(testDataFile2);
            Constant.SetConfig("Configuration\\AppSettings.json");

        }

        [Obsolete]
        public void AddAndSearchCrash()
        {
            Init();
            if (jsonArray1 != null)
            {
                foreach (var testData in jsonArray1)
                {


                    //Login to the application
                    report = loginPage.LoginToApplication(testData);
                    foreach (string screenshot in loginPage.GetLoginPageScreenshots())
                        screenshotList.Add(screenshot);

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

                    //Navigate to crash details screen
                    report.AddRange(crashPage.AddCrashDetailsWithOptionalFields(testData, testDataFile1));


                    //Navigate to vehicle tab
                    report.AddRange(vehicleDetailsPage.NavigateToVehicleTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab"));

                    //Navigate to vehicle details screen
                    report.AddRange(vehicleDetailsPage.EnterOptionalVehicleDetails(testData, testDataFile1, 1));


                    //Navigate to casualty tab
                    report.AddRange(casualtyDetailsPage.NavigateToCasualtyTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab"));

                    //Click on add new button
                    report.AddRange(casualtyDetailsPage.ClickOnAddNewButton());

                    //Navigate to casuality details screen
                    report.AddRange(casualtyDetailsPage.EnterOptionalCasualtyDetails(testData, testDataFile1, 1));


                    //Validate and Save Crash Record
                    report.AddRange(casualtyDetailsPage.SaveCrashRecordAndVerifyErrorToast(testData));
                    foreach (string screenshot in casualtyDetailsPage.GetCasualtyPageScreenshots())
                        screenshotList.Add(screenshot);

                }
            }

            if (jsonArray2 != null)
            {
                foreach (var testData in jsonArray2)
                {

                    //Navigate to crash tab
                    report.AddRange(crashPage.NavigateToEditCrashTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Crash Tab"));

                    //Navigate to crash details screen
                    report.AddRange(crashPage.AddCrashDetailsWithOptionalFields(testData, testDataFile2));


                    //Navigate to vehicle tab
                    report.AddRange(vehicleDetailsPage.NavigateToVehicleTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab"));

                    //Navigate to vehicle details screen
                    report.AddRange(vehicleDetailsPage.EnterOptionalVehicleDetails(testData, testDataFile2, 1));

                    //Navigate to casualty tab
                    report.AddRange(casualtyDetailsPage.NavigateToCasualtyTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab"));

                    //Navigate to casuality details screen
                    report.AddRange(casualtyDetailsPage.EnterOptionalCasualtyDetails(testData, testDataFile2, 1));


                    //Validate and Save Crash Record
                    report.AddRange(casualtyDetailsPage.SaveCrashRecord(testData));
                    foreach (string screenshot in casualtyDetailsPage.GetCasualtyPageScreenshots())
                        screenshotList.Add(screenshot);

                    //Logout from the application
                    report.AddRange(logoutPage.LogoutFromApplication(testData));
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "LogoutFromApplication_Screenshot"));
                    Exit();
                }



            }
            
        }


        public void Exit()
        {
            driver.Quit();
            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
        }

    }
}
