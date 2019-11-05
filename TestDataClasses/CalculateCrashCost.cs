using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{

    public class CalculateCrashCost
    {
        public int id { get; set; }
        public DateRange dateRange { get; set; }
        public string dateType { get; set; }
        public int fromDate { get; set; }
        public int toDate { get; set; }
        public string selectedAreaGeoJson { get; set; }

    }

    public class BeginDate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class EndDate
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }

    public class DateRange
    {
        public BeginDate beginDate { get; set; }
        public DateTime beginJsDate { get; set; }
        public EndDate endDate { get; set; }
        public DateTime endJsDate { get; set; }
        public string formatted { get; set; }
        public int beginEpoc { get; set; }
        public int endEpoc { get; set; }
    }


}
