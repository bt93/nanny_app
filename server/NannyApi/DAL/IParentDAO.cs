using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NannyApi.DAL
{
    public interface IParentDAO
    {
        /// <summary>
        /// Returns A full list of parents on the database
        /// </summary>
        /// <returns>List of Parent Objects</returns>
        public List<Parent> GetParents();
        /// <summary>
        /// Will return any parent that is connected to a single child
        /// </summary>
        /// <param name="childId"></param>
        /// <returns>List of Parent Objects</returns>
        public List<Parent> GetParentsByChild(int childId);
        /// <summary>
        /// Returns a single parent from their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parent Object</returns>
        public Parent GetParentById(int id);
        /// <summary>
        /// Creates a new parent and returns it
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>Parent Object</returns>
        public Parent AddParent(Parent parent);
    }
}
