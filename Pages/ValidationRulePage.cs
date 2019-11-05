using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.APITestScript;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FrameworkSetup.Pages
{
    internal class ValidationRulePage
    {
        private IWebDriver driver;
        private int step = 0;
        private List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        private JObject jObject;
        public static string retrieveCrashRefNo = string.Empty;


        private readonly string addNewBtn = "addNewBtn",
                                surnameDropdown = "surnameDropdown",
                                blankDropdown = "blankDropdown",
                                textArea = "textArea",
                                saveBtn = "saveBtn";


        private readonly string Surname = "Surname",
                                Blank = "Blank",
                                Message = "Message";


        public ValidationRulePage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("ValidationRulePageSelector.json");
        }

        public List<TestReportSteps> AddValidationRule(JToken inputJson)
        {
            screenshotList.Clear();
            step = 0;

            listOfReport = ConfigFile.GetReportFile("SaveValidationRuleReport.json");
            Thread.Sleep(Constant.waitTimeoutForExport);
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            CheckForToaster();

            ReusableComponents.JEClick(driver, "XPath", jObject[addNewBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            CheckForToaster();
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

            ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[surnameDropdown].ToString(), inputJson[Surname].ToString());
            ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[blankDropdown].ToString(), inputJson[Blank].ToString());
            ReusableComponents.SendKeys(driver, "XPath", jObject[textArea].ToString(), inputJson[Message].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.JEClick(driver, "XPath", jObject[saveBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            return listOfReport;
        }

        public List<TestReportSteps> DeleteValidationRule(JToken inputJson)
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("DeleteValidationRuleReport.json");

            var rules = ValidationRule.GetValidationRule();
            listOfReport[step++].SetActualResultFail("");
            foreach (var rule in rules.data)
            {
                if (rule.ruleMessages.FirstOrDefault().message == inputJson[Message].ToString())
                {
                    var result = ValidationRule.DeleteValidationRule(rule.ruleMessages.FirstOrDefault().ruleId.ToString());
                    if (result.data)
                    {
                        listOfReport[step++].SetActualResultFail("");
                    }
                    break;
                }
            }
            return listOfReport;
        }

        private void CheckForToaster()
        {
            if (ReusableComponents.FindIfElementExists(driver, "XPath", Constant.toastSelector))
            {
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            }
        }
    }
}