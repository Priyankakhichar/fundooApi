using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "nvarchar(150)")]
        public string FirstName
        {
            get; set;
        }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName
        {
            get; set;
        }
    }
}
