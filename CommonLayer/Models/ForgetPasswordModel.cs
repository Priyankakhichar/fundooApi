////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ForgetPasswordModel.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------

namespace CommonLayer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class ForgetPasswordModel
    {
        [ForeignKey("UserRegistration")]
        public string EmailId
        {
            get; set;
        }
    }
}
