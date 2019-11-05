

using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FrameworkSetup.Pages
{
    class SummaryPrintReportPage
    {
        RestClient client;
        string authToken;
        readonly JsonDeserializer deserialize = new JsonDeserializer();
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public string selector;
        public List<string> screenshotList = new List<string>();
        List<FormFieldData> crashData = new List<FormFieldData>();
        List<FormFieldData> vehicleData = new List<FormFieldData>();
        List<FormFieldData> casualtyData = new List<FormFieldData>();


        private string verifyReportPageTitle = "verifyReportPageTitle";
        private string filterOption = "filterOption";
        private string checkValidRecords = "checkValidRecords";
        private string fromDate = "fromDate";
        private string toDate = "toDate";
        private string applyButton = "applyButton";
        private string crashTab = "crashTab";
        private string crashRefNo = "crashRefNo";
        private string vehicleTab = "vehicleTab";
        private string casualtyTab = "casualtyTab";
        private string runButton = "runButton";
        private string reportPages = "reportPages";
        private string reportTable = "reportTable";
        private string column1 = "column1";
        private string column2 = "column2";
        private string tableTitle = "tableTitle";
        private string crashDetailsTitle = "crashDetailsTitle";
        private string crashIdTitleInCrashDetails = "crashIdTitleInCrashDetails";
        private string crashRefNoTitleInCrashDetails = "crashRefNoTitleInCrashDetails";
        private string vehicleDetailsTitle = "vehicleDetailsTitle";
        private string crashIdTitleInVehicleDetails = "crashIdTitleInVehicleDetails";
        private string casualtyDetailsTitle = "casualtyDetailsTitle";
        private string crashIdTitleInCasualtyDetails = "crashIdTitleInCasualtyDetails";
        private string saveReport = "saveReport";
        private string pdfFormat = "pdfFormat";
        private string okButton = "okButton";
        private string crashIdHeader = "crashIdHeader";
        private string saveTemplate = "saveTemplate";
        private string templateName = "templateName";
        private string save = "save";
        private string templateHeader = "templateHeader";
        private string deleteTemplate = "deleteTemplate";
        private string openTemplate = "openTemplate";
        private string templateNameInList = "templateNameInList";
        private string openTemplateFromList = "openTemplateFromList";
        private string yesButton = "yesButton";
        private string closeButton = "closeButton";
        private string templateSaveToastMessage = "templateSaveToastMessage";
        private string templateDeleteToastMessage = "templateDeleteToastMessage";
        private string templateType = "templateType";
        private string crashTabFields = "crashTabFields";
        private string vehicleTabFields = "vehicleTabFields";
        private string casualtyTabFields = "casualtyTabFields";
        private string excelFormat = "excelFormat";
        private string htmlFormat = "htmlFormat";
        private string wordFormat = "wordFormat";
        private string documentFormat = "documentFormat";
        private string dataFormat = "dataFormat";
        private string casualtyDetails = "casualtyDetails";
        private string column3 = "column3";
        private string reportTableSelector = "reportTableSelector";



        public SummaryPrintReportPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("SummaryPrintReportPageSelector.json");
        }

        /// <summary>
        /// Verify web application summary print report page displayed
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifySummaryPrintReport.json");
            try
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportPageTitle].ToString());
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Run summary print report 
        /// </summary>
        public List<TestReportSteps>  RunSummaryPrintReportPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RunSummaryPrintReport.json");
            try
            {
                //Click 'Run' button
                ReusableComponents.Click(driver, "XPath", jObject[runButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Apply filter and run summary print report
        /// </summary>
        public List<TestReportSteps> ApplyFilterAndRunReport()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyFilterAndRunSummaryPrintReport.json");
            try
            {
                //Click filter option
                ReusableComponents.Click(driver, "XPath", jObject[filterOption].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Check 'Valid Records'
                ReusableComponents.Click(driver, "XPath", jObject[checkValidRecords].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

                //Click 'Apply' button
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                listOfReport[step++].SetActualResultFail("");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Verify generated summary print report details 
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyGeneratedSummaryPrintReport(JToken inputjson, string CrashRefNo)
        {

            step = 0;
            screenshotList.Clear();
            int reportTableCount, reportPageCount, tableCounter, innerCounter = 0;
            bool isCrashRefNoFound = false, IsCrashTableVerified = false, IsVehicleTableVerified = false, isCasualtyTableVerified = false;
            bool isCrashTitleVerified = false, isVehicleTitleVerified = false, isCasualtyTitleVerified = false;
            string crashId = string.Empty, title1Selector, title2Selector;
            listOfReport = ConfigFile.GetReportFile("VerifyGeneratedSummaryPrintReport.json");
            try
            {
                Constant.waitTimeout = 5;
                reportPageCount = ReusableComponents.ElementsCount(driver, "XPath", jObject[reportPages].ToString());
                for (int pageCounter = 1; pageCounter <= reportPageCount; pageCounter++)
                {
                    string pageSelector = jObject[reportPages].ToString() + "[" + pageCounter + "]" + jObject[reportTable].ToString();
                    reportTableCount = ReusableComponents.ElementsCount(driver, "XPath", pageSelector);
                    tableCounter = 1;
                    innerCounter = 1;
                    if (!IsCrashTableVerified)
                    {
                        //Verify table 'Crash Details'
                        for (tableCounter = 1; tableCounter <= reportTableCount; tableCounter++)
                        {
                            selector = pageSelector + "[" + tableCounter + "]" + jObject[tableTitle].ToString();

                            if (!isCrashTitleVerified)
                            {
                                try
                                {
                                    if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selector, inputjson[crashDetailsTitle].ToString()))
                                    {
                                        Console.WriteLine("User was able to verify title 'Crash Details'");
                                        listOfReport[step++].SetActualResultFail("");
                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Crash Details"));

                                        tableCounter += 2;
                                        title1Selector = pageSelector + "[" + tableCounter + "]" + jObject[column1].ToString();
                                        title2Selector = pageSelector + "[" + tableCounter + "]" + jObject[column3].ToString();

                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title1Selector, inputjson[crashIdTitleInCrashDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash Id' in Crash Details");
                                            listOfReport[step++].SetActualResultFail("");

                                        }
                                        else
                                        {
                                            step++;
                                        }
                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title2Selector, inputjson[crashRefNoTitleInCrashDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash ref. No' in Crash Details");
                                            listOfReport[step++].SetActualResultFail("");
                                            isCrashTitleVerified = true;
                                        }
                                        else
                                        {
                                            
                                            step++;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("title not found in loop" + e);
                                }
                            }

                            if (isCrashTitleVerified)
                            {
                                //Verify crash ref no
                                for (innerCounter = tableCounter; innerCounter <= reportTableCount; innerCounter++)
                                {
                                    string selectorCrashRefNo = pageSelector + "[" + innerCounter + "]" + jObject[column3].ToString();
                                    try
                                    {
                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selectorCrashRefNo, CrashRefNo))
                                        {
                                            crashId = ReusableComponents.RetrieveText(driver, "XPath", pageSelector + "[" + innerCounter + "]" + jObject[column1].ToString());
                                            isCrashRefNoFound = true;

                                            ReusableComponents.ScrollToElement(driver, "XPath", pageSelector + "[" + innerCounter + "]" + jObject[column1].ToString());
                                            break;
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Crash ref no not verified" + e);
                                    }
                                }


                                if (isCrashRefNoFound)
                                {
                                    Console.WriteLine("User was able to verify 'Crash ref. No' in Crash Details");
                                    listOfReport[step++].SetActualResultFail("");

                                    IsCrashTableVerified = true;
                                    break;
                                }
                                else
                                {
                                    IsCrashTableVerified = false;
                                    break;
                                }
                            }

                        }
                    }

                    if (IsCrashTableVerified && (!IsVehicleTableVerified))
                    {
                        //Verify table 'Vehicle Details'
                        for (tableCounter = innerCounter; tableCounter <= reportTableCount; tableCounter++)
                        {
                            selector = pageSelector + "[" + tableCounter + "]" + jObject[tableTitle].ToString();
                            if (!isVehicleTitleVerified)
                            {
                                try
                                {
                                    if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selector, inputjson[vehicleDetailsTitle].ToString()))
                                    {
                                        Console.WriteLine("User was able to verify title 'Vehicle Details'");
                                        listOfReport[step++].SetActualResultFail("");

                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Vehicle Details"));

                                        tableCounter += 2;
                                        title1Selector = pageSelector + "[" + tableCounter + "]" + jObject[column1].ToString();
                                        title2Selector = pageSelector + "[" + tableCounter + "]" + jObject[column2].ToString();

                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title1Selector, inputjson[crashIdTitleInVehicleDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash Id' in Vehicle Details");
                                            listOfReport[step++].SetActualResultFail("");
                                            isVehicleTitleVerified = true;
                                            IsVehicleTableVerified = true;

                                        }
                                        else
                                        {
                                            step++;
                                        }
        
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Vehicle details not found" + e);
                                }
                            }
                            

                            
                        }
                    }

                    if (IsVehicleTableVerified && (!isCasualtyTableVerified))
                    {
                        ReusableComponents.ScrollToElement(driver, "XPath", jObject[casualtyDetails].ToString());
                        //Verify table 'Casualty Details'
                        for (tableCounter = innerCounter; tableCounter <= reportTableCount; tableCounter++)
                        {
                            selector = pageSelector + "[" + tableCounter + "]" + jObject[tableTitle].ToString();

                            if (!isCasualtyTitleVerified)
                            {
                                try
                                {
                                    if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", selector, inputjson[casualtyDetailsTitle].ToString()))
                                    {
                                        Console.WriteLine("User was able to verify title 'Casualty Details'");
                                        listOfReport[step++].SetActualResultFail("");

                                        //Capture screenshot
                                        screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Casualty Details"));

                                        tableCounter += 2;
                                        title1Selector = pageSelector + "[" + tableCounter + "]" + jObject[column1].ToString();
                                        title2Selector = pageSelector + "[" + tableCounter + "]" + jObject[column2].ToString();

                                        if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", title1Selector, inputjson[crashIdTitleInCasualtyDetails].ToString()))
                                        {
                                            Console.WriteLine("User was able to verify title 'Crash Id' in Casualty Details");
                                            listOfReport[step++].SetActualResultFail("");
                                            isCasualtyTitleVerified = true;
                                            isCasualtyTableVerified = true;
                                        }
                                        else
                                        {
                                            step++;
                                        }
                                        
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Casualty title not verified in loop " + e);
                                }
                            }

                        

                        }
                    }

                    if (IsCrashTableVerified && IsVehicleTableVerified && isCasualtyTableVerified)
                    {
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Not able to verify summary print report generated.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Verify export report functionality to excel format
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportExportToExcel()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SummaryPageVerifyExportToExcelReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashIdHeader].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[excelFormat].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[excelFormat].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[excelFormat].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());
                listOfReport[step++].SetActualResultFail("");



                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".xlsx"))
                    listOfReport[step++].SetActualResultFail("");




            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to PDF format
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportExportToPDF()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SummaryPageVerifyExportToPDFReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashIdHeader].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[pdfFormat].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[pdfFormat].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[pdfFormat].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());
                listOfReport[step++].SetActualResultFail("");



                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".pdf"))
                    listOfReport[step++].SetActualResultFail("");


            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Verify export report functionality to Word format
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportExportToWord()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SummaryPageVerifyExportToWordReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashIdHeader].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[wordFormat].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[wordFormat].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[wordFormat].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());
                listOfReport[step++].SetActualResultFail("");



                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".docx"))
                    listOfReport[step++].SetActualResultFail("");




            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to HTML format
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportExportToHTML()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SummaryPageVerifyExportToHtmlReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashIdHeader].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[htmlFormat].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[htmlFormat].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[htmlFormat].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());
                listOfReport[step++].SetActualResultFail("");



                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".html"))
                    listOfReport[step++].SetActualResultFail("");





            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to Document format
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportExportToDocument()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SummaryPageVerifyExportToDocumentReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashIdHeader].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[documentFormat].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[documentFormat].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[documentFormat].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());
                listOfReport[step++].SetActualResultFail("");



                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".mdc"))
                    listOfReport[step++].SetActualResultFail("");





            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to Document format
        /// </summary>
        public List<TestReportSteps> VerifySummaryPrintReportExportToDataFile()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("SummaryPageVerifyExportToDataFileReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashIdHeader].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[dataFormat].ToString());
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[dataFormat].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[dataFormat].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".csv"))
                    listOfReport[step++].SetActualResultFail("");




            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        public List<TestReportSteps> SelectFieldsFromSummaryPrintReport(string testDataFile)
        {

            screenshotList.Clear();
            List<TestReportSteps> sampleReport = new List<TestReportSteps>();
            TestReportSteps addReport = new TestReportSteps();
            int stepCount = 0;


            client = new RestClient(Constant.imaapUrl);
            string apiName = "/api/authenticate/mobile";
            var loginRequest = new RestRequest(apiName, Method.POST);
            loginRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
            var login = new { username = "Gokul", password = "Trl@12345" };
            loginRequest.RequestFormat = DataFormat.Json;
            loginRequest.AddJsonBody(login);
            IRestResponse loginResponse = client.Execute(loginRequest);
            var output = deserialize.Deserialize<SampleMasterData>(loginResponse);
            authToken = output?.Data;

            string apiNameMaster = "/api/formfields";
            var getFormFieldRequest = new RestRequest(apiNameMaster, Method.GET);
            getFormFieldRequest.RequestFormat = DataFormat.Json;
            getFormFieldRequest.AddHeader("Content-type", "application/json");
            getFormFieldRequest.AddHeader("x-bif-private-token", authToken);
            getFormFieldRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
            IRestResponse getFormFieldResponse = client.Execute(getFormFieldRequest);
            var getFormFieldResponseDto = deserialize.Deserialize<FormFields>(getFormFieldResponse);
           
            //Adding API response to List
            for (int i = 1; i < getFormFieldResponseDto.data.Count; i++)
            {
                if (getFormFieldResponseDto.data[i].formId == 1 && getFormFieldResponseDto.data[i].isVisibleInARF == true)
                {
                    var a = new FormFieldData()
                    {

                        name = getFormFieldResponseDto.data[i].formFieldName[0].name,
                        propertyName = getFormFieldResponseDto.data[i].propertyName
                    };
                    crashData.Add(a);
                }
                else if (getFormFieldResponseDto.data[i].formId == 2 && getFormFieldResponseDto.data[i].isVisibleInARF == true)
                {
                    var a = new FormFieldData()
                    {

                        name = getFormFieldResponseDto.data[i].formFieldName[0].name,
                        propertyName = getFormFieldResponseDto.data[i].propertyName
                    };
                    vehicleData.Add(a);
                }
                else if (getFormFieldResponseDto.data[i].formId == 3 && getFormFieldResponseDto.data[i].isVisibleInARF == true)
                {
                    var a = new FormFieldData()
                    {

                        name = getFormFieldResponseDto.data[i].formFieldName[0].name,
                        propertyName = getFormFieldResponseDto.data[i].propertyName
                    };
                    casualtyData.Add(a);
                }
            }


            //Click 'Crash' tab
            ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());


            addReport.testObjective = "Verify that user is able to select form fields from crash,vehicle,casualty tabs";

            //retrieve master data
            JArray crashmasterData = ReusableComponents.RetrieveMasterData("CrashDataMasterPageSelector.json");
            JArray testData = ConfigFile.RetrieveInputTestData(testDataFile);
            string testInput = string.Empty;


            //Click 'Crash Reference Number'
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashRefNo].ToString());
            ReusableComponents.Click(driver, "XPath", jObject[crashRefNo].ToString());



            //Select Fields from Crash Tab
            foreach (JToken data in crashmasterData)
            {

                string uiSelector = data["apiKey"].ToString();
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

                        for(int i=0;i<crashData.Count;i++)
                        {
                            if (crashData[i].propertyName == uiSelector)
                            {
                                selector = crashData[i].name;
                                break;
                            }

                        }
                        stepCount++;
                        //Set report
                        addReport.stepName = stepCount.ToString();
                        addReport.stepDescription = "Select"+" '" + data["apiKey"].ToString() + "'";
                        addReport.expectedResult = ("User is able to " + "Select" + " '" + data["apiKey"].ToString() + "'");
                        addReport.actualResultPass = ("User was able to " + "Select" + " '" + data["apiKey"].ToString() + "'");


                        ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", selector, jObject[crashTabFields].ToString());
                        addReport.SetActualResultFail("");

                        sampleReport.Add(addReport);
                        addReport = new TestReportSteps();

                    }
                    catch (Exception e)
                    {
                        listOfReport[stepCount].SetActualResultFail("Failed");
                        Console.WriteLine("Not able to perform action" + e);
                        break;
                    }
                }

            }

            //Click 'Crash' tab
            ReusableComponents.Click(driver, "XPath", jObject[crashTab].ToString());

            //Click 'Vehicle' tab
            ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());


            //retrieve master data
            JArray vehiclemasterData = ReusableComponents.RetrieveMasterData("VehicleDataMasterPageSelector.json");
            JArray vehicletestData = ConfigFile.RetrieveInputTestData(testDataFile);
            string vehicletestInput = string.Empty;



            //Select Fields from vehicle Tab
            foreach (JToken data in vehiclemasterData)
            {

                string uiSelector = data["apiKey"].ToString();
                foreach (JObject dataInput in vehicletestData)
                {
                    string check = data["selector"].ToString();

                    if (dataInput.ContainsKey(check))
                        vehicletestInput = dataInput[check].ToString();
                    else
                        vehicletestInput = string.Empty;



                }
                if (vehicletestInput.Length > 0)
                {
                    try
                    {

                        for (int i = 0; i < vehicleData.Count; i++)
                        {
                            if (vehicleData[i].propertyName == uiSelector)
                            {
                                selector = vehicleData[i].name;
                                break;
                            }

                        }
                        stepCount++;
                        //Set report
                        addReport.stepName = stepCount.ToString();
                        addReport.stepDescription = "Select" + " '" + data["apiKey"].ToString() + "'";
                        addReport.expectedResult = ("User is able to " + "Select" + " '" + data["apiKey"].ToString() + "'");
                        addReport.actualResultPass = ("User was able to " + "Select" + " '" + data["apiKey"].ToString() + "'");


                        ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", selector, jObject[vehicleTabFields].ToString());
                        addReport.SetActualResultFail("");

                        sampleReport.Add(addReport);
                        addReport = new TestReportSteps();

                    }
                    catch (Exception e)
                    {
                        listOfReport[stepCount].SetActualResultFail("Failed");
                        Console.WriteLine("Not able to perform action" + e);
                        break;
                    }

                }

               
            }

            //Click 'Vehicle' tab
            ReusableComponents.Click(driver, "XPath", jObject[vehicleTab].ToString());

            //Click 'Casualty' tab
            ReusableComponents.Click(driver, "XPath", jObject[casualtyTab].ToString());


            //retrieve master data
            JArray casualtymasterData = ReusableComponents.RetrieveMasterData("CasualtyDataMasterPageSelector.json");
            JArray casualtytestData = ConfigFile.RetrieveInputTestData(testDataFile);
            string casualtytestInput = string.Empty;

            //Select Fields from casualty Tab
            foreach (JToken data in casualtymasterData)
            {

                string uiSelector = data["apiKey"].ToString();
                foreach (JObject dataInput in casualtytestData)
                {
                    string check = data["selector"].ToString();

                    if (dataInput.ContainsKey(check))
                        casualtytestInput = dataInput[check].ToString();
                    else
                        casualtytestInput = string.Empty;



                }
                if (casualtytestInput.Length > 0)
                {
                    try
                    {

                        for (int i = 0; i < casualtyData.Count; i++)
                        {
                            if (casualtyData[i].propertyName == uiSelector)
                            {
                                selector = casualtyData[i].name;
                                break;
                            }

                        }
                        stepCount++;
                        //Set report
                        addReport.stepName = stepCount.ToString();
                        addReport.stepDescription = "Select" + " '" + data["apiKey"].ToString() + "'";
                        addReport.expectedResult = ("User is able to " + "Select" + " '" + data["apiKey"].ToString() + "'");
                        addReport.actualResultPass = ("User was able to " + "Select" + " '" + data["apiKey"].ToString() + "'");


                        ReusableComponents.SelectFromSummaryPrintDropdown(driver, "XPath", selector, jObject[casualtyTabFields].ToString());
                        addReport.SetActualResultFail("");

                        sampleReport.Add(addReport);
                        addReport = new TestReportSteps();

                    }
                    catch (Exception e)
                    {
                        listOfReport[stepCount].SetActualResultFail("Failed");
                        Console.WriteLine("Not able to perform action" + e);
                        break;
                    }

                }


            }


            return sampleReport;

        }



        /// <summary>
        /// Verify add and delete template functionality
        /// </summary>
        public List<TestReportSteps> AddAndDeleteTemplateInSummaryPrintPage(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("AddAndDeleteTemplateInSummaryPrintPageReport.json");
            try
            {
                //Click on save template button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveTemplate].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveTemplate].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on save template button");


                //Enter template name
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[templateName].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[templateName].ToString(), inputjson[templateName].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to enter template name");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Template Name"));

                //Click on save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[save].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[save].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on save button");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Save Template"));

                //Verify Toast Message
                if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[templateSaveToastMessage].ToString()))
                    listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to verify toast message");

                //Verify template header
                if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[templateHeader].ToString(), inputjson[templateName].ToString() + " - " + inputjson[templateType].ToString()))
                        listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to verify template header");


                //Click on open template button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[openTemplate].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[openTemplate].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on open template button");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Open Template"));

                //Select template name
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[templateNameInList].ToString());
                ReusableComponents.SelectFromTemplatesDropdown(driver, "XPath", jObject[templateNameInList].ToString(), inputjson[templateName].ToString() + " - " + inputjson[templateType].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to select template name");


                //Click on open template button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[openTemplateFromList].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[openTemplateFromList].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on open template button");

                //Click on delete template button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteTemplate].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[deleteTemplate].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on delete template button");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Delete Template"));

                //Click on yes button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[yesButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[yesButton].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on yes button");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Yes"));

                //Verify toast message
                if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[templateDeleteToastMessage].ToString()))
                    listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to verify toast message");

                //Click on close button
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[closeButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[closeButton].ToString());
                listOfReport[step++].SetActualResultFail("");
                Console.WriteLine("Able to click on close button");

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify add and delete template functionality in summary print report page.Exception caught : " + e);
            }
            return listOfReport;
        }








        /// <summary>        /// Verify export report functionality to Document format        /// </summary>        public List<TestReportSteps> VerifyReportExportToDocument(string reportType)        {            step = 0;            screenshotList.Clear();            listOfReport = ConfigFile.GetReportFile("ReportVerifyExportToDocumentReport.json");
            listOfReport[0].SetTestObjective("Verify that " + reportType + " export to Document functionality is working as expected");
            try            {                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[reportTableSelector].ToString());                ReusableComponents.ScrollToElement(driver, "XPath", jObject[saveReport].ToString());                ReusableComponents.JEClick(driver, "XPath", jObject[saveReport].ToString());                listOfReport[step++].SetActualResultFail("");                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[documentFormat].ToString());                ReusableComponents.ScrollToElement(driver, "XPath", jObject[documentFormat].ToString());                ReusableComponents.JEClick(driver, "XPath", jObject[documentFormat].ToString());                listOfReport[step++].SetActualResultFail("");                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[okButton].ToString());                ReusableComponents.JEClick(driver, "XPath", jObject[okButton].ToString());                listOfReport[step++].SetActualResultFail("");                Thread.Sleep(Constant.waitTimeoutForExport);

                //Verify exported details
                bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
                if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".mdc"))
                    listOfReport[step++].SetActualResultFail("");            }            catch (Exception e)            {                Console.WriteLine("No able to verify summary print report page.Exception caught : " + e);            }            return listOfReport;        }



    


    /// <summary>
    /// Retrieves summary print report page screenshots
    /// </summary>
    /// <returns></returns>
        public List<string> GetSummaryPrintReportPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }

        
        
    }
}
