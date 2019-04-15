using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Address { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string Position { get; set; }
    }
}
