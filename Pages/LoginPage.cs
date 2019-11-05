
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{
   
    class LoginPage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        JObject jObject;
        public List<string> screenshotList = new List<string>();

        private string username = "username";
        private string password = "password";
        private string loginButton = "loginButton";

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("LoginPageSelector.json");
           
        }

        /// <summary>
        /// Login to iMAAP web application
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> LoginToApplication(JToken inputjson)
        {
            try
            {
                listOfReport = ConfigFile.GetReportFile("LoginPageReport.json");

                //Enter username
                ReusableComponents.SendKeys(driver, "Id", jObject[username].ToString(), inputjson[username].ToString());

                //Set report
                Console.WriteLine("User was able to enter username");
                listOfReport[step++].SetActualResultFail("");

                //Enter password
                ReusableComponents.SendKeys(driver, "Id", jObject[password].ToString(), inputjson[password].ToString());

                //Set report
                Console.WriteLine("User was able to enter password");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "LoginPage"));

                //Click login button
                ReusableComponents.Click(driver, "Id", jObject[loginButton].ToString());

                //Set report
                Console.WriteLine("User was able to click login button");
                listOfReport[step++].SetActualResultFail("");
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetLoginPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
