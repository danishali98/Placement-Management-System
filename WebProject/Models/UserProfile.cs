using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    [NotMapped]
    public class UserProfile
    {
        public string email { get; set; }

        public string userName { get; set; }

        public string name { get; set; }

        public string phone { get; set; }

        public string role { get; set; }
    }
}