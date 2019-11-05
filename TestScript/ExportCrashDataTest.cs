
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;


/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Verify the Crash Data exported to TXT, CSV & XLSX formats
Test Objective              :To Verify the Crash Data exported to TXT, CSV & XLSX formats
Author 				        :Experion
Script Name 			    :TS020_Web_Verify the Crash Data exported to TXT, CSV & XLSX formats
Script Created on 		    :21/09/2019
#######################################################################
*/


namespace FrameworkSetup.TestScript
{
    internal class ExportCrashDataTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private ExportCrashPage exportCrashPage;
        private HomePage homePage;
        private JArray jsonArray;
        private string testObjective, scriptName, testDataFile;
        private List<string> screenshotList = new List<string>();
        private List<TestReportSteps> report;

        public ExportCrashDataTest()
        {
            ExportCrashDataTestSuite();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            logoutPage = new LogoutPage(driver);
            exportCrashPage = new ExportCrashPage(driver);
            testObjective = "To verify that export crash data is working fine";
            scriptName = "TS020_Web_Verify the Crash Data exported to TXT, CSV & XLSX formats";
            testDataFile = "AddAndSearchCrashTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        /// <summary>
        /// Exports the crash data test suite.
        /// </summary>
        private void ExportCrashDataTestSuite()
        {
            Init();
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
                {
                    report = loginPage.LoginToApplication(testData);
                    foreach (string screenshot in loginPage.GetLoginPageScreenshots())
                        screenshotList.Add(screenshot);

                    ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.webLoader);

                    //Verify home page
                    report.AddRange(homePage.VerifyHomePage());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Home page"));

                    //Navigate to export crash screen
                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Export"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Wait for toaster to disappear
                    ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

                    //Export crash records
                    report.AddRange(exportCrashPage.ExportCrash());
                    foreach (string screenshot in exportCrashPage.GetPasswordPolicyPageScreenshots())
                        screenshotList.Add(screenshot);

                    //Logout from application
                    report.AddRange(logoutPage.LogoutFromApplication(testData));
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));
                }
            }
            Exit();
        }

        /// <summary>
        /// Exits this instance.
        /// </summary>
        private void Exit()
        {
            Report.WriteResultToHtml(driver, report, screenshotList, testObjective, scriptName);
            driver.Quit();
        }
    }
}