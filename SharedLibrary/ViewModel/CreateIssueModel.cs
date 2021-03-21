
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLibrary.ViewModel
{ 
    public class CreateIssueModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }


        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
