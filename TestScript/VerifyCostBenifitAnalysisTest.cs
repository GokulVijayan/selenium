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
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Cost Benefit Analysis functionality is working as expected
Test Objective              :To verify that iMAAP web portal - Cost Benefit Analysis functionality is working as expected
Author 				        :Experion
Script Name 			    :TS027_Web_Verify Cost Benefit Analysis
Script Created on 		    :24/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class VerifyCostBenifitAnalysisTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        CalculateCrashCost calculateCrashCostData;
        RunCba runCbaData;
        Cba cbaData;
        CrashPage crashPage;
        VehiclePage vehiclePage;
        CasualtyPage casualtyPage;
        SummaryPrintReportPage summaryPrintReportPage;
        SCAConfigurationPage sCAConfigurationPage;
        CBAConfigurationPage cBAConfigurationPage;
        StickAnalysisPage stickAnalysisPage;
        QueryBuilderPage queryBuilderPage;
        CounterMeasureLibraryPage counterMeasureLibraryPage;
        ApiReusableComponents apiReusableComponents;
        LogoutPage logoutPage;
        JArray jsonArray;
        string testObjective, scriptName, testDataFile, calculateCmCostData;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report, plotCrashReport, safetyCameraReport, deleteCrashReport, calculateCrashCostReport, verifyCbaReport, calculateCmCostReport, runCbaReport;
        public static List<CrashRootObject> crashData;
        private List<SafetyCameraAnalysis> safetyCameraData;
        List<PlotCrashData> plotCrashData;
        List<QueryBuilder> queryBuilderData;
        int testCheck = 0;
        static string crashDeleteData;

        public VerifyCostBenifitAnalysisTest()
        {
            VerifyCostBenifitAnalysisTestSuite();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            crashPage = new CrashPage(driver);
            vehiclePage = new VehiclePage(driver);
            casualtyPage = new CasualtyPage(driver);
            summaryPrintReportPage = new SummaryPrintReportPage(driver);
            logoutPage = new LogoutPage(driver);
            sCAConfigurationPage = new SCAConfigurationPage(driver);
            cBAConfigurationPage = new CBAConfigurationPage(driver);
            queryBuilderPage = new QueryBuilderPage(driver);
            apiReusableComponents = new ApiReusableComponents();
            stickAnalysisPage = new StickAnalysisPage(driver);
            counterMeasureLibraryPage = new CounterMeasureLibraryPage(driver); 
            testObjective = "To verify that iMAAP web portal - Cost Benefit functionality is working as expected";
            scriptName = "TS027_Web_Verify Cost Benefit Analysis";
            testDataFile = "SCAConfigurationTest.json";
            crashData = RetrieveTestData.GetMultipleCrashDataBody(ConfigFile.RetrieveAPIAddCrashInputTestData(testDataFile));
            safetyCameraData = RetrieveTestData.GetSafetyCameraDataBody(ConfigFile.RetrieveSafetyCameraTestData(testDataFile));
            plotCrashData = RetrieveTestData.GetMultiplePlotCrashBody(ConfigFile.RetrievePlotCrashesTestData(testDataFile));
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            queryBuilderData = RetrieveTestData.GetQueryBuilderDataBody(ConfigFile.RetrieveQueryBuilderTestData(testDataFile));
            crashDeleteData = ConfigFile.RetrieveDeleteCrashTestData(testDataFile);
            calculateCrashCostData = RetrieveTestData.GetCalculateCrashCostDataBody(ConfigFile.RetrieveCalculateCrashCostTestData(testDataFile));
            cbaData= RetrieveTestData.GetCbaDataBody(ConfigFile.RetrieveCbaTestData(testDataFile));
            calculateCmCostData = ConfigFile.RetrieveCMCostTestData(testDataFile);
            runCbaData= RetrieveTestData.GetRunCbaBody(ConfigFile.RetrieveRunCbaTestData(testDataFile));
            Constant.SetConfig("Configuration\\AppSettings.json");
        }

        
        public void VerifyCostBenifitAnalysisTestSuite()
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

                            //Navigate to sca configuration screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("SCA Configuration"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify SCA Configuration screen
                            report.AddRange(sCAConfigurationPage.VerifySCAConfiguration(testData));
                            foreach (string screenshot in sCAConfigurationPage.GetSCAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Edit Fixed Camera SCA Configuration 
                            report.AddRange(sCAConfigurationPage.EditFixedCameraSCAConfiguration(testData));
                            foreach (string screenshot in sCAConfigurationPage.GetSCAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Edit Mobile Camera SCA Configuration
                            report.AddRange(sCAConfigurationPage.EditMobileCameraSCAConfiguration(testData));
                            foreach (string screenshot in sCAConfigurationPage.GetSCAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Edit Average Speed Camera SCA Configuration
                            report.AddRange(sCAConfigurationPage.EditAverageSpeedCameraSCAConfiguration(testData));
                            foreach (string screenshot in sCAConfigurationPage.GetSCAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Edit Red Light Camera SCA Configuration
                            report.AddRange(sCAConfigurationPage.EditRedLightCameraSCAConfiguration(testData));
                            foreach (string screenshot in sCAConfigurationPage.GetSCAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Navigate to sca configuration screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("CBA Configuration"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify CBA Configuration screen
                            report.AddRange(cBAConfigurationPage.VerifyCBAConfiguration(testData));
                            foreach (string screenshot in cBAConfigurationPage.GetCBAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Edit CBA General Configuration 
                            report.AddRange(cBAConfigurationPage.EditCBAGeneralConfiguration(testData));
                            foreach (string screenshot in cBAConfigurationPage.GetCBAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Edit CBA General Configuration 
                            report.AddRange(cBAConfigurationPage.EditCBACrashCostConfiguration(testData));
                            foreach (string screenshot in cBAConfigurationPage.GetCBAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Edit CBA General Configuration 
                            report.AddRange(cBAConfigurationPage.EditCBAValueConversionConfiguration(testData));
                            foreach (string screenshot in cBAConfigurationPage.GetCBAConfigurationPageScreenshots())
                                screenshotList.Add(screenshot);
                            

                            //Add Crash Record
                            report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[0]));

                            //Plot crashes using circle tool
                            plotCrashReport = GenerateReport.GetReportFile("PlotCrashesUsingCircleToolReport.json");
                            report.AddRange(apiReusableComponents.PlotCrashes(plotCrashData[0], plotCrashReport));

                            //Verify safety camera analysis for Red Light Camera using location plotted by circle tool
                            safetyCameraData[0].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport= GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[0], safetyCameraReport,"Red Light","Major"));
                            safetyCameraData[1].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[1], safetyCameraReport, "Red Light", "Minor"));


                            //Plot crashes using rectangle tool
                            plotCrashReport = GenerateReport.GetReportFile("PlotCrashesUsingRectangleToolReport.json");
                            report.AddRange(apiReusableComponents.PlotCrashes(plotCrashData[1], plotCrashReport));

                            //Verify safety camera analysis for Red Light Camera using location plotted by rectangle tool
                            safetyCameraData[2].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[2], safetyCameraReport, "Red Light", "Major"));
                            safetyCameraData[3].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[3], safetyCameraReport, "Red Light", "Minor"));


                            //Plot crashes using polygon tool
                            plotCrashReport = GenerateReport.GetReportFile("PlotCrashesUsingPolygonToolReport.json");
                            report.AddRange(apiReusableComponents.PlotCrashes(plotCrashData[2], plotCrashReport));

                            //Verify safety camera analysis for Red Light Camera using location plotted by polygon tool
                            safetyCameraData[4].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[4], safetyCameraReport, "Red Light", "Major"));
                            safetyCameraData[5].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[5], safetyCameraReport, "Red Light", "Minor"));



                            //Plot crashes using line tool
                            plotCrashReport = GenerateReport.GetReportFile("PlotCrashesUsingLineToolReport.json");
                            report.AddRange(apiReusableComponents.PlotCrashes(plotCrashData[3], plotCrashReport));

                            //Verify safety camera analysis for Fixed Camera using location plotted by line tool
                            safetyCameraData[6].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[6], safetyCameraReport, "Fixed", "Urban"));
                            safetyCameraData[7].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[7], safetyCameraReport, "Fixed", "Rural"));


                            //Verify safety camera analysis for Mobile Camera using location plotted by line tool
                            safetyCameraData[8].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[8], safetyCameraReport, "Mobile", "Urban"));
                            safetyCameraData[9].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[9], safetyCameraReport, "Mobile", "Rural"));


                            //Verify safety camera analysis for Average Speed Camera using location plotted by line tool
                            safetyCameraData[10].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[10], safetyCameraReport, "Average Speed", "Urban"));
                            safetyCameraData[11].crashIds = ApiReusableComponents.idsList;
                            safetyCameraReport = GenerateReport.GetReportFile("VerifySecurityCameraAnalysisReport.json");
                            report.AddRange(apiReusableComponents.PerformSafetyCameraAnalysis(safetyCameraData[11], safetyCameraReport, "Average Speed", "Rural"));

                            //Delete Crash Record
                            deleteCrashReport = GenerateReport.GetReportFile("DeleteCrashUsingAPIReport.json");
                            report.AddRange(apiReusableComponents.DeleteCrashRecord(apiReusableComponents.SetDeleteCrashData(),deleteCrashReport));


                            //Add Crash Records
                            for(int i=1;i<crashData.Count-1;i++)
                            {
                                //Add Crash Record
                                report.AddRange(apiReusableComponents.AddCrashForVerfyingStickAnalysis(crashData[i]));
                            }



                            //Navigate to Query Builder Page
                            report.AddRange(queryBuilderPage.NavigateToQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select crash form fields
                            string crashField = testData["crashReferenceNumber"].ToString();
                            report.AddRange(queryBuilderPage.SelectCrashFormFields(queryBuilderData, testData, crashField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Select vehicle form fields
                            string vehicleField = testData["drivingPosition"].ToString();
                            report.AddRange(queryBuilderPage.SelectVehicleFormFields(queryBuilderData, testData, vehicleField));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Apply Query
                            report.AddRange(queryBuilderPage.ApplyQuery(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Navigate to stick analysis screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Stick Analysis"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify stick analysis screen
                            report.AddRange(stickAnalysisPage.VerifyStickAnalysisPage());
                            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Stick Analysis page"));


                            //Select Fields from stick analysis report
                            report.AddRange(stickAnalysisPage.SelectFieldsFromStickAnalysisPage(testData));
                            foreach (string screenshot in stickAnalysisPage.GetStickAnalysisPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Run stick analysis report
                            report.AddRange(stickAnalysisPage.RunStickAnalysisReport());


                            //Verify stick analysis report
                            report.AddRange(stickAnalysisPage.VerifyStickAnalysisReport(crashData.Count-2));
                            foreach (string screenshot in stickAnalysisPage.GetStickAnalysisPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Clear Query
                            report.AddRange(queryBuilderPage.ClearQueryBuilder(testData));
                            foreach (string screenshot in queryBuilderPage.GetQueryBuilderPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Delete Crash Records             
                            report.AddRange(apiReusableComponents.DeleteMultipleCrashRecord(deleteCrashReport, crashDeleteData));

                            //Add Crash Record
                            report.AddRange(apiReusableComponents.AddMultipleCrashUsingAPI(crashData[5]));


                            //Navigate to counter measure library screen
                            report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Countermeasure Library"));
                            foreach (string screenshot in homePage.GetHomePageScreenshots())
                                screenshotList.Add(screenshot);

                            //Verify counter measure screen
                            report.AddRange(counterMeasureLibraryPage.VerifyCounterMeasureLibraryPage());
                            foreach (string screenshot in counterMeasureLibraryPage.GetCounterMeasureLibraryPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Add countermeasure category
                            report.AddRange(counterMeasureLibraryPage.SaveCounterMeasureCategory(testData));
                            foreach (string screenshot in counterMeasureLibraryPage.GetCounterMeasureLibraryPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Add countermeasure details
                            report.AddRange(counterMeasureLibraryPage.SaveCounterMeasureDetails(testData));
                            foreach (string screenshot in counterMeasureLibraryPage.GetCounterMeasureLibraryPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Add possible cause
                            report.AddRange(counterMeasureLibraryPage.AddPossibleCause(testData));
                            foreach (string screenshot in counterMeasureLibraryPage.GetCounterMeasureLibraryPageScreenshots())
                                screenshotList.Add(screenshot);


                            //Link cause with countermeasure
                            report.AddRange(counterMeasureLibraryPage.AddLinkBetweenCauseAndCounterMeasure(testData));
                            foreach (string screenshot in counterMeasureLibraryPage.GetCounterMeasureLibraryPageScreenshots())
                                screenshotList.Add(screenshot);

                            //Calculate Crash Cost
                            calculateCrashCostReport = GenerateReport.GetReportFile("CalculateCrashCostReport.json");
                            report.AddRange(apiReusableComponents.CalculateCrashCost(calculateCrashCostReport, calculateCrashCostData));
                     

                            //Verify cba
                            verifyCbaReport = GenerateReport.GetReportFile("VerifyCbaReport.json");
                            report.AddRange(apiReusableComponents.VerifyCba(verifyCbaReport, cbaData));


                            //Calculate CM Cost
                            calculateCmCostReport = GenerateReport.GetReportFile("CalculateCMCostReport.json");
                            report.AddRange(apiReusableComponents.CalculateCmCost(calculateCmCostReport, calculateCmCostData));


                            //Run Cba Report
                            runCbaReport = GenerateReport.GetReportFile("RunCbaReport.json");
                            report.AddRange(apiReusableComponents.RunCba(runCbaReport, runCbaData));

                            //Delete countermeasure category
                            report.AddRange(counterMeasureLibraryPage.DeleteCounterMeasureCategory(testData));
                            foreach (string screenshot in counterMeasureLibraryPage.GetCounterMeasureLibraryPageScreenshots())
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