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
    }
}
