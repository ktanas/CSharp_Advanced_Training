using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationComponent.CustomAttributes;

namespace AttributesExamples.Models
{
    public class Department
    {
        [RequiredAttribute]
        public int Id { get; set; }
        [RequiredAttribute]
        [StringLengthAttribute(15,2)]
        public string DeptShortName { get; set; }
        public string DeptLongName { get; set; }

    }
}
