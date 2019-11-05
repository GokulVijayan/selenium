using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using iMAAPTestAPI.CrashRecords;
using iMAAPTestAPI;
using FrameworkSetup.APITestScript;
using Ex_haft.Utilities.Reports;
using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.GenericComponents;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Audit Trail for all User Action Functionality is working as expected
Test Objective              :To verify that iMAAP web portal -  Audit Trail for all User Actions
Author 				        :Experion
Script Name 			    :TS011_Web_Verify audit trail for the user actions
Script Created on 		    :09/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    class VerifyAuditTrailTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        ApiReusableComponents apiReusableComponents;
        StandardReportPage standardReportPage;
        SummaryPrintReportPage summaryPrintReportPage;
        AuditTrailPage auditTrailPage;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        int testCheck = 0;
        string crashDeleteData;
        List<CrashRootObject> crashData;

        public VerifyAuditTrailTest()
        {
            VerifAuditTrail();
        }
        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            auditTrailPage = new AuditTrailPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            apiReusableComponents = new ApiReusableComponents();
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            standardReportPage = new StandardReportPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Audit trail functionality for adding, deleting, and updation for a crash record is working as expected ";
            scriptName = "TS011_Web_Verify audit trail for the user actions";
            testDataFile = "AuditTrailTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            crashData = RetrieveTestData.GetMultipleCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
            crashDeleteData = ConfigFile.RetrieveDeleteCrashTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }
        public void VerifAuditTrail()
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


                            //Navigate to Audit trail page
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Audit Trail"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            ////Verify standard report page
                            report.AddRange(auditTrailPage.VerifyAuditTrailPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Audit Trail page"));

                            for (int i = 0; i < crashData.Count; i++)
                            {

                                //Add crash using API
                                Int64 timeStampSample = auditTrailPage.ToUnixTimeStamp(DateTime.Today);
                                crashData[i].crash.fieldValues[0].value = timeStampSample.ToString();
                                report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[i]));
                                foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                    screenshotList.Add(screenshot);

                            }

                            //Select options and run report
                            auditTrailPage.SelectOptions(testData);

                            //Read table
                            string[,] array = auditTrailPage.ReadTable();

                            //Verify audit Trail for add crash
                            report.AddRange(auditTrailPage.VerifyAuditTrailForAddCrash(array, testData));
                            foreach (string screenshot in auditTrailPage.GetAuditTrailPageScreenshots())
                                screenshotList.Add(screenshot);

                            //update crash 
                            apiReusableComponents.UpdateCrashUsingAPI(testData);

                            //Select edit check box
                            auditTrailPage.SelectEditcheckBox(testData);

                            //Read table
                            string[,] array1 = auditTrailPage.ReadTable();

                            //Verify audit trail for editing crash details
                            report.AddRange(auditTrailPage.VerifyAuditTrailForUpdateCrash(array1, testData));
                            foreach (string screenshot in auditTrailPage.GetAuditTrailPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Delete crash record
                            apiReusableComponents.DeleteCrashUsingAPI(crashDeleteData);

                            //Select edit check box
                            auditTrailPage.SelectDeletecheckBox(testData);

                            //Read table
                            string[,] array2 = auditTrailPage.ReadTable();

                            //Verify audit trail for Deleting crash records
                            report.AddRange(auditTrailPage.VerifyAuditTrailForDeleteCrash(array2, testData));
                            foreach (string screenshot in auditTrailPage.GetAuditTrailPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Logout from application
                            report.AddRange(logoutPage.LogoutFromApplication(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));
                            Exit();
                        }
                    }
                    catch (Exception e)
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
