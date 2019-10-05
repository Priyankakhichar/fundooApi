////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "AccountController.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// label model class
    /// </summary>
    public class LabelModel
    {

        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// user id declared as foregin key
        /// </summary>
        [ForeignKey("UserRegistration")]
        public string UserId
        {
            get; set;
        }

        /// <summary>
        /// label name
        /// </summary>
        public string LableName
        {
            get; set;
        }

        /// <summary>
        /// created date
        /// </summary>
        public DateTime? CreatedDate
        {
            get; set;
        }

        /// <summary>
        /// modified date
        /// </summary>
        public DateTime? ModifiedDate
        {
            get; set;
        }
       
    }
}
