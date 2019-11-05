

using Ex_haft.Configuration;
using Ex_haft.GenericComponents;
using Ex_haft.TestDataClasses;
using Ex_haft.Utilities;
using FrameworkSetup.TestDataClasses;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;

namespace FrameworkSetup.TestScript
{

    class SetFormFieldsTest
    {

        RestClient client;
        string authToken;
        readonly JsonDeserializer deserialize = new JsonDeserializer();
        public SetFormFieldsTest()
        {
            SetFormField();
        }




        public void SetFormField()
        {
            client = new RestClient(Constant.imaapUrl);
            string apiName = "/api/authenticate/mobile";
            var loginRequest = new RestRequest(apiName, Method.POST);
            loginRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
            var login = new { username = "Gokul", password = "Trl@12345" };
            loginRequest.RequestFormat = DataFormat.Json;
            loginRequest.AddJsonBody(login);
            IRestResponse loginResponse = client.Execute(loginRequest);
            var output = deserialize.Deserialize<SampleMasterData>(loginResponse);
            authToken = output?.Data;

            string apiNameMaster = "/api/masterdata/{timestamp}";
            var getMasterRequest = new RestRequest(apiNameMaster, Method.GET);
            getMasterRequest.RequestFormat = DataFormat.Json;
            getMasterRequest.AddHeader("Content-type", "application/json");
            getMasterRequest.AddHeader("x-bif-private-token", authToken);
            getMasterRequest.AddHeader("x-app-client-token", "afdb9cc5ed6c336008b3d01bf90c75a2");
            getMasterRequest.AddParameter("timestamp", "0", ParameterType.UrlSegment);
            IRestResponse getMasterResponse = client.Execute(getMasterRequest);
            var getMasterResponseDto = deserialize.Deserialize<MasterData>(getMasterResponse);
            var crashData = new List<PostData>();
            var vehicleData = new List<PostData>();
            var casualtyData = new List<PostData>();
            //Adding API response to List
            for (int i = 1; i < getMasterResponseDto.data.fields.Count; i++)
            {
                if (getMasterResponseDto.data.fields[i].formId == 1 && getMasterResponseDto.data.fields[i].isVisibleInARF == true)
                {
                    var a = new PostData()
                    {
                        key = getMasterResponseDto.data.fields[i].propertyName,
                        value = getMasterResponseDto.data.fields[i].propertyName + "00",
                        propertyTypeId = getMasterResponseDto.data.fields[i].propertyTypeId
                    };
                    crashData.Add(a);
                }
                else if (getMasterResponseDto.data.fields[i].formId == 2 && getMasterResponseDto.data.fields[i].isVisibleInARF == true)
                {
                    var a = new PostData()
                    {
                        key = getMasterResponseDto.data.fields[i].propertyName,
                        value = getMasterResponseDto.data.fields[i].propertyName,
                        propertyTypeId = getMasterResponseDto.data.fields[i].propertyTypeId
                    };
                    vehicleData.Add(a);
                }
                else if (getMasterResponseDto.data.fields[i].formId == 3 && getMasterResponseDto.data.fields[i].isVisibleInARF == true)
                {
                    var a = new PostData()
                    {
                        key = getMasterResponseDto.data.fields[i].propertyName,
                        value = getMasterResponseDto.data.fields[i].propertyName,
                        propertyTypeId = getMasterResponseDto.data.fields[i].propertyTypeId
                    };
                    casualtyData.Add(a);
                }
            }

            //Write API Response for crash related fields to selector file
            string crashDataJson = JsonConvert.SerializeObject(crashData.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(ReusableComponents.GetAbsoluteFilePath("UISelectors\\CrashDataPageSelector.json"), crashDataJson);

            //Write API Response for vehicle related fields to selector file
            string vehicleDataJson = JsonConvert.SerializeObject(vehicleData.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(ReusableComponents.GetAbsoluteFilePath("UISelectors\\VehicleDataPageSelector.json"), vehicleDataJson);

            //Write API Response for casualty related fields to selector file
            string casualtyDataJson = JsonConvert.SerializeObject(casualtyData.ToArray(), Formatting.Indented);
            System.IO.File.WriteAllText(ReusableComponents.GetAbsoluteFilePath("UISelectors\\CasualtyDataPageSelector.json"), casualtyDataJson);

            //Write Crash page uiselector file
            var crashUiJsonResult = ReusableComponents.RetrieveUISelectors("CrashDataPageSelector.json");
            var crashSelectors = JsonConvert.DeserializeObject<List<PostData>>(crashUiJsonResult);
            var crashJsonResult = ReusableComponents.RetrieveMasterDataSelectors("CrashDataMasterPageSelector.json");
            var crashMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(crashJsonResult);
            var crashJObjects = ConfigFile.RetrieveUIMap("CrashPageSelector.json");
            ReusableComponents.GenerateARFSelectorFiles(crashMasterDataSelectors, crashSelectors, crashJObjects);
            string crashSelectorJson = JsonConvert.SerializeObject(crashJObjects, Formatting.Indented);
            System.IO.File.WriteAllText(ReusableComponents.GetAbsoluteFilePath("UISelectors\\CrashPageSelector.json"), crashSelectorJson);

            //Write Crash page uiselector file
            var vehicleUiJsonResult = ReusableComponents.RetrieveUISelectors("VehicleDataPageSelector.json");
            var vehicleSelectors = JsonConvert.DeserializeObject<List<PostData>>(vehicleUiJsonResult);
            var vehicleJsonResult = ReusableComponents.RetrieveMasterDataSelectors("VehicleDataMasterPageSelector.json");
            var vehicleMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(vehicleJsonResult);
            var vehicleJObjects = ConfigFile.RetrieveUIMap("VehiclePageSelector.json");
            ReusableComponents.GenerateARFSelectorFiles(vehicleMasterDataSelectors, vehicleSelectors, vehicleJObjects);
            string vehicleSelectorJson = JsonConvert.SerializeObject(vehicleJObjects, Formatting.Indented);
            System.IO.File.WriteAllText(ReusableComponents.GetAbsoluteFilePath("UISelectors\\VehiclePageSelector.json"), vehicleSelectorJson);


            //Write Crash page uiselector file
            var casualtyUijsonResult = ReusableComponents.RetrieveUISelectors("CasualtyDataPageSelector.json");
            var casualtySelectors = JsonConvert.DeserializeObject<List<PostData>>(casualtyUijsonResult);
            var casualtyJsonResult = ReusableComponents.RetrieveMasterDataSelectors("CasualtyDataMasterPageSelector.json");
            var casualtyMasterDataSelectors = JsonConvert.DeserializeObject<List<RetrieveMasterData>>(casualtyJsonResult);
            var casualtyJObjects = ConfigFile.RetrieveUIMap("CasualtyPageSelector.json");
            ReusableComponents.GenerateARFSelectorFiles(casualtyMasterDataSelectors, casualtySelectors, casualtyJObjects);
            string casualtySelectorJson = JsonConvert.SerializeObject(casualtyJObjects, Formatting.Indented);
            System.IO.File.WriteAllText(ReusableComponents.GetAbsoluteFilePath("UISelectors\\CasualtyPageSelector.json"), casualtySelectorJson);




        }



    }



}
