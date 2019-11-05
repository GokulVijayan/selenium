using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using FrameworkSetup.Pages;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Cross Tab Report functionality
Test Objective              :To verify that iMAAP web portal - Cross Tab Report functionality is working as expected
Author 				        :Experion
Script Name 			    :TS007_Web_Verify Cross Tab Report
Script Created on 		    :20/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    class CrossTabReportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        ApiReusableComponents apiReusableComponents;
        CrossTabReportPage crossTabReportPage;
        QueryBuilderPage queryBuilderPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;
        string testDataFile, APIDataFile;
        List<QueryBuilder> queryBuilderData;

        public CrossTabReportTest()
        {
            VerifyCrossTabReport();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            apiReusableComponents = new ApiReusableComponents();
            crossTabReportPage = new CrossTabReportPage(driver);
            queryBuilderPage = new QueryBuilderPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Cross Tab Report functionality is working as expected";
            scriptName = "TS007_Web_Verify Cross Tab Report";
            testDataFile = "CrossTabReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            APIDataFile = ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile);
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void VerifyCrossTabReport()
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

                        //Navigate to Cross Tab report
                        report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Cross Tab"));
                        foreach (string screenshot in homePage.GetHomePageScreenshots())
                            screenshotList.Add(screenshot);

                        //Run Cross Tab report
                        report.AddRange(crossTabReportPage.RunCrossTabReport(testData));
                        foreach (string screenshot in crossTabReportPage.GetCrossTabScreenshots())
                            screenshotList.Add(screenshot);

                        //Retrieve Cross Tab report data
                        report.AddRange(crossTabReportPage.RetrieveCrossTabReportData(testData));
                        foreach (string screenshot in crossTabReportPage.GetCrossTabScreenshots())
                            screenshotList.Add(screenshot);

                        //Add crash using API
                        report.AddRange(apiReusableComponents.AddCrashUsingAPI(APIDataFile));

                        //Tap Run button
                        report.AddRange(crossTabReportPage.TapRunButton());

                        //Verify Cross Tab report
                        report.AddRange(crossTabReportPage.VerifyCrossTabReport(testData));
                        foreach (string screenshot in crossTabReportPage.GetCrossTabScreenshots())
                            screenshotList.Add(screenshot);

                        //Export Cross Tab report
                        report.AddRange(crossTabReportPage.ExportCrossTabReport());

                        //Navigate to query builder page
                        report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                        foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                            screenshotList.Add(screenshot);

                        //Select crash form fields
                        string crashField = testData["crashDriverService"].ToString();
                        report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashField));
                        foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                            screenshotList.Add(screenshot);

                        //Select vehicle form fields
                        string vehicleField = testData["vehicleDrivingPosition"].ToString();
                        report.AddRange(queryBuilderPage.SelectVehicleFormFields(queryBuilderData, testData, vehicleField));
                        foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                            screenshotList.Add(screenshot);

                        //Apply query
                        report.AddRange(queryBuilderPage.ApplyQuery(testData));
                        foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                            screenshotList.Add(screenshot);

                        //Navigate to Cross Tab report
                        report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Cross Tab"));
                        foreach (string screenshot in homePage.GetHomePageScreenshots())
                            screenshotList.Add(screenshot);

                        //Run Cross Tab report
                        report.AddRange(crossTabReportPage.RunCrossTabReport(testData));
                        foreach (string screenshot in crossTabReportPage.GetCrossTabScreenshots())
                            screenshotList.Add(screenshot);

                        //Verify applied query
                        report.AddRange(crossTabReportPage.VerifyAppliedQueryResult(testData));
                        foreach (string screenshot in crossTabReportPage.GetCrossTabScreenshots())
                            screenshotList.Add(screenshot); 

                        //Clear applied query
                        report.AddRange(queryBuilderPage.ClearQueryBuilder(testData));
                        foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
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
