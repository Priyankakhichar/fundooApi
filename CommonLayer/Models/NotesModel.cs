using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
   public class NotesModel
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public string Color
        {
            get; set;
        }

        [ForeignKey("UserRegistration")]
        public string UserId
        {
            get; set;
        }
        public EnumNoteType NoteType
        {
            get; set;
        }
        public DateTime CreateDate
        {
            get; set;
        }
        public DateTime ModifiedDate
        {
            get; set;
        }
    }
}
