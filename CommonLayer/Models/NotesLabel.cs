using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class NotesLabel
    {
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
        
        [ForeignKey("UserRegistration")]
        public string UserId
        {
            get; set;
        }
    }
}
