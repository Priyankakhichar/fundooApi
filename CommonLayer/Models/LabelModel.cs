using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace CommonLayer.Models
{
   public class LabelModel
    {
        public int Id
        {
            get;
            set;
        }

        [ForeignKey("UserRegistration")]
        public string UserId
        {
            get; set;
        }
        public string LableName
        {
            get; set;
        }
        public DateTime CreatedDate
        {
            get; set;
        }
        public DateTime ModifiedDate
        {
            get; set;
        }
    }
}
