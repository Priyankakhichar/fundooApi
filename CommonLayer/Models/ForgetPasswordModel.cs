using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class ForgetPasswordModel
    {
        [ForeignKey("UserRegistration")]
        public string EmailId
        {
            get; set;
        }
    }
}
