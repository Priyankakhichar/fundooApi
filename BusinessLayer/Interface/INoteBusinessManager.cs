using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBusinessManager
    {
          Task<bool> AddNotes(NotesModel model);
          Task<int> DeleteNotes(int id);
          Task<bool> UpdateNotes(NotesModel model, int id);
        List<NotesModel> GetNotes(string userId);
    }
}
