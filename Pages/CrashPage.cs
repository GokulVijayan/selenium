
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{
    class CrashPage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        JObject jObject;
        public static string retrieveCrashRefNo=string.Empty;
        readonly JsonDeserializer deserialize = new JsonDeserializer();
        private string crashTab = "crashTab";
        private string numberOfVehicles = "numberOfVehicles";
        private string crashDate = "crashDate";
        private string searchbtn = "searchbtn";
        private string EnterSearchItem = "enterSearchItem";
        private string searchicon = "searchicon";
        private string cancelButton = "cancelButton";
        private string deleteButton = "deleteButton";
        private string yesButton = "yesButton";
        private string okButton = "okButton";
        private string verifyDeleteText = "verifyDeleteText";
        private string verifyNoRecord = "verifyNoRecord";
        private string noRecordOkButton = "noRecordOkButton";
        private string verifyCrashDelete = "verifyCrashDelete";
        private string navToCrashTabEdit = "navToCrashTabEdit";
      




        public CrashPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("CrashPageSelector.json");
        }

        /// <summary>
        /// Enters crash details without providing all mandatory fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EnterCrashDetailsWithoutAllMandatoryFields(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;

            listOfReport = ConfigFile.GetReportFile("EnterCrashDetailsWithoutAllMandatoryFieldsReport.json");

            //Click Crash tab
            ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            //Enter Number of vehicles
            ReusableComponents.Clear(driver, "Id", jObject[numberOfVehicles].ToString());
            ReusableComponents.SendKeys(driver, "Id", jObject[numberOfVehicles].ToString(), inputjson[numberOfVehicles].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Crash Details - ARF"));

            return listOfReport;
        }

        /// <summary>
        /// Add crash details for summary print report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddCrashDetailsForSummaryPrintReport(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("AddCrashDetailsForSummaryPrintReport.json");

            //Select Crash Date
            ReusableComponents.ScrollToElement(driver, "XPath", jObject[crashDate].ToString());
            ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[crashDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Crash Details - ARF"));

            return listOfReport;

        }

        


        /// <summary>
        /// Enter crash details by filling all mandatory and optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <param name="testDataFile"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddCrashDetailsWithAllFields(JToken inputjson,string testDataFile)
        {
            screenshotList.Clear();
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to enter crash details in mandatory and optional fields";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            //Click Crash tab
            ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);



            //retrieve master data
            JArray crashmasterData = ReusableComponents.RetrieveMasterData("CrashDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("CrashPageSelector.json");



            
            //Enter mandatory and optional details
            foreach (JToken data in crashmasterData)
            {
                
                    string uiSelector = jObjects[data["selector"].ToString()].ToString();
                    string type = property[data["propertyTypeId"].ToString()].ToString();
                    foreach (JToken dataInput in testData)
                    {
                        string check = data["selector"].ToString();
                        testInput = dataInput[check].ToString();
                    }
                    try
                    {
                        stepCount++;
                        //Set report
                        addReport.stepName = stepCount.ToString();
                        addReport.stepDescription = (ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");
                        addReport.expectedResult = ("User is able to " + ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");
                        addReport.actualResultPass = ("User was able to " + ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");

                        
                        ReusableComponents.PerformActionBasedOnType(driver, type, uiSelector, testInput);
                        addReport.SetActualResultFail("");

                        sampleReport.Add(addReport);
                        addReport = new TestReportSteps();

                    }
                    catch (Exception e)
                    {
                        listOfReport[step].SetActualResultFail("Failed");
                        Console.WriteLine("Not able to perform action" + e);
                        break;
                    }
                
            }
            
            return sampleReport;
        }

        /// <summary>
        /// Enter crash details by filling mandatory and some of the optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <param name="testDataFile"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddCrashDetailsWithOptionalFields(JToken inputjson, string testDataFile)
        {
            screenshotList.Clear();
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to enter crash details in mandatory and optional fields";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            //Click Crash tab
            ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);



            //retrieve master data
            JArray crashmasterData = ReusableComponents.RetrieveMasterData("CrashDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("CrashPageSelector.json");




            //Enter mandatory and optional details
            foreach (JToken data in crashmasterData)
            {

                string uiSelector = jObjects[data["selector"].ToString()].ToString();
                string type = property[data["propertyTypeId"].ToString()].ToString();
                foreach (JObject dataInput in testData)
                {
                    string check = data["selector"].ToString();
                    if (dataInput.ContainsKey(check))
                    testInput = dataInput[check].ToString();
                    else
                    testInput = string.Empty;


                }
                if (testInput.Length > 0)
                {
                    try
                    {
                        stepCount++;
                        //Set report
                        addReport.stepName = stepCount.ToString();
                        addReport.stepDescription = (ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");
                        addReport.expectedResult = ("User is able to " + ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");
                        addReport.actualResultPass = ("User was able to " + ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");


                        ReusableComponents.PerformActionBasedOnType(driver, type, uiSelector, testInput);
                        addReport.SetActualResultFail("");

                        sampleReport.Add(addReport);
                        addReport = new TestReportSteps();

                    }
                    catch (Exception e)
                    {
                        listOfReport[step].SetActualResultFail("Failed");
                        Console.WriteLine("Not able to perform action" + e);
                        break;
                    }
                }

            }

            return sampleReport;
        }



        /// <summary>
        /// Edit crash details by filling mandatory and some of the optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <param name="testDataFile"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditCrashDetailsWithOptionalFields(JToken inputjson, string testDataFile)
        {
            screenshotList.Clear();
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to edit crash details";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);



            //retrieve master data
            JArray crashmasterData = ReusableComponents.RetrieveMasterData("CrashDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("CrashPageSelector.json");




            //Enter mandatory and optional details
            foreach (JToken data in crashmasterData)
            {

                string uiSelector = jObjects[data["selector"].ToString()].ToString();
                string type = property[data["propertyTypeId"].ToString()].ToString();
                foreach (JObject dataInput in testData)
                {
                    string check = data["selector"].ToString();
                    if (dataInput.ContainsKey(check))
                        testInput = dataInput[check].ToString();
                    else
                        testInput = string.Empty;


                }
                if (testInput.Length > 0)
                {
                    try
                    {
                        stepCount++;
                        //Set report
                        addReport.stepName = stepCount.ToString();
                        addReport.stepDescription = (ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");
                        addReport.expectedResult = ("User is able to " + ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");
                        addReport.actualResultPass = ("User was able to " + ReusableComponents.RetrieveReportAction(type) + " '" + data["apiKey"].ToString() + "'");


                        ReusableComponents.PerformActionBasedOnType(driver, type, uiSelector, testInput);
                        addReport.SetActualResultFail("");

                        sampleReport.Add(addReport);
                        addReport = new TestReportSteps();

                    }
                    catch (Exception e)
                    {
                        listOfReport[step].SetActualResultFail("Failed");
                        Console.WriteLine("Not able to perform action" + e);
                        break;
                    }
                }

            }

            return sampleReport;
        }


        /// <summary>
        /// Search for a crash record
        /// </summary>
        /// <param name="inputjson"></param>
        /// <param name="crashRefNo"></param>
        /// <returns></returns>
        public List<TestReportSteps> SearchCrashRecords(JToken inputjson, string crashRefNo)
        {
            screenshotList.Clear();
            step = 0;
            //bool isCrashRefNoverfied;

            listOfReport = ConfigFile.GetReportFile("SearchCrashRecordsReport.json");

            //Click Search button
            ReusableComponents.Click(driver, "XPath", jObject[searchbtn].ToString());
            //Set Report
            Console.WriteLine("User was able to click on the search button");
            listOfReport[step++].SetActualResultFail("");
            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ARF-Search button"));

            //Enter crash ref no. as search criteria       
            ReusableComponents.SendKeys(driver, "XPath", jObject[EnterSearchItem].ToString(), crashRefNo);
            //Set Report
            Console.WriteLine("User was able to enter the crash ref no. as the search criteria");
            listOfReport[step++].SetActualResultFail("");
            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ARF-Search retreivel"));

            //Click Search icon            
            ReusableComponents.Click(driver, "XPath", jObject[searchicon].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            //Set Report
            Console.WriteLine("User was able to click search icon");
            listOfReport[step++].SetActualResultFail("");

            //Click OK button
            ReusableComponents.Click(driver, "XPath", jObject[okButton].ToString());
            //Set Report
            Console.WriteLine("User was able to click on the OK button");
            listOfReport[step++].SetActualResultFail("");


            return listOfReport;

        }


        public List<TestReportSteps> DeleteCrashRecord(JToken inputjson, string crashRefNo)
        {
            screenshotList.Clear();
            step = 0;
            bool isToastVerified;
            listOfReport = ConfigFile.GetReportFile("DeleteCrashReport.json");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashTab].ToString());

            //Click Cancel button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[cancelButton].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[cancelButton].ToString());
            //Set Report
            Console.WriteLine("User was able to click on the cancel button");
            listOfReport[step++].SetActualResultFail("");


            //Click Delete button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteButton].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[deleteButton].ToString());
            //Set Report
            Console.WriteLine("User was able to click on the delete button");
            listOfReport[step++].SetActualResultFail("");

            //Click Yes button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[yesButton].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[yesButton].ToString());
            //Set Report
            Console.WriteLine("User was able to click on the Yes button");
            listOfReport[step++].SetActualResultFail("");


            //Verify toast message
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", Constant.toastSelector);
            isToastVerified = ReusableComponents.ContainsText(ReusableComponents.RetrieveText(driver, "XPath", Constant.toastSelector), inputjson[verifyDeleteText].ToString());
            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg-Delete"));

            if (isToastVerified)
            {
                //Set Report
                Console.WriteLine("User was able to verify toast message");
                listOfReport[step++].SetActualResultFail("");
            }



            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ///Click Search button
            ReusableComponents.Click(driver, "XPath", jObject[searchbtn].ToString());
            //Set Report
            Console.WriteLine("User was able to click on the search button");
            listOfReport[step++].SetActualResultFail("");



            //Enter crash ref no. as search criteria
            ReusableComponents.SendKeys(driver, "XPath", jObject[EnterSearchItem].ToString(), crashRefNo);
            //Set Report
            Console.WriteLine("User was able to Enter the crash ref no. as the search criteria");
            listOfReport[step++].SetActualResultFail("");

            //Click Search icon
            ReusableComponents.Click(driver, "XPath", jObject[searchicon].ToString());
            //Set Report
            Console.WriteLine("User was able to click search icon");
            listOfReport[step++].SetActualResultFail("");


            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            bool deleteStatus = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[verifyNoRecord].ToString(), inputjson[verifyCrashDelete].ToString());
            //Set Report
            Console.WriteLine("User was able to verify crash record is deleted");
            if (deleteStatus == true)
                listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify-Delete"));

            //Click ok button
            ReusableComponents.Click(driver, "XPath", jObject[noRecordOkButton].ToString());
            //Set Report
            Console.WriteLine("User was able to click ok button icon");
            listOfReport[step++].SetActualResultFail("");


            return listOfReport;

        }


        /// <summary>
        /// Navigate to crash tab from edit crash screen
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> NavigateToEditCrashTab()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("NavigateToEditCrashTabReport.json");

            //Click Crash Tab
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[navToCrashTabEdit].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[navToCrashTabEdit].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            listOfReport[step++].SetActualResultFail("");


            return listOfReport;
        }


        /// <summary>
        /// Returns crash page screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetCrashPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }

        
    }
}
