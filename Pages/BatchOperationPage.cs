using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FrameworkSetup.Pages
{
    internal class BatchOperationPage
    {
        private readonly IWebDriver driver;
        private int step = 0;
        private List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        private readonly JObject jObject;
        private CasualtyPage _casualtyPage;


        private readonly string filterBtn = "filterBtn",
                                crashRefNoInput = "crashRefNoInput",
                                searchBtn = "searchBtn",
                                table = "table",
                                selectCheckBox = "selectCheckBox",
                                validationBtn = "validationBtn",
                                canvas = "canvas",
                                validRecCount = "validRecCount";

        public BatchOperationPage(IWebDriver driver)
        {
            this.driver = driver;
            _casualtyPage = new CasualtyPage(this.driver);
            jObject = ConfigFile.RetrieveUIMap("BatchOperationPageSelector.json");
        }

        public List<TestReportSteps> ValidateCrash()
        {
            screenshotList.Clear();
            step = 0;
            listOfReport = ConfigFile.GetReportFile("BatchOperationReport.json");

            CheckForReportValidity();

            if (ReusableComponents.RetrieveAndCompareText(driver, "XPath", jObject[validRecCount].ToString(), "1"))
            {
                listOfReport[step++].SetActualResultFail("");
            }

            //driver.Navigate().Refresh();

            //CheckColorValidity();

            return listOfReport;
        }

        public void ReloadPage()
        {
            driver.Navigate().Refresh();

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[canvas].ToString());
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
        }

        private void CheckForReportValidity()
        {
            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());
            CheckForToaster();

            ReusableComponents.JEClick(driver, "XPath", jObject[filterBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.SendKeys(driver, "XPath", jObject[crashRefNoInput].ToString(), _casualtyPage.GetCrashRefNo());
            //ReusableComponents.SendKeys(driver, "XPath", jObject[crashRefNoInput].ToString(), "20191001111");
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);

            ReusableComponents.JEClick(driver, "XPath", jObject[searchBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            CheckForToaster();

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());
            Thread.Sleep(1000);
            ReusableComponents.JEClick(driver, "XPath", jObject[selectCheckBox].ToString());
            listOfReport[step++].SetActualResultFail("");

            CheckForToaster();

            ReusableComponents.JEClick(driver, "XPath", jObject[validationBtn].ToString());
            listOfReport[step++].SetActualResultFail("");

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
        }

        private void CheckColorValidity()
        {
            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            CheckForToaster();

            ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[table].ToString());
            CheckForToaster();

            ReusableComponents.JEClick(driver, "XPath", jObject[filterBtn].ToString());

            ReusableComponents.SendKeys(driver, "XPath", jObject[crashRefNoInput].ToString(), _casualtyPage.GetCrashRefNo());

            CheckForToaster();

            ReusableComponents.JEClick(driver, "XPath", jObject[searchBtn].ToString());

            ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
            CheckForToaster();
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