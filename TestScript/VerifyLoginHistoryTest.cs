using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Export Form Fields functionality
Test Objective              :To verify that iMAAP web portal - Login History details are valid.
Author 				        :Experion
Script Name 			    :TS028_Verify Login History
Script Created on 		    :26/09/2019
#######################################################################
*/
namespace FrameworkSetup.TestScript
{
    class VerifyLoginHistoryTest
    {
        private OpenQA.Selenium.IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        LoginHistoryPage loginHistoryPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;
        string testDataFile;

        public VerifyLoginHistoryTest()
        {
            VerifyLoginHistory();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            loginHistoryPage = new LoginHistoryPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Login History details are valid.";
            scriptName = "TS028_Verify Login History";
            testDataFile = "LoginHistoryTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void VerifyLoginHistory()
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

                        //Navigate to LoginHistory
                        report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Login History"));
                        foreach (string screenshot in homePage.GetHomePageScreenshots())
                            screenshotList.Add(screenshot);

                        //Verify Login History
                        var result = loginHistoryPage.VerifyLoginHistory(testData);
                        report.AddRange(result.Item1);
                        foreach (string screenshot in loginHistoryPage.GetLoginHistoryScreenshots())
                            screenshotList.Add(screenshot);
                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Login history."));

                        //Logout from application
                        report.AddRange(logoutPage.LogoutApplication(testData));
                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout to test login history data."));

                        //Login to application
                        IConfigurationRoot config = ConfigFile.GetAppConfig("Configuration\\AppSettings.json");
                        driver.Navigate().GoToUrl(config.GetSection("appSettings")["url"]);
                        report.AddRange(loginPage.LoginApplication(testData));
                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Login to test login history data."));
                        homePage.VerifyHomePage();
                        homePage.NavigateToScreenFromHamburgerMenu("Login History");

                        //Verify previous login in Login History
                        report.AddRange(loginHistoryPage.VerifyPreviousLoginInHistory(testData, result.Item2));
                        foreach (string screenshot in loginHistoryPage.GetLoginHistoryScreenshots())
                            screenshotList.Add(screenshot);

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
