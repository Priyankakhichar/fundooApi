using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class NotesCollaboration
    {
        public int Id
        {
            get; set;
        }
        [ForeignKey("NotesModel")]
        public int NoteId
        {
            get; set;
        }
        [ForeignKey("UserRegistration")]
        public string UserId
        {
            get; set;
        }
        [ForeignKey("UserRegistration")]
        public string CreatedBy
        {
            get; set;
        }
    }
}
