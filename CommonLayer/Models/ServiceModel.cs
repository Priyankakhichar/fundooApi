using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
   public class ServiceModel
    {
        
        public int Id
        {
            get; set;
        }

        [Required]
        public string ServiceName
        {
            get; set;
        }

        [Required]
        public bool IsDeleted
        {
            get; set;
        }

        [Required]
        public DateTime? CreatedDate
        {
            get;set;
        }

        [Required]
        public DateTime? ModifiedDate
        {
            get;set;
        }
    }
}
