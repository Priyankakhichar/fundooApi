////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ILabelBusinessManager.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Label Business manager interface
    /// </summary>
    public interface ILabelBusinessManager
    {
        /// <summary>
        /// Add label method for adding the label
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddLabel(LabelModel model);

        /// <summary>
        /// delete label method to remove the label
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteLabel(int id);

        /// <summary>
        /// update label is for updating the label
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> UpdateLabel(LabelModel model, int id);

        /// <summary>
        /// get label method to get all labels by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<LabelModel> GetLabel(string userId);
    }
}
