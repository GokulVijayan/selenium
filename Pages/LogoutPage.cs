
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{
    class LogoutPage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        private string profileBtn = "profileBtn";
        private string logoutBtn = "logoutBtn";
        private string verifyLogoutText = "verifyLogoutText";
        JObject jObject;


        public LogoutPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("LogoutPageSelector.json");
        }

        /// <summary>
        /// Logout from application
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> LogoutFromApplication(JToken inputjson)
        {
            listOfReport = ConfigFile.GetReportFile("LogoutPageReport.json");
            bool isFound;

            try
            {

                //Click profile button
                ReusableComponents.JEClick(driver, "XPath", jObject[profileBtn].ToString());

                //Set report
                listOfReport[step++].SetActualResultFail("");

                //Click Logout button
                ReusableComponents.JEClick(driver, "XPath", jObject[logoutBtn].ToString());

                //Set report
                listOfReport[step++].SetActualResultFail("");

                //Verify logout
                isFound = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[verifyLogoutText].ToString(), inputjson[verifyLogoutText].ToString());
                if (isFound)
                {
                    //Set report
                    listOfReport[step++].SetActualResultFail("");
                    Console.WriteLine("Not able to verify 'Successful logout' text");
                }
                else
                {
                    Console.WriteLine("User was not able to logout from the page");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception in logout page : " + e);
            }
            return listOfReport;
        }
    }
}
