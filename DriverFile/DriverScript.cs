
using Ex_haft.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FrameworkSetup.DriverFile
{
    [TestFixture]
    class DriverScript
    {
     
        /// <summary>
        /// Driver Script-The Test Execution Starts from here
        /// </summary>
        [Test]
        public void Test()
        {

            Assembly testAssembly;

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/FrameworkSetup.dll";
            Console.WriteLine(assemblyFolder);
            testAssembly = Assembly.LoadFile(assemblyFolder);
            Console.WriteLine(testAssembly);


            string json = ConfigFile.RetrieveTestScripts("Driver.json");
            var scriptConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (var keyValue in scriptConfig)
            {
                if (keyValue.Value.Equals("true"))
                {
                    string className = "FrameworkSetup.TestScript." + keyValue.Key;
                    Type runObject = testAssembly.GetType(className);
                    Activator.CreateInstance(runObject);
                }
            }

        }
    }
}
