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
    class HomePage
    {
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public List<string> screenshotList = new List<string>();

        private string profileIcon = "profileIcon";
        private string hamburgerMenu = "hamburgerMenu";
        private string mapView = "mapView";
        private string plusBtn = "plusBtn";
        private string continueBtn = "continueBtn";
        private string verifyARF = "verifyARF";

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("HomePageSelector.json");
        }

        /// <summary>
        /// Verify web application home page displayed
        /// </summary>
        public List<TestReportSteps> VerifyHomePage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyHomePageReport.json");
            try
            {
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[profileIcon].ToString());
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify home page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Navigate to screen from hamburger menu
        /// </summary>
        /// <param name="menuItem"></param>
        /// <returns></returns>
        public List<TestReportSteps> NavigateToScreenFromHamburgerMenu(string menuItem)
        {
            bool isFound = true;
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("NavigateToScreenFromHamburgerMenuReport.json");

            //Click hamburger menu
            ReusableComponents.JEClick(driver, "Id", jObject[hamburgerMenu].ToString());
            listOfReport[step].SetTestObjective(listOfReport[step].GetTestObjective() + menuItem);
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Menu"));

            //Click selected menu option
            string mainListSelector = "//ul[@class='trl-c-menu__content--menu-items']/li";
            int count = ReusableComponents.ElementsCount(driver, "XPath", mainListSelector);
            for (int i = 1; i <= count; i++)
            {
                string innerList = mainListSelector + "[" + i + "]/ul/li";
                int innerListCount = driver.FindElements(By.XPath(innerList)).Count;
                for (int j = 1; j <= innerListCount; j++)
                {
                    //Retrieve and Compare text
                    string innerSelector = innerList + "[" + j + "]/a/label";
                    isFound = ReusableComponents.RetrieveAndCompareText(driver, "XPath", innerSelector, menuItem);

                    if (isFound)
                    {
                        ReusableComponents.Click(driver, "XPath", innerSelector);
                        listOfReport[step].SetStepDescription(listOfReport[step].GetStepDescription() + menuItem);
                        listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + menuItem);
                        listOfReport[step].SetActualResultPass(listOfReport[step].GetActualResultPass() + menuItem);
                        listOfReport[step++].SetActualResultFail("");
                        break;
                    }
                }

                if (isFound)
                {
                    Console.WriteLine("Navigated to screen" + menuItem);
                    break;
                }
            }
            return listOfReport;
        }

        /// <summary>
        /// Navigate to ARF screen
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> NavigateToARF()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("NavigateToARFReport.json");

            //Wait for page
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[mapView].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);


            driver.Navigate().Refresh();
            //Wait for page
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[mapView].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);


            //Click '+' button
            ReusableComponents.JEClick(driver, "XPath", jObject[plusBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[continueBtn].ToString());

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Starting declaration statement"));

            //Click 'Continue' button
            ReusableComponents.Click(driver, "XPath", jObject[continueBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            
            //Verify ARF
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyARF].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ARF"));

            return listOfReport;
        }

        /// <summary>
        /// Retrieves home page screenshots
        /// </summary>
        /// <returns></returns>
        public List<string> GetHomePageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
