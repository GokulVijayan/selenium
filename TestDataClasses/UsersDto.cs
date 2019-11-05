using System.Collections.Generic;

namespace FrameworkSetup.TestDataClasses
{
    public class UsersDto
    {
        public List<ResponseMessage> responseMessages { get; set; }
        public string data { get; set; }
        public int status { get; set; }
    }

    
}