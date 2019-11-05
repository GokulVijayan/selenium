
using System.Collections.Generic;
using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;


/*
 #######################################################################
 Test Tool/Version 		:Selenium
 Test Case Automated 	:Verify that a vehicle and a casualty details of Crash record can be deleted
 Test Objective         :To verify that a vehicle and a casualty details of Crash record can be deleted
 Author 				:Experion
 Script Name 			:TS003_Web_Verify deleting the vehicle and casualty details of a Crash record 
 Script Created on 		:05/09/2019
 #######################################################################
*/


namespace FrameworkSetup.TestScript
{
    class DeleteVehicleAndCasualtyTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        LogoutPage logoutPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        JArray jsonArray, jsonArray1;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        string testDataFile, testDataFile1;
        public DeleteVehicleAndCasualtyTest()
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
            testObjective = "To verify that vehicle and casualty details of a Crash record can be deleted.";
            scriptName = "TS003_Web_Verify deleting the vehicle and casualty details of a Crash record ";
            testDataFile1 = "AddAndSearchCrashTest.json";
            testDataFile = "DeleteVehicleAndCasualtyTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            jsonArray1 = ConfigFile.RetrieveInputTestData(testDataFile1);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void AddCrashRecord()
        {
            Init();
            if (jsonArray1 != null)
            {
                foreach (var testData in jsonArray1)
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
                    report.AddRange(crashPage.AddCrashDetailsWithOptionalFields(testData, testDataFile1));

                    //Navigate to vehicle tab
                    report.AddRange(vehiclePage.NavigateToVehicleTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab"));


                    //Enter mandatory and optional fields in vehicle form
                    report.AddRange(vehiclePage.EnterMandatoryAndOptionalVehicleDetails(testData, testDataFile1, 1));

                    //Navigate to casualty tab
                    report.AddRange(casualtyPage.NavigateToCasualtyTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab"));

                    //Click on add new button
                    report.AddRange(casualtyPage.ClickOnAddNewButton());

                    //Enter some of the optional fields in casualty form
                    report.AddRange(casualtyPage.EnterOptionalCasualtyDetails(testData, testDataFile1, 1));

                }
            }
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
                {
                    //Navigate to vehicle tab
                    report.AddRange(vehiclePage.NavigateToVehicleTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab2"));

                    //Click on add new button
                    report.AddRange(casualtyPage.ClickOnAddNewButton());

                    //Enter some of the optional fields in vehicle form
                    report.AddRange(vehiclePage.EnterOptionalVehicleDetails(testData, testDataFile,2));

                    //Navigate to casualty tab
                    report.AddRange(casualtyPage.NavigateToCasualtyTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab2"));

                    //Click on add new button
                    report.AddRange(casualtyPage.ClickOnAddNewButton());

                    //Enter some of the optional fields in casualty form
                    report.AddRange(casualtyPage.EnterOptionalCasualtyDetails(testData, testDataFile,2));

                    //Save Crash Record
                    report.AddRange(casualtyPage.SaveCrashRecord(testData));
                    foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                        screenshotList.Add(screenshot);

                    //Navigate to vehicle tab
                    report.AddRange(vehiclePage.NavigateToVehicleTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Before Deleting Vehicle"));

                    //Delete Vehicle2 from last saved crash record 
                    report.AddRange(vehiclePage.DeleteVehicleRecord(testData));
                    foreach (string screenshot in vehiclePage.GetVehiclePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Navigate to casualty tab
                    report.AddRange(casualtyPage.NavigateToCasualtyTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Before Deleting Casualty"));

                    //Delete Casualty2 from last saved crash record
                    report.AddRange(casualtyPage.DeleteCasualtyRecord(testData));
                    foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                        screenshotList.Add(screenshot);

                    //Save Crash Record
                    report.AddRange(casualtyPage.SaveCrashRecord(testData));
                    foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
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
