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
   
    class UserAccountPage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public static string testUserName;
        public static string userNamePassword;
        public static bool isToastVerified = false;
        JObject jObject;
        public List<string> screenshotList = new List<string>();
        private string addUserAccount = "addUserAccount";
        private string userName = "userName";
        private string firstName = "firstName";
        private string email = "email";
        private string confirmEmail = "confirmEmail";
        private string userPassword = "userPassword";
        private string confirmPassword = "confirmPassword";
        private string userRole = "userRole";
        private string department = "department";
        private string save = "save";
        private string verifySaveUserAccount = "verifySaveUserAccount";



        public UserAccountPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("UserAccountPageSelector.json");
           
        }

        /// <summary>
        /// Add User Account
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddUserAccount(JToken inputjson,string userRoleName)
        {
            try
            {
                listOfReport = ConfigFile.GetReportFile("AddUserAccountReport.json");

                //Click on Add User Account button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addUserAccount].ToString());
                CheckForToaster();
                ReusableComponents.JEClick(driver, "XPath", jObject[addUserAccount].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                //Set report
                Console.WriteLine("User was able to click on add user account button");
                listOfReport[step++].SetActualResultFail("");

                //Enter user name
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[userName].ToString());
                testUserName = inputjson[userName].ToString() + ReusableComponents.getRandomNumber(4);
                ReusableComponents.SendKeys(driver, "XPath", jObject[userName].ToString(), testUserName);

                //Set report
                Console.WriteLine("User was able to enter user name");
                listOfReport[step++].SetActualResultFail("");


                //Enter first name
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[firstName].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[firstName].ToString(), inputjson[firstName].ToString());
                //Set report
                Console.WriteLine("User was able to enter first name");
                listOfReport[step++].SetActualResultFail("");


                ///Enter email
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[email].ToString());
                string emailId = inputjson[userName].ToString() + ReusableComponents.getRandomNumber(4) + inputjson[email].ToString();
                ReusableComponents.SendKeys(driver, "XPath", jObject[email].ToString(), emailId);
                //Set report
                Console.WriteLine("User was able to enter email");
                listOfReport[step++].SetActualResultFail("");


                ///Enter confirm email
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[confirmEmail].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[confirmEmail].ToString(), emailId);
                //Set report
                Console.WriteLine("User was able to enter email");
                listOfReport[step++].SetActualResultFail("");


                ///Enter password
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[userPassword].ToString());
                userNamePassword = inputjson[userPassword].ToString();
                ReusableComponents.SendKeys(driver, "XPath", jObject[userPassword].ToString(), inputjson[userPassword].ToString());
                //Set report
                Console.WriteLine("User was able to enter password");
                listOfReport[step++].SetActualResultFail("");


                ///Enter confirm password
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[confirmPassword].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[confirmPassword].ToString(), inputjson[confirmPassword].ToString());
                //Set report
                Console.WriteLine("User was able to enter email");
                listOfReport[step++].SetActualResultFail("");


                //Select Department
                ReusableComponents.SelectFromUserAccountDropdown(driver, "XPath", jObject[department].ToString(), inputjson[department].ToString());
                //Set report
                Console.WriteLine("User was able to select department");
                listOfReport[step++].SetActualResultFail("");

                //Select user role
                ReusableComponents.SelectFromUserAccountDropdown(driver, "XPath", jObject[userRole].ToString(), userRoleName);
                //Set report
                Console.WriteLine("User was able to select user role");
                listOfReport[step++].SetActualResultFail("");



                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Add User Account Page"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[save].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[save].ToString());

              
                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifySaveUserAccount].ToString());
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
                Console.WriteLine("Add User Account failed : " + e);
            }
            return listOfReport;
        }



        


        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserAccountPageScreenshots()
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
