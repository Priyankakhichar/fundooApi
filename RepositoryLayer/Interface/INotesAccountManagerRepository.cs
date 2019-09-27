using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
   public interface INotesAccountManagerRepository
    {
        Task<bool> AddNotes(NotesModel model);
        Task<int> DeleteNotes(int id);
        Task<bool> UpdatesNotes(NotesModel model, int id);
        List<NotesModel> GetNotes(string userId);
    }
}
