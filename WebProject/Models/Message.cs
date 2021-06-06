using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string rollNum { get; set; }

        [Required]
        public string subject { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public string status { get; set; }

        [Required]
        public DateTime time { get; set; }
    }
}