using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FrameworkSetup.Pages
{
   
    class UserRolePage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public static bool isToastVerified = false;
        JObject jObject;
        public List<string> screenshotList = new List<string>();
        public static string userName=string.Empty;
        private string addUserRole = "addUserRole";
        private string userRoleName = "userRoleName";
        private string pagePriveleges = "pagePriveleges";
        private string fieldPriveleges = "fieldPriveleges";
        private string applicationPages = "applicationPages";
        private string adminPages = "adminPages";
        private string form = "form";
        private string save = "save";
        private string verifySaveUserRole = "verifySaveUserRole";

        public UserRolePage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("UserRolePageSelector.json");
           
        }

        /// <summary>
        /// Add User Role
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddUserRole(JToken inputjson)
        {
            try
            {
                listOfReport = ConfigFile.GetReportFile("AddUserRoleReport.json");

                //Click on Add User Role button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addUserRole].ToString());
                CheckForToaster();
                Thread.Sleep(Constant.waitTimeout);
                ReusableComponents.JEClick(driver, "XPath", jObject[addUserRole].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Set report
                Console.WriteLine("User was able to click on add user role button");
                listOfReport[step++].SetActualResultFail("");

                //Enter user role name
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[userRoleName].ToString());
                userName = inputjson[userRoleName].ToString() + ReusableComponents.getRandomNumber(4);
                ReusableComponents.SendKeys(driver, "XPath", jObject[userRoleName].ToString(), userName);

                //Set report
                Console.WriteLine("User was able to enter user role name");
                listOfReport[step++].SetActualResultFail("");


                //Click page privilege tab
                ReusableComponents.Click(driver, "XPath", jObject[pagePriveleges].ToString());

                //Set report
                Console.WriteLine("User was able to click page privileges");
                listOfReport[step++].SetActualResultFail("");


                //Click application pages
                ReusableComponents.Click(driver, "XPath", jObject[applicationPages].ToString());

                //Set report
                Console.WriteLine("User was able to click application pages");
                listOfReport[step++].SetActualResultFail("");


                //Click admin pages
                ReusableComponents.Click(driver, "XPath", jObject[adminPages].ToString());

                //Set report
                Console.WriteLine("User was able to click application pages");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Admin Pages"));


                //Click field privilege tab
                ReusableComponents.Click(driver, "XPath", jObject[fieldPriveleges].ToString());

                //Set report
                Console.WriteLine("User was able to click field privileges");
                listOfReport[step++].SetActualResultFail("");


                //Click form fields
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[form].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[form].ToString());

                //Set report
                Console.WriteLine("User was able to click form fields");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Form Fields"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[save].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[save].ToString());

              
                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifySaveUserRole].ToString());
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            }
            catch(Exception e)
            {
                Console.WriteLine("Add User Role failed : " + e);
            }
            return listOfReport;
        }



        


        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserRolePageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }

        /// <summary>
        /// Check For Toaster
        /// </summary>
        private void CheckForToaster()
        {
            if (ReusableComponents.FindIfElementExists(driver, "XPath", Constant.toastSelector))
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            }
        }
    }
}
