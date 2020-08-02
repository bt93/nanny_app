using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface IDiaperDAO
    {
        /// <summary>
        /// Adds a diaper change
        /// </summary>
        /// <param name="diaper"></param>
        /// <returns>Diaper</returns>
        public Diaper AddDiaper(Diaper diaper);
        /// <summary>
        /// Deletes a diaper change
        /// </summary>
        /// <param name="diaperId"></param>
        /// <returns></returns>
        public int DeleteDiaper(int diaperId);
        /// <summary>
        /// Gets a single diaper change
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <param name="diaperId"></param>
        /// <returns>Diaper</returns>
        public Diaper GetADiaperBySession(int sessionId, int careTakerId, int diaperId);
        /// <summary>
        /// Gets all the diapers by the sessions
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <returns>List of Diapers</returns>
        public List<Diaper> GetAllDiapersBySession(int sessionId, int careTakerId);
        /// <summary>
        /// Updates a diaper change
        /// </summary>
        /// <param name="diaper"></param>
        /// <param name="careTakerId"></param>
        /// <returns>Diaper</returns>
        public Diaper UpdateDiaper(Diaper diaper, int careTakerId);
    }
}
