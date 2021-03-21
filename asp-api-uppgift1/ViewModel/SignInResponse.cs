using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.ViewModel
{
    public class SignInResponse
    {
        public bool Succeeded { get; set; }
        public SignInResponseResult Result { get; set; }
    }


    public class SignInResponseResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
