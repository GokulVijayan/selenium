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
Test Case Automated 		:Export Password Policies
Test Objective              :To verify that iMAAP web portal - Export Password Policy funtionality is working as expected
Author 				        :Experion
Script Name 			    :TS022_Web_Export Master Data-Password Policy details
Script Created on 		    :26/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    internal class ExportPasswordPolicyTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private PasswordPolicyPage passwordPolicyPage;
        private HomePage homePage;
        private JArray jsonArray;
        private string testObjective, scriptName, testDataFile;
        private List<string> screenshotList = new List<string>();
        private List<TestReportSteps> report;

        public ExportPasswordPolicyTest()
        {
            ExportPasswordPolicyTestSuite();
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
            passwordPolicyPage = new PasswordPolicyPage(driver);
            testObjective = "To verify that export password policies is working fine";
            scriptName = "TS022_Web_Export Master Data-Password Policy details";
            testDataFile = "AddAndSearchCrashTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        /// <summary>
        /// Exports the password policy test suite.
        /// </summary>
        private void ExportPasswordPolicyTestSuite()
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

                    //Navigate to password policy screen
                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Password Policy"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Wait for toaster to disappear
                    ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

                    //Export password policies
                    report.AddRange(passwordPolicyPage.ExportPasswordPolicies());
                    foreach (string screenshot in passwordPolicyPage.GetPasswordPolicyPageScreenshots())
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