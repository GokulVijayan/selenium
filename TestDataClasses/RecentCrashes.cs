using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMAAPTestAPI
{
    public class Data
    {
        public int id { get; set; }
        public string internalName { get; set; }
        public string type { get; set; }
        public List<FieldTypeName> fieldTypeName { get; set; }
    }
}
