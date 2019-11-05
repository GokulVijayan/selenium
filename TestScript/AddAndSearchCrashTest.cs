using System;
using System.Collections.Generic;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using NUnit.Framework;
using RestSharp;
using FrameworkSetup.TestDataClasses;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using Ex_haft.Utilities.Reports;
using Ex_haft.Configuration;
using Ex_haft.Utilities;


/*
 #######################################################################
 Test Tool/Version 		:Selenium
 Test Case Automated 	:Verify that crash record can be added by filling both mandatory and optional fields and is able to search for that record
 Test Objective         :To verify that crash record can be added by filling both mandatory and optional fields and is able to search for that record
 Author 				:Experion
 Script Name 			:TS002_Web_Verify adding a crash record with all form fields and search that crash record 
 Script Created on 		:28/08/2019
 #######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class AddAndSearchCrashTest
    {

        private IWebDriver driver;
        LoginPage loginPage;
        LogoutPage logoutPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        string testDataFile;
        public AddAndSearchCrashTest()
        {
            AddCrashRecord();
        }

        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            logoutPage = new LogoutPage(driver);
            vehiclePage = new VehiclePage(driver);
            crashPage = new CrashPage(driver);
            casualtyPage = new CasualtyPage(driver);
            testObjective = "To verify that crash record can be added by filling both mandatory and optional fields and is able to search for that record.";
            scriptName = "TS002_Web_Verify adding a crash record with all form fields and search that crash record ";
            testDataFile = "AddAndSearchCrashTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void AddCrashRecord()
        {
            Init();
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
                {
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

                    //Enter mandatory and optional fields in crash form
                    report.AddRange(crashPage.AddCrashDetailsWithAllFields(testData, testDataFile));

                   
                    //Navigate to vehicle tab
                    report.AddRange(vehiclePage.NavigateToVehicleTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab"));


                    //Enter mandatory and optional fields in vehicle form
                    report.AddRange(vehiclePage.EnterMandatoryAndOptionalVehicleDetails(testData, testDataFile,1));


                    
                   //Navigate to casualty tab
                   report.AddRange(casualtyPage.NavigateToCasualtyTab());
                   screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab"));


                   //Click on add new button
                   report.AddRange(casualtyPage.ClickOnAddNewButton());

                   //Enter mandatory and optional fields in casualty form
                   report.AddRange(casualtyPage.EnterMandatoryAndOptionalCasualtyDetails(testData, testDataFile,1));


                    //Save Crash Record
                    report.AddRange(casualtyPage.SaveCrashRecord(testData));
                   foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                       screenshotList.Add(screenshot);

                    //Navigate to  search screen
                    report.AddRange(crashPage.SearchCrashRecords(testData, casualtyPage.GetCrashRefNo()));
                    foreach (string screenshot in crashPage.GetCrashPageScreenshots())
                        screenshotList.Add(screenshot);



                    //Logout from application
                    report.AddRange(logoutPage.LogoutFromApplication(testData));
                   screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));
                   
                    Exit();
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
