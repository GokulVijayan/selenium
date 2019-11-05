
using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
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
        List<TestReportSteps> listOfReport;
        JObject jObject;
        public List<string> screenshotList = new List<string>();

        private string queryBuilderIcon = "queryBuilderIcon";
        private string crashTab = "crashTab";
        private string querySelector = "querySelector";
        private string queryBuilderType = "queryBuilderType";
        private string noOfVehicles = "noOfVehicles";
        private string queryCondition = "queryCondition";

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
                listOfReport = ConfigFile.GetReportFile("NavigateToQueryBuilderPageReport.json");

                //Click on query builder icon
                ReusableComponents.Click(driver, "XPath", jObject[queryBuilderIcon].ToString());

                //Set report
                Console.WriteLine("User was able to click on query builder button");
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.webLoader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashTab].ToString());
                //Set report
                Console.WriteLine("User was able to click on query builder button");
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "QueryBuilderPage"));
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Navigate to Query Builder Screen Failed : " + e);
            }
            return listOfReport;
        }

        public List<TestReportSteps> SelectFormFields(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                listOfReport = ConfigFile.GetReportFile("SelectFormFieldsFromQueryBuilderPageReport.json");

                //Click on query builder icon
                ReusableComponents.ClickQueryBuilderType(driver, "XPath", inputjson[queryBuilderType].ToString());

                //Set report
                Console.WriteLine("User was able to select query builder type");
                listOfReport[step].SetStepDescription(listOfReport[step].GetStepDescription() + inputjson[queryBuilderType].ToString());
                listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + inputjson[queryBuilderType].ToString());
                listOfReport[step].SetActualResultPass(listOfReport[step].GetActualResultPass() + inputjson[queryBuilderType].ToString());
                listOfReport[step++].SetActualResultFail("");
                

                ReusableComponents.SelectFromQueryBuilderDropdown(driver, "XPath", inputjson[noOfVehicles].ToString());
                //Set report
                Console.WriteLine("User was able to select form field");
                listOfReport[step].SetStepDescription(listOfReport[step].GetStepDescription() + inputjson[noOfVehicles].ToString());
                listOfReport[step].SetExpectedResult(listOfReport[step].GetExpectedResult() + inputjson[noOfVehicles].ToString());
                listOfReport[step].SetActualResultPass(listOfReport[step].GetActualResultPass() + inputjson[noOfVehicles].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "QueryBuilderApply"));

                //Click on query builder icon
                ReusableComponents.ClickQueryBuilderType(driver, "XPath", inputjson[queryBuilderType].ToString());

                //Set report
                Console.WriteLine("User was able to deselect query builder type");
                listOfReport[step++].SetActualResultFail("");



            }
            catch (Exception e)
            {
                Console.WriteLine("Form Fields was not selected : " + e);
            }
            return listOfReport;
        }


        public List<TestReportSteps> SelectQueryCriteria(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();
                listOfReport = ConfigFile.GetReportFile("SelectQueryCriteriaFromQueryBuilderPageReport.json");

                //Select query option
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", querySelector, inputjson[queryCondition].ToString());
                //Set report
                Console.WriteLine("User was able to select query condition");
                listOfReport[step++].SetActualResultFail("");


            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
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
