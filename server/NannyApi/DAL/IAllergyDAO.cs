using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    interface IAllergyDAO
    {
        /// <summary>
        /// Gets a list of every allergy
        /// </summary>
        /// <returns>List of Allergies</returns>
        public List<Allergy> GetAllergies();
        /// <summary>
        /// Lists all the allergies by their type id
        /// </summary>
        /// <param name="typeId">The id of the allergy types</param>
        /// <returns>List of Allergies</returns>
        public List<Allergy> GetAllergiesByType(int typeId);
    }
}
