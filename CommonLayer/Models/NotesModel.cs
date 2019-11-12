////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesModel.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using CommonLayer.Enum;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// notes model class
    /// </summary>
    public class NotesModel
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
        /// title
        /// </summary>
        public string Title
        {
            get; set;
        }
        
        
        /// <summary>
        /// description
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// color
        /// </summary>
        public string Color
        {
            get; set;
        }


        /// <summary>
        /// enum type 
        /// </summary>
        [Range(0, 2,
         ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public EnumNoteType NoteType
        {
            get; set;
        }
  
        public bool IsPin
        {
            get; set;
        }
        public DateTime? Reminder
        {
            get; set;
        }
        public string Image
        {
            get; set;
        }
        /// <summary>
        /// created date
        /// </summary>
        public DateTime? CreateDate
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

        public IList<LabelModel> labelIdList { get; set; }

        [NotMapped]
        public IList<int> Notes
        {
            get; set;
        }
    }
}
