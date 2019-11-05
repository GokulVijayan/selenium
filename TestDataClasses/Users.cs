using System.Collections.Generic;

namespace FrameworkSetup.TestDataClasses
{
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string confirmpassword { get; set; }
        public string passwordhash { get; set; }
        public int authenticationtypeid { get; set; }
        public string email { get; set; }
        public string confirmemail { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string departmentId { get; set; }
        public string designationName { get; set; }
        public string userRoleId { get; set; }
        public bool isLocked { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public bool isCurrentUser { get; set; }
        public bool hasFullPrivilege { get; set; }
        public int preferredLanguageId { get; set; }
        public List<object> userDataSecurityDetails { get; set; }
        public List<DataSecuritySelection> dataSecuritySelection { get; set; }
    }

    public class DataSecuritySelection
    {
        public bool isSelected { get; set; }
        public int accessType { get; set; }
        public int lookupId { get; set; }
        public string userDataSecurityDetails { get; set; }
    }
}