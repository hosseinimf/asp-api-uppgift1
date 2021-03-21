using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.Models
{
    public class Comment
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int IssueId { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }


        public DateTime Created { get; set; }

    }
}
