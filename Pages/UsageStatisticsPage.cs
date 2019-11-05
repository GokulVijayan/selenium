using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FrameworkSetup.Pages
{
    internal class UsageStatisticsPage
    {
        private IWebDriver driver;
        private int step = 0;
        private List<TestReportSteps> listOfReport;
        private JObject jObject;
        public List<string> screenshotList = new List<string>();
        private string testDataFile;
        private JArray jsonArray;


        private string runBtn = "runBtn";
        private string rowField = "rowField";
        private string columnField = "columnField";
        private string userText = "userText";
        private string loginStatusText = "loginStatusText";
        private string tableRowCount = "tableRowCount";
        private string rowSelector = "rowSelector";
        private string colSelector = "colSelector";
        private string rowStart = "rowStart";
        private string total = "total";
        private string columnCount = "columnCount";


        public UsageStatisticsPage(IWebDriver driver)
        {
            this.driver = driver;
            testDataFile = "UsageStatisticsTest.json";
            jsonArray = ConfigFile.RetrieveInputTestData(testDataFile);
            jObject = ConfigFile.RetrieveUIMap("UsageStatisticsSelector.json");
        }

        /// <summary>
        /// Runs the analysis.
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> RunAnalysis()
        {
            try
            {
                screenshotList.Clear();
                step = 0;
                listOfReport = ConfigFile.GetReportFile("UsageStatisticsRunAnalysisReport.json");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[userText].ToString(), jObject[rowField].ToString());
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.DragAndDrop(driver, "XPath", jObject[loginStatusText].ToString(), jObject[columnField].ToString());
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.JEClick(driver, "XPath", jObject[runBtn].ToString());
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "LoginPage"));
                listOfReport[step++].SetActualResultFail("");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetUsageStatisticsScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }

        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public int GetFirstRow(JToken inputJson)
        {
            step = 0;
            int firstRowNumber = 0;
            screenshotList.Clear();
            try
            {
                int count = ReusableComponents.GetRowsInTable(driver, jObject[tableRowCount].ToString());
                for (int i = 1; i <= count; i++)
                {
                    string cellSelector = jObject[rowSelector].ToString() + "[" + i + "]" + jObject[colSelector].ToString();
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string textInCell = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string b = inputJson[rowStart].ToString();
                    if (textInCell.Contains(b))
                    {
                        firstRowNumber = i;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify usage statistics report page.Exception caught : " + e);
            }
            return firstRowNumber;
        }

        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public int GetTableRow(JToken inputJson)
        {
            step = 0;
            int rowOfTotal = 0;
            screenshotList.Clear();
            try
            {
                int count = ReusableComponents.GetRowsInTable(driver, jObject[tableRowCount].ToString());
                for (int i = 1; i <= count; i++)
                {
                    string c = jObject[rowSelector].ToString() + "[" + i + "]" + jObject[colSelector].ToString();
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", c);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", c);
                    string b = inputJson[total].ToString();
                    if (a.Contains(b))
                    {
                        rowOfTotal = i;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify usage statistics report page.Exception caught : " + e);
            }
            return rowOfTotal;
        }

        /// <summary>
        /// Get no. of rows in table
        /// </summary>
        /// <returns></returns>
        public int GetTableCol(JToken inputJson, int rowOfCrashDateMonth)
        {
            step = 0;
            int colOfTotal = 0;
            screenshotList.Clear();
            try
            {
                string colCount = inputJson[columnCount].ToString();
                //Convert.ToInt32(colCount)
                for (int j = 1; j <= Convert.ToInt32(colCount); j++)
                {
                    string d = jObject[rowSelector].ToString() + "[" + rowOfCrashDateMonth + "]" + "//td" + "[" + j + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", d);
                    string e = ReusableComponents.RetrieveText(driver, "XPath", d);
                    string f = inputJson[total].ToString();
                    if (e.Contains(f))
                    {
                        colOfTotal = j;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify usage statistics report page.Exception caught : " + e);
            }
            return colOfTotal;
        }

        /// <summary>
        /// read values in table
        /// </summary>
        /// <returns></returns>
        public string[,] ReadTable(int rowOfTotal, int colOfTotal, int rowOfCrashDateMonth)
        {
            step = 0;
            int j = 0, i = 0;
            int row = 0, col = 0;
            screenshotList.Clear();
            string cellSelector = "";
            string[,] array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
            try
            {
                for (row = rowOfCrashDateMonth; row <= rowOfTotal; row++)
                {
                    for (col = 1; col <= colOfTotal; col++)
                    {
                        cellSelector = jObject[rowSelector].ToString() + "[" + row + "]" + "//td" + "[" + col + "]";
                        ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                        array[i, j] = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                        j++;
                    }
                    i++;
                    j = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify usage statistics report page.Exception caught : " + e);
            }
            return array;
        }
    }
}