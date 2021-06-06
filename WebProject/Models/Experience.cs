using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class Experience
    {
        public Experience() { }

        [Key]
        public int Id { get; set; }

        [Required]
        public string rollNum { get; set; }

        [Required]
        public string organization { get; set; }

        [Required]
        public string location { get; set; }

        [Required]
        public string position { get; set; }

        public string details { get; set; }

        [Required]
        public string startDate { get; set; }

        [Required]
        public string endDate { get; set; }

        public int stipend { get; set; }
    }
}