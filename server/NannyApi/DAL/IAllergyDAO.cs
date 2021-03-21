using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface IAllergyDAO
    {
        /// <summary>
        /// Gets a list of every allergy
        /// </summary>
        /// <returns>List of Allergies</returns>
        public List<Allergy> GetAllergies();
        /// <summary>
        /// Get's a list of all the allergy types
        /// </summary>
        /// <returns>List of AllergyTypes</returns>
        public List<AllergyType> getAllergyTypes();
        /// <summary>
        /// Lists all the allergies by their type id
        /// </summary>
        /// <param name="typeId">The id of the allergy types</param>
        /// <returns>List of Allergies</returns>
        public List<Allergy> GetAllergiesByType(int typeId);
        /// <summary>
        /// Returns a list of all the allergies that a specific child has.
        /// </summary>
        /// <returns>List of Allergies</returns>
        public List<Allergy> GetAllergiesByChildId(int childId);
        /// <summary>
        /// Adds an allergy to the child
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="allergyId"></param>
        /// <returns>True if the the row is affected</returns>
        public bool AddAllergyToChild(int childId, int allergyId);
        /// <summary>
        /// Removes an allergy from a child
        /// </summary>
        /// <param name="childId"></param>
        /// <param name="allergyId"></param>
        /// <returns>True if the row is affected</returns>
        public bool RemoveAllergyFromChild(int childId, int allergyId);
    }
}
