using Ex_haft.Configuration;
using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using iMAAPTestAPI;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FrameworkSetup.APITestScript
{
    internal class GetExportStatus
    {
        private static RestClient client;
        private static string authToken;
        private static readonly JsonDeserializer deserialize = new JsonDeserializer();
        private static Stopwatch Stopwatch = new Stopwatch();

        public static ExportStatusDto GetExportStatusMethod()
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
                var output = deserialize.Deserialize<SampleMasterData>(loginResponse);
                authToken = output?.Data;

                client = new RestClient(Constant.imaapUrl);
                apiName = "/api/export/status";
                var exportStatus = new RestRequest(apiName, Method.POST);
                var exportStatusBody = ConfigFile.RetriveExportStatusData("GetExportStatusTest.json");
                var exportStatusData = RetrieveTestData.GetExportDataBody(exportStatusBody);
                exportStatus.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                exportStatus.AddHeader("x-bif-private-token", authToken);
                exportStatus.AddHeader("selectedapplicationid", "1");
                exportStatus.AddHeader("selectedpageid", "4");
                exportStatus.AddHeader("timezone-offset", "-330");
                exportStatus.AddJsonBody(exportStatusData);
                exportStatus.RequestFormat = DataFormat.Json;
                Stopwatch.Start();
                IRestResponse exportStatusResponse = client.Execute(exportStatus);
                Stopwatch.Stop();
                Stopwatch.Reset();
                var exportStatusDto = deserialize.Deserialize<ExportStatusDto>(exportStatusResponse);
                return exportStatusDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }
    }
}