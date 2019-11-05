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
Test Case Automated 		:Export Form Fields functionality
Test Objective              :To verify that iMAAP web portal - Export Form Field details to excel is working as expected
Author 				        :Experion
Script Name 			    :TS023_Web_Export Master Data -Form field details
Script Created on 		    :25/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    class ExportFormFieldTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        FormFieldPage formFieldPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;
        string testDataFile;

        public ExportFormFieldTest()
        {
            VerifyFormFieldExport();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            formFieldPage = new FormFieldPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Export Form Fields functionality is working as expected";
            scriptName = "TS023_Web_Export Master Data -Form field details";
            testDataFile = "ExportFormFieldTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void VerifyFormFieldExport()
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

                        //Navigate to Form Field
                        report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Form Field"));
                        foreach (string screenshot in homePage.GetHomePageScreenshots())
                            screenshotList.Add(screenshot);
                        
                        //Wait for toaster to disappear
                        ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

                        //Export form field
                        report.AddRange(formFieldPage.ExportFormFields());
                        foreach (string screenshot in formFieldPage.GetFormFieldScreenshots())
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
