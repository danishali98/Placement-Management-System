using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class StudentRecord
    {
        public StudentRecord()
        {
            experienceList = new List<Experience>();
            messageList = new List<Message>();
        }

        [Key]
        [Required]
        public string rollNum { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string discipline { get; set; }

        [Required]
        public string cgpa { get; set; }

        [Required]
        public string batch { get; set; }

        public List<Experience> experienceList { get; set; }

        public List<Message> messageList { get; set; }
    }
}