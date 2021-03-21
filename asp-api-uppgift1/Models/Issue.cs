using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.Models
{
    public class Issue
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CustomerId { get; set; }



        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }

        [Required]
        public Status Status { get; set; }


        public DateTime Created { get; set; }


        public virtual User User { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
