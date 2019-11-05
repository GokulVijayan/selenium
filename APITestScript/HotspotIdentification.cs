using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;
using Nancy.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace FrameworkSetup.APITestScript
{
    class HotspotIdentification
    {
        RestClient client;
        string authToken;
        readonly JsonDeserializer deserialize = new JsonDeserializer();
        public string hotspotTypeName;
        int hotspotTypeId;
        public List<string> screenshotList = new List<string>();
        List<TestReportSteps> listOfReports, hotspotReport, ksiReport, addHotspotTypeReport, addHotspotReport;
        Stopwatch Stopwatch = new Stopwatch();
        double responseTime;

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
                var login = new { username = "sysadmin", password = "Trl@123456" };
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
        /// Verify hotspot analysis API - /api/blackspotanalysis.
        /// </summary>
        public List<TestReportSteps> HotspotAnalysis(string APIDataFile, string reportName)
        {
            try
            {
                //Test Objective: To verify that hotspot can be identified using configured rule 1.
                client = new RestClient(Constant.imaapGisUrl);
                var hotspotData = RetrieveTestData.GetHotspotAnalysisDataBody(APIDataFile);
                hotspotReport = GenerateReport.GetReportFile(reportName + ".json");
                string apiName = "/api/blackspotanalysis";
                var blackspotRequest = new RestRequest(apiName, Method.POST);
                blackspotRequest.RequestFormat = DataFormat.Json;
                blackspotRequest.AddHeader("Content-type", "application/json");
                blackspotRequest.AddHeader("x-bif-private-token", authToken);
                blackspotRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                blackspotRequest.AddHeader("language", "1");
                blackspotRequest.AddHeader("selectedapplicationid", "1");
                blackspotRequest.AddHeader("selectedpageid", "24");
                blackspotRequest.AddHeader("timezone-offset", "-330");
                blackspotRequest.AddJsonBody(hotspotData);
                Stopwatch.Start();
                IRestResponse response = client.Execute(blackspotRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var responseDto = deserialize.Deserialize<HotspotResponse>(response);
                if (responseDto.responseMessages[0].messageText.Trim().Equals("Successfully performed analysis with specified parameters.") && responseDto.responseMessages[0].messageKey.Trim().Equals("Messages.ANALYSIS_COMPLETED") && response.StatusCode == HttpStatusCode.OK)
                {
                    hotspotReport[0].actualResultFail = string.Empty;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                listOfReports.AddRange(hotspotReport);
                throw new NotImplementedException();
            }

            return hotspotReport;
        }

        /// <summary>
        /// Verify hotspot analysis API - /api/blackspotanalysis.
        /// </summary>
        public List<TestReportSteps> ConfigureKsiDefinition(string APIDataFile)
        {
            try
            {
                //Test Objective: To verify that Ksi definitions can be configured successfully.
                AuthenticateUsingAPI();
                client = new RestClient(Constant.imaapUrl);
                var KsiData = RetrieveTestData.GetKsiDefinitionDataBody(APIDataFile);
                ksiReport = GenerateReport.GetReportFile("ConfigureKsiDefinitionReport.json");
                string apiName = "/api/ksidefinitions";
                var ksiRequest = new RestRequest(apiName, Method.POST);
                ksiRequest.RequestFormat = DataFormat.Json;
                ksiRequest.AddHeader("Content-type", "application/json");
                ksiRequest.AddHeader("x-bif-private-token", authToken);
                ksiRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                ksiRequest.AddJsonBody(KsiData);
                Stopwatch.Start();
                IRestResponse response = client.Execute(ksiRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var responseDto = deserialize.Deserialize<KsiResponse>(response);
                if (responseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED") && response.StatusCode == HttpStatusCode.OK)
                {
                    ksiReport[0].actualResultFail = string.Empty;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                listOfReports.AddRange(ksiReport);
                throw new NotImplementedException();
            }

            return ksiReport;
        }

        public List<TestReportSteps> AddHotspotType(string hotspotTypeDataFile, string saveBlackspotDataFile, string potentialHotspotDataFile)
        {
            try
            {

                //Test Objective: To verify that a Hotspot type can be added using the BlackspotTypes API.
                client = new RestClient(Constant.imaapUrl);
                BlackspotType hotspotData = RetrieveTestData.GetAddHotspotTypeDataBody(hotspotTypeDataFile);
                addHotspotTypeReport = GenerateReport.GetReportFile("AddHotspotTypeReport.json");
                string apiName = "/api/blackspottypes";
                var addHotpotRequest = new RestRequest(apiName, Method.POST);
                addHotpotRequest.RequestFormat = DataFormat.Json;
                addHotpotRequest.AddHeader("Content-type", "application/json");
                addHotpotRequest.AddHeader("x-bif-private-token", authToken);
                addHotpotRequest.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                addHotpotRequest.AddJsonBody(hotspotData);
                Stopwatch.Start();
                IRestResponse addHotspotResponse = client.Execute(addHotpotRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var addHotspotResponseDto = deserialize.Deserialize<HotspotTypeDto>(addHotspotResponse);
                hotspotTypeId = hotspotData.blackspotTypeName[0].id;
                if (addHotspotResponseDto != null && addHotspotResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.RECORD_SAVED") && addHotspotResponse.StatusCode == HttpStatusCode.OK)
                {
                    addHotspotTypeReport[0].actualResultFail = string.Empty;
                }

                addHotspotTypeReport[0].reqResponseTime = responseTime;
                listOfReports.AddRange(addHotspotTypeReport);
                GenerateReport.WriteTestLog(apiName, new JavaScriptSerializer().Serialize(hotspotData), addHotspotResponse.Content, "POST");
            }
            catch (Exception e)
            {
                listOfReports.AddRange(addHotspotTypeReport);
                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            try
            {

                //Test Objective: To verify that a Potential Hotspot can be saved with hotspot type added using Blackspot API.
                client = new RestClient(Constant.imaapGisUrl);
                SaveBlackspot saveHostspotData = RetrieveTestData.GetSaveBlackspotDataBody(saveBlackspotDataFile);
                addHotspotReport = GenerateReport.GetReportFile("SavePotentialBlackspotReport.json");
                string apiName = "/api/blackspot";
                var addHotspotRequest = new RestRequest(apiName, Method.POST);
                addHotspotRequest.RequestFormat = DataFormat.Json;
                addHotspotRequest.AddHeader("Content-type", "application/json");
                addHotspotRequest.AddHeader("x-bif-private-token", authToken);
                addHotspotRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                addHotspotRequest.AddJsonBody(saveHostspotData);
                Stopwatch.Start();
                IRestResponse addHotspotResponse = client.Execute(addHotspotRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                hotspotTypeName = saveHostspotData.blackspotInformation.attributes.name;
                var addHotspotResponseDto = deserialize.Deserialize<HotspotTypeDto>(addHotspotResponse);
                if (addHotspotResponseDto != null && addHotspotResponseDto.responseMessages[0].messageKey.Trim().Equals("Messages.BLACKSPOT_SAVED") && addHotspotResponse.StatusCode == HttpStatusCode.OK)
                {
                    addHotspotReport[0].actualResultFail = string.Empty;
                    GenerateReport.WriteTestLog(apiName, new JavaScriptSerializer().Serialize(saveHostspotData), addHotspotResponse.Content, "POST");

                }

                else
                {
                    try
                    {

                        //Test Objective: To verify that a Potential Hotspot can be saved with hotspot type added using Blackspot API.
                        var potentialHotspotData = RetrieveTestData.GetPotentialBlackspotDataBody(potentialHotspotDataFile);
                        string apiName2 = "/api/blackspot";
                        var addHotspotRequest2 = new RestRequest(apiName2, Method.POST);
                        addHotspotRequest2.RequestFormat = DataFormat.Json;
                        addHotspotRequest2.AddHeader("Content-type", "application/json");
                        addHotspotRequest2.AddHeader("x-bif-private-token", authToken);
                        addHotspotRequest2.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                        addHotspotRequest2.AddJsonBody(potentialHotspotData);
                        Stopwatch.Start();
                        IRestResponse addHotspotResponse2 = client.Execute(addHotspotRequest2);
                        Stopwatch.Stop();
                        responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                        Stopwatch.Reset();
                        var addHotspotResponseDto2 = deserialize.Deserialize<HotspotTypeDto>(addHotspotResponse2);
                        if (addHotspotResponseDto2 != null && addHotspotResponseDto2.responseMessages[0].messageKey.Trim().Equals("Messages.BLACKSPOT_DETAILS_UPDATED") && addHotspotResponse2.StatusCode == HttpStatusCode.OK)
                        {
                            addHotspotReport[0].actualResultFail = string.Empty;
                        }
                    }
                    catch (Exception e)
                    {
                        listOfReports.AddRange(addHotspotReport);
                        Console.WriteLine(e);
                        throw new NotImplementedException();
                    }
                }
                addHotspotReport[0].reqResponseTime = responseTime;
                listOfReports.AddRange(addHotspotReport);
            }
            catch (Exception e)
            {
                listOfReports.AddRange(addHotspotReport);
                Console.WriteLine(e);
                throw new NotImplementedException();
            }

            return listOfReports;
       

        }

        /// <summary>
        /// Verify hotspot report API - /api/blackspots/standardreport.
        /// </summary>
        public List<TestReportSteps> HostspotReport(string APIDataFile)
        {
            try
            {
                bool isReportVerified = false;
                //Test Objective: To verify that hotspot report is displayed properly.
                client = new RestClient(Constant.imaapUrl);
                var hotspotData = RetrieveTestData.GetHotspotReportDataBody(APIDataFile);
                hotspotReport = GenerateReport.GetReportFile("HotspotReport.json");
                string apiName = "/api/blackspots/standardreport";
                var hotspotReportRequest = new RestRequest(apiName, Method.POST);
                hotspotReportRequest.RequestFormat = DataFormat.Json;
                hotspotReportRequest.AddHeader("Content-type", "application/json");
                hotspotReportRequest.AddHeader("x-bif-private-token", authToken);
                hotspotReportRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
                hotspotReportRequest.AddJsonBody(hotspotData);
                Stopwatch.Start();
                IRestResponse response = client.Execute(hotspotReportRequest);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var responseDto = deserialize.Deserialize<HotspotResponse>(response);
                int length = responseDto.data.potentialBlackspots.Count;
                for (int i = 0; i < length; i++)
                {
                    if (responseDto.data.potentialBlackspots[i].name.Equals(hotspotTypeName) && responseDto.data.potentialBlackspots[i].blackspotTypeId == hotspotTypeId && response.StatusCode == HttpStatusCode.OK)
                    {
                        isReportVerified = true;
                        break;
                    }
                }
                if(isReportVerified)
                    hotspotReport[0].actualResultFail = string.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                listOfReports.AddRange(hotspotReport);
                throw new NotImplementedException();
            }

            return hotspotReport;
        }

    }
}
