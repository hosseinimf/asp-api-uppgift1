using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.ViewModel
{
    public class CreateCommentModel
    {
        public int IssueId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool Succeeded { get; set; }
    }
}
