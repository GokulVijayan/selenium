using Ex_haft.Utilities;
using Ex_haft.Utilities.Reports;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FrameworkSetup.APITestScript
{
    internal class GetPasswordPolicy
    {
        private static RestClient client;
        private static string authToken;
        private static readonly JsonDeserializer deserialize = new JsonDeserializer();
        private static Stopwatch Stopwatch = new Stopwatch();
        private static double responseTime;

        public static PasswordPolicyDto GetPasswordPolicies()
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

                client = new RestClient(Constant.commonApiQc);
                apiName = "/api/passwordpolicies";
                var passwordPolicy = new RestRequest(apiName, Method.GET);
                passwordPolicy.AddHeader("x-app-client-token", "f75ee33acd81b75f7de6c31ceb495e70");
                passwordPolicy.AddHeader("x-bif-private-token", authToken);
                passwordPolicy.AddHeader("selectedapplicationid", "1");
                passwordPolicy.AddHeader("selectedpageid", "39");
                passwordPolicy.AddHeader("timezone-offset", "-330");
                passwordPolicy.RequestFormat = DataFormat.Json;
                Stopwatch.Start();
                IRestResponse passwordPolicyResponse = client.Execute(passwordPolicy);
                Stopwatch.Stop();
                responseTime = Stopwatch.Elapsed.TotalMilliseconds;
                Stopwatch.Reset();
                var passwordPolicyDto = deserialize.Deserialize<PasswordPolicyDto>(passwordPolicyResponse);
                return passwordPolicyDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }
    }
}