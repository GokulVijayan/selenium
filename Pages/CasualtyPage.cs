using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Ex_haft.GenericComponents;


namespace FrameworkSetup.Pages
{
    class CasualtyPage
    {

        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        JObject jObject;
        public static string retrieveCrashRefNo;

        private string validateAndSaveBtn = "validateAndSaveBtn";
        private string iAgreeBtn = "iAgreeBtn";
        private string casualtyTab = "casualtyTab";
        private string addNewBtn = "addNewBtn";
        private string injuryRefNo = "injuryRefNo";
        private string vehicleReferenceNo = "vehicleReferenceNo";
        private string casualtyClassDropDown = "casualtyClassDropDown";
        private string toastMsg = "toastMsg";
        private string crashRefNoGenerated = "crashRefNoGenerated";
        private string toastDisplayed;
        private string validateErrorMsg = "validateErrorMsg";
        private string deleteBtn = "deleteBtn";
        private string deleteYes = "deleteYesBtn";
        private string saveButton = "saveButton";
        private string casualtyTab1 = "casualtyTab1";
        public CasualtyPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("CasualtyPageSelector.json");
        }

        /// <summary>
        /// Add casualty details for summary print report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddCasualtyDetailsForSummaryPrintReport(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            bool isToastVerified;
            listOfReport = ConfigFile.GetReportFile("AddCasualtyDetailsForSummaryPrintReport.json");

            //Click Casualty tab
            ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            //Click 'Add New' button
            ReusableComponents.JEClick(driver, "XPath", jObject[addNewBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Enter Injury Reference Number
            ReusableComponents.SendKeys(driver, "Id", jObject[injuryRefNo].ToString(), inputjson[injuryRefNo].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Enter vehicle ref no in Casualty tab
            ReusableComponents.SendKeys(driver, "Id", jObject[vehicleReferenceNo].ToString(), inputjson[vehicleReferenceNo].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Select casualty class
            ReusableComponents.SelectFromDropdown(driver, "XPath", jObject[casualtyClassDropDown].ToString(), inputjson[casualtyClassDropDown].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Details - ARF"));

            //Click 'Validate and Save' button
            ReusableComponents.JEClick(driver, "XPath", jObject[validateAndSaveBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Click 'I Agree' button
            ReusableComponents.Click(driver, "XPath", jObject[iAgreeBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            //Verify toast message
            isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[toastMsg].ToString());
            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

            if (isToastVerified)
            {
                listOfReport[step++].SetActualResultFail("");
            }

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            retrieveCrashRefNo = ReusableComponents.RetrieveText(driver, "XPath", jObject[crashRefNoGenerated].ToString());

            return listOfReport;

        }


        /// <summary>
        /// Enters casualty details for all optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EnterMandatoryAndOptionalCasualtyDetails(JToken inputjson,string testDataFile,int casualtyCount)
        {
            screenshotList.Clear();
            step = 0;
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to enter casualty details in mandatory and optional fields for casualty "+ casualtyCount;

           
            //retrieve master data
            JArray casualtymasterData = ReusableComponents.RetrieveMasterData("CasualtyDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("CasualtyPageSelector.json");


            //Enter mandatory and optional details
            foreach (JToken data in casualtymasterData)
            {
               
                    string uiSelector = jObjects[data["selector"].ToString()].ToString();
                    string type = property[data["propertyTypeId"].ToString()].ToString();
                    foreach (JToken dataInput in testData)
                    {
                        string check = data["selector"].ToString();
                    for (int i = 0; i <= casualtyCount - 1; i++)
                    {
                        if (i == casualtyCount - 1)
                        {
                            if (data["propertyTypeId"].ToString() == "1" || data["propertyTypeId"].ToString() == "2" || data["propertyTypeId"].ToString() == "3")
                                uiSelector = uiSelector + "2" + i;
                            else
                            {
                                string text = data["apiKey"].ToString() + "2" + i;
                                uiSelector = uiSelector.Replace(data["apiKey"].ToString(), text);

                            }
                        }

                    }
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
        /// Enters casualty details for some of the optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EnterOptionalCasualtyDetails(JToken inputjson, string testDataFile,int casualtyCount)
        {
            screenshotList.Clear();
            step = 0;
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to enter casualty details in mandatory and optional fields for casualty "+ casualtyCount;


            //retrieve master data
            JArray casualtymasterData = ReusableComponents.RetrieveMasterData("CasualtyDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("CasualtyPageSelector.json");


            //Enter mandatory and optional details
            foreach (JToken data in casualtymasterData)
            {

                string uiSelector = jObjects[data["selector"].ToString()].ToString();
                string type = property[data["propertyTypeId"].ToString()].ToString();
                foreach (JObject dataInput in testData)
                {
                    string check = data["selector"].ToString();
                    for (int i = 0; i <= casualtyCount - 1; i++)
                    {
                        if (i == casualtyCount - 1)
                        {
                            if (data["propertyTypeId"].ToString() == "1" || data["propertyTypeId"].ToString() == "2" || data["propertyTypeId"].ToString() == "3")
                                uiSelector = uiSelector + "2" + i;
                            else
                            {
                                string text = data["apiKey"].ToString() + "2" + i;
                                uiSelector = uiSelector.Replace(data["apiKey"].ToString(), text);

                            }
                        }

                    }
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
        /// Edit casualty details for some of the optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditOptionalCasualtyDetails(JToken inputjson, string testDataFile, int casualtyCount)
        {
            screenshotList.Clear();
            step = 0;
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to edit details of casualty "+ casualtyCount;


            //retrieve master data
            JArray casualtymasterData = ReusableComponents.RetrieveMasterData("CasualtyDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("CasualtyPageSelector.json");


            //Enter mandatory and optional details
            foreach (JToken data in casualtymasterData)
            {

                string uiSelector = jObjects[data["selector"].ToString()].ToString();
                string type = property[data["propertyTypeId"].ToString()].ToString();
                foreach (JObject dataInput in testData)
                {
                    string check = data["selector"].ToString();
                    for (int i = 0; i <= casualtyCount - 1; i++)
                    {
                        if (i == casualtyCount - 1)
                        {
                            if (data["propertyTypeId"].ToString() == "1" || data["propertyTypeId"].ToString() == "2" || data["propertyTypeId"].ToString() == "3")
                                uiSelector = uiSelector + "2" + i;
                            else
                            {
                                string text = data["apiKey"].ToString() + "2" + i;
                                uiSelector = uiSelector.Replace(data["apiKey"].ToString(), text);

                            }
                        }

                    }
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
        /// Navigate to casualty tab
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> NavigateToCasualtyTab()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("NavigatetoCasualtyTabReport.json");

            //Click Casualty tab
            ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            


            return listOfReport;

        }


        /// <summary>
        /// Click on Add new button
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> ClickOnAddNewButton()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("ClickOnAddNewButtonReport.json");

            //Click 'Add New' button
            ReusableComponents.JEClick(driver, "XPath", jObject[addNewBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            return listOfReport;

        }


        /// <summary>
        /// Save Crash Record
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SaveCrashRecord(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            bool isToastVerified;
            listOfReport = ConfigFile.GetReportFile("SaveCrashRecordReport.json");

            //Click 'Validate and Save' button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[validateAndSaveBtn].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[validateAndSaveBtn].ToString());
            listOfReport[step++].SetActualResultFail("");


            //Click 'I Agree' button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[iAgreeBtn].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[iAgreeBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", Constant.toastSelector);

            //Verify toast message
            toastDisplayed = ReusableComponents.RetrieveText(driver, "XPath", Constant.toastSelector);
            isToastVerified = ReusableComponents.ContainsText(toastDisplayed, inputjson[toastMsg].ToString());

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msgs"));

            if (isToastVerified)
            {
                listOfReport[step++].SetActualResultFail("");
            }

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            retrieveCrashRefNo = ReusableComponents.RetrieveText(driver, "XPath", jObject[crashRefNoGenerated].ToString());


            return listOfReport;

        }




        /// <summary>
        /// Save Crash Record
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SaveCrashRecordWithoutValidation(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            bool isToastVerified;
            listOfReport = ConfigFile.GetReportFile("SaveCrashRecordReport.json");

            //Click 'Validate and Save' button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveButton].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[saveButton].ToString());
            listOfReport[step++].SetActualResultFail("");


            //Click 'I Agree' button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[iAgreeBtn].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[iAgreeBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", Constant.toastSelector);

            //Verify toast message
            toastDisplayed = ReusableComponents.RetrieveText(driver, "XPath", Constant.toastSelector);
            isToastVerified = ReusableComponents.ContainsText(toastDisplayed, inputjson[toastMsg].ToString());

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msgs"));

            if (isToastVerified)
            {
                listOfReport[step++].SetActualResultFail("");
            }

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            retrieveCrashRefNo = ReusableComponents.RetrieveText(driver, "XPath", jObject[crashRefNoGenerated].ToString());


            return listOfReport;

        }


        /// <summary>
        /// Validate and Save Crash Record and toast error message verification
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> SaveCrashRecordAndVerifyErrorToast(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            bool isToastVerified;
            listOfReport = ConfigFile.GetReportFile("ValidateAndSaveCrashReport.json");

            //Click 'Validate and Save' button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[validateAndSaveBtn].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[validateAndSaveBtn].ToString());
            listOfReport[step++].SetActualResultFail("");


            //Click 'I Agree' button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[iAgreeBtn].ToString());
            ReusableComponents.JEClick(driver, "XPath", jObject[iAgreeBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", Constant.toastSelector);

            //Verify toast message
            toastDisplayed = ReusableComponents.RetrieveText(driver, "XPath", Constant.toastSelector);
            isToastVerified = ReusableComponents.ContainsText(toastDisplayed, inputjson[validateErrorMsg].ToString());

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Error Toast Msgs"));

            if (isToastVerified)
            {
                listOfReport[step++].SetActualResultFail("");
            }

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);


            return listOfReport;

        }


        /// <summary>
        /// Delete last vehicle record 
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> DeleteCasualtyRecord(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("DeleteCasualtyReport.json");

            //Retrieve casualty tab text and count
            String casualtyTabText1 = ReusableComponents.RetrieveText(driver, "XPath", jObject[casualtyTab1].ToString());
            int casualtyCount1 = ReusableComponents.GetNumber(casualtyTabText1);

            //click delete button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteBtn].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[deleteBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Delete casualty popup"));

            //click yes button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteYes].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[deleteYes].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "After Delete casualty 2"));

            //Retrieve vehicle tab text and count
            String casualtyTabText2 = ReusableComponents.RetrieveText(driver, "XPath", jObject[casualtyTab1].ToString());
            int casualtyCount2 = ReusableComponents.GetNumber(casualtyTabText2);

            //Verify count decrements by one for Vehicle after deleting
            if (casualtyCount2 == casualtyCount1 - 1)
                listOfReport[step++].SetActualResultFail("");
            else
                listOfReport[step++].SetActualResultPass("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify casualty deletion"));
            return listOfReport;
        }


        /// <summary>
        /// Retrieves crash reference number
        /// </summary>
        /// <returns></returns>
        public string GetCrashRefNo()
        {
            return retrieveCrashRefNo;
        }

        /// <summary>
        /// Returns the screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetCasualtyPageScreenshots()
        {
            return screenshotList;
        }
    }
}
