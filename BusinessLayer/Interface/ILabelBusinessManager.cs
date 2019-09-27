﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBusinessManager
    {
        Task<bool> AddLabel(LabelModel model);
        Task<string> DeleteLabel(int id);
        Task<string> UpdateLabel(LabelModel model, int id);
        List<LabelModel> GetLabel(string userId);
    }
}
