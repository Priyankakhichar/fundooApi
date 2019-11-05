////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "NotesLabel.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Notes label model class 
    /// </summary>
    public class NotesLabel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// foreign key note id from notes Model class
        /// </summary>
        [ForeignKey("NotesModel")]
        public int NoteId
        {
            get; set;
        }

        /// <summary>
        /// foreign key label id from label class
        /// </summary>
        [ForeignKey("LabelModel")]
        public int LabelId
        {
            get; set;
        }
        
        /// <summary>
        /// user id 
        /// </summary>
        [ForeignKey("UserRegistration")]
        public string UserId
        {
            get; set;
        }
    }
}
