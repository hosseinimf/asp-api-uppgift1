﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.ViewModel
{
    public class CreateIssueResponseModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
    }
}
