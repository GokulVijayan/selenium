using System.Collections.Generic;

namespace FrameworkSetup.TestDataClasses
{
    public class RolesDto
    {
        public List<object> responseMessages { get; set; }
        public List<RoleDatum> data { get; set; }
        public int status { get; set; }
    }

    public class RoleName
    {
        public int id { get; set; }
        public int roleId { get; set; }
        public int languageId { get; set; }
        public string name { get; set; }
    }

    public class RoleDatum
    {
        public bool roleType { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public int? landingPageId { get; set; }
        public List<RoleName> roleName { get; set; }
    }
}