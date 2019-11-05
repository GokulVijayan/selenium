using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using FrameworkSetup.Pages;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;
using iMAAPTestAPI.CrashRecords;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:User Defined Report functionality is working as expected
Test Objective              :To verify that iMAAP web portal - User Defined Report functionality is working as expected
Author 				        :Experion
Script Name 			    :TS1-Generate user defined report and add,delete template
Script Created on 		    :11/08/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class VerifyUserDefinedReportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        ApiReusableComponents apiReusableComponents;
        UserDefinedReportPage userDefinedReportPage;
        LogoutPage logoutPage;
        QueryBuilderPage queryBuilderPage;
        List<QueryBuilder> queryBuilderData;
        CrashRootObject crashData;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0,crashCount,updatedCrashCount;

        public VerifyUserDefinedReportTest()
        {
            VerifyUserDefinedReport();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            userDefinedReportPage = new UserDefinedReportPage(driver);
            queryBuilderPage = new QueryBuilderPage(driver);
            logoutPage = new LogoutPage(driver);
            apiReusableComponents = new ApiReusableComponents();
            testObjective = "To verify that iMAAP web portal - User Defined Report functionality is working as expected";
            scriptName = "TS1-Generate user defined report and add,delete template";
            testDataFile = "UserDefinedReportTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
            Constant.SetConfig("Configuration\\AppSettings.json");
            crashData= RetrieveTestData.GetCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
        }

        
        public void VerifyUserDefinedReport()
        {
            Init();
            if (jsonArray != null)
            {
                foreach (var testData in jsonArray)
                {
                    try
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


                            crashData.crash.fieldValues[0].value = ReusableComponents.ToUnixTimeStamp(DateTime.Today).ToString();
                            apiReusableComponents.AddMultipleCrashUsingAPI(crashData);


                            //Navigate to user defined report screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("User Defined Reports"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify user defined report page
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "User Defined Report page"));


                            //Apply filter in user defined Report
                            report.AddRange(userDefinedReportPage.ApplyFilterInUserDefinedReport());
                            foreach (string screenshot in userDefinedReportPage.GetUserDefinedReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Select Form Fields in user defined Report
                            report.AddRange(userDefinedReportPage.RunUserDefinedReport(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "UserDefinedReport"));

                            //Retrieves Crash Count
                            crashCount = userDefinedReportPage.GetCrashCount(testData);

                            //Navigate to crash records screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Crash Records"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Navigate to ARF
                            report.AddRange(homePage.NavigateToARF());
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Enter mandatory and optional fields in crash form
                            report.AddRange(crashPage.AddCrashDetailsWithOptionalFields(testData, testDataFile));


                            //Navigate to vehicle tab
                            report.AddRange(vehiclePage.NavigateToVehicleTab());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Tab"));


                            //Enter mandatory and optional fields in vehicle form
                            report.AddRange(vehiclePage.EnterOptionalVehicleDetails(testData, testDataFile, 1));
                         
                            //Save Crash Record
                            report.AddRange(casualtyPage.SaveCrashRecord(testData));
                            foreach (string screenshot in casualtyPage.GetCasualtyPageScreenshots())
                                screenshotList.Add(screenshot);

                           
                            //Navigate to user defined report screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("User Defined Reports"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify user defined report page
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "User Defined Report pages"));


                            //Apply filter in user defined Report
                            report.AddRange(userDefinedReportPage.ApplyFilterInUserDefinedReport());
                            foreach (string screenshot in userDefinedReportPage.GetUserDefinedReportPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Select Form Fields in user defined Report
                            report.AddRange(userDefinedReportPage.RunUserDefinedReport(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "UserDefinedReportt"));

                            //Retrieves Crash Count
                            updatedCrashCount = userDefinedReportPage.GetCrashCount(testData);

                            //Verify User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReport(crashCount,updatedCrashCount));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VerifyUserDefinedReport"));



                            //Navigate to Query Builder Page
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select crash form fields
                            string crashField = testData["crashReferenceNumber"].ToString();
                            queryBuilderData[0].fieldValue = casualtyPage.GetCrashRefNo();
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Apply Query
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Close Query Builder Page
                            report.AddRange(queryBuilderPage.CloseQueryBuilderPage());


                            //Navigate to user defined report screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("User Defined Reports"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify user defined report page
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "User Defined Report page"));


                            //Select Form Fields in user defined Report
                            report.AddRange(userDefinedReportPage.RunUserDefinedReport(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "UserDefinedReport"));


                            //Retrieves Crash Count
                            updatedCrashCount = userDefinedReportPage.GetQueryBuilderCrashCount(testData);

                            //Verify User Defined Report
                            report.AddRange(userDefinedReportPage.VerifyUserDefinedReport(0, updatedCrashCount));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VerifyUserDefinedReport"));


                            //Verify Add and Delete Template Functionality in User Defined Report
                            report.AddRange(userDefinedReportPage.AddAndDeleteTemplateInUserDefinedPage(testData));
                            foreach (string screenshot in userDefinedReportPage.GetUserDefinedReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Logout from application
                            report.AddRange(logoutPage.LogoutFromApplication(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));

                            Exit();
                        }
                    }
                    catch(Exception e)
                    {
                        Exit();
                        Console.WriteLine(e);
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