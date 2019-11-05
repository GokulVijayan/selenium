
using System.Collections.Generic;
using FrameworkSetup.Pages;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Microsoft.Extensions.Configuration;
using FrameworkSetup.APITestScript;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;


/*
 #######################################################################
 Test Tool/Version 		:Selenium
 Test Case Automated 	:Verify that user is able to add new user and is able to login to the application using that user
 Test Objective         :To verify that user is able to add new user and is able to login to the application using that user
 Author 				:Experion
 Script Name 			:TS029_create new user & login
 Script Created on 		:28/09/2019
 #######################################################################
*/

namespace FrameworkSetup.TestScript
{

    class AddUserRoleTest
    {

        private IWebDriver driver;
        LoginPage loginPage;
        LogoutPage logoutPage;
        HomePage homePage;
        UserAccountPage userAccountPage;
        ApiReusableComponents apiReusableComponents;
        Users userData;
        UserRolePage userRolePage;
        JArray jsonArray;
        string testObjective, scriptName;
        List<string> screenshotList = new List<string>();
        List<TestReportSteps> report;
        string testDataFile;
        public AddUserRoleTest()
        {
            AddUserRole();
        }

        public void Init()
        {
            driver = ConfigFile.Init("Configuration\\AppSettings.json");
            loginPage = new LoginPage(driver);
            homePage = new HomePage(driver);
            logoutPage = new LogoutPage(driver);
            userAccountPage = new UserAccountPage(driver);
            userRolePage = new UserRolePage(driver);
            apiReusableComponents = new ApiReusableComponents();
            testObjective = "To verify that user is able to add new user and is able to login to the application using that user.";
            scriptName = "TS029_create new user & login ";
            testDataFile = "AddUserRoleTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            Constant.SetConfig("Configuration\\AppSettings.json");
            userData = RetrieveTestData.GetUserDataBody(ConfigFile.RetriveUserData(testDataFile));
        }


        public void AddUserRole()
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


                    

                    //Navigate to add user role screen
                    report.AddRange(homePage.NavigateToScreenFromHamburgerMenu("User Role"));
                    foreach (string screenshot in homePage.GetHomePageScreenshots())
                        screenshotList.Add(screenshot);

                    //Add user role
                    report.AddRange(userRolePage.AddUserRole(testData));
                    foreach (string screenshot in userRolePage.GetUserRolePageScreenshots())
                        screenshotList.Add(screenshot);

                    

                    //Add user account
                    report.AddRange(apiReusableComponents.AddUser(userData,UserRolePage.userName));


                    //Logout from application
                    report.AddRange(logoutPage.LogoutFromApplication(testData));
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Logout"));


                    //Login to application
                    IConfigurationRoot config = ConfigFile.GetAppConfig("Configuration\\AppSettings.json");
                    driver.Navigate().GoToUrl(config.GetSection("appSettings")["url"]);
                    report.AddRange(loginPage.LoginToApplication(ApiReusableComponents.userName, ApiReusableComponents.password));
                    foreach (string screenshot in loginPage.GetLoginPageScreenshots())
                        screenshotList.Add(screenshot);

                    //Verify home page
                    report.AddRange(homePage.VerifyHomePage());
                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Home page"));

                    Exit();
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
