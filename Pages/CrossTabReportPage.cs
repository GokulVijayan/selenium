using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FrameworkSetup.Pages
{
    class CrossTabReportPage
    {
        readonly IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        readonly JObject jObject;
        string cellValue, grandTotal1, grandTotal2;

        private string attributesTab = "attributesTab";
        private string crashTab = "crashTab";
        private string crashOption = "crashOption";
        private string vehicleTab = "vehicleTab";
        private string vehicleOption = "vehicleOption";
        private string casualtyTab = "casualtyTab";
        private string casualtyOption = "casualtyOption";
        private string checkboxCrashTab = "checkboxCrashTab";
        private string checkboxVehicleTab = "checkboxVehicleTab";
        private string checkboxCasualtyTab = "checkboxCasualtyTab";
        private string checkbox2 = "checkbox2";
        private string tableRow1 = "tableRow1";
        private string tableRow2 = "tableRow2";
        private string tableColumn1 = "tableColumn1";
        private string tableColumn2 = "tableColumn2";
        private string tableValue1 = "tableValue1";
        private string tableValue2 = "tableValue2";
        private string tableValue3 = "tableValue3";
        private string runBtn = "runBtn";
        private string drivingPosition = "drivingPosition";
        private string driverService = "driverService";
        private string grandTotal = "grandTotal";
        private string exportButton = "exportButton";
        private string expandCrossTabMenu = "expandCrossTabMenu";
        private string collapseCrossTabMenu = "collapseCrossTabMenu";
        private string verifyMenu = "verifyMenu";

        public CrossTabReportPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("CrossTabReportPageSelector.json");
        }

        /// <summary>
        /// Select options and run cross tab report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> RunCrossTabReport(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;

            listOfReport = ConfigFile.GetReportFile("CheckAndRunCrossTabReport.json");

            //Click Attributes tab
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            ReusableComponents.JEClick(driver, "XPath", jObject[attributesTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Click option from crash tab
            ReusableComponents.JEClick(driver, "XPath", jObject[crashTab].ToString());
            ReusableComponents.SelectCheckBoxValue(driver, "XPath", jObject[checkboxCrashTab].ToString(), jObject[checkbox2].ToString(), inputjson[crashOption].ToString());
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Selected crash form field"));

            ReusableComponents.JEClick(driver, "XPath", jObject[crashTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Click option from vehicle tab
            ReusableComponents.JEClick(driver, "XPath", jObject[vehicleTab].ToString());
            ReusableComponents.SelectCheckBoxValue(driver, "XPath", jObject[checkboxVehicleTab].ToString(), jObject[checkbox2].ToString(), inputjson[vehicleOption].ToString());
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Selected vehicle form field"));

            ReusableComponents.JEClick(driver, "XPath", jObject[vehicleTab].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Click option from casualty tab
            ReusableComponents.JEClick(driver, "XPath", jObject[casualtyTab].ToString());
            ReusableComponents.SelectCheckBoxValue(driver, "XPath", jObject[checkboxCasualtyTab].ToString(), jObject[checkbox2].ToString(), inputjson[casualtyOption].ToString());

            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Selected casualty form field"));
            listOfReport[step++].SetActualResultFail("");

            //Click 'Run' button
            ReusableComponents.Click(driver, "XPath", jObject[runBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            return listOfReport;
        }

        /// <summary>
        /// Tap 'Run' button
        /// </summary>
        public List<TestReportSteps> TapRunButton()
        {
            screenshotList.Clear();
            step = 0;

            listOfReport = ConfigFile.GetReportFile("RunCrossTabReport.json");
            
            //Click 'Run' button
            ReusableComponents.Click(driver, "XPath", jObject[runBtn].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            listOfReport[step++].SetActualResultFail("");

            return listOfReport;

        }

        /// <summary>
        /// Tap side menu to expand
        /// </summary>
        public void ExpandCrossTabMenu()
        {
            //Click 'CrossTab' menu
            ReusableComponents.Click(driver, "XPath", jObject[expandCrossTabMenu].ToString());
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[verifyMenu].ToString());
         
        }

        /// <summary>
        /// Tap side menu to collapse
        /// </summary>
        public void CollapseCrossTabMenu()
        {
            //Click 'CrossTab' menu
            ReusableComponents.Click(driver, "XPath", jObject[collapseCrossTabMenu].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", jObject[verifyMenu].ToString());

        }

        /// <summary>
        /// Retrieve cross tab report data
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> RetrieveCrossTabReportData(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            int rowIndex, columnIndex, grandTotalRow, grandTotalColumn;
            listOfReport = ConfigFile.GetReportFile("RetrieveCrossTabReportData.json");

            //Tap menu
            CollapseCrossTabMenu();

            //Retrieve cell index position
            rowIndex = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableRow1].ToString(), jObject[tableRow2].ToString(), inputjson[drivingPosition].ToString());
            grandTotalRow = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableRow1].ToString(), jObject[tableRow2].ToString(), inputjson[grandTotal].ToString());
            columnIndex = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableColumn1].ToString(), jObject[tableColumn2].ToString(), inputjson[driverService].ToString());
            grandTotalColumn = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableColumn1].ToString(), jObject[tableColumn2].ToString(), inputjson[grandTotal].ToString());

            //Retrieve cell value
            if (rowIndex == 0||columnIndex == 0)
                cellValue = "0";
            else
                cellValue = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + rowIndex + "]" + jObject[tableValue2].ToString() + "[" + columnIndex + "]" + jObject[tableValue3].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Retrieve grand total value
            if (rowIndex == 0)
                grandTotal1 = "0";
            else
                grandTotal1 = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + rowIndex + "]" + jObject[tableValue2].ToString() + "[" + grandTotalColumn + "]" + jObject[tableValue3].ToString());

            grandTotal2 = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + grandTotalRow + "]" + jObject[tableValue2].ToString() + "[" + grandTotalColumn + "]" + jObject[tableValue3].ToString());
            listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Cross Tab Report"));

            //Tap menu
            ExpandCrossTabMenu();

            return listOfReport;
        }

        /// <summary>
        /// Verify updated data in cross tab report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> VerifyCrossTabReport(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            string newcellValue, newgrandTotal1, newgrandTotal2;
            int rowIndex, columnIndex, grandTotalRow, grandTotalColumn;
            listOfReport = ConfigFile.GetReportFile("VerifyCrossTabReport.json");

            //Tap menu
            CollapseCrossTabMenu();

            //Retrieve cell index position
            rowIndex = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableRow1].ToString(), jObject[tableRow2].ToString(), inputjson[drivingPosition].ToString());
            grandTotalRow = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableRow1].ToString(), jObject[tableRow2].ToString(), inputjson[grandTotal].ToString());
            columnIndex = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableColumn1].ToString(), jObject[tableColumn2].ToString(), inputjson[driverService].ToString());
            grandTotalColumn = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableColumn1].ToString(), jObject[tableColumn2].ToString(), inputjson[grandTotal].ToString());

            //Retrieve cell value
            newcellValue = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + rowIndex + "]" + jObject[tableValue2].ToString() + "[" + columnIndex + "]" + jObject[tableValue3].ToString());
            cellValue = Convert.ToString(Convert.ToInt32(cellValue) + 1);

            if (ReusableComponents.CompareText(cellValue, newcellValue))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Retrieve grand total value
            newgrandTotal1 = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + rowIndex + "]" + jObject[tableValue2].ToString() + "[" + grandTotalColumn + "]" + jObject[tableValue3].ToString());
            newgrandTotal2 = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + grandTotalRow + "]" + jObject[tableValue2].ToString() + "[" + grandTotalColumn + "]" + jObject[tableValue3].ToString());
            grandTotal1 = Convert.ToString(Convert.ToInt32(grandTotal1) + 1);
            grandTotal2 = Convert.ToString(Convert.ToInt32(grandTotal2) + 1);

            if (ReusableComponents.CompareText(grandTotal1, newgrandTotal1)&&ReusableComponents.CompareText(grandTotal2,newgrandTotal2))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Verify Cross Tab Report"));

            //Tap menu
            ExpandCrossTabMenu();

            return listOfReport;
        }

        /// <summary>
        /// Export cross tab report
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> ExportCrossTabReport()
        {
            screenshotList.Clear();
            step = 0;

            listOfReport = ConfigFile.GetReportFile("ExportCrossTabReport.json");
            
            //Tap Export button
            ReusableComponents.JEClick(driver, "XPath", jObject[exportButton].ToString());
            listOfReport[step++].SetActualResultFail("");

            Thread.Sleep(Constant.waitTimeoutForExport);

            //Verify exported details
            bool reportExportStatus = ReusableComponents.CheckFileDownloaded(ReusableComponents.RetrieveFileName());
            if (reportExportStatus == true && Path.GetExtension(ReusableComponents.RetrieveFileName()).Contains(".xlsx"))
                listOfReport[step++].SetActualResultFail("");

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Export Cross Tab Report"));

            return listOfReport;
        }

        /// <summary>
        /// Verify result of applied query
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyAppliedQueryResult(JToken inputjson)
        {
            screenshotList.Clear();
            step = 0;
            int rowIndex, columnIndex, grandTotalRow, grandTotalColumn;
            string resultingCellValue, resultingGrandTotal1, resultingGrandTotal2;

            listOfReport = ConfigFile.GetReportFile("VerifyCrossTabQueryResult.json");
            listOfReport[step].SetStepDescription(listOfReport[step].GetStepDescription() + " " + inputjson[driverService].ToString() + ".");
            listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + " " + inputjson[driverService].ToString() + ".");
            listOfReport[step].SetActualResultPass(listOfReport[step].GetExpectedResult() + " " + inputjson[driverService].ToString() + ".");

            //Tap menu
            CollapseCrossTabMenu();

            //Verify column and Retrieve cell index position
            rowIndex = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableRow1].ToString(), jObject[tableRow2].ToString(), inputjson[drivingPosition].ToString());
            listOfReport[step++].SetActualResultFail("");

            grandTotalRow = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableRow1].ToString(), jObject[tableRow2].ToString(), inputjson[grandTotal].ToString());
            columnIndex = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableColumn1].ToString(), jObject[tableColumn2].ToString(), inputjson[driverService].ToString());
            grandTotalColumn = ReusableComponents.FindElementIndex(driver, "XPath", jObject[tableColumn1].ToString(), jObject[tableColumn2].ToString(), inputjson[grandTotal].ToString());

            //Retrieve and verify cell value
            resultingCellValue = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + rowIndex + "]" + jObject[tableValue2].ToString() + "[" + columnIndex + "]" + jObject[tableValue3].ToString());

            if (ReusableComponents.CompareText(cellValue, resultingCellValue))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Retrieve and verify grand total value
            resultingGrandTotal1 = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + rowIndex + "]" + jObject[tableValue2].ToString() + "[" + grandTotalColumn + "]" + jObject[tableValue3].ToString());
            resultingGrandTotal2 = ReusableComponents.RetrieveText(driver, "XPath", jObject[tableValue1].ToString() + "[" + grandTotalRow + "]" + jObject[tableValue2].ToString() + "[" + grandTotalColumn + "]" + jObject[tableValue3].ToString());
       
            if (ReusableComponents.CompareText(cellValue, resultingGrandTotal1) && ReusableComponents.CompareText(cellValue, resultingGrandTotal2))
                listOfReport[step++].SetActualResultFail("");
            else
                step++;

            //Capture screenshot
            screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Query Result - Cross Tab Report"));

            //Tap menu
            ExpandCrossTabMenu();

            return listOfReport;
        }

        /// <summary>
        /// Returns cross tab report page screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetCrossTabScreenshots()
        {
            List<string> result = screenshotList;
            return result;
        }
    }
}
