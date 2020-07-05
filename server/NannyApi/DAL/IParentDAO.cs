using NannyApi.Models;
using System.Collections.Generic;

namespace NannyApi.DAL
{
    public interface IParentDAO
    {
        /// <summary>
        /// Returns A full list of parents on the database
        /// </summary>
        /// <returns>List of Parent Objects</returns>
        public List<Parent> GetParents(int caretakerId);
        /// <summary>
        /// Will return any parent that is connected to a single child
        /// </summary>
        /// <param name="childId"></param>
        /// <returns>List of Parent Objects</returns>
        public List<Parent> GetParentsByChild(int childId, int caretakerId);
        /// <summary>
        /// Returns a single parent from their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parent Object</returns>
        public Parent GetParentById(int id, int caretakerId);
        /// <summary>
        /// Creates a new parent and returns it
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>Parent Object</returns>
        public Parent AddParent(Parent parent, int childId);
        /// <summary>
        /// Updates a parent in the database
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>Parent Object</returns>
        public Parent UpdateParent(Parent parent);
        /// <summary>
        /// Updates child_parent table to add exsiting parent to a child
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="parentId"></param>
        public bool AddExsistingParent(int childId, int parentId);
    }
}
