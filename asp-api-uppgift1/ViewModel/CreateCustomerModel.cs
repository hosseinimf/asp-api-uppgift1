using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.ViewModel
{
    public class CreateCustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
