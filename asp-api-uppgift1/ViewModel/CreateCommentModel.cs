using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.ViewModel
{
    public class CreateCommentModel
    {
        public int IssueId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
