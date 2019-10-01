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

   /// <summary>
   /// forget password model class
   /// </summary>
    public class ForgetPasswordModel
    {
        /// <summary>
        /// foregin key email id from user registration
        /// </summary>
        [ForeignKey("UserRegistration")]
        public string EmailId
        {
            get; set;
        }
    }
}
