using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class CustomUserLogin
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UniqueId { get; set; }

        public string UserAgent { get; set; }

        [ForeignKey(name: "User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
