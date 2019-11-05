
using Ex_haft.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
namespace FrameworkSetup.TestDataClasses
{
    internal class ExportCrashExcelDto
    {
        [Column(1)]
        public string CrashRefNo { get; set; }

        [Column(2)]
        public string NumberOfVehicles { get; set; }

        [Column(3)]
        public string NumberOfCasualities { get; set; }
    }
}