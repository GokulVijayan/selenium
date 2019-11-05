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
Test Tool/Version 		    :Selenium
Test Case Automated 		:Export Lookup Details
Test Objective              :To verify that iMAAP web portal - Export Lookup details funtionality is working as expected
Author 				        :Experion
Script Name 			    :TS024_Web_Export Master Data-Lookup details
Script Created on 		    :25/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    internal class ExportLookUpDataTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private LookupPage lookupPage;
        private HomePage homePage;
        private JArray jsonArray;
        private string testObjective, scriptName, testDataFile;
        private List<string> screenshotList = new List<string>();
        private List<TestReportSteps> report;

        public ExportLookUpDataTest()
        {
            ExportLookUpData();
        }

        private void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            logoutPage = new LogoutPage(driver);
            lookupPage = new LookupPage(driver);
            testObjective = "To verify that export lookup details is working fine";
            scriptName = "TS024_Web_Export Master Data-Lookup details";
            testDataFile = "AddAndSearchCrashTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        /// <summary>
        /// Exports the look up data.
        /// </summary>
        private void ExportLookUpData()
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

                    //Navigate to Lookup screen
                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Lookup"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Wait for toaster to disappear
                    ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

                    //Export lookup
                    report.AddRange(lookupPage.ExportLookups());
                    foreach (string screenshot in lookupPage.GetLookupPageScreenshots())
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