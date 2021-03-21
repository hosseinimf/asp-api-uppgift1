using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asp_api_uppgift1.Models
{
    public class Customer
    {
        [Required]
        [Key]
        public long Id { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }


        public DateTime Created { get; set; }

        public string FullName => $"{FirstName} {LastName}";


        public virtual ICollection<Issue> Issues { get; set; }
    }
}
