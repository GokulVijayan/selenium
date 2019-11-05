using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;
using iMAAPTestAPI.CrashRecords;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using Newtonsoft.Json;
using MassTransit.Serialization.JsonConverters;
using Ex_haft.Utilities.Reports;
using Ex_haft.Utilities;
using Ex_haft.GenericComponents;

namespace FrameworkSetup.APITestScript
{
    
    public class ApiReusableComponents
    {
        RestClient client, gisClient;
        public static string authToken, crashReferenceNumber, userRoleId,userName,password= "Trl@123456";
        readonly JsonDeserializer deserialize = new JsonDeserializer();
        public static CbaDto cbaResponseDto;
        public static CalculateCmCostDto cmResponseDto;
        public string idList = "";
        public string editRowversion, editCrashid;
        public static List<int> idsList = new List<int>();
        public List<string> screenshotList = new List<string>();
        List<FormFieldData> crashData = new List<FormFieldData>();
        List<FormFieldData> vehicleData = new List<FormFieldData>();
        List<FormFieldData> casualtyData = new List<FormFieldData>();
        List<TestReportSteps> listOfReports, addCrashReport, updateCrashReport,userReport, searchCrashReport, deleteCrashReport;
        Stopwatch Stopwatch = new Stopwatch();
        double responseTime;
        int flag, crashCount;
        public static List<int> crashIdList;
        public static List<int> rowVersionList;
        public static List<string> crashReferenceNumberList;
        public static int crashId, rowVersion;
        public static long casualtyId, vehicleId;

        public ApiReusableComponents()
        {
            crashIdList = new List<int>();
            rowVersionList = new List<int>();
            crashReferenceNumberList = new List<string>();
        }


        /// <summary>
        /// Verify Authenticate using the API - api/authenticate/mobile.
        /// </summary>

        public List<TestReportSteps> AuthenticateUsingAPI()
        {
            try
            {

                //Test Objective: To verify that user is able to login with valid credentials using the API - api/authenticate/mobile.
                client = new RestClient(Constant.imaapUrl);
                string apiName = "/api/authenticate/mobile";
                var loginRequest = new RestRequest(apiName, Method.POST);
                loginRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                var login = new { username = "ReshmaPradeep", password = "Trl@123456" };
                loginRequest.RequestFormat = DataFormat.Json;
                loginRequest.AddJsonBody(login);
                Stopwatch.Start();
                IRestResponse loginResponse = client.Execute(loginRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var output = deserialize.Deserialize<SampleMasterData>(loginResponse);
                authToken = output?.Data;
                listOfReports = GenerateReport.GetReportFile("LoginReport.json");
                if (!string.IsNullOrWhiteSpace(authToken))
                {
                    listOfReports[0].actualResultFail = string.Empty;
                }
                var loginCredentials = new { username = "sysadmin", password = "**********" };


            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();

            }


            return listOfReports;




        }

        /// <summary>
        /// Verify Add a crash using the API - /api/crashdetails.
        /// </summary>

        public List<TestReportSteps> AddCrashUsingAPI(string APIDataFile)
        {
            try
            {
                AuthenticateUsingAPI();
                //Test Objective: To verify that a Crash Record can be added using the Crashdetails API.
                client = new RestClient(Constant.imaapUrl);
                var crashData = RetrieveTestData.GetCrashDataBody(APIDataFile);
                addCrashReport = GenerateReport.GetReportFile("AddCrashReport.json");
                string apiName = "/api/crashdetails";
                var addCrashRequest = new RestRequest(apiName, Method.POST);
                addCrashRequest.RequestFormat = DataFormat.Json;
                addCrashRequest.AddHeader("Content-Type", "application/json");
                addCrashRequest.AddHeader("x-bif-private-token", authToken);
                addCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                addCrashRequest.AddJsonBody(crashData);
                Stopwatch.Start();
                IRestResponse addCrashResponse = client.Execute(addCrashRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var addCrashResponseDto = deserialize.Deserialize<CrashRecordRootObject>(addCrashResponse);
                crashId = addCrashResponseDto.data.crashId;
                rowVersion = addCrashResponseDto.data.crashDetails.rowVersion;
                crashReferenceNumber = addCrashResponseDto.data.crashDetails.crash.fieldValues[0].value;
                if (addCrashResponseDto != null && addCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED") && addCrashResponse.StatusCode == HttpStatusCode.OK)
                {
                    addCrashReport[0].actualResultFail = string.Empty;
                }
                listOfReports.AddRange(addCrashReport);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                listOfReports.AddRange(addCrashReport);
                throw new NotImplementedException();
            }

            return listOfReports;
        }


        /// <summary>
        /// Verify Add a crash using the API - /api/crashdetails.
        /// </summary>

        public List<TestReportSteps> AddCrashForVerfyingStickAnalysis(CrashRootObject crashData)
        {
            try
            {
                //Test Objective: To verify that a Crash Record can be added using the Crashdetails API.
                client = new RestClient(Constant.imaapUrl);
                addCrashReport = GenerateReport.GetReportFile("AddCrashReport.json");
                string apiName = "/api/crashdetails";
                var addCrashRequest = new RestRequest(apiName, Method.POST);
                addCrashRequest.RequestFormat = DataFormat.Json;
                addCrashRequest.AddHeader("Content-Type", "application/json");
                addCrashRequest.AddHeader("x-bif-private-token", authToken);
                addCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                addCrashRequest.AddJsonBody(crashData);
                Stopwatch.Start();
                IRestResponse addCrashResponse = client.Execute(addCrashRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var addCrashResponseDto = deserialize.Deserialize<CrashRecordRootObject>(addCrashResponse);
                crashIdList.Add(addCrashResponseDto.data.crashId);
                rowVersionList.Add(addCrashResponseDto.data.crashDetails.rowVersion);
                crashReferenceNumberList.Add(addCrashResponseDto.data.crashDetails.crash.fieldValues[0].value);
                if (addCrashResponseDto != null && addCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED") && addCrashResponse.StatusCode == HttpStatusCode.OK)
                {
                    addCrashReport[0].actualResultFail = string.Empty;
                }
               


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return addCrashReport;
        }


        /// <summary>
        /// Verify Update a crash using the API - /api/crashdetails.
        /// </summary>



        public List<TestReportSteps> UpdateCrashUsingAPI(JToken inputjson)
        {

            try
            {
                updateCrashReport = GenerateReport.GetReportFile("UpdateCrashReport.json");
                searchCrashReport = GenerateReport.GetReportFile("SearchCrashForMobileReport.json");
                SearchCrash searchCrash = new SearchCrash();
                searchCrash.id = new List<int>();
                searchCrash.id.Add(crashId);
                string apiName = "/api/crashdetails/" + crashId.ToString();
                var searchCrashRequest = new RestRequest(apiName, Method.GET);
                searchCrashRequest.RequestFormat = DataFormat.Json;
                searchCrashRequest.AddHeader("Content-type", "application/json");
                searchCrashRequest.AddHeader("x-bif-private-token", authToken);
                searchCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                searchCrashRequest.AddHeader("selectedapplicationid", "1");
                searchCrashRequest.AddHeader("selectedpageid", "3");
                searchCrashRequest.AddHeader("timezone-offset", "-330");
                Stopwatch.Start();
                IRestResponse searchCrashResponse = client.Execute(searchCrashRequest);
                Stopwatch.Stop();
                var searchCrashResponseDto = deserialize.Deserialize<EditCrashRecordRootObject>(searchCrashResponse);
                rowVersion = searchCrashResponseDto.data.rowVersion;
                searchCrashReport[0].actualResultFail = string.Empty;
                searchCrashReport[0].reqResponseTime = responseTime;


                //Update crash data
                var editTemp = searchCrashResponseDto.data;
                editTemp.casualties[0].fieldValues[0].value = "2";
                apiName = "/api/crashdetails";
                var updateCrashRequest = new RestRequest(apiName, Method.POST);
                updateCrashRequest.RequestFormat = DataFormat.Json;
                updateCrashRequest.AddHeader("Content-type", "application/json");
                updateCrashRequest.AddHeader("x-bif-private-token", authToken);
                updateCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                searchCrashRequest.AddHeader("selectedapplicationid", "1");
                searchCrashRequest.AddHeader("selectedpageid", "3");
                searchCrashRequest.AddHeader("timezone-offset", "-330");
                updateCrashRequest.AddJsonBody(editTemp);
                Stopwatch.Start();
                IRestResponse updateCrashResponse = client.Execute(updateCrashRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var updateCrashResponseDto = deserialize.Deserialize<CrashRecordRootObject>(updateCrashResponse);

                var editTempResponse = updateCrashResponseDto.data?.crashDetails;
                editCrashid = updateCrashResponseDto.data.crashId.ToString();
                editRowversion = updateCrashResponseDto.data.crashDetails.rowVersion.ToString();
                if (updateCrashResponseDto != null && updateCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED"))
                {
                    updateCrashReport[0].actualResultFail = string.Empty;
                }


            }

            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();

            }

            return updateCrashReport;
        }

        /// <summary>
        /// Verify Delete a crash using the API 
        /// </summary>
        public List<TestReportSteps> DeleteCrashUsingAPI(string data)
        {

            try
            {
                updateCrashReport = GenerateReport.GetReportFile("UpdateCrashReport.json");
                deleteCrashReport = GenerateReport.GetReportFile("DeleteCrashAPIReport.json");
                SearchCrash searchCrash = new SearchCrash();
                searchCrash.id = new List<int>();
                searchCrash.id.Add(crashId);
                string apiName = "/api/crashes";
                var deleteCrashRequest = new RestRequest(apiName, Method.POST);
                deleteCrashRequest.RequestFormat = DataFormat.Json;
                deleteCrashRequest.AddHeader("Content-type", "application/json");
                deleteCrashRequest.AddHeader("x-bif-private-token", authToken);
                deleteCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                deleteCrashRequest.AddHeader("selectedapplicationid", "1");
                deleteCrashRequest.AddHeader("selectedpageid", "1");
                deleteCrashRequest.AddHeader("timezone-offset", "-330");
                deleteCrashRequest.AddHeader("Accept", "*/*");
                deleteCrashRequest.AddHeader("language", "1");
                var crashInputData = JsonConvert.DeserializeObject<List<DeleteCrash>>(data);
                crashInputData.FirstOrDefault().crashId = Convert.ToInt32(editCrashid);
                crashInputData.FirstOrDefault().rowVersion = Convert.ToInt32(editRowversion);
                string deleteCrashBody = Newtonsoft.Json.JsonConvert.SerializeObject(crashInputData);
                deleteCrashRequest.AddJsonBody(deleteCrashBody);
                Stopwatch.Start();
                IRestResponse deleteCrashResponse = client.Execute(deleteCrashRequest);
                var deleteCrashResponseDto = deserialize.Deserialize<DeleteCrashResponse>(deleteCrashResponse);
                if (deleteCrashResponseDto != null && deleteCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_DELETED") && deleteCrashResponse.StatusCode == HttpStatusCode.OK)
                {
                    deleteCrashReport[0].actualResultFail = string.Empty;
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();

            }

            return searchCrashReport;
        }


        /// <summary>
        /// Verify Add a multiple crash using the API - /api/crashdetails.
        /// </summary>

        public List<TestReportSteps> AddMultipleCrashUsingAPI(CrashRootObject crashData)
        {
            try
            {
                AuthenticateUsingAPI();
                //Test Objective: To verify that a Crash Record can be added using the Crashdetails API.
                client = new RestClient(Constant.imaapUrl);
                addCrashReport = GenerateReport.GetReportFile("AddCrashReport.json");
                string apiName = "/api/crashdetails";
                var addCrashRequest = new RestRequest(apiName, Method.POST);
                addCrashRequest.RequestFormat = DataFormat.Json;
                addCrashRequest.AddHeader("Content-Type", "application/json");
                addCrashRequest.AddHeader("x-bif-private-token", authToken);
                addCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                addCrashRequest.AddJsonBody(crashData);
                Stopwatch.Start();
                IRestResponse addCrashResponse = client.Execute(addCrashRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var addCrashResponseDto = deserialize.Deserialize<CrashRecordRootObject>(addCrashResponse);
                crashId = addCrashResponseDto.data.crashId;
                rowVersion = addCrashResponseDto.data.crashDetails.rowVersion;
                casualtyId = addCrashResponseDto.data.casualties[0];
                vehicleId = addCrashResponseDto.data.vehicles[0];
                crashReferenceNumber = addCrashResponseDto.data.crashDetails.crash.fieldValues[0].value;
                if (addCrashResponseDto != null && addCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED") && addCrashResponse.StatusCode == HttpStatusCode.OK)
                {
                    addCrashReport[0].actualResultFail = string.Empty;
                }
                listOfReports.AddRange(addCrashReport);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                listOfReports.AddRange(addCrashReport);
                throw new NotImplementedException();
            }

            return listOfReports;
        }



        public List<TestReportSteps> AddMultipleCrashForHotspot(List<CrashRootObject> crashData)
        {
            try
            {
                int numberOfInputs = crashData.Count;
                for (int i = 0; i < numberOfInputs; i++)
                {
                    AuthenticateUsingAPI();
                    //Test Objective: To verify that a Crash Record can be added using the Crashdetails API.
                    client = new RestClient(Constant.imaapUrl);
                    addCrashReport = GenerateReport.GetReportFile("AddCrashReport.json");
                    string apiName = "/api/crashdetails";
                    var addCrashRequest = new RestRequest(apiName, Method.POST);
                    addCrashRequest.RequestFormat = DataFormat.Json;
                    addCrashRequest.AddHeader("Content-type", "application/json");
                    addCrashRequest.AddHeader("x-bif-private-token", authToken);
                    addCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                    addCrashRequest.AddJsonBody(crashData[i]);
                    Stopwatch.Start();
                    IRestResponse addCrashResponse = client.Execute(addCrashRequest);
                    Stopwatch.Stop();
                    responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                    Stopwatch.Reset();
                    var addCrashResponseDto = deserialize.Deserialize<CrashRecordRootObject>(addCrashResponse);
                    crashIdList.Add(addCrashResponseDto.data.crashId);
                    rowVersionList.Add(addCrashResponseDto.data.crashDetails.rowVersion);
                    crashReferenceNumberList.Add(addCrashResponseDto.data.crashDetails.crash.fieldValues[0].value);
                    if (addCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED") || addCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_UPDATED_BY_OTHER_USER"))
                    {
                        addCrashReport[0].actualResultFail = string.Empty;
                    }
                    listOfReports.AddRange(addCrashReport);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                listOfReports.AddRange(addCrashReport);
                throw new NotImplementedException();
            }

            return listOfReports;
        }



        /// <summary>
        /// Verify Delete a crash record using the API - /api/crashes/.
        /// </summary>

        public  List<TestReportSteps> DeleteCrashRecord(string deleteCrashData, List<TestReportSteps> deleteCrashReport)
        {
            try
            {
                //Test Objective: To verify that a Crash Record can be deleted using the Crashdetails API.
                string apiName = "/api/crashes/";
                var deleteCrashRequest = new RestRequest(apiName, Method.POST);
                deleteCrashRequest.RequestFormat = DataFormat.Json;
                deleteCrashRequest.AddHeader("Content-Type", "application/json");
                deleteCrashRequest.AddHeader("x-bif-private-token",authToken);
                deleteCrashRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                deleteCrashRequest.AddJsonBody(deleteCrashData);
                Stopwatch.Start();
                IRestResponse deleteCrashResponse = client.Execute(deleteCrashRequest);
                var deleteCrashResponseDto = deserialize.Deserialize<DeleteCrashResponse>(deleteCrashResponse);
                if (deleteCrashResponseDto != null && deleteCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_DELETED") && deleteCrashResponse.StatusCode == HttpStatusCode.OK)
                {
                    deleteCrashReport[0].actualResultFail = string.Empty;
                }


               

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return deleteCrashReport;
        }


        /// <summary>
        /// Verify Delete multiple crash record using the API - /api/crashes/.
        /// </summary>

        public List<TestReportSteps> DeleteMultipleCrashRecord(List<TestReportSteps> deleteCrashReport,string crashData)
        {
            try
            {
                //Test Objective: To verify that a Crash Record can be deleted using the Crashdetails API.
                string apiName = "/api/crashes/";
                for (int i = 0; i < crashIdList.Count; i++)
                {
                    string deleteCrashData = SetDeleteCrashData(crashIdList[i], rowVersionList[i], crashData);
                    var deleteCrashRequest = new RestRequest(apiName, Method.POST);
                    deleteCrashRequest.RequestFormat = DataFormat.Json;
                    deleteCrashRequest.AddHeader("Content-Type", "application/json");
                    deleteCrashRequest.AddHeader("x-bif-private-token", authToken);
                    deleteCrashRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                    deleteCrashRequest.AddHeader("language", "1");
                    deleteCrashRequest.AddHeader("selectedapplicationid", "1");
                    deleteCrashRequest.AddHeader("selectedpageid", "1");
                    deleteCrashRequest.AddHeader("timezone-offset", "-180");
                    deleteCrashRequest.AddHeader("Accept", "*/*");
                    deleteCrashRequest.AddJsonBody(deleteCrashData);
                    Stopwatch.Start();
                    IRestResponse deleteCrashResponse = client.Execute(deleteCrashRequest);
                    var deleteCrashResponseDto = deserialize.Deserialize<DeleteCrashResponse>(deleteCrashResponse);
                    if (deleteCrashResponseDto != null && deleteCrashResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_DELETED") && deleteCrashResponse.StatusCode == HttpStatusCode.OK)
                    {
                        deleteCrashReport[0].actualResultFail = string.Empty;
                    }

                }


            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return deleteCrashReport;
        }






        /// <summary>
        /// Plot Crashes using the API - /api/crashdetails
        /// </summary>

        public List<TestReportSteps> PlotCrashes(PlotCrashData plotCrashData, List<TestReportSteps> spatialAnalysisUsingCircleToolReport)
        {
            try
            {
                idsList.Clear();
                idList = "";
                gisClient = new RestClient(Constant.imaapGisUrl);
                string apiName = "api/plotcrashes";
                var getPlotCrashes = new RestRequest(apiName, Method.POST);
                getPlotCrashes.RequestFormat = DataFormat.Json;
                getPlotCrashes.AddHeader("Content-Type", "application/json");
                getPlotCrashes.AddHeader("x-bif-private-token", authToken);
                getPlotCrashes.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                getPlotCrashes.AddJsonBody(plotCrashData);
                Stopwatch.Start();
                IRestResponse getPlotCrashResponse = gisClient.Execute(getPlotCrashes);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var plotCrashResponseDto = deserialize.Deserialize<PlotCrashDto>(getPlotCrashResponse);
                crashCount = plotCrashResponseDto.data.totalFeatures;
                for (int i = 0; i < crashCount; i++)
                {
                    idList = idList + plotCrashResponseDto.data.features[i].properties.Id.ToString() + ",";
                    idsList.Add(plotCrashResponseDto.data.features[i].properties.Id);
                    if (plotCrashResponseDto.data.features[i].properties.ReferenceNumber == crashReferenceNumber)
                    {
                        flag = 0;
                        break;
                    }
                    else
                    {
                        flag = 1;
                    }
                }
                if (plotCrashResponseDto != null && getPlotCrashResponse.StatusCode == HttpStatusCode.OK && flag == 0)
                {
                    spatialAnalysisUsingCircleToolReport[0].actualResultFail = string.Empty;
                }

                
                
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return spatialAnalysisUsingCircleToolReport;
        }


        /// <summary>
        /// Verify safety camera analysis using the API - /api/safetycameraanalysis/.
        /// </summary>

        public List<TestReportSteps> PerformSafetyCameraAnalysis(SafetyCameraAnalysis safetyCameraData, List<TestReportSteps> safetyCameraAnalysis,string cameraType,string junction)
        {
            try
            {
                //Test Objective: To verify safety camera analysis using the api/safetycameraanalysis/ API.
                string apiName = "api/safetycameraanalysis/";
                var safetyCameraRequest = new RestRequest(apiName, Method.POST);
                safetyCameraRequest.RequestFormat = DataFormat.Json;
                safetyCameraRequest.AddHeader("Content-Type", "application/json");
                safetyCameraRequest.AddHeader("x-bif-private-token", authToken);
                safetyCameraRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                safetyCameraRequest.AddHeader("selectedapplicationid", "1");
                safetyCameraRequest.AddHeader("selectedpageid", "20");

                safetyCameraRequest.AddJsonBody(safetyCameraData);
                Stopwatch.Start();
                IRestResponse safetyCameraResponse = client.Execute(safetyCameraRequest);
                var safetyCameraResponseDto = deserialize.Deserialize<SafetyCameraAnalysisDto>(safetyCameraResponse);
                
                if (safetyCameraResponseDto != null  && safetyCameraResponse.StatusCode == HttpStatusCode.OK && safetyCameraResponseDto.data.Contains(cameraType))
                {
                    safetyCameraAnalysis[0].testObjective = safetyCameraAnalysis[0].GetTestObjective() + "Verify that report displays " + cameraType + " Camera as suitable for the " + junction + " location";
                    safetyCameraAnalysis[0].actualResultPass = safetyCameraAnalysis[0].GetActualResultPass() + "User is able to verify that report displays " + cameraType + " Camera as suitable for the " + junction + " location";
                    safetyCameraAnalysis[0].actualResultFail = string.Empty;
                }
                else
                {
                    safetyCameraAnalysis[0].testObjective = safetyCameraAnalysis[0].GetTestObjective() + "Verify that report displays " + cameraType + " Camera as suitable for the " + junction + " location";
                    safetyCameraAnalysis[0].actualResultFail = safetyCameraAnalysis[0].GetActualResultPass() + "User is able to verify that report displays " + cameraType + " Camera as suitable for the " + junction + " location";
                    safetyCameraAnalysis[0].actualResultPass = string.Empty;

                }



            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return safetyCameraAnalysis;
        }


        /// <summary>
        /// Sets data for deleting a crash record
        /// </summary>
        /// <returns></returns>
        public string SetDeleteCrashData()
        {

            DeleteCrash deleteCrash = new DeleteCrash();
            deleteCrash.crashId = crashId;
            deleteCrash.rowVersion = rowVersion;
            string deleteCrashBody = Newtonsoft.Json.JsonConvert.SerializeObject(deleteCrash);
            deleteCrashBody = deleteCrashBody.Replace("\r\n", "");
            return deleteCrashBody;
        }


        /// <summary>
        /// Sets data for deleting a crash record
        /// </summary>
        /// <returns></returns>
        public string SetDeleteCrashData(int crashIdNumber,int rowVersionNumber,string crashData)
        {

            var crashInputData = JsonConvert.DeserializeObject<List<DeleteCrash>>(crashData);
            crashInputData.FirstOrDefault().crashId = crashIdNumber;
            crashInputData.FirstOrDefault().rowVersion = rowVersionNumber;
            string deleteCrashBody = Newtonsoft.Json.JsonConvert.SerializeObject(crashInputData);
            return deleteCrashBody;
        }


        /// <summary>
        /// Verify Calculate Crash Cost using the API - api/calculatecmcost.
        /// </summary>

        public List<TestReportSteps> CalculateCrashCost(List<TestReportSteps> calculateReport, CalculateCrashCost crashCostData)
        {
            try
            {
                //Test Objective: To verify calculating cm cost using api/calculatecmcost API.
                string apiName = "api/calculatecrashcost";
                var calculateRequest = new RestRequest(apiName, Method.POST);
                calculateRequest.RequestFormat = DataFormat.Json;
                calculateRequest.AddHeader("Content-Type", "application/json");
                calculateRequest.AddHeader("x-bif-private-token", authToken);
                calculateRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                calculateRequest.AddHeader("language", "1");
                calculateRequest.AddHeader("selectedapplicationid", "1");
                calculateRequest.AddHeader("selectedpageid", "28");
                calculateRequest.AddHeader("timezone-offset", "-180");
                calculateRequest.AddJsonBody(crashCostData);
                Stopwatch.Start();
                IRestResponse calculatResponse = client.Execute(calculateRequest);
                var calculatResponseDto = deserialize.Deserialize<CalculateCrashCostDto>(calculatResponse);
                if (calculatResponseDto != null  && calculatResponse.StatusCode == HttpStatusCode.OK)
                {
                    calculateReport[0].actualResultFail = string.Empty;
                }

             
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return calculateReport;
        }



        /// <summary>
        /// Verify Cba using the API - api/countermeasures/cba.
        /// </summary>

        public List<TestReportSteps> VerifyCba(List<TestReportSteps> cbaReport, Cba cbaData)
        {
            try
            {
                //Test Objective: To verify calculating cba using api/countermeasures/cba API.
                string apiName = "api/countermeasures/cba";
                var cbaRequest = new RestRequest(apiName, Method.POST);
                cbaRequest.RequestFormat = DataFormat.Json;
                cbaRequest.AddHeader("Content-Type", "application/json");
                cbaRequest.AddHeader("x-bif-private-token", authToken);
                cbaRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                cbaRequest.AddHeader("language", "1");
                cbaRequest.AddHeader("selectedapplicationid", "1");
                cbaRequest.AddHeader("selectedpageid", "28");
                cbaRequest.AddHeader("timezone-offset", "-180");
                cbaRequest.AddJsonBody(cbaData);
                Stopwatch.Start();
                IRestResponse cbaResponse = client.Execute(cbaRequest);
                cbaResponseDto = deserialize.Deserialize<CbaDto>(cbaResponse);
                if (cbaResponseDto != null && cbaResponseDto.data.Count>=1 && cbaResponse.StatusCode == HttpStatusCode.OK)
                {
                    cbaReport[0].actualResultFail = string.Empty;
                }


            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return cbaReport;
        }

        /// <summary>
        /// Verify Cm Cost using the API - api/calculatecmcost.
        /// </summary>

        public List<TestReportSteps> CalculateCmCost(List<TestReportSteps> cmReport, string cmData)
        {
            try
            {
                //Test Objective: To verify calculating cm cost using api/calculatecmcost API.
                string apiName = "api/calculatecmcost";
                var cmRequest = new RestRequest(apiName, Method.POST);
                cmRequest.RequestFormat = DataFormat.Json;
                cmRequest.AddHeader("Content-Type", "application/json");
                cmRequest.AddHeader("x-bif-private-token", authToken);
                cmRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                cmRequest.AddHeader("language", "1");
                cmRequest.AddHeader("selectedapplicationid", "1");
                cmRequest.AddHeader("selectedpageid", "28");
                cmRequest.AddHeader("timezone-offset", "-180");
                var cmCostData = JsonConvert.DeserializeObject<List<CalculateCmCost>>(cmData);
                cmCostData.FirstOrDefault().name = cbaResponseDto.data[0].countermeasure.name;
                cmCostData.FirstOrDefault().year = cbaResponseDto.data[0].countermeasure.constructionYear;
                //cmData.cbaCounterMeasureSchemeId = cbaResponseDto.data[0].countermeasure.counterMeasureCategoryId;
                cmCostData.FirstOrDefault().cbaCounterMeasureId = cbaResponseDto.data[0].countermeasure.id;
                string cmCalculateCostData = Newtonsoft.Json.JsonConvert.SerializeObject(cmCostData);
                cmRequest.AddJsonBody(cmCalculateCostData);
                Stopwatch.Start();
                IRestResponse cmResponse = client.Execute(cmRequest);
                cmResponseDto = deserialize.Deserialize<CalculateCmCostDto>(cmResponse);
                if (cmResponseDto != null  && cmResponse.StatusCode == HttpStatusCode.OK)
                {
                    cmReport[0].actualResultFail = string.Empty;
                }


            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return cmReport;
        }


        /// <summary>
        /// Verify Run Cba using the API -api/cba/run.
        /// </summary>

        public List<TestReportSteps> RunCba(List<TestReportSteps> cbaReport, RunCba cbaData)
        {
            try
            {
                //Test Objective: To verify calculating cba using api/countermeasures/cba API.
                string apiName = "api/cba/run";
                var cbaRequest = new RestRequest(apiName, Method.POST);
                cbaRequest.RequestFormat = DataFormat.Json;
                cbaRequest.AddHeader("Content-Type", "application/json");
                cbaRequest.AddHeader("x-bif-private-token", authToken);
                cbaRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                cbaRequest.AddHeader("language", "1");
                cbaRequest.AddHeader("selectedapplicationid", "1");
                cbaRequest.AddHeader("selectedpageid", "28");
                cbaRequest.AddHeader("timezone-offset", "-180");
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].actualMaintenanceCostInBaseYear = cmResponseDto.data.cbaActualCountermeasureDetails[0].actualMaintenanceCostInBaseYear;
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].actualConstructionCostInBaseYear = cmResponseDto.data.cbaActualCountermeasureDetails[0].actualConstructionCostInBaseYear;
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].totalEstimatedConstructionCost= Convert.ToInt32(cmResponseDto.data.cbaActualCountermeasureDetails[0].totalEstimatedConstructionCost);
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].totalEstimatedConstructionCostInBaseYear = cmResponseDto.data.cbaActualCountermeasureDetails[0].totalEstimatedConstructionCostInBaseYear;
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].totalEstimatedMaintenanceCost = cmResponseDto.data.cbaActualCountermeasureDetails[0].totalEstimatedMaintenanceCost;
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].totalEstimatedMaintenanceCostInBaseYear = cmResponseDto.data.cbaActualCountermeasureDetails[0].totalEstimatedMaintenanceCostInBaseYear;
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].countermeasure = cmResponseDto.data.cbaActualCountermeasureDetails[0].counterDetailmeasures;
                cbaData.cbaActualCountermeasure.cbaActualCountermeasureDetails[0].cbaActualCountermeasure = cmResponseDto.data.cbaActualCountermeasureDetails[0].countermeasure; 

                cbaRequest.AddJsonBody(cbaData);
                Stopwatch.Start();
                IRestResponse cbaResponse = client.Execute(cbaRequest);
                var runCbaResponseDto = deserialize.Deserialize<RunCbaDto>(cbaResponse);
                if (runCbaResponseDto != null  && cbaResponse.StatusCode == HttpStatusCode.OK)
                {
                    cbaReport[0].actualResultFail = string.Empty;
                }


            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return cbaReport;
        }


        /// <summary>
        /// Verify Add user using the API - api/users.
        /// </summary>

        public List<TestReportSteps> AddUser(Users userData,string roleName)
        {
            try
            {
                AuthenticateUsingAPI();
                //Test Objective: To verify Add user using the API -api/users API.
                string apiName = "api/roles";
                userReport = GenerateReport.GetReportFile("UserReport.json");
                var rolesRequest = new RestRequest(apiName, Method.GET);
                rolesRequest.RequestFormat = DataFormat.Json;
                rolesRequest.AddHeader("Content-Type", "application/json");
                rolesRequest.AddHeader("x-bif-private-token", authToken);
                rolesRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                rolesRequest.AddHeader("language", "1");
                rolesRequest.AddHeader("selectedapplicationid", "1");
                rolesRequest.AddHeader("selectedpageid", "41");
                rolesRequest.AddHeader("timezone-offset", "-180");
                Stopwatch.Start();
                IRestResponse userResponse = client.Execute(rolesRequest);
                var userResponseDto = deserialize.Deserialize<RolesDto>(userResponse);
                if (userResponseDto != null  && userResponse.StatusCode == HttpStatusCode.OK)
                {
                    for(int i=0;i< userResponseDto.data.Count;i++)
                    {
                        if(userResponseDto.data[i].roleName[0].name== roleName)
                        {
                            userRoleId = userResponseDto.data[i].roleName[0].roleId.ToString();
                        }
                    }
                }


                string userApiName = "api/users";
                var addRolesRequest = new RestRequest(userApiName, Method.POST);
                addRolesRequest.RequestFormat = DataFormat.Json;
                addRolesRequest.AddHeader("Content-Type", "application/json");
                addRolesRequest.AddHeader("x-bif-private-token", authToken);
                addRolesRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                addRolesRequest.AddHeader("language", "1");
                addRolesRequest.AddHeader("selectedapplicationid", "1");
                addRolesRequest.AddHeader("selectedpageid", "40");
                addRolesRequest.AddHeader("timezone-offset", "-180");
                string emailData = "TestUser" + ReusableComponents.getRandomNumber(4) + "@mailinator.com";
                userData.email = emailData;
                userData.confirmemail = emailData;
                userName= "TestUser" + ReusableComponents.getRandomNumber(4);
                userData.username = userName;
                userData.userRoleId = userRoleId;
                addRolesRequest.AddJsonBody(userData);
                IRestResponse addUserResponse = client.Execute(addRolesRequest);
                var addUserResponseDto = deserialize.Deserialize<UsersDto>(addUserResponse);
                if (addUserResponseDto != null && addUserResponseDto.responseMessages[0].messageKey== "Messages.RECORD_SAVED" && userResponse.StatusCode == HttpStatusCode.OK)
                {
                    userReport[0].actualResultFail = string.Empty;
                }


            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return userReport;
        }


        /// <summary>
        /// Verify entering crash time
        /// </summary>

        public string GetTimestampToEnter()
        {
            DateTime dt = System.DateTime.Now;
            var date = dt.Date;
            var Times = new DateTimeOffset(date).ToUnixTimeSeconds();
            string timeStamp = Times.ToString();
            return timeStamp;
        }


    }
}
