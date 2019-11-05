using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FrameworkSetup.Pages
{
    class LoginHistoryPage
    {
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        public List<string> loginTimes = new List<string>();
        readonly JObject jObject;

        private string userSelector = "userSelector";
        private string searchUserName = "searchUserName";
        private string calenderButton = "calenderButton";
        private string calenderCurrentDate = "calenderCurrentDate";
        private string calenderEndDateButton = "calenderEndDateButton";
        private string calenderOkButton = "calenderOkButton";
        private string searchButton = "searchButton";
        private string searchSelectAll = "searchSelectAll";
        private string resetButton = "resetButton";
        private string nextPageButton = "nextPageButton";
        private string firstPageButton = "firstPageButton";
        private string lastPageButton = "lastPageButton";
        private string dateRangeSelectorTextField = "dateRangeSelectorTextField";
        private string grid = "grid";
        private string pageCount = "pageCount";
        private string searchResultUserName = "searchResultUserName";
        private string gridSelector = "gridSelector";

        public LoginHistoryPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("LoginHistoryPageSelector.json");
        }

        public (List<TestReportSteps>, List<string>) VerifyLoginHistory(JToken inputjson)
        {

            screenshotList.Clear();
            step = 0;

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            listOfReport = ConfigFile.GetReportFile("VerifyLoginHistory.json");

            // View login history.
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Login History"));
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);


            // Search by logged in  user name.
            Thread.Sleep(Constant.waitTimeoutForExport);
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[userSelector].ToString());

            ReusableComponents.MultiSelect(driver, "XPath", jObject[userSelector].ToString(), inputjson[searchSelectAll].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[userSelector].ToString());
            ReusableComponents.MultiSelect(driver, "XPath", jObject[userSelector].ToString(), inputjson[searchUserName].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Select a user name in search box"));

            // Select current date in date selector.
            ReusableComponents.JEClick(driver, "XPath", jObject[calenderButton].ToString());
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[calenderCurrentDate].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[calenderCurrentDate].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[calenderEndDateButton].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[calenderCurrentDate].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[calenderOkButton].ToString());
            
            listOfReport[step++].SetActualResultFail("");
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Select current date in date selector"));

            // Click search button.
            ReusableComponents.JEClick(driver, "XPath", jObject[searchButton].ToString());


            // Collect login times without logout of current user in current hour.
            CollectLogins(inputjson[searchResultUserName].ToString());

            // Verify search result.
            if (VerifySearchResult(inputjson[searchResultUserName].ToString()))
            {
                listOfReport[step++].SetActualResultFail("");
            }
            else
            {
                step++;
            }

            // Click reset button.
            ReusableComponents.JEClick(driver, "XPath", jObject[resetButton].ToString());

            // Verify result.
            var filteredDateRange = driver.FindElement(By.XPath(jObject[dateRangeSelectorTextField].ToString())).Text;
            if (string.IsNullOrEmpty(filteredDateRange))
            {
                listOfReport[step++].SetActualResultFail("");
            }
            else
            {
                step++;
            }
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Login History Filter Reset"));

            return (listOfReport, loginTimes);
        }

        private void CollectLogins(string user)
        {
            var totalPages = driver.FindElement(By.XPath(jObject[pageCount].ToString()));
            var pages = Convert.ToInt32(totalPages.Text.Split('/')[1]);
            int rowCount;
            for (int i = 0; i < pages && (loginTimes == null || !loginTimes.Any()); i++)
            {
                rowCount = driver.FindElements(By.XPath(jObject[grid].ToString())).Count;
                for (int j = 2; j <= rowCount; j++)
                {
                    var userNameCell = jObject[gridSelector].ToString() + j + "]/div[1]";
                    var loginCell = jObject[gridSelector].ToString() + j + "]/div[3]";
                    var logOutCell = jObject[gridSelector].ToString() + j + "]/div[4]";

                    var userName = driver.FindElement(By.XPath(userNameCell)).Text;
                    var login = driver.FindElement(By.XPath(loginCell)).Text.Split(':')[0];
                    var logout = driver.FindElement(By.XPath(logOutCell)).Text;
                    if (!string.IsNullOrEmpty(login) && string.IsNullOrEmpty(logout) && userName.Equals(user))
                    {
                        loginTimes.Add(login);
                    }
                }
                // Navigate to next page.
                ReusableComponents.JEClick(driver, "XPath", jObject[nextPageButton].ToString());
            }
        }

        public List<TestReportSteps> VerifyPreviousLoginInHistory(JToken testData, List<string> loginDateTimes)
        {
            screenshotList.Clear();
            var stepCount = 0;
            var listReport = ConfigFile.GetReportFile("VerifyLoginHistoryData.json");
            var isPreviousLoginFound = false;

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[pageCount].ToString());
            var totalPages = driver.FindElement(By.XPath(jObject[pageCount].ToString()));
            var pages = Convert.ToInt32(totalPages.Text.Split('/')[1]);

            isPreviousLoginFound = CheckPreviousLogOut(loginDateTimes, pages, testData[searchResultUserName].ToString());


            // View login history.
            listReport[stepCount++].SetActualResultFail("");

            // Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Previous logout"));
            return listReport;
        }

        private bool CheckPreviousLogOut(List<string> loginDateTimes, int pages, string user)
        {
            if(loginDateTimes == null || !loginDateTimes.Any())
            {
                return false;
            }
            int rowCount;
            for (int i = 0; i < pages; i++)
            {
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[grid].ToString());
                rowCount = driver.FindElements(By.XPath(jObject[grid].ToString())).Count;
                for (int j = 2; j <= rowCount; j++)
                {
                    var userNameCell = jObject[gridSelector].ToString() + j + "]/div[1]";
                    var loginCell = jObject[gridSelector].ToString() + j + "]/div[3]";
                    var logOutCell = jObject[gridSelector].ToString() + j + "]/div[4]";

                    var userName = driver.FindElement(By.XPath(userNameCell)).Text;
                    var login = driver.FindElement(By.XPath(loginCell)).Text.Split(':')[0];
                    var logout = driver.FindElement(By.XPath(logOutCell)).Text;

                    if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(logout) && userName.Equals(user)
                        && loginDateTimes.Contains(login))
                    {
                        return true;
                    }
                }
                // Navigate to next page.
                ReusableComponents.JEClick(driver, "XPath", jObject[nextPageButton].ToString());
            }

            return false;
        }

        private bool VerifySearchResult(string user)
        {
            var rowCount = driver.FindElements(By.XPath(jObject[grid].ToString())).Count;

            var currentDate = DateTime.Now.ToString("dd-MMM-yyyy");
            List<string> userNames = new List<string>();
            List<string> loginDates = new List<string>();
            List<string> logoutDates = new List<string>();

            // Get user name and login and logout dates from first page.
            GetCurrentPageUserNameAndLoginDates(rowCount, userNames, loginDates, logoutDates);

            // Navigate to last page.
            ReusableComponents.JEClick(driver, "XPath", jObject[lastPageButton].ToString());

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[grid].ToString());
            Thread.Sleep(1000);
            rowCount = driver.FindElements(By.XPath(jObject[grid].ToString())).Count;

            // Get user name and login and logout dates from last page.
            GetCurrentPageUserNameAndLoginDates(rowCount, userNames, loginDates, logoutDates);

            // Navigate to first page.
            ReusableComponents.JEClick(driver, "XPath", jObject[firstPageButton].ToString());

            if (userNames != null && userNames.Any() && userNames.Any(x => !x.Equals(user))
               ||
               loginDates != null && loginDates.Any() && loginDates.Any(x => !x.Equals(currentDate))
               ||
               logoutDates != null && logoutDates.Any() && logoutDates.Any(y => !y.Equals(currentDate))
              )
                return false;
            else
                return true;
        }

        private void GetCurrentPageUserNameAndLoginDates(int rowCount, List<string> userNames, List<string> loginDates, List<string> logoutDates)
        {
            for (int j = 2; j <= rowCount; j++)
            {
                var userNameCell = jObject[gridSelector].ToString() + j + "]/div[1]";
                var loginCell = jObject[gridSelector].ToString() + j + "]/div[3]";
                var logOutCell = jObject[gridSelector].ToString() + j + "]/div[4]";

                var userName = driver.FindElement(By.XPath(userNameCell)).Text;
                var login = driver.FindElement(By.XPath(loginCell)).Text.Split(' ')[0];
                var logoutDate = driver.FindElement(By.XPath(logOutCell)).Text;

                string logout = string.Empty;
                if (!string.IsNullOrEmpty(logoutDate))
                {
                    logout = driver.FindElement(By.XPath(logOutCell)).Text.Split(' ')[0];
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    userNames.Add(userName);
                }
                if (!string.IsNullOrEmpty(login))
                {
                    loginDates.Add(login);
                }
                if (!string.IsNullOrEmpty(logout))
                {
                    logoutDates.Add(logout);
                }

            }
        }

        /// <summary>
        /// Returns login history page screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetLoginHistoryScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
