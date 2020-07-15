using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface IChildDAO
    {
        /// <summary>
        /// Creates a new child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        Child AddChild(Child child, int caretakerId);
        /// <summary>
        /// Removes a child from the database
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="caretakerId"></param>
        /// <returns>Bool</returns>
        bool DeleteChild(int childId, int caretakerId);

        /// <summary>
        /// Returns a child by its id
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="careTakerId"></param>
        /// <returns></returns>
        Child GetChildById(int childId, int careTakerId);

        /// <summary>
        /// Returns a list of children
        /// </summary>
        /// <returns>List<Child> Objects</returns>
        List<Child> GetChildren(int careTakerId);
        /// <summary>
        /// Updates a child
        /// </summary>
        /// <param name="child"></param>
        /// <param name="caretakerId"></param>
        /// <returns>Child Object</returns>
        Child UpdateChild(Child child, int childId, int caretakerId);
    }
}
