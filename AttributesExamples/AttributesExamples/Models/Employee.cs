using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using ValidationComponent.CustomAttributes;

namespace AttributesExamples.Models
{
    public class Employee
    {
        [RequiredAttribute]
        public int Id { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15,"Field {0} 's length must not be shorter than {2} or longer than {1} characters",2)]
        public string FirstName { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15, "Field {0} 's length must not be shorter than {2} or longer than {1} characters", 2)]
        public string LastName { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15, "Field {0} 's length must not be shorter than {2} or longer than {1} characters", 2)]
        [RegularExpressionAttribute(@"\s*\(?0\d{4}\)?\s*\d{6}\s*)|(\s*\(?0\d{3}\)?\s*\d{3}\s*\d{4}\s*")]
        [JsonIgnoreAttribute]
        public string PhoneNumber { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15, "Field {0} 's length must not be shorter than {2} or longer than {1} characters", 2)]
        [RegularExpressionAttribute(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?$")]
        public string EmailAddress { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15, "Field {0} 's length must not be shorter than {2} or longer than {1} characters", 2)]
        [RegularExpressionAttribute(@"^ ?(([BEGLMNSWbeglmnsw][0-9][0-9]?)|(([A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][0-9]?)|(([ENWenw][0-9][A-HJKSTUWa-hjkstuw])|([ENWenw][A-HK-Ya-hk-y][0-9][ABEHMNPRVWXYabehmnprvwxy])))) ?[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$")]
        [JsonIgnoreAttribute]
        public string PostCode { get; set; }
    }
}
