using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI.CrashRecords;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    class RetrieveTestData
    {

        /// <summary>
        /// Retrieves test data for crash record
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static CrashRootObject GetCrashDataBody(string fileUniqueName)
        {

            
            var crashInputData = JsonConvert.DeserializeObject<List<CrashRootObject>>(fileUniqueName);
            var crashData = crashInputData.FirstOrDefault();
            return crashData;
        }



        /// <summary>
        /// Retrieves test data for crash record
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static List<CrashRootObject> GetMultipleCrashDataBody(string fileUniqueName)
        {
           
            var crashInputData = JsonConvert.DeserializeObject<List<CrashRootObject>>(fileUniqueName);
            return crashInputData;
        }


        /// <summary>
        /// Retrieves test data for plot crashes
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static List<PlotCrashData> GetMultiplePlotCrashBody(string fileUniqueName)
        {

            var plotCrashData = JsonConvert.DeserializeObject<List<PlotCrashData>>(fileUniqueName);
            return plotCrashData;
        }


        /// <summary>
        /// Retrieves test data for applying query builder
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static List<QueryBuilder> GetQueryBuilderDataBody(string fileUniqueName)
        {

            var queryBuilderInputData = JsonConvert.DeserializeObject<List<QueryBuilder>>(fileUniqueName);
            return queryBuilderInputData;
        }


        /// <summary>
        /// Retrieves test data for exporting status
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static ExportStatus GetExportDataBody(string fileUniqueName)
        {

            var exportData = JsonConvert.DeserializeObject<List<ExportStatus>>(fileUniqueName);
            return exportData.FirstOrDefault();
        }


        /// <summary>
        /// Retrieves test data for user role
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static Users GetUserDataBody(string fileUniqueName)
        {

            var userData = JsonConvert.DeserializeObject<List<Users>>(fileUniqueName);
            return userData.FirstOrDefault();
        }


        /// <summary>
        /// Retrieves test data for verfying safety camera analysis
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static List<SafetyCameraAnalysis> GetSafetyCameraDataBody(string fileUniqueName)
        {

            var queryBuilderInputData = JsonConvert.DeserializeObject<List<SafetyCameraAnalysis>>(fileUniqueName);
            return queryBuilderInputData;
        }


        /// <summary>
        /// Retrieves test data for calculating crash cost
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static CalculateCrashCost GetCalculateCrashCostDataBody(string fileUniqueName)
        {

            var calculateCrashInputData = JsonConvert.DeserializeObject<List<CalculateCrashCost>>(fileUniqueName);
            return calculateCrashInputData.FirstOrDefault();
        }


        /// <summary>
        /// Retrieves test data for calculating cba
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static Cba GetCbaDataBody(string fileUniqueName)
        {

            var cbaInputData = JsonConvert.DeserializeObject<List<Cba>>(fileUniqueName);
            return cbaInputData.FirstOrDefault();
        }


        /// <summary>
        /// Retrieves test data for run cba
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static RunCba GetRunCbaBody(string fileUniqueName)
        {

            var cmInputData = JsonConvert.DeserializeObject<List<RunCba>>(fileUniqueName);
            return cmInputData.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves test data in string format
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static string GetDataBody(string fileUniqueName)
        {

            var fileName = GenerateReport.GetAbsoluteFilePath("TestDataJsonInputs\\") + fileUniqueName;
            string json;
            using (StreamReader reader = File.OpenText(fileName))
            {
                json = File.ReadAllText(fileName);
            }
            return json;
        }




        /// <summary>
        /// Retrieves test script 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string RetrieveTestScripts(string fileName)
        {
            fileName = GenerateReport.GetAbsoluteFilePath("DriverFile\\") + fileName;
            string json = File.ReadAllText(fileName);
            JObject jObject = JObject.Parse(json);
            return jObject.ToString();
        }

        /// <summary>
        /// Retrieves test data for search crash record
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static JObject GetSearchCrashDataBody(string fileUniqueName)
        {

            var fileName = GenerateReport.GetAbsoluteFilePath("TestDataJsonInputs\\") + fileUniqueName;
            string json;
            using (StreamReader reader = File.OpenText(fileName))
            {
                json = File.ReadAllText(fileName);
            }
            JObject jObject = JObject.Parse(json);
            return jObject;
        }



        /// <summary>
        /// Retrieves test data for hotspot analysis
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static IdentifyHotspot GetHotspotAnalysisDataBody(string fileUniqueName)
        {
            var hotspotInputData = JsonConvert.DeserializeObject<List<IdentifyHotspot>>(fileUniqueName);
            return hotspotInputData.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves test data for hotspot report
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static BlackspotReportData GetHotspotReportDataBody(string fileUniqueName)
        {
            var hotspotReportInputData = JsonConvert.DeserializeObject<List<BlackspotReportData>>(fileUniqueName);
            return hotspotReportInputData.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves test data for hotspot analysis
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static KsiDefinition GetKsiDefinitionDataBody(string fileUniqueName)
        {
            var ksiInputData = JsonConvert.DeserializeObject<List<KsiDefinition>>(fileUniqueName);
            return ksiInputData.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves Hostspot Type test data
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static BlackspotType GetAddHotspotTypeDataBody(string fileUniqueName)
        {
            var blackspotTypeInputData = JsonConvert.DeserializeObject<List<BlackspotType>>(fileUniqueName);
            return blackspotTypeInputData.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves Save Blackspot test data
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static SaveBlackspot GetSaveBlackspotDataBody(string fileUniqueName)
        {
            var saveBlackspotInputData = JsonConvert.DeserializeObject<List<SaveBlackspot>>(fileUniqueName);
            return saveBlackspotInputData.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves Potential Blackspot test data
        /// </summary>
        /// <param name="fileUniqueName"></param>
        /// <returns></returns>
        public static BlackspotManagement GetPotentialBlackspotDataBody(string fileUniqueName)
        {
            var potentialInputData = JsonConvert.DeserializeObject<List<BlackspotManagement>>(fileUniqueName);
            return potentialInputData.FirstOrDefault();
        }
    }
}
