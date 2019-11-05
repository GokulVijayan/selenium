

using Ex_haft.Utilities;

namespace FrameworkSetup.TestDataClasses
{
    public class FormFieldExcelDto
    {
        [Column(1)]
        public string Form { get; set; }

        [Column(2)]
        public string Name { get; set; }

        [Column(3)]
        public string Type { get; set; }

        [Column(4)]
        public string Lookup { get; set; }

        [Column(5)]
        public string Length { get; set; }

        [Column(6)]
        public string Order { get; set; }
    }
}
