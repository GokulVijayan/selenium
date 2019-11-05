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
    class SCAConfigurationPage
    {

        IWebDriver driver;
        int step = 0;
        List<TestReportSteps> listOfReport;
        public List<string> screenshotList = new List<string>();
        JObject jObject;

        public static bool isToastVerified = false;

        private string fixedvalue = "fixedvalue";
        private string mobile = "mobile";
        private string averageSpeed = "averageSpeed";
        private string redLight = "redLight";
        private string lengthMin = "lengthMin";
        private string lengthMax = "lengthMax";
        private string ksi = "ksi";
        private string pic = "pic";
        private string urban = "urban";
        private string rural= "rural";
        private string savedFixed = "savedFixed";
        private string lengthMinMobile = "lengthMinMobile";
        private string lengthMaxMobile = "lengthMaxMobile";
        private string ksiMobile = "ksiMobile";
        private string picMobile = "picMobile";
        private string ruralMobile = "ruralMobile";
        private string urbanMobile = "urbanMobile";
        private string saveMobile = "saveMobile";
        private string lengthMinAverageSpeed = "lengthMinAverageSpeed";
        private string lengthMaxAverageSpeed = "lengthMaxAverageSpeed";
        private string ksiAverageSpeed = "ksiAverageSpeed";
        private string picAverageSpeed = "picAverageSpeed";
        private string urbanAverageSpeed = "urbanAverageSpeed";
        private string ruralAverageSpeed = "ruralAverageSpeed";
        private string saveAverageSpeed = "saveAverageSpeed";
        private string ksiRedLight = "ksiRedLight";
        private string majorJunction = "majorJunction";
        private string minorJunction = "minorJunction";
        private string saveRedLight = "saveRedLight";
        private string verifyScaToastMessage = "verifyScaToastMessage";

        public SCAConfigurationPage(IWebDriver driver)
        {
            this.driver = driver;
            jObject = ConfigFile.RetrieveUIMap("SCAConfigurationPageSelector.json");
        }


        /// <summary>
        /// Verify SCA Configuration Screen
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> VerifySCAConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("VerifySCAConfigurationPageReport.json");

                //Verify SCA Configuration Page
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[fixedvalue].ToString());
                listOfReport[step++].SetActualResultFail("");

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Toast Msg"));


            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }




        /// <summary>
        /// Edit Fixed Camera SCA Configuration
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditFixedCameraSCAConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditSCAConfigurationForFixedCameraReport.json");

                //Click on Fixed Tab
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[fixedvalue].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[fixedvalue].ToString());

                //Set report
                Console.WriteLine("User was able to click Fixed Tab");
                listOfReport[step++].SetActualResultFail("");
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Enter Min Length
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[lengthMin].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[lengthMin].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[lengthMin].ToString(), inputjson[lengthMin].ToString());

                //Set report
                Console.WriteLine("User was able to enter min length");
                listOfReport[step++].SetActualResultFail("");


                //Enter Max Length
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[lengthMax].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[lengthMax].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[lengthMax].ToString(), inputjson[lengthMax].ToString());

                //Set report
                Console.WriteLine("User was able to enter max length");
                listOfReport[step++].SetActualResultFail("");


                //Enter KSI Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[ksi].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[ksi].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[ksi].ToString(), inputjson[ksi].ToString());

                //Set report
                Console.WriteLine("User was able to enter KSI Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter PIC Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[pic].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[pic].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[pic].ToString(), inputjson[pic].ToString());

                //Set report
                Console.WriteLine("User was able to enter PIC Value");
                listOfReport[step++].SetActualResultFail("");

                //Enter Urban Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[urban].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[urban].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[urban].ToString(), inputjson[urban].ToString());

                //Set report
                Console.WriteLine("User was able to enter Urban Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter Rural Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[rural].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[rural].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[rural].ToString(), inputjson[rural].ToString());

                //Set report
                Console.WriteLine("User was able to enter Rural Value");
                listOfReport[step++].SetActualResultFail("");


                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[savedFixed].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[savedFixed].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterFixedValues"));

                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyScaToastMessage].ToString());
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
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Edit Average Speed Camera SCA Configuration
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditAverageSpeedCameraSCAConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditSCAConfigurationForAverageSpeedCameraReport.json");

                //Click on average Speed Tab
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[averageSpeed].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[averageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to click average Speed Tab");
                listOfReport[step++].SetActualResultFail("");
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);


                //Enter Min Length
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[lengthMinAverageSpeed].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[lengthMinAverageSpeed].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[lengthMinAverageSpeed].ToString(), inputjson[lengthMinAverageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to enter min length");
                listOfReport[step++].SetActualResultFail("");


                //Enter Max Length
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[lengthMaxAverageSpeed].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[lengthMaxAverageSpeed].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[lengthMaxAverageSpeed].ToString(), inputjson[lengthMaxAverageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to enter max length");
                listOfReport[step++].SetActualResultFail("");


                //Enter KSI Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[ksiAverageSpeed].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[ksiAverageSpeed].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[ksiAverageSpeed].ToString(), inputjson[ksiAverageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to enter KSI Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter PIC Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[picAverageSpeed].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[picAverageSpeed].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[picAverageSpeed].ToString(), inputjson[picAverageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to enter PIC Value");
                listOfReport[step++].SetActualResultFail("");

                //Enter Urban Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[urbanAverageSpeed].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[urbanAverageSpeed].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[urbanAverageSpeed].ToString(), inputjson[urbanAverageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to enter Urban Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter Rural Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[ruralAverageSpeed].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[ruralAverageSpeed].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[ruralAverageSpeed].ToString(), inputjson[ruralAverageSpeed].ToString());

                //Set report
                Console.WriteLine("User was able to enter Rural Value");
                listOfReport[step++].SetActualResultFail("");


                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveAverageSpeed].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveAverageSpeed].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterFixedValues"));

                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyScaToastMessage].ToString());
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
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }





        /// <summary>
        /// Edit Mobile Camera SCA Configuration
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditMobileCameraSCAConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditSCAConfigurationForMobileCameraReport.json");

                //Click on mobile Tab
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[mobile].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[mobile].ToString());

                //Set report
                Console.WriteLine("User was able to click Fixed Tab");
                listOfReport[step++].SetActualResultFail("");
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);



                //Enter Min Length
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[lengthMinMobile].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[lengthMinMobile].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[lengthMinMobile].ToString(), inputjson[lengthMinMobile].ToString());

                //Set report
                Console.WriteLine("User was able to enter min length");
                listOfReport[step++].SetActualResultFail("");


                //Enter Max Length
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[lengthMaxMobile].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[lengthMaxMobile].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[lengthMaxMobile].ToString(), inputjson[lengthMaxMobile].ToString());

                //Set report
                Console.WriteLine("User was able to enter max length");
                listOfReport[step++].SetActualResultFail("");


                //Enter KSI Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[ksiMobile].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[ksiMobile].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[ksiMobile].ToString(), inputjson[ksiMobile].ToString());

                //Set report
                Console.WriteLine("User was able to enter KSI Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter PIC Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[picMobile].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[picMobile].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[picMobile].ToString(), inputjson[picMobile].ToString());

                //Set report
                Console.WriteLine("User was able to enter PIC Value");
                listOfReport[step++].SetActualResultFail("");

                //Enter Urban Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[urbanMobile].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[urbanMobile].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[urbanMobile].ToString(), inputjson[urbanMobile].ToString());

                //Set report
                Console.WriteLine("User was able to enter Urban Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter Rural Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[ruralMobile].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[ruralMobile].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[ruralMobile].ToString(), inputjson[ruralMobile].ToString());

                //Set report
                Console.WriteLine("User was able to enter Rural Value");
                listOfReport[step++].SetActualResultFail("");


                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveMobile].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveMobile].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterMobileValues"));

                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyScaToastMessage].ToString());
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Mobile Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }


        /// <summary>
        /// Edit Red Light Camera SCA Configuration
        /// </summary>
        /// <param name="inputjson"></param>
        /// <returns></returns>
        public List<TestReportSteps> EditRedLightCameraSCAConfiguration(JToken inputjson)
        {
            try
            {
                step = 0;
                screenshotList.Clear();

                listOfReport = ConfigFile.GetReportFile("EditSCAConfigurationForRedLightCameraReport.json");

                //Click on red Light Tab
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[redLight].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[redLight].ToString());

                //Set report
                Console.WriteLine("User was able to click red Light Tab");
                listOfReport[step++].SetActualResultFail("");
                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);
              
                //Enter KSI Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[ksiRedLight].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[ksiRedLight].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[ksiRedLight].ToString(), inputjson[ksiRedLight].ToString());

                //Set report
                Console.WriteLine("User was able to enter KSI Value");
                listOfReport[step++].SetActualResultFail("");


             
                //Enter Urban Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[majorJunction].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[majorJunction].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[majorJunction].ToString(), inputjson[majorJunction].ToString());

                //Set report
                Console.WriteLine("User was able to enter Urban Value");
                listOfReport[step++].SetActualResultFail("");


                //Enter Rural Value
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[minorJunction].ToString());
                ReusableComponents.Clear(driver, "XPath", jObject[minorJunction].ToString());
                ReusableComponents.SendKeys(driver, "XPath", jObject[minorJunction].ToString(), inputjson[minorJunction].ToString());

                //Set report
                Console.WriteLine("User was able to enter Rural Value");
                listOfReport[step++].SetActualResultFail("");


                //Click save button
                ReusableComponents.WaitUntilElementVisible(driver, "XPath", jObject[saveRedLight].ToString());
                ReusableComponents.Click(driver, "XPath", jObject[saveRedLight].ToString());

                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "EnterRedLightValues"));

                //Set report
                Console.WriteLine("User was able to click save button");
                listOfReport[step++].SetActualResultFail("");


                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.loader);

                //Verify toast message
                isToastVerified = ReusableComponents.RetrieveAndCompareText(driver, "XPath", Constant.toastSelector, inputjson[verifyScaToastMessage].ToString());
                //Capture screenshot
                screenshotList.Add(CaptureScreenshot.TakeSingleSnapShot(driver, "Mobile Toast Msg"));

                if (isToastVerified)
                {
                    listOfReport[step++].SetActualResultFail("");
                }

                ReusableComponents.WaitUntilElementInvisible(driver, "XPath", Constant.toastSelector);
            }
            catch (Exception e)
            {
                Console.WriteLine("Login failed : " + e);
            }
            return listOfReport;
        }



        /// <summary>
        /// Returns the screenshotlist
        /// </summary>
        /// <returns></returns>
        public List<string> GetSCAConfigurationPageScreenshots()
        {
            return screenshotList;
        }
    }
}
