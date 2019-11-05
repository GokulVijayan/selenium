using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;



/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Batch Operation functionality
Test Objective              :To verify that batch operation functionality is working as expected
Author 				        :Experion
Script Name 			    :TS031_verify batch operation report
Script Created on 		    :20/09/2019
#######################################################################
*/
namespace FrameworkSetup.TestScript
{
    internal class BatchOperationTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private CrashPage crashPage;
        private VehiclePage vehiclePage;
        private CasualtyPage casualtyPage;
        private HomePage homePage;
        private ValidationRulePage validationRulePage;
        private BatchOperationPage batchOperationPage;
        private JArray jsonArray, jsonArray1;
        private string testObjective, scriptName, testDataFile, validationData;
        private List<string> screenshotList = new List<string>();
        private List<TestReportSteps> report;

        public BatchOperationTest()
        {
            BatchOperationTestSuite();
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
            vehiclePage = new VehiclePage(driver);
            crashPage = new CrashPage(driver);
            casualtyPage = new CasualtyPage(driver);
            validationRulePage = new ValidationRulePage(driver);
            batchOperationPage = new BatchOperationPage(driver);
            testObjective = "To verify batch operation functionality";
            scriptName = "TS031_verify batch operation report";
            testDataFile = "AddAndSearchCrashTest.json";
            validationData = "AddValidationRuleTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            jsonArray1 = ConfigFile.RetrieveInputTestData(validationData);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        /// <summary>
        /// Batches the operation test suite.
        /// </summary>
        private void BatchOperationTestSuite()
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

                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Validation Rule Management"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Add validation rule
                    if (jsonArray1 != null)
                    {
                        foreach (var testData1 in jsonArray1)
                        {
                            report.AddRange(validationRulePage.AddValidationRule(testData1));
                        }
                    }

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
                    report.AddRange(vehiclePage.EnterMandatoryAndOptionalVehicleDetails(testData, testDataFile, 1));

                    //Navigate to casualty tab
                    report.AddRange(casualtyPage.NavigateToCasualtyTab());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Tab"));

                    //Click on add new button
                    report.AddRange(casualtyPage.ClickOnAddNewButton());

                    //Enter mandatory and optional fields in casualty form
                    report.AddRange(casualtyPage.EnterMandatoryAndOptionalCasualtyDetails(testData, testDataFile, 1));

                    //Save Crash Record
                    report.AddRange(casualtyPage.SaveCrashRecordWithoutValidation(testData));
                    foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                        screenshotList.Add(screenshot);

                    batchOperationPage.ReloadPage();

                    Thread.Sleep(Constant.waitTimeoutForExport);
                    ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                    //Navigate to batch operation screen
                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Batch Operation"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Delete validation rule
                    if (jsonArray1 != null)
                    {
                        foreach (var testData1 in jsonArray1)
                        {
                            report.AddRange(validationRulePage.DeleteValidationRule(testData1));
                        }
                    }

                    //Validate crash from batch operation screen
                    report.AddRange(batchOperationPage.ValidateCrash());

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