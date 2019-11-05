
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
    class CounterMeasureLibraryPage
    {

        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        JObject jObject;
        public static bool isToastVerified = false;
        public static string counterMeasureDetailsName,possibleCauseForCrash;

        private string counterMeasureHeader= "counterMeasureHeader";
        private string counterMeasuresTab = "counterMeasuresTab";
        private string possibleCauseTab = "possibleCauseTab";
        private string linkingScreenTab = "linkingScreenTab";
        private string addCMC = "addCMC";
        private string counterMeasureCategory = "counterMeasureCategory";
        private string saveCounterMeasureCategory = "saveCounterMeasureCategory";
        private string addCounterMeasureDetails = "addCounterMeasureDetails";
        private string counterMeasureName = "counterMeasureName";
        private string counterMeasureCategoryDropDown = "counterMeasureCategoryDropDown";
        private string source = "source";
        private string deleteCounterMeasure = "deleteCounterMeasure";
        private string yesButton = "yesButton";
        private string addPossibleCause = "addPossibleCause";
        private string possibleCause = "possibleCause";
        private string possibleCasuseDescription = "possibleCasuseDescription";
        private string savePossibleCause = "savePossibleCause";
        private string closePossibleCause = "closePossibleCause";
        private string add = "add";
        private string possibleCauseDropDown = "possibleCauseDropDown";
        private string counterMeasureDropDown = "counterMeasureDropDown";
        private string saveLink = "saveLink";
        private string closeAddLink = "closeAddLink";
        private string counterMeasureCategorySelector = "counterMeasureCategorySelector";
        private string counterMeasureHeadingSelector = "counterMeasureHeadingSelector";
        private string verifyCounterMeasureDeleteToastMessage = "verifyCounterMeasureDeleteToastMessage";
        private string verifyCounterMeasureDetailsSaveToastMessage = "verifyCounterMeasureDetailsSaveToastMessage";
        private string verifyCounterMeasureSaveToastMessage = "verifyCounterMeasureSaveToastMessage";
        private string verifyLinkingSaveToastMessage = "verifyLinkingSaveToastMessage";
        private string verifyPossibleCauseSaveToastMessage = "verifyPossibleCauseSaveToastMessage";
        private string crashType = "crashType";
        private string applicableArea = "applicableArea";
        private string estimatedConstructionCost = "estimatedConstructionCost";
        private string impactedCrashSeverity = "impactedCrashSeverity";
        private string unitOfMeasure = "unitOfMeasure";
        private string knownMaintenanceCost = "knownMaintenanceCost";
        private string applicableControlType = "applicableControlType";
        private string treatmentLifeTime = "treatmentLifeTime";
        private string applicableTreatmentType = "applicableTreatmentType";
        private string estimatedEffectivness = "estimatedEffectivness";
        private string applicableForCba = "applicableForCba";


        public CounterMeasureLibraryPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("CounterMeasurePageSelector.json");
        }


        /// <summary>
        /// Save Counter Measure Details
        /// </summary>
        /// <input>inputjson</input>
        /// <returns></returns>
        public List<TestReportSteps> SaveCounterMeasureDetails(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("AddCounterMeasureDetailsReport.json");

                //Click on add cm button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addCounterMeasureDetails].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[addCounterMeasureDetails].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                listOfReport[step++].SetActualResultFail("");

                ReusableComponents.Click(driver, "XPath", jObject[applicableForCba].ToString());
                //Enter counter measure details name
                counterMeasureDetailsName = inputjson[counterMeasureName].ToString() + ReusableComponents.getRandomNumber(4);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[counterMeasureName].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[counterMeasureName].ToString(), counterMeasureDetailsName);
                listOfReport[step++].SetActualResultFail("");

                //Select applicable area type
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[applicableArea].ToString(), inputjson[applicableArea].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter estimated construction cost
                ReusableComponents.SendKeys(driver, "XPath", jObject[estimatedConstructionCost].ToString(), inputjson[estimatedConstructionCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select impacted crash severity
                ReusableComponents.MultiSelect(driver, "XPath", jObject[impactedCrashSeverity].ToString(), inputjson[impactedCrashSeverity].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select unit of measure
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[unitOfMeasure].ToString(), inputjson[unitOfMeasure].ToString());
                listOfReport[step++].SetActualResultFail("");


                //Select counter measure category
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[counterMeasureCategoryDropDown].ToString(), inputjson[counterMeasureCategory].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter known maintenance cost
                ReusableComponents.Clear(driver, "XPath", jObject[knownMaintenanceCost].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[knownMaintenanceCost].ToString(), inputjson[knownMaintenanceCost].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select applicable control type
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[applicableControlType].ToString(), inputjson[applicableControlType].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter treatment lifetime
                ReusableComponents.SendKeys(driver, "XPath", jObject[treatmentLifeTime].ToString(), inputjson[treatmentLifeTime].ToString());
                listOfReport[step++].SetActualResultFail("");


                //Select applicable treatment type
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[applicableTreatmentType].ToString(), inputjson[applicableTreatmentType].ToString());
                listOfReport[step++].SetActualResultFail("");


                //Enter estimated effectivness
                ReusableComponents.SendKeys(driver, "XPath", jObject[estimatedEffectivness].ToString(), inputjson[estimatedEffectivness].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select Source
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[source].ToString(), inputjson[source].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterCounterMeasureDetails"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveCounterMeasureCategory].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveCounterMeasureCategory].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyCounterMeasureDetailsSaveToastMessage].ToString());

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
                Console.WriteLine("Save Counter Measure Category failed : " + e);
            }
            return listOfReport;
        }




        /// <summary>
        /// Add Link between cause and countermeasure
        /// </summary>
        /// <input>inputjson</input>
        /// <returns></returns>
        public List<TestReportSteps> AddLinkBetweenCauseAndCounterMeasure(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("AddLinkBetweenCauseAndCounterMeasureReport.json");

                //Click on linking screen tab
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[linkingScreenTab].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[linkingScreenTab].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                listOfReport[step++].SetActualResultFail("");

                //Select Cause
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[crashType].ToString(), inputjson[crashType].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Click on add button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[add].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[add].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Select possible cause
                ReusableComponents.SelectFromSelectDropdown(driver, "XPath", jObject[possibleCauseDropDown].ToString(), possibleCauseForCrash);
                listOfReport[step++].SetActualResultFail("");

                //Select countermeasure
                ReusableComponents.SelectFromTemplatesDropdown(driver, "XPath", jObject[counterMeasureDropDown].ToString(), counterMeasureDetailsName);
                listOfReport[step++].SetActualResultFail("");


                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "LinkCounterMeasureDetails"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveLink].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveLink].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyLinkingSaveToastMessage].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
                ReusableComponents.Click(driver, "XPath", jObject[closeAddLink].ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine("Save Counter Measure Category failed : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Verify Counter Measure Library Page Screen
        /// </summary>
        /// <returns></returns>
        public List<TestReportSteps> VerifyCounterMeasureLibraryPage()
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("VerifyCounterMeasurePageReport.json");

                //Verify Counter Measure Page
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[counterMeasureHeader].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "CounterMeasureHeader"));


               

            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Save Counter Measure Category
        /// </summary>
        /// <input>inputjson</input>
        /// <returns></returns>
        public List<TestReportSteps> SaveCounterMeasureCategory(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("AddCounterMeasureCategoryReport.json");

                //Click on add cmc button
                ReusableComponents.Click(driver, "XPath", jObject[counterMeasuresTab].ToString());
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addCMC].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[addCMC].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Enter counter measure category name
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[counterMeasureCategory].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[counterMeasureCategory].ToString(),inputjson[counterMeasureCategory].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterCounterMeasureCategory"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveCounterMeasureCategory].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveCounterMeasureCategory].ToString());
                listOfReport[step++].SetActualResultFail("");

               
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyCounterMeasureSaveToastMessage].ToString());
                
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
                Console.WriteLine("Save Counter Measure Category failed : " + e);
            }
            return listOfReport;
        }



       



        /// <summary>
        /// Delete Counter Measure Category
        /// </summary>
        /// <input>inputjson</input>
        /// <returns></returns>
        public List<TestReportSteps> DeleteCounterMeasureCategory(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("DeleteCounterMeasureCategoryReport.json");

                //Select Counter Measure Category
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[counterMeasuresTab].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[counterMeasuresTab].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.SelectCounterMeasureCategory(driver, "XPath", jObject[counterMeasureCategorySelector].ToString(),jObject[counterMeasureHeadingSelector].ToString(), inputjson[counterMeasureCategory].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Click on delete button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[deleteCounterMeasure].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[deleteCounterMeasure].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Click on yes button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[yesButton].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[yesButton].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "DeleteCounterMeasureCategory"));

              
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyCounterMeasureDeleteToastMessage].ToString());

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
                Console.WriteLine("Delete Counter Measure Category failed : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Add Possible Cause
        /// </summary>
        /// <input>inputjson</input>
        /// <returns></returns>
        public List<TestReportSteps> AddPossibleCause(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("AddPossibleCauseReport.json");

                //Click on Possible Cause Tab
                ReusableComponents.ScrollToElement(driver, "XPath", jObject[possibleCauseTab].ToString());
                ReusableComponents.JEClick(driver, "XPath", jObject[possibleCauseTab].ToString());
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                listOfReport[step++].SetActualResultFail("");

                //Click on Possible Cause Tab
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[addPossibleCause].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[addPossibleCause].ToString());
                listOfReport[step++].SetActualResultFail("");


                //Enter possible cause
                possibleCauseForCrash = inputjson[possibleCause].ToString() + ReusableComponents.getRandomNumber(4); 
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[possibleCause].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[possibleCause].ToString(), possibleCauseForCrash);
                listOfReport[step++].SetActualResultFail("");

                //Enter possible cause description
                
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[possibleCasuseDescription].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[possibleCasuseDescription].ToString(), inputjson[possibleCasuseDescription].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterPossibleCause"));

                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[savePossibleCause].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[savePossibleCause].ToString());
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyPossibleCauseSaveToastMessage].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
                ReusableComponents.Click(driver, "XPath", jObject[closePossibleCause].ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine("Save Counter Measure Category failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Returns the screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetCounterMeasureLibraryPageScreenshots()
        {
            return screenshotList;
        }
    }
}
