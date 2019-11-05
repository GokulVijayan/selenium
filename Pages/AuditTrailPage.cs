using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{

    class AuditTrailPage
    {
        IWebDriver driver;
        int step = 0, rowOfCasualtyId, rowOfCrashId, rowOfVehicleId, colOfUserName, colOfOperation, colOfEntity, colOfNewValue, colOfProperty, colOfOldValue, rowOfProperty;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public List<string> screenshotList = new List<string>();
        string selector = string.Empty;
        private string verifyAuditTrailTitle = "verifyAuditTrailTitle";
        private string selectUsersDropDown = "selectUsersDropDown";
        private string selectAllCheckBox = "//div[@class='wj-header wj-select-all wj-listbox-item']//label//input";
        private string usersList = "usersList";
        private string addCheckBox = "addCheckBox";
        private string editCheckBox = "editCheckBox";
        private string deleteCheckBox = "deleteCheckBox";
        private string customDateCheckBox = "customDateCheckBox";
        private string last3DaysCheckBox = "last3DaysCheckBox";
        private string searchButton = "searchButton";
        private string cellLocator = "cellLocator";
        private string tableSelector = "tableSelector";
        private string rowCountSelector = "rowCountSelector";
        private string colCountSelector = "colCountSelector";
        private string colCountForTableheadSelector = "colCountForTableheadSelector";
        private string username = "username";
        private string addOperation = "addOperation";
        private string crashEntity = "crashEntity";
        private string vehicleEntity = "vehicleEntity";
        private string casualtyEntity = "casualtyEntity";
        private string updateOperation = "updateOperation";
        private string deleteOperation = "deleteOperation";

        public AuditTrailPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("AuditTrailPageSelector.json");
        }

        /// <summary>
        /// Login to iMAAP web application
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SelectOptions(JToken inputJson)
        {
            bool isFound = false;
            string j = string.Empty;
            string b = string.Empty;
            int i = 0;
            try
            {

                //Click Drop down button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[selectUsersDropDown].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[selectUsersDropDown].ToString());

                //Deselect the Select All check box
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", selectAllCheckBox);
                ReusableComponents.Click(driver, "XPath", selectAllCheckBox);

                int count = ReusableComponents.GetRowsInTable(driver, jObject[usersList].ToString());
                
                for (i = 1; i <= count; i++)
                {
                    selector = jObject[usersList].ToString() + "[" + i + "]";
                    ReusableComponents.ScrollToElement(driver, "XPath", selector);
                    j = ReusableComponents.RetrieveText(driver, "XPath", selector);
                    b = inputJson[username].ToString();
                    if(j.Equals(b))
                    {
                        isFound = true;
                        break;
                    }
                }
                if (isFound == true)
                {
                    string selector1 = selector + "/label/input";
                    ReusableComponents.Click(driver, "XPath", selector1);
                }

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[selectUsersDropDown].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[selectUsersDropDown].ToString());

                //Select Add check box
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[addCheckBox].ToString());

                //Deselect custom date check box
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[customDateCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[customDateCheckBox].ToString());

                //Select last 3 days check box
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[last3DaysCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[last3DaysCheckBox].ToString());

                //Click search button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[searchButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[searchButton].ToString());

                //switch to detailed view
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", "//span[@class='trl-c-switch__text--on']");
                ReusableComponents.Click(driver, "XPath", "//span[@class='trl-c-switch__text--on']");

            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Select delete checkbox
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SelectDeletecheckBox(JToken inputJson)
        {
            string j = string.Empty;
            string b = string.Empty;
            try
            {

                //DeSelect edit check box
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[editCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[editCheckBox].ToString());

                //Select delete checkbox
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[deleteCheckBox].ToString());

                //Click search button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[searchButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[searchButton].ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Select edit checkbox
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SelectEditcheckBox(JToken inputJson)
        {
            string j = string.Empty;
            string b = string.Empty;
            try
            {

                //DeSelect Add check box
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[addCheckBox].ToString());

                //Select delete checkbox
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[editCheckBox].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[editCheckBox].ToString());

                //Click search button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[searchButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[searchButton].ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// read values in table
        /// </summary>
        /// <returns></returns>
        public string[,] ReadTable()
        {
            step = 0;
            int j = 0, i = 0;
            int rowCount = 0, colCount = 0;
            screenshotList.Clear();
            string cellSelector = "";
            //string[,] array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[tableSelector].ToString());
            rowCount = ReusableComponents.GetRowsInTable(driver, jObject[rowCountSelector].ToString());
            colCount = ReusableComponents.GetRowsInTable(driver, jObject[colCountSelector].ToString());
            string[,] array = new string[rowCount, colCount];
            try
            {
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + col + "]";
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
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return array;
        }


        /// <summary>
        /// Verify audit trail for addition of crash
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyAuditTrailForAddCrash(string[,] array, JToken inputjson)
        {
            step = 0;
            int rowCount = 0, colCount = 0,colCountForTablehead;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyAuditTrailForAddCrashReport.json");
           
            string cellSelector = "";
            //string[,] array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
            rowCount = ReusableComponents.GetRowsInTable(driver, jObject[rowCountSelector].ToString());
            colCount = ReusableComponents.GetRowsInTable(driver, jObject[colCountSelector].ToString());
            colCountForTablehead = ReusableComponents.GetRowsInTable(driver, jObject[colCountForTableheadSelector].ToString());
            try
            {
                for (int col = 1; col <= colCountForTablehead; col++)
                {

                    cellSelector = "//table[@class='table table-striped table-bordered table-sm']//thead/tr//th[" + col + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string b = "User Name";
                    string c = "Operation";
                    string d = "Entity";
                    string e = "New Value";
                    if (a.Contains(b))
                    {
                        colOfUserName = col - 1;
                    }
                    else if(a.Contains(c))
                    {
                        colOfOperation = col - 1;
                    }
                    else if (a.Contains(d))
                    {
                        colOfEntity = col - 1;
                    }
                    else if (a.Contains(e))
                    {
                        colOfNewValue = col - 1;
                    }

                }

                for (int row = 1; row <= rowCount; row++)
                {
                    
                        cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 7 + "]";
                        ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                        string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                        string crashId = (ApiReusableComponents.crashId).ToString();
                        if (a.Contains(crashId))
                        {
                            rowOfCrashId = row - 1;
                            break;
                        }
                }
                for (int row = 1; row <= rowCount; row++)
                {

                    cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 7 + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string vehicleId = (ApiReusableComponents.vehicleId).ToString();
                    if (a.Contains(vehicleId))
                    {
                        rowOfVehicleId = row - 1;
                        break;
                    }
                }
                for (int row = 1; row <= rowCount; row++)
                {

                    cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 7 + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string casualtyId = (ApiReusableComponents.casualtyId).ToString();
                    if (a.Contains(casualtyId))
                    {
                        rowOfCasualtyId = row - 1;
                        break;
                    }
                }

                string userNameInAddCasualty = array[rowOfCasualtyId, colOfUserName];
                string operationInAddCasualty = array[rowOfCasualtyId, colOfOperation];
                string entityInAddCasualty = array[rowOfCasualtyId, colOfEntity];

                string userNameInAddVehicle = array[rowOfVehicleId, colOfUserName];
                string operationInAddVehicle = array[rowOfVehicleId, colOfOperation];
                string entityInAddVehicle = array[rowOfVehicleId, colOfEntity];

                string userNameInAddCrash = array[rowOfCrashId, colOfUserName];
                string operationInAddCrash = array[rowOfCrashId, colOfOperation];
                string entityInAddCrash = array[rowOfCrashId, colOfEntity];

                if ((userNameInAddCrash) == inputjson[username].ToString() && (operationInAddCrash == inputjson[addOperation].ToString()) && (entityInAddCrash == inputjson[crashEntity].ToString()))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }

               
                if ((userNameInAddVehicle) == inputjson[username].ToString() && (operationInAddVehicle == inputjson[addOperation].ToString()) && (entityInAddVehicle == inputjson[vehicleEntity].ToString()))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }

                if ((userNameInAddCasualty) == inputjson[username].ToString() && (operationInAddCasualty == inputjson[addOperation].ToString()) && (entityInAddCasualty == inputjson[casualtyEntity].ToString()))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Audit Trail for add crash"));


            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;

        }

        /// <summary>
        /// Verify audit trail for updation of crash details
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyAuditTrailForUpdateCrash(string[,] array, JToken inputjson)
        {
            step = 0;
            int rowCount = 0, colCount = 0, colCountForTablehead;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyAuditTrailForUpdateCrashReport.json");
            string cellSelector = "";
            //string[,] array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
            rowCount = ReusableComponents.GetRowsInTable(driver, jObject[rowCountSelector].ToString());
            colCount = ReusableComponents.GetRowsInTable(driver, jObject[colCountSelector].ToString());
            colCountForTablehead = ReusableComponents.GetRowsInTable(driver, jObject[colCountForTableheadSelector].ToString());
            try
            {
                for (int col = 1; col <= colCountForTablehead; col++)
                {

                    cellSelector = "//table[@class='table table-striped table-bordered table-sm']//thead/tr//th[" + col + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string b = "User Name";
                    string c = "Operation";
                    string d = "Entity";
                    string e = "New Value";
                    string f = "Property";
                    string g = "Old Value";
                    if (a.Contains(b))
                    {
                        colOfUserName = col - 1;
                    }
                    else if (a.Contains(c))
                    {
                        colOfOperation = col - 1;
                    }
                    else if (a.Contains(d))
                    {
                        colOfEntity = col - 1;
                    }
                    else if (a.Contains(e))
                    {
                        colOfNewValue = col - 1;
                    }
                    else if (a.Contains(f))
                    {
                        colOfProperty = col - 1;
                    }
                    else if (a.Contains(g))
                    {
                        colOfOldValue = col - 1;
                    }

                }

                for (int row = 1; row <= rowCount; row++)
                {

                    cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 5 + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string crashId = (ApiReusableComponents.crashId).ToString();
                    if (a.Contains("Casualty Reference Number"))
                    {
                        rowOfProperty= row - 1;
                        break;
                    }
                }
               
                string userNameInUpdateCrash = array[rowOfProperty, colOfUserName];
                string operationInUpdateCrash = array[rowOfProperty, colOfOperation];
                string entityInUpdateCrash = array[rowOfProperty, colOfEntity];
                string oldValueInUpdateCrash = array[rowOfProperty, colOfOldValue];
                string NewValueInUpdateCrash = array[rowOfProperty, colOfNewValue];

               

                if ((userNameInUpdateCrash) == inputjson[username].ToString() && (operationInUpdateCrash == inputjson[updateOperation].ToString()) && (entityInUpdateCrash == inputjson[casualtyEntity].ToString()) && (oldValueInUpdateCrash == "1") && (NewValueInUpdateCrash == "2"))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Audit Trail for modified crash"));

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;

        }

        /// <summary>
        /// Verify audit trail for Delete crash
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyAuditTrailForDeleteCrash(string[,] array, JToken inputjson)
        {
            step = 0;
            int rowCount = 0, colCount = 0, colCountForTablehead;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyAuditTrailForDeleteCrashReport.json");
            string cellSelector = "";
            //string[,] array = new string[(rowOfTotal - (rowOfCrashDateMonth - 1)), colOfTotal];
            rowCount = ReusableComponents.GetRowsInTable(driver, jObject[rowCountSelector].ToString());
            colCount = ReusableComponents.GetRowsInTable(driver, jObject[colCountSelector].ToString());
            colCountForTablehead = ReusableComponents.GetRowsInTable(driver, jObject[colCountForTableheadSelector].ToString());
            try
            {
                for (int col = 1; col <= colCountForTablehead; col++)
                {

                    cellSelector = "//table[@class='table table-striped table-bordered table-sm']//thead/tr//th[" + col + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string b = "User Name";
                    string c = "Operation";
                    string d = "Entity";
                    string e = "New Value";
                    if (a.Contains(b))
                    {
                        colOfUserName = col - 1;
                    }
                    else if (a.Contains(c))
                    {
                        colOfOperation = col - 1;
                    }
                    else if (a.Contains(d))
                    {
                        colOfEntity = col - 1;
                    }
                    else if (a.Contains(e))
                    {
                        colOfOldValue = col - 1;
                    }

                }

                for (int row = 1; row <= rowCount; row++)
                {

                    cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 6 + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string crashId = (ApiReusableComponents.crashId).ToString();
                    if (a.Contains(crashId))
                    {
                        rowOfCrashId = row - 1;
                        break;
                    }
                }
                for (int row = 1; row <= rowCount; row++)
                {

                    cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 6 + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string vehicleId = (ApiReusableComponents.vehicleId).ToString();
                    if (a.Contains(vehicleId))
                    {
                        rowOfVehicleId = row - 1;
                        break;
                    }
                }
                for (int row = 1; row <= rowCount; row++)
                {

                    cellSelector = jObject[cellLocator].ToString() + "[" + row + "]" + "//td" + "[" + 6 + "]";
                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", cellSelector);
                    string a = ReusableComponents.RetrieveText(driver, "XPath", cellSelector);
                    string casualtyId = (ApiReusableComponents.casualtyId).ToString();
                    if (a.Contains(casualtyId))
                    {
                        rowOfCasualtyId = row - 1;
                        break;
                    }
                }

                string userNameInDeleteCasualty = array[rowOfCasualtyId, colOfUserName];
                string operationInDeleteCasualty = array[rowOfCasualtyId, colOfOperation];
                string entityInDeleteCasualty = array[rowOfCasualtyId, colOfEntity];

                string userNameInDeleteVehicle = array[rowOfVehicleId, colOfUserName];
                string operationInDeleteVehicle = array[rowOfVehicleId, colOfOperation];
                string entityInDeleteVehicle = array[rowOfVehicleId, colOfEntity];

                string userNameInDeleteCrash = array[rowOfCrashId, colOfUserName];
                string operationInDeleteCrash = array[rowOfCrashId, colOfOperation];
                string entityInDeleteCrash = array[rowOfCrashId, colOfEntity];


                if ((userNameInDeleteCrash) == inputjson[username].ToString() && (operationInDeleteCrash == inputjson[deleteOperation].ToString()) && (entityInDeleteCrash == inputjson[crashEntity].ToString()))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }

                if ((userNameInDeleteVehicle) == inputjson[username].ToString() && (operationInDeleteVehicle == inputjson[deleteOperation].ToString()) && (entityInDeleteVehicle == inputjson[vehicleEntity].ToString()))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }

                if ((userNameInDeleteCasualty) == inputjson[username].ToString() && (operationInDeleteCasualty == inputjson[deleteOperation].ToString()) && (entityInDeleteCasualty == inputjson[casualtyEntity].ToString()))
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                else
                {
                    listOfReport[step++].SetActualResultPass("");
                }
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Audit Trail for delete crash"));


            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify standard print report page.Exception caught : " + e);
            }
            return listOfReport;

        }

        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetAuditTrailPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }

        /// <summary>
        /// To the UNIX time stamp.
        /// </summary>
        /// <param name="dateTimeUtc">The date time UTC.</param>
        /// <returns></returns>
        public long ToUnixTimeStamp(DateTime dateTimeUtc)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan timeSpan = dateTimeUtc - epoch;
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }

        /// <summary>
        /// Verify web application Audit trail page displayed
        /// </summary>
        public List<TestReportSteps> VerifyAuditTrailPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyAuditTrailPageReport.json");
            try
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyAuditTrailTitle].ToString());
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }
    }
}
