////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ServiceModel.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// service model class
    /// </summary>
    public class ServiceModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// Service Name
        /// </summary>
        [Required]
        public string ServiceName
        {
            get; set;
        }

        /// <summary>
        /// deleted
        /// </summary>
        [Required]
        public bool IsDeleted
        {
            get; set;
        }

        /// <summary>
        /// created date
        /// </summary>
        [Required]
        public DateTime? CreatedDate
        {
            get;set;
        }

        /// <summary>
        /// modified date
        /// </summary>
        [Required]
        public DateTime? ModifiedDate
        {
            get;set;
        }
    }
}
