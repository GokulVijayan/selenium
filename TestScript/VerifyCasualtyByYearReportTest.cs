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
using System.Text;


/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Standard Report functionality of Casualty Severity by Year is working as expected
Test Objective              :To verify that iMAAP web portal -  Standard Report functionality of Casualty Severity by Year is working as expected
Author 				        :Experion
Script Name 			    :TS011_Web_Casualty Severity by Year Standard Report
Script Created on 		    :09/09/2019
#######################################################################
*/


namespace FrameworkSetup.TestScript
{
    class VerifyCasualtyByYearReportTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        ApiReusableComponents apiReusableComponents;
        CasualtyPage casualtyPage;
        QueryBuilderPage queryBuilderPage;
        SummaryPrintReportPage summaryPrintReportPage;
        LogoutPage logoutPage;
        StandardReportPage standardReportPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        List<CrashRootObject> crashData;
        List<QueryBuilder> queryBuilderData;
        int testCheck = 0;
        string[,] arrayNew;


        public VerifyCasualtyByYearReportTest()
        {
            AddCasualtyByYearReport();

        }

        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            apiReusableComponents = new ApiReusableComponents();
            queryBuilderPage = new QueryBuilderPage(driver);
            standardReportPage = new StandardReportPage(driver);
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Standard Report functionality of Casualty Severity By Year.";
            scriptName = "TS011_Web_Casualty Severity by Year Standard Report";
            testDataFile = "VerifyCasualtySeverityByYearTestData.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            crashData = RetrieveTestData.GetMultipleCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        public void AddCasualtyByYearReport()
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


                            //Navigate to crash records screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Standard Report"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify standard report page
                            report.AddRange(standardReportPage.VerifyStandardReportPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Standard Report page"));

                            //Navigate to Casualty Severity by year page.
                            report.AddRange(standardReportPage.RunReportWithoutFilter("Casualty Severity by Year"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            for (int i = 0; i < crashData.Count; i++)
                            {
                                //Get table dimensions
                                int rowOfCrashDateYear = standardReportPage.GetFirstRow(testData);
                                int rowOfTotal = standardReportPage.GetTableRow(testData);
                                int colOfTotal = standardReportPage.GetTableCol(testData, rowOfCrashDateYear);

                                //Retrieve value to be checked from the table in the report
                                string[,] array = new string[(rowOfTotal - (rowOfCrashDateYear - 1)), colOfTotal];
                                array = standardReportPage.ReadTable(rowOfTotal, colOfTotal, rowOfCrashDateYear);


                                //Add crash using API
                                Int32 timeStampSample = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                crashData[i].crash.fieldValues[0].value = timeStampSample.ToString();
                                report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[i]));


                                //Click on run button
                                standardReportPage.ClickRunButton();

                                //Get table dimensions
                                int rowOfCrashDateYearNew = standardReportPage.GetFirstRow(testData);
                                int rowOfTotalNew = standardReportPage.GetTableRow(testData);
                                int colOfTotalNew = standardReportPage.GetTableCol(testData, rowOfCrashDateYear);

                                //Retrieve value from the updated report.
                                arrayNew = new string[(rowOfTotal - (rowOfCrashDateYear - 1)), colOfTotal];
                                arrayNew = standardReportPage.ReadTable(rowOfTotal, colOfTotal, rowOfCrashDateYear);

                                //compare table values before and after creating the crash record
                                report.AddRange(standardReportPage.CompareTableOfCasualtyByYear(testData, crashData[i], array,
                                    arrayNew, rowOfTotal, colOfTotal, rowOfCrashDateYear));


                            }

                            //Verify export feature in Summary Print Report
                            report.AddRange(summaryPrintReportPage.VerifyReportExportToDocument("Casualty Severity by Year"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Navigate to query builder
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select crash form
                            queryBuilderData[0].fieldValue = ApiReusableComponents.crashReferenceNumber;
                            string crashFormField = testData["crashReferenceNumber"].ToString();
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashFormField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Apply query builder
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Close Query Builder Page
                            report.AddRange(queryBuilderPage.CloseQueryBuilderPage());


                            //Select corresponding report
                            report.AddRange(standardReportPage.RunReportAfterQueryApplied("Casualty Severity by Year"));
                            foreach (string screenshot in standardReportPage.GetStandardReportPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Get table dimensions
                            int rowOfYearAfterQuery = standardReportPage.GetFirstRow(testData);
                            int rowOfTotalAfterQuery = standardReportPage.GetTableRow(testData);
                            int colOfTotalAfterQuery = standardReportPage.GetTableCol(testData, rowOfYearAfterQuery);

                            //Retrieve table value 
                            string[,] array3 = new string[(rowOfTotalAfterQuery - (rowOfYearAfterQuery - 1)), colOfTotalAfterQuery];
                            array3 = standardReportPage.ReadTable(rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfYearAfterQuery);

                            //Validate the report generated.
                            report.AddRange(standardReportPage.CompareTableAfterQuery(testData, arrayNew, array3, rowOfTotalAfterQuery, colOfTotalAfterQuery, rowOfYearAfterQuery));

                            //Logout from application
                            report.AddRange(logoutPage.LogoutFromApplication(testData));
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));


                            Exit();

                        }

                    }
                    catch (Exception ex)
                    {
                        Exit();
                        Console.WriteLine("Error" + ex);
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

