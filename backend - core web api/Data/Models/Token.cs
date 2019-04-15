using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Token
    {
        public Token()
        {
            this.IsActive = true;
        }
        public int Id { get; set; }
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public bool IsActive { get; set; }

        public DateTime RefreshTokenExpiryDate { get; set; }

        public string UniqueId { get; set; }

        [ForeignKey(name:"User")]
        public string  UserId {get;set;}
        public IdentityUser User { get; set; }
    }
}
