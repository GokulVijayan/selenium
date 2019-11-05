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
    class CBAConfigurationPage
    {

        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        JObject jObject;
        public static bool isToastVerified = false;

        private string generalConfiguration = "generalConfiguration";
        private string crashCostConfiguration = "crashCostConfiguration";
        private string valueConversion = "valueConversion";
        private string baseYear = "baseYear";
        private string numberOfAnalysisYear = "numberOfAnalysisYear";
        private string discountFactor = "discountFactor";
        private string defaultEffectivness = "defaultEffectivness";
        private string defaultAnnualMaintenanceCost = "defaultAnnualMaintenanceCost";
        private string damageOnly = "damageOnly";
        private string generalConfigurationSave = "generalConfigurationSave";
        private string crashCostYear = "crashCostYear";
        private string slightCost = "slightCost";
        private string seriousCost = "seriousCost";
        private string fatalCost = "fatalCost";
        private string damageCost = "damageCost";
        private string crashCostSave = "crashCostSave";
        private string gdpDeflatorValue = "gdpDeflatorValue";
        private string realGdpGrowth = "realGdpGrowth";
        private string saveValueConversion = "saveValueConversion";
        private string verifyCbaToastMessage = "verifyCbaToastMessage";

        public CBAConfigurationPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("CBAConfigurationPageSelector.json");
        }


        /// <summary>
        /// Verify CBA Configuration Screen
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> VerifyCBAConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("VerifyCBAConfigurationPageReport.json");

                //Verify SCA Configuration Page
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[generalConfiguration].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CBA Configuration"));


            }
            catch (Exception e)
            {
                Console.WriteLine("CBA Verification failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Edit CBA General Configuration 
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditCBAGeneralConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditCBAConfigurationForGeneralReport.json");

                //Click General Configuration Tab
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[generalConfiguration].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[generalConfiguration].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Base Year
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[baseYear].ToString());
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[baseYear].ToString(), inputjson[baseYear].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Number of Analysis Year
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[numberOfAnalysisYear].ToString());
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[numberOfAnalysisYear].ToString(), inputjson[numberOfAnalysisYear].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Discount Factor
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[discountFactor].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[discountFactor].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[discountFactor].ToString(), inputjson[discountFactor].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Default Effectiveness
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[defaultEffectivness].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[defaultEffectivness].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[defaultEffectivness].ToString(), inputjson[defaultEffectivness].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Annual Maintenance cost
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[defaultAnnualMaintenanceCost].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[defaultAnnualMaintenanceCost].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[defaultAnnualMaintenanceCost].ToString(), inputjson[defaultAnnualMaintenanceCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Damage only to injury ratio
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[damageOnly].ToString());
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[damageOnly].ToString(), inputjson[damageOnly].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Base Year
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[baseYear].ToString());
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[baseYear].ToString(), inputjson[baseYear].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CBA Configuration"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[generalConfigurationSave].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[generalConfigurationSave].ToString());


                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyCbaToastMessage].ToString());
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);


            }
            catch (Exception e)
            {
                Console.WriteLine("Edit CBA Generation Configuration failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Edit CBA Crash Cost Configuration 
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditCBACrashCostConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditCBAConfigurationForCrashCostReport.json");

                //Click Crash Cost Configuration Tab
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashCostConfiguration].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[crashCostConfiguration].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Year
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashCostYear].ToString());
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[crashCostYear].ToString(), inputjson[crashCostYear].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Slight Cost
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[slightCost].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[slightCost].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[slightCost].ToString(), inputjson[slightCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Serious Cost
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[seriousCost].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[seriousCost].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[seriousCost].ToString(), inputjson[seriousCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Fatal Cost
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[fatalCost].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[fatalCost].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[fatalCost].ToString(), inputjson[fatalCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Damage Only cost
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[damageCost].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[damageCost].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[damageCost].ToString(), inputjson[damageCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CBA Configuration"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[crashCostSave].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[crashCostSave].ToString());


                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyCbaToastMessage].ToString());
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);


            }
            catch (Exception e)
            {
                Console.WriteLine("Edit CBA Configuration for Crash Count failed : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Edit CBA Value Conversion Configuration 
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditCBAValueConversionConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditCBAConfigurationForValueConversionReport.json");

                //Click Value Conversion Configuration Tab
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[valueConversion].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[valueConversion].ToString());
                listOfReport[step++].SetActualResultFail("");


                //Enter GDP Deflator Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[gdpDeflatorValue].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[gdpDeflatorValue].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[gdpDeflatorValue].ToString(), inputjson[gdpDeflatorValue].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter Real GDP Growth 
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[realGdpGrowth].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[realGdpGrowth].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[realGdpGrowth].ToString(), inputjson[realGdpGrowth].ToString());
                listOfReport[step++].SetActualResultFail("");
               
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CBA Configuration"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveValueConversion].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveValueConversion].ToString());


                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyCbaToastMessage].ToString());
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);


            }
            catch (Exception e)
            {
                Console.WriteLine("Edit CBA Configuration for Value Conversion failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Returns the screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetCBAConfigurationPageScreenshots()
        {
            return screenshotList;
        }
    }
}
