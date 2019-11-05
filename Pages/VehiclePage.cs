
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using FindsByAttribute = SeleniumExtras.PageObjects.FindsByAttribute;
using How = SeleniumExtras.PageObjects.How;

namespace FrameworkSetup.Pages
{
    class VehiclePage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        JObject jObject;


        private string vehicleTab = "vehicleTab";
        private string vehicleRegistartionNo = "vehicleRegistartionNo";
        private string vehicleReferenceNumber1 = "vehicleReferenceNumber1";
        private string deleteYes = "deleteYesBtn";
        private string deleteBtn = "deleteBtn";


        public VehiclePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            jObject = ConfigFile.RetrieveUIMap("VehiclePageSelector.json");

        }

        

       

        /// <summary>
        /// Add vehicle details for summary print report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> AddVehicleDetailsForSummaryPrintReport(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("AddVehicleDetailsForSummaryPrintReport.json");

            //Click Vehicle tab
            ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            //Enter Vehicle Registration No (VRN)
            ReusableComponents.SendKeys(driver, "Id", jObject[vehicleRegistartionNo].ToString(), inputjson[vehicleRegistartionNo].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Enter Vehicle Reference No
            ReusableComponents.SendKeys(driver, "Id", jObject[vehicleReferenceNumber1].ToString(), inputjson[vehicleReferenceNumber1].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Details - ARF"));

            return listOfReport;
        }


        /// <summary>
        /// Enters vehicle details for all optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EnterMandatoryAndOptionalVehicleDetails(JToken inputjson,string testDataFile, int vehicleCount)
        {
            screenshotList.Clear();
            step = 0;

            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to enter vehicle details in mandatory and optional fields for vehicle "+ vehicleCount;

            //retrieve master data
            JArray vehiclemasterData = ReusableComponents.RetrieveMasterData("VehicleDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("VehiclePageSelector.json");




            //Enter mandatory and optional details
            foreach (JToken data in vehiclemasterData)
            {
               
                    string uiSelector = jObjects[data["selector"].ToString()].ToString();
                    string type = property[data["propertyTypeId"].ToString()].ToString();
                    foreach (JToken dataInput in testData)
                    {
                        string check = data["selector"].ToString();
                    for (int i = 0; i <= vehicleCount - 1; i++)
                    {
                        if (i == vehicleCount - 1)
                        {
                            if (data["propertyTypeId"].ToString() == "1" || data["propertyTypeId"].ToString() == "2" || data["propertyTypeId"].ToString() == "3")
                                uiSelector = uiSelector + "1" + i;
                            else
                            {
                                string text = data["apiKey"].ToString() + "1" + i;
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
        /// Enters vehicle details for some of the optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EnterOptionalVehicleDetails(JToken inputjson, string testDataFile,int vehicleCount)
        {
            screenshotList.Clear();
            step = 0;

            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to enter vehicle details in mandatory and optional fields for vehicle "+ vehicleCount;

            //retrieve master data
            JArray vehiclemasterData = ReusableComponents.RetrieveMasterData("VehicleDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("VehiclePageSelector.json");




            //Enter mandatory and optional details
            foreach (JToken data in vehiclemasterData)
            {

                string uiSelector = jObjects[data["selector"].ToString()].ToString();
                string type = property[data["propertyTypeId"].ToString()].ToString();
                foreach (JObject dataInput in testData)
                {
                    string check = data["selector"].ToString();
                    for (int i = 0; i <= vehicleCount - 1; i++)
                    {
                        if (i == vehicleCount - 1)
                        {
                            if (data["propertyTypeId"].ToString() == "1" || data["propertyTypeId"].ToString() == "2" || data["propertyTypeId"].ToString() == "3")
                                uiSelector = uiSelector + "1" + i;
                            else
                            {
                                string text = data["apiKey"].ToString() + "1" + i;
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
        /// Edit vehicle details for some of the optional fields
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditOptionalVehicleDetails(JToken inputjson, string testDataFile, int vehicleCount)
        {
            screenshotList.Clear();
            step = 0;

            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;
            addReport.testObjective = "Verify that user is able to edit vehicle details for vehicle "+ vehicleCount;

            //retrieve master data
            JArray vehiclemasterData = ReusableComponents.RetrieveMasterData("VehicleDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            JObject property = ConfigFile.RetrieveProperty("PropertyType.json");
            string testInput = string.Empty;

            var jObjects = ConfigFile.RetrieveUIMap("VehiclePageSelector.json");




            //Enter mandatory and optional details
            foreach (JToken data in vehiclemasterData)
            {

                string uiSelector = jObjects[data["selector"].ToString()].ToString();
                string type = property[data["propertyTypeId"].ToString()].ToString();
                foreach (JObject dataInput in testData)
                {
                    string check = data["selector"].ToString();
                    for (int i = 0; i <= vehicleCount - 1; i++)
                    {
                        if (i == vehicleCount - 1)
                        {
                            if (data["propertyTypeId"].ToString() == "1" || data["propertyTypeId"].ToString() == "2" || data["propertyTypeId"].ToString() == "3")
                                uiSelector = uiSelector + "1" + i;
                            else
                            {
                                string text = data["apiKey"].ToString() + "1" + i;
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
        /// Add vehicle details for summary print report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> NavigateToVehicleTab()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("NavigateToVehicleTabReport.json");

            //Click Vehicles Tab
            ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            listOfReport[step++].SetActualResultFail("");

            
            return listOfReport;
        }

        /// <summary>
        /// Delete last vehicle record 
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> DeleteVehicleRecord(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("DeleteVehicleReport.json");

            //Retrieve vehicle tab text and count
            String vehicleTabText1 = ReusableComponents.RetrieveText(driver, "XPath", jObject[vehicleTab].ToString());
            int vehicleCount1 = ReusableComponents.GetNumber(vehicleTabText1);

            //click delete button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteBtn].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[deleteBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Delete vehicle popup"));

            //click yes button
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteYes].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[deleteYes].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "After Delete vehicle2"));

            //Retrieve vehicle tab text and count
            String vehicleTabText2 = ReusableComponents.RetrieveText(driver, "XPath", jObject[vehicleTab].ToString());
            int vehicleCount2 = ReusableComponents.GetNumber(vehicleTabText2);

            //Verify count decrements by one for Vehicle after deleting
            if (vehicleCount2 == vehicleCount1 - 1)
                listOfReport[step++].SetActualResultFail("");
            else
                listOfReport[step++].SetActualResultPass("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify vehicle count decrements"));
            return listOfReport;
        }

        /// <summary>
        /// Retrieves vehicle page screenshots
        /// </summary>
        /// <returns></returns>
        public List<string> GetVehiclePageScreenshots()
        {
            return screenshotList;
        }
    }
}