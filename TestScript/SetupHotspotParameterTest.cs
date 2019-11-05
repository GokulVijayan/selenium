using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using FrameworkSetup.Pages;
using iMAAPTestAPI;
using iMAAPTestAPI.CrashRecords;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Collections.Generic;

/*
#######################################################################
Test Tool/Version 		    :Selenium
Test Case Automated 		:Verifu Hotspot parameter setup
Test Objective              :To verify that user is able to setup hotspot parameters
Author 				        :Experion
Script Name 			    :TS026_Web_Verify Hotspot report
Script Created on 		    :25/09/2019
#######################################################################
*/

namespace FrameworkSetup.TestScript
{
    class SetupHotspotParameterTest
    {
        private IWebDriver driver;
        LoginPage loginPage;
        HomePage homePage;
        HotspotParameterPage hotspotParameterPage;
        LogoutPage logoutPage;
        ApiReusableComponents apiReusableComponents;
        HotspotIdentification identifyHotspot;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report, deleteCrashReport;
        int testCheck = 0;
        string testDataFile, CrashDataFile;
        List<CrashRootObject> APIDataFile;
        static string crashDeleteData;
        string HotspotDataFile1, HotspotDataFile2, HotspotDataFile3, HotspotDataFile4, HotspotDataFile5, KsiDataFile,
            SaveBlackspotDataFile, HotspotTypeDataFile, PotentialHotspotDataFile, HotspotReportDataFile;

        public SetupHotspotParameterTest()
        {
            SetupHotspotParameter();
        }


        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            apiReusableComponents = new ApiReusableComponents();
            identifyHotspot = new HotspotIdentification();
            hotspotParameterPage = new HotspotParameterPage(driver);
            logoutPage = new LogoutPage(driver);
            testObjective = "To verify that iMAAP web portal - Hotspot Parameter Setup functionality is working as expected";
            scriptName = "TS026_Web_Verify Hotspot report";
            testDataFile = "SetupHotspotParameterTest.json";
            CrashDataFile = ConfigFile.RetrieveAPITestData(testDataFile, "APICrashTestData");
            APIDataFile = RetrieveTestData.GetMultipleCrashDataBody(CrashDataFile);
            HotspotDataFile1 = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotRule1");
            HotspotDataFile2 = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotRule2");
            HotspotDataFile3 = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotRule3");
            HotspotDataFile4 = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotRule4");
            HotspotDataFile5 = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotRule5");
            KsiDataFile = ConfigFile.RetrieveAPITestData(testDataFile, "KsiDefinition");
            HotspotTypeDataFile = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotType");
            SaveBlackspotDataFile = ConfigFile.RetrieveAPITestData(testDataFile, "SaveHotspot");
            PotentialHotspotDataFile = ConfigFile.RetrieveAPITestData(testDataFile, "PotentialHotspot");
            HotspotReportDataFile = ConfigFile.RetrieveAPITestData(testDataFile, "HotspotReport");
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            crashDeleteData = ConfigFile.RetrieveDeleteCrashTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
        }


        public void SetupHotspotParameter()
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

                        report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Hotspot Parameter"));
                        foreach (string screenshot in homePage.GetHomePageScreenshots())
                            screenshotList.Add(screenshot);

                        //Verify access controls displayed
                        report.AddRange(hotspotParameterPage.VerifyAccessControls(testData));
                        foreach (string screenshot in hotspotParameterPage.GetHotspotParameterScreenshots())
                            screenshotList.Add(screenshot);

                        
                        //Save hotspot parameters
                        report.AddRange(hotspotParameterPage.SaveHotspotParameters(testData));
                        foreach (string screenshot in hotspotParameterPage.GetHotspotParameterScreenshots())
                            screenshotList.Add(screenshot);
                        

                        //Navigate to Hotspot Identification
                         report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("Hotspot Identification"));
                         foreach (string screenshot in homePage.GetHomePageScreenshots())
                             screenshotList.Add(screenshot);

                         
                         //Verify hotspot parameters
                        report.AddRange(hotspotParameterPage.VerifyHotspotParametersDisplayed(testData));
                         foreach (string screenshot in hotspotParameterPage.GetHotspotParameterScreenshots())
                             screenshotList.Add(screenshot);
                         

                        //Add crash using API
                        report.AddRange(apiReusableComponents.AddMultipleCrashForHotspot(APIDataFile));

                        //Identify hotspot using rule 1
                        report.AddRange(identifyHotspot.HotspotAnalysis(HotspotDataFile1, "IdentifyHotspotRule1Report"));

                        //Configure Ksi Definition
                        report.AddRange(identifyHotspot.ConfigureKsiDefinition(KsiDataFile));

                        //Identify hotspot using rule 2
                        report.AddRange(identifyHotspot.HotspotAnalysis(HotspotDataFile2, "IdentifyHotspotRule2Report"));

                        //Identify hotspot using rule 3
                        report.AddRange(identifyHotspot.HotspotAnalysis(HotspotDataFile3, "IdentifyHotspotRule3Report"));

                        //Identify hotspot using rule 4
                        report.AddRange(identifyHotspot.HotspotAnalysis(HotspotDataFile4, "IdentifyHotspotRule4Report"));

                        //Identify hotspot using rule 5
                        report.AddRange(identifyHotspot.HotspotAnalysis(HotspotDataFile5, "IdentifyHotspotRule5Report"));

                        //Add hotspot type and save hostpot
                        report.AddRange(identifyHotspot.AddHotspotType(HotspotTypeDataFile, SaveBlackspotDataFile, PotentialHotspotDataFile));

                        //Verify hotspot report
                        report.AddRange(identifyHotspot.HostspotReport(HotspotReportDataFile));


                        //Delete Crash Records  
                        deleteCrashReport = GenerateReport.GetReportFile("DeleteCrashUsingAPIReport.json");
                        report.AddRange(apiReusableComponents.DeleteMultipleCrashRecord(deleteCrashReport, crashDeleteData));

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