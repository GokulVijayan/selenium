using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSetup.TestDataClasses
{
    class KsiDefinition
    {
        public int ksiType { get; set; }
        public int lookupDetailId { get; set; }
        public IList<KsiLookupValue> lookupValues { get; set; }
        public bool isSelected { get; set; }
        public bool isKSI { get; set; }
    }
}
