
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace FrameworkSetup.Pages
{
    class UserDefinedReportPage
    {

        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        readonly JObject jObject;
        public List<string> screenshotList = new List<string>();

        private string verifyReportPageTitle = "verifyReportPageTitle";
        private string filterOption = "filterOption";
        private string checkValidRecords = "checkValidRecords";
        private string fromDate = "fromDate";
        private string toDate = "toDate";
        private string applyButton = "applyButton";
        private string crashTab = "crashTab";
        private string rows = "rows";
        private string columns = "columns";
        private string runButton = "runButton";
        private string severityOfCrash = "severityOfCrash";
        private string crashDate = "crashDate";
        private string collisionType = "collisionType";
        private string weatherCondition = "weatherCondition";
        private string saveReport = "saveReport";
        private string okButton = "okButton";
        private string pdfFormat = "pdfFormat";
        private string excelFormat = "excelFormat";
        private string htmlFormat = "htmlFormat";
        private string wordFormat = "wordFormat";
        private string documentFormat = "documentFormat";
        private string dataFormat = "dataFormat";
        private string verifyReportHeading = "verifyReportHeading";
        private string reportHeading = "reportHeading";
        private string crashSeverity = "crashSeverity";
        private string templateHeader = "templateHeader";
        private string templateName = "templateName";
        private string templateNameInList = "templateNameInList";
        private string saveTemplate = "saveTemplate";
        private string save = "save";
        private string templateSaveToastMessage = "templateSaveToastMessage";
        private string templateDeleteToastMessage = "templateDeleteToastMessage";
        private string openTemplate = "openTemplate";
        private string openTemplateFromList = "openTemplateFromList";
        private string deleteTemplate = "deleteTemplate";
        private string yesButton = "yesButton";
        private string templateType = "templateType";



        public UserDefinedReportPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("UserDefinedReportPageSelector.json");
        }

        /// <summary>
        /// Verify web application user defined report page displayed
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportPage()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyUserDefinedReport.json");
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
        /// Apply filter in user defined report
        /// </summary>
        public List<TestReportSteps> ApplyFilterInUserDefinedReport()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("ApplyFilterInUserDefinedReport.json");
            try
            {
                //Click filter option
                ReusableComponents.Click(driver, "XPath", jObject[filterOption].ToString());
                Console.WriteLine("User was able to click on filter option");
                listOfReport[step++].SetActualResultFail("");

                //Check 'Valid Records'
                ReusableComponents.Click(driver, "XPath", jObject[checkValidRecords].ToString());
                Console.WriteLine("User was able to click on valid records option");
                listOfReport[step++].SetActualResultFail("");

                //Select 'From date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[fromDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                Console.WriteLine("User was able to select from date from calender");
                listOfReport[step++].SetActualResultFail("");

                //Select 'To date'
                ReusableComponents.SelectDateFromCalender(driver, "XPath", jObject[toDate].ToString(), DateTime.Now.ToString(Constant.calenderPickerDateFormat));
                Console.WriteLine("User was able to select to date from calender");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Apply filter"));

                //Click 'Apply' button
                ReusableComponents.Click(driver, "XPath", jObject[applyButton].ToString());
                Console.WriteLine("User was able to click on apply button");
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to apply filter in user defined report.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Run user defined report
        /// </summary>
        public List<TestReportSteps> RunUserDefinedReport(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("RunUserDefinedReport.json");
            try
            {
                //Click on crash tab
                ReusableComponents.Click(driver,"XPath", jObject[crashTab].ToString());
                Console.WriteLine("User was able to click on crash tab");
                listOfReport[step++].SetActualResultFail("");

                //Drag and drop severity of crash field to row section
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[severityOfCrash].ToString(), jObject[rows].ToString());
                Console.WriteLine("User was able to drag and drop severity of crash to row section");
                listOfReport[step++].SetActualResultFail("");

                //Drag and drop collision type field to column section
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[collisionType].ToString());
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[collisionType].ToString(), jObject[rows].ToString());
                Console.WriteLine("User was able to drag and drop collision type to row section");
                listOfReport[step++].SetActualResultFail("");


                //Drag and drop crash date field to row section
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[crashDate].ToString());
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[crashDate].ToString(), jObject[columns].ToString());
                Console.WriteLine("User was able to drag and drop crash date to columns section");
                listOfReport[step++].SetActualResultFail("");

                //Drag and drop weather condition field to column section
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[weatherCondition].ToString());
                ReusableComponents.DragAndDrop(driver, "XPath", jObject[weatherCondition].ToString(), jObject[columns].ToString());
                Console.WriteLine("User was able to drag and drop weather condition to columns section");
                listOfReport[step++].SetActualResultFail("");

                //Click on run button
                ReusableComponents.Click(driver, "XPath", jObject[runButton].ToString());
                Console.WriteLine("User was able to click on run button");
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
              
            }
            catch (Exception e)
            {
                Console.WriteLine("User was not able to run user defined report"+e);
            }
            return listOfReport;
        }




        /// <summary>
        /// Verify export report functionality to excel format
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportExportToExcel()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("UserDefinedPageVerifyExportToExcelReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportHeading].ToString());
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
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to PDF format
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportExportToPDF()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("UserDefinedPageVerifyExportToPDFReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportHeading].ToString());
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
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Verify export report functionality to Word format
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportExportToWord()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("UserDefinedPageVerifyExportToWordReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportHeading].ToString());
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
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to HTML format
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportExportToHTML()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("UserDefinedPageVerifyExportToHtmlReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportHeading].ToString());
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
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to Document format
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportExportToDocument()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("UserDefinedPageVerifyExportToDocumentReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportHeading].ToString());
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
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify export report functionality to Document format
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReportExportToDataFile()
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("UserDefinedPageVerifyExportToDataFileReport.json");
            try
            {

                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportHeading].ToString());
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
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }

        /// <summary>
        /// Retrieve Crash Count
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public int GetCrashCount(JToken inputjson)
        {
            int count = 0,reportCount=15;
            for (int i=1;i<=Constant.severityCount+Constant.collisionCount;i++)
            {
                int rowCount = reportCount + i;
                string path = jObject[reportHeading].ToString() + rowCount + "]//td[1]";
                string text = ReusableComponents.RetrieveText(driver, "XPath", path);
                if (text.Contains(inputjson[crashSeverity].ToString()))
                {
                    //count = "2";
                    for (int j = 1; j <= Constant.collisionCount; j++)
                    {
                        string collisionText= ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + rowCount + "]//td["+j+"]");
                        if (collisionText.Contains(inputjson[collisionType].ToString()))
                        {
                            for (int k = 1; k < Constant.weatherConditionCount; k++)
                            {
                                string weatherConditionText = ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + reportCount + "]//td[" + k + "]");
                                if (weatherConditionText.Contains(inputjson[weatherCondition].ToString()))
                                {
                                    int p = k + 2;
                                    string crashCount= ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + rowCount + "]//td[" + p + "]");
                                    count = Convert.ToInt32(ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + rowCount + "]//td[" + p + "]"));
                                    return count;
                                }
                            }
                        }
                    }
                }

            }



            return count;
            

        }




        /// <summary>
        /// Retrieve Query Builder Crash Count
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public int GetQueryBuilderCrashCount(JToken inputjson)
        {
            int count = 0, reportCount =11;
            for (int i = 1; i <= Constant.severityCount + Constant.collisionCount; i++)
            {
                int rowCount = reportCount + i;
                string path = jObject[reportHeading].ToString() + rowCount + "]//td[1]";
                string text = ReusableComponents.RetrieveText(driver, "XPath", path);
                if (text.Contains(inputjson[crashSeverity].ToString()))
                {
                    //count = "2";
                    for (int j = 1; j <= Constant.collisionCount; j++)
                    {
                        string collisionText = ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + rowCount + "]//td[" + j + "]");
                        if (collisionText.Contains(inputjson[collisionType].ToString()))
                        {
                            for (int k = 1; k < Constant.weatherConditionCount; k++)
                            {
                                string weatherConditionText = ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + reportCount + "]//td[" + k + "]");
                                if (weatherConditionText.Contains(inputjson[weatherCondition].ToString()))
                                {
                                    int p = k + 2;
                                    string crashCount = ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + rowCount + "]//td[" + p + "]");
                                    count = Convert.ToInt32(ReusableComponents.RetrieveText(driver, "XPath", jObject[reportHeading].ToString() + rowCount + "]//td[" + p + "]"));
                                    return count;
                                }
                            }
                        }
                    }
                }

            }



            return count;


        }





        /// <summary>
        /// Verify User Defined Report
        /// </summary>
        public List<TestReportSteps> VerifyUserDefinedReport(int crashCount,int updatedCrashCount)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("VerifyGeneratedUserDefinedReport.json");
            try
            {

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyReportPageTitle].ToString());
                listOfReport[step++].SetActualResultFail("");


                if(updatedCrashCount==crashCount+1)
                listOfReport[step++].SetActualResultFail("");

              
            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify User Defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify add and delete template functionality
        /// </summary>
        public List<TestReportSteps> AddAndDeleteTemplateInUserDefinedPage(JToken inputjson)
        {
            step = 0;
            screenshotList.Clear();
            listOfReport = ConfigFile.GetReportFile("AddAndDeleteTemplateInUserDefinedPageReport.json");
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

                
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
              

            }
            catch (Exception e)
            {
                Console.WriteLine("No able to verify add and delete template functionality in user defined report page.Exception caught : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Retrieves user defined report page screenshots
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserDefinedReportPageScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
