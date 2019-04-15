using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.ViewModels
{
    public class RefreshTokenViewModel
    {
        [Required]
        public string token { get; set; }

        [Required]
        public string refreshToken { get; set; }

        [Required]
        public string uniqueId { get; set; }
    }
}
