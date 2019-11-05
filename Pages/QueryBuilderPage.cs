using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.TestDataClasses;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FrameworkSetup.Pages
{

    class QueryBuilderPage
    {
        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport, crashFieldReport, vehicleFieldReport, casualtyFieldReport, applyQueryReport;
        JObject jObject;
        bool crashStatus = false, vehicleStatus = false, casualtyStatus = false;
        public List<string> screenshotList = new List<string>();

        private string queryBuilderIcon = "queryBuilderIcon";
        private string crashTab = "crashTab";
        private string vehicleTab = "vehicleTab";
        private string casualtyTab = "casualtyTab";
        private string crashTabSelector = "crashTabSelector";
        private string vehicleTabSelector = "vehicleTabSelector";
        private string casualtyTabSelector = "casualtyTabSelector";
        private string crashFieldSelector = "crashFieldSelector";
        private string vehicleFieldSelector = "vehicleFieldSelector";
        private string casualtyFieldSelector = "casualtyFieldSelector";
        private string dateButton = "dateButton";
        private string vehicleDropDown = "vehicleDropDown";
        private string casualtyDropDown = "casualtyDropDown";
        private string crashDropDown = "crashDropDown";
        private string crashInput = "crashInput";
        private string vehicleInput = "vehicleInput";
        private string casualtyInput = "casualtyInput";
        private string applyQuery = "applyQuery";
        private string appliedQuery = "appliedQuery";
        private string resetQuery = "resetQuery";
        private string yesButton = "yesButton";
        private string buildQuery = "buildQuery";
        private string closeButton = "closeButton";


        public QueryBuilderPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("QueryBuilderPageSelector.json");

        }

        /// <summary>
        /// Navigate to Query Builder Screen
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> NavigateToQueryBuilder(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("NavigateToQueryBuilderPageReport.json");

                //Click on query builder icon
                ReusableComponents.Click(driver, "XPath", jObject[queryBuilderIcon].ToString());

                //Set report
                Console.WriteLine("User was able to click on query builder button");
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashTab].ToString());

                //Set report
                Console.WriteLine("User was able to verify query builder page");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "QueryBuilderPage"));

            }
            catch (Exception e)
            {
                Console.WriteLine("Navigate to Query Builder Screen Failed : " + e);
            }
            return listOfReport;
        }

        public List<TestReportSteps> SelectFormFields(List<QueryBuilder> queryBuilderData, JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                crashFieldReport = ConfigFile.GetReportFile("SetupCrashFieldsinQueryBuilderReport.json");
                vehicleFieldReport = ConfigFile.GetReportFile("SetupVehicleFieldsinQueryBuilderReport.json");
                casualtyFieldReport = ConfigFile.GetReportFile("SetupCasualtyFieldsinQueryBuilderReport.json");

                var crashJsonResult = ReusableComponents.RetrieveMasterDataSelectors("CrashDataMasterPageSelector.json");
                var crashMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(crashJsonResult);

                var vehicleJsonResult = ReusableComponents.RetrieveMasterDataSelectors("VehicleDataMasterPageSelector.json");
                var vehicleMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(vehicleJsonResult);

                var casualtyJsonResult = ReusableComponents.RetrieveMasterDataSelectors("CasualtyDataMasterPageSelector.json");
                var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(casualtyJsonResult);

                for (int i = 0; i < queryBuilderData.Count; i++)
                {
                    if (queryBuilderData.Count > 1)
                    {

                        for (int j = 0; j < crashMasterDataSelectors.Count; j++)
                        {
                            if (crashMasterDataSelectors[j].selector == queryBuilderData[0].fieldName && crashStatus == false)
                            {
                                //Click Crash Tab
                                ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                //Set report
                                Console.WriteLine("User was able to click crash tab");
                                crashFieldReport[step++].SetActualResultFail("");

                                //Select Field
                                ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[0].displayName, jObject[crashTabSelector].ToString());
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[0].displayName);
                                crashFieldReport[step++].SetActualResultPass("User is able to select field" + queryBuilderData[0].displayName);


                                //Select Query
                                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[crashFieldSelector].ToString(), queryBuilderData[0].query);
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[0].query);
                                crashFieldReport[step++].SetActualResultPass("User is able to select query" + queryBuilderData[0].query);


                                if (crashMasterDataSelectors[j].propertyTypeId == 4)
                                {
                                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[dateButton].ToString());
                                    ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[dateButton].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    crashFieldReport[step++].SetActualResultFail("");

                                    ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click crash tab");
                                    crashFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CrashQueryBuilderPage"));
                                    crashStatus = true;


                                }
                                else if (crashMasterDataSelectors[j].propertyTypeId == 6 || crashMasterDataSelectors[j].propertyTypeId == 10 || crashMasterDataSelectors[j].propertyTypeId == 11 || crashMasterDataSelectors[j].propertyTypeId == 9)
                                {
                                    ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[0].fieldValue, jObject[crashDropDown].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    crashFieldReport[step++].SetActualResultFail("");

                                    ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click crash tab");
                                    crashFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CrashQueryBuilderPage"));
                                    crashStatus = true;

                                }
                                else if (crashMasterDataSelectors[j].propertyTypeId == 1 || crashMasterDataSelectors[j].propertyTypeId == 2 || crashMasterDataSelectors[j].propertyTypeId == 3 || crashMasterDataSelectors[j].propertyTypeId == 7)
                                {
                                    ReusableComponents.SendKeys(driver, "XPath", jObject[crashInput].ToString(), queryBuilderData[0].fieldValue);
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    crashFieldReport[step++].SetActualResultFail("");

                                    ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click crash tab");
                                    crashFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CrashQueryBuilderPage"));
                                    crashStatus = true;

                                }

                            }
                        }

                    }

                    for (int j = 0; j < vehicleMasterDataSelectors.Count; j++)
                    {
                        if (queryBuilderData.Count > 2)
                        {

                            step = 0;

                            if (vehicleMasterDataSelectors[j].selector == queryBuilderData[1].fieldName && vehicleStatus == false)
                            {
                                //Click Vehicle Tab
                                ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
                                //Set report
                                Console.WriteLine("User was able to click vehicle tab");
                                vehicleFieldReport[step++].SetActualResultFail("");

                                //Select Field
                                ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[1].displayName, jObject[vehicleTabSelector].ToString());
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[1].displayName);
                                vehicleFieldReport[step++].SetActualResultPass("User is able to select field" + queryBuilderData[1].displayName);


                                //Select Query
                                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[vehicleFieldSelector].ToString(), queryBuilderData[0].query);
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[1].query);
                                vehicleFieldReport[step++].SetActualResultPass("User is able to select query" + queryBuilderData[1].query);


                                if (vehicleMasterDataSelectors[j].propertyTypeId == 6 || vehicleMasterDataSelectors[j].propertyTypeId == 10 || vehicleMasterDataSelectors[j].propertyTypeId == 11 || vehicleMasterDataSelectors[j].propertyTypeId == 9)
                                {
                                    //Select Value
                                    ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[1].fieldValue, jObject[vehicleDropDown].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    vehicleFieldReport[step++].SetActualResultFail("");

                                    //Click Vehicle Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click vehicle tab");
                                    vehicleFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VehicleQueryBuilderPage"));
                                    vehicleStatus = true;
                                    crashFieldReport.AddRange(vehicleFieldReport);


                                }
                                else if (vehicleMasterDataSelectors[j].propertyTypeId == 1 || vehicleMasterDataSelectors[j].propertyTypeId == 2 || vehicleMasterDataSelectors[j].propertyTypeId == 3 || vehicleMasterDataSelectors[j].propertyTypeId == 7)
                                {
                                    //Select Value
                                    ReusableComponents.SendKeys(driver, "XPath", jObject[vehicleInput].ToString(), queryBuilderData[1].fieldValue);
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    vehicleFieldReport[step++].SetActualResultFail("");

                                    //Click Vehicle Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click vehicle tab");
                                    vehicleFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VehicleQueryBuilderPage"));
                                    vehicleStatus = true;
                                    crashFieldReport.AddRange(vehicleFieldReport);

                                }
                            }
                        }

                    }

                    for (int j = 0; j < casualtyMasterDataSelectors.Count; j++)
                    {
                        if (queryBuilderData.Count == 3)
                        {

                            step = 0;


                            if (casualtyMasterDataSelectors[j].selector == queryBuilderData[2].fieldName && casualtyStatus == false)
                            {
                                //Click Casualty Tab
                                ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
                                //Set report
                                Console.WriteLine("User was able to click casualty tab");
                                casualtyFieldReport[step++].SetActualResultFail("");

                                //Select Field
                                ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[2].displayName, jObject[casualtyTabSelector].ToString());
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[2].displayName);
                                casualtyFieldReport[step++].SetActualResultPass("User is able to select field" + queryBuilderData[2].displayName);


                                //Select Query
                                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[casualtyFieldSelector].ToString(), queryBuilderData[2].query);
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[2].query);
                                casualtyFieldReport[step++].SetActualResultPass("User is able to select query" + queryBuilderData[2].query);


                                if (casualtyMasterDataSelectors[j].propertyTypeId == 6 || casualtyMasterDataSelectors[j].propertyTypeId == 10 || casualtyMasterDataSelectors[j].propertyTypeId == 11 || casualtyMasterDataSelectors[j].propertyTypeId == 9)
                                {
                                    //Select Value
                                    ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[2].fieldValue, jObject[casualtyDropDown].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to select value");
                                    casualtyFieldReport[step++].SetActualResultFail("");

                                    //Click Casualty Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click casualty tab");
                                    casualtyFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CasualtyQueryBuilderPage"));
                                    casualtyStatus = true;
                                    crashFieldReport.AddRange(casualtyFieldReport);
                                }
                                else if (casualtyMasterDataSelectors[j].propertyTypeId == 1 || casualtyMasterDataSelectors[j].propertyTypeId == 2 || casualtyMasterDataSelectors[j].propertyTypeId == 3 || casualtyMasterDataSelectors[j].propertyTypeId == 7)
                                {
                                    //Select Value
                                    ReusableComponents.SendKeys(driver, "XPath", jObject[casualtyInput].ToString(), queryBuilderData[2].fieldValue);
                                    //Set report
                                    Console.WriteLine("User was able to select value");


                                    //Click Casualty Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click casualty tab");
                                    casualtyFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CasualtyQueryBuilderPage"));
                                    casualtyStatus = true;
                                    crashFieldReport.AddRange(casualtyFieldReport);
                                }

                            }
                        }

                    }

                }
                applyQueryReport = ConfigFile.GetReportFile("ApplyQueryBuilderReport.json");
                step = 0;
                //Click on Apply Query Button
                ReusableComponents.Click(driver, "XPath", jObject[applyQuery].ToString());
                //Set report
                Console.WriteLine("User was able to click apply query button");
                applyQueryReport[step++].SetActualResultFail("");
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ApplyQueryButton"));

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.webLoader);
                bool status = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[appliedQuery].ToString(), inputjson[appliedQuery].ToString());
                if (status == true)
                {
                    applyQueryReport[step++].SetActualResultFail("");

                }
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ApplyQuery"));

                if (vehicleStatus == true)
                    crashFieldReport.AddRange(vehicleFieldReport);
                if (casualtyStatus == true)
                    crashFieldReport.AddRange(casualtyFieldReport);
                crashFieldReport.AddRange(applyQueryReport);

            }
            catch (Exception e)
            {
                Console.WriteLine("Form Fields was not selected : " + e);
            }
            return crashFieldReport;
        }



        public List<TestReportSteps> SelectCrashFormFields(List<QueryBuilder> queryBuilderData, JToken inputjson, string crashField)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                crashFieldReport = ConfigFile.GetReportFile("SetupCrashFieldsinQueryBuilderReport.json");
                var crashJsonResult = ReusableComponents.RetrieveMasterDataSelectors("CrashDataMasterPageSelector.json");
                var crashMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(crashJsonResult);

                for (int i = 0; i < queryBuilderData.Count; i++)
                {
                    if (queryBuilderData.Count >= 1)
                    {

                        for (int j = 0; j < crashMasterDataSelectors.Count; j++)
                        {
                            if (crashMasterDataSelectors[j].selector == queryBuilderData[0].fieldName && crashStatus == false)
                            {
                                //Click Crash Tab
                                ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                //Set report
                                Console.WriteLine("User was able to click crash tab");
                                crashFieldReport[step++].SetActualResultFail("");

                                //Select Field
                                ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[0].displayName, jObject[crashTabSelector].ToString());
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[0].displayName);
                                //crashFieldReport[step++].SetActualResultPass("User is able to select field"+ queryBuilderData[0].displayName);
                                crashFieldReport[step++].SetActualResultFail("");

                                //Select Query
                                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[crashFieldSelector].ToString(), queryBuilderData[0].query);
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[0].query);
                                //crashFieldReport[step++].SetActualResultPass("User is able to select query" + queryBuilderData[0].query);
                                crashFieldReport[step++].SetActualResultFail("");

                                if (crashMasterDataSelectors[j].propertyTypeId == 4)
                                {
                                    ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashField].ToString());
                                    ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[crashField].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    crashFieldReport[step++].SetActualResultFail("");

                                    ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click crash tab");
                                    crashFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CrashQueryBuilderPage"));
                                    crashStatus = true;


                                }
                                else if (crashMasterDataSelectors[j].propertyTypeId == 6 || crashMasterDataSelectors[j].propertyTypeId == 10 || crashMasterDataSelectors[j].propertyTypeId == 11 || crashMasterDataSelectors[j].propertyTypeId == 9)
                                {
                                    ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[0].fieldValue, jObject[crashField].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    crashFieldReport[step++].SetActualResultFail("");

                                    ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click crash tab");
                                    crashFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CrashQueryBuilderPage"));
                                    crashStatus = true;

                                }
                                else if (crashMasterDataSelectors[j].propertyTypeId == 1 || crashMasterDataSelectors[j].propertyTypeId == 2 || crashMasterDataSelectors[j].propertyTypeId == 3 || crashMasterDataSelectors[j].propertyTypeId == 7)
                                {
                                    ReusableComponents.SendKeys(driver, "XPath", jObject[crashField].ToString(), queryBuilderData[0].fieldValue);
                                    //Set report
                                    Console.WriteLine("User was able to Select Value");
                                    crashFieldReport[step++].SetActualResultFail("");

                                    ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click crash tab");
                                    crashFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CrashQueryBuilderPage"));
                                    crashStatus = true;

                                }

                            }
                        }
                    }
                }



            }
            catch (Exception e)
            {
                Console.WriteLine("Form Fields was not selected : " + e);
            }
            return crashFieldReport;
        }



        public List<TestReportSteps> SelectVehicleFormFields(List<QueryBuilder> queryBuilderData, JToken inputjson, string vehicleField)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                vehicleFieldReport = ConfigFile.GetReportFile("SetupVehicleFieldsinQueryBuilderReport.json");

                var vehicleJsonResult = ReusableComponents.RetrieveMasterDataSelectors("VehicleDataMasterPageSelector.json");
                var vehicleMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(vehicleJsonResult);


                for (int i = 0; i < queryBuilderData.Count; i++)
                {
                    if (queryBuilderData.Count > 1)
                    {

                        for (int j = 0; j < vehicleMasterDataSelectors.Count; j++)
                        {
                            if (queryBuilderData.Count >= 2)
                            {



                                if (vehicleMasterDataSelectors[j].selector == queryBuilderData[1].fieldName && vehicleStatus == false)
                                {
                                    //Click Vehicle Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click vehicle tab");
                                    vehicleFieldReport[step++].SetActualResultFail("");

                                    //Select Field
                                    ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[1].displayName, jObject[vehicleTabSelector].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to select field" + queryBuilderData[1].displayName);
                                    //vehicleFieldReport[step++].SetActualResultPass("User is able to select field" + queryBuilderData[1].displayName);
                                    vehicleFieldReport[step++].SetActualResultFail("");

                                    //Select Query
                                    ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[vehicleFieldSelector].ToString(), queryBuilderData[0].query);
                                    //Set report
                                    Console.WriteLine("User was able to select field" + queryBuilderData[1].query);
                                    //vehicleFieldReport[step++].SetActualResultPass("User is able to select query" + queryBuilderData[1].query);
                                    vehicleFieldReport[step++].SetActualResultFail("");

                                    if (vehicleMasterDataSelectors[j].propertyTypeId == 6 || vehicleMasterDataSelectors[j].propertyTypeId == 10 || vehicleMasterDataSelectors[j].propertyTypeId == 11 || vehicleMasterDataSelectors[j].propertyTypeId == 9)
                                    {
                                        //Select Value
                                        ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[1].fieldValue, jObject[vehicleField].ToString());
                                        //Set report
                                        Console.WriteLine("User was able to Select Value");
                                        vehicleFieldReport[step++].SetActualResultFail("");

                                        //Click Vehicle Tab
                                        ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
                                        //Set report
                                        Console.WriteLine("User was able to click vehicle tab");
                                        vehicleFieldReport[step++].SetActualResultFail("");
                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VehicleQueryBuilderPage"));
                                        vehicleStatus = true;



                                    }
                                    else if (vehicleMasterDataSelectors[j].propertyTypeId == 1 || vehicleMasterDataSelectors[j].propertyTypeId == 2 || vehicleMasterDataSelectors[j].propertyTypeId == 3 || vehicleMasterDataSelectors[j].propertyTypeId == 7)
                                    {
                                        //Select Value
                                        ReusableComponents.SendKeys(driver, "XPath", jObject[vehicleField].ToString(), queryBuilderData[1].fieldValue);
                                        //Set report
                                        Console.WriteLine("User was able to Select Value");
                                        vehicleFieldReport[step++].SetActualResultFail("");

                                        //Click Vehicle Tab
                                        ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());
                                        //Set report
                                        Console.WriteLine("User was able to click vehicle tab");
                                        vehicleFieldReport[step++].SetActualResultFail("");
                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "VehicleQueryBuilderPage"));
                                        vehicleStatus = true;


                                    }
                                }
                            }

                        }


                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Form Fields was not selected : " + e);
            }
            return vehicleFieldReport;
        }



        public List<TestReportSteps> SelectCasualtyFormFields(List<QueryBuilder> queryBuilderData, JToken inputjson, string casualtyField)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                casualtyFieldReport = ConfigFile.GetReportFile("SetupCasualtyFieldsinQueryBuilderReport.json");

                var casualtyJsonResult = ReusableComponents.RetrieveMasterDataSelectors("CasualtyDataMasterPageSelector.json");
                var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(casualtyJsonResult);

                for (int i = 0; i < queryBuilderData.Count; i++)
                {

                    for (int j = 0; j < casualtyMasterDataSelectors.Count; j++)
                    {
                        if (queryBuilderData.Count == 3)
                        {




                            if (casualtyMasterDataSelectors[j].selector == queryBuilderData[2].fieldName && casualtyStatus == false)
                            {
                                //Click Casualty Tab
                                ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
                                //Set report
                                Console.WriteLine("User was able to click casualty tab");
                                casualtyFieldReport[step++].SetActualResultFail("");

                                //Select Field
                                ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[2].displayName, jObject[casualtyTabSelector].ToString());
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[2].displayName);
                                //casualtyFieldReport[step++].SetActualResultPass("User is able to select field" + queryBuilderData[2].displayName);
                                casualtyFieldReport[step++].SetActualResultFail("");

                                //Select Query
                                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[casualtyFieldSelector].ToString(), queryBuilderData[2].query);
                                //Set report
                                Console.WriteLine("User was able to select field" + queryBuilderData[2].query);
                                //casualtyFieldReport[step++].SetActualResultPass("User is able to select query" + queryBuilderData[2].query);
                                casualtyFieldReport[step++].SetActualResultFail("");

                                if (casualtyMasterDataSelectors[j].propertyTypeId == 6 || casualtyMasterDataSelectors[j].propertyTypeId == 10 || casualtyMasterDataSelectors[j].propertyTypeId == 11 || casualtyMasterDataSelectors[j].propertyTypeId == 9)
                                {
                                    //Select Value
                                    ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", queryBuilderData[2].fieldValue, jObject[casualtyField].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to select value");
                                    casualtyFieldReport[step++].SetActualResultFail("");

                                    //Click Casualty Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click casualty tab");
                                    casualtyFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CasualtyQueryBuilderPage"));
                                    casualtyStatus = true;

                                }
                                else if (casualtyMasterDataSelectors[j].propertyTypeId == 1 || casualtyMasterDataSelectors[j].propertyTypeId == 2 || casualtyMasterDataSelectors[j].propertyTypeId == 3 || casualtyMasterDataSelectors[j].propertyTypeId == 7)
                                {
                                    //Select Value
                                    ReusableComponents.SendKeys(driver, "XPath", jObject[casualtyField].ToString(), queryBuilderData[2].fieldValue);
                                    //Set report
                                    Console.WriteLine("User was able to select value");


                                    //Click Casualty Tab
                                    ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());
                                    //Set report
                                    Console.WriteLine("User was able to click casualty tab");
                                    casualtyFieldReport[step++].SetActualResultFail("");
                                    //Capture screenshot
                                    screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CasualtyQueryBuilderPage"));
                                    casualtyStatus = true;

                                }

                            }
                        }

                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Form Fields was not selected : " + e);
            }
            return casualtyFieldReport;
        }



        /// <summary>
        /// Apply Query
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> ApplyQuery(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                applyQueryReport = ConfigFile.GetReportFile("ApplyQueryBuilderReport.json");

                //Click on Apply Query Button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[applyQuery].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[applyQuery].ToString());

                //Set report
                Console.WriteLine("User was able to click apply query button");
                applyQueryReport[step++].SetActualResultFail("");
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ApplyQueryButton"));

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.webLoader);
                bool status = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[appliedQuery].ToString(), inputjson[appliedQuery].ToString());
                if (status == true)
                {
                    applyQueryReport[step++].SetActualResultFail("");

                }
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ApplyQuery"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return applyQueryReport;




        }

        /// <summary>
        /// Clear Applied Query
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> ClearQueryBuilder(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                listOfReport = ConfigFile.GetReportFile("ClearAppliedQueryReport.json");

                //Click on reset query icon
                ReusableComponents.Click(driver, "XPath", jObject[resetQuery].ToString());

                //Set report
                Console.WriteLine("User was able to click on reset query button");
                listOfReport[step++].SetActualResultFail("");
                //Capture screenshot
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "ResetQueryBuilderPage"));


                //Click on yes button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[yesButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[yesButton].ToString());

                //Set report
                Console.WriteLine("User was able to click on yes button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                bool status = ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[buildQuery].ToString(), inputjson[buildQuery].ToString());
                if (status == true)
                {
                    listOfReport[step++].SetActualResultFail("");
                }


                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "QueryBuilderPageAfterclearingQuery"));

            }
            catch (Exception e)
            {
                Console.WriteLine("Navigate to Query Builder Screen Failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Close query builder page
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> CloseQueryBuilderPage()
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                listOfReport = ConfigFile.GetReportFile("CloseQueryBuilderPageReport.json");

                //Click on close icon
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[closeButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[closeButton].ToString());

                //Set report
                Console.WriteLine("User was able to click on close button");
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("User was able to click close button : " + e);
            }
            return listOfReport;
        }




        /// <summary>
        /// Retrieve list of screenshots captured
        /// </summary>
        /// <returns></returns>
        public List<string> GetQueryBuilderPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
