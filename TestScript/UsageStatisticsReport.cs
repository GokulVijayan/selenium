
using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;



/*
#######################################################################
Test Tool/Version 		:Selenium
Test Case Automated 		:Usage Statistics Report functionality is working as expected
Test Objective              :To verify that iMAAP web portal - Usage Statistics Report functionality is working as expected
Author 				        :Experion
Script Name 			    :TS030_verify usage statistics report
Script Created on 		    :29/09/2019
#######################################################################
*/


namespace FrameworkSetup.TestScript
{
    internal class UsageStatisticsReport
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private HomePage homePage;
        private UsageStatisticsPage usageStatisticsPage;
        private JArray jsonArray;
        private string testObjective, scriptName, testDataFile;
        private List<string> screenshotList = new List<string>();
        private List<TestReportSteps> report;

        public UsageStatisticsReport()
        {
            UsageStatisticsTestSuite();
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
            usageStatisticsPage = new UsageStatisticsPage(driver);
            testObjective = "To verify Usage Statistics Report is working fine";
            scriptName = "TC020_Web_Verify Usage Statistics report is working fine";
            testDataFile = "AddAndSearchCrashTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        /// <summary>
        /// Usages the statistics test suite.
        /// </summary>
        private void UsageStatisticsTestSuite()
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

                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Usage Statistics"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    report.AddRange(usageStatisticsPage.RunAnalysis());
                    foreach (string screenshot in loginPage.GetLoginPageScreenshots())
                        screenshotList.Add(screenshot);

                    //Logout from application
                    report.AddRange(logoutPage.LogoutFromApplication(testData));
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));

                    Exit();
                }
            }
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