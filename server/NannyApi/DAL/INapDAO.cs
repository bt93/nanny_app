using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface INapDAO
    {
        /// <summary>
        /// Adds a nap
        /// </summary>
        /// <param name="nap"></param>
        /// <returns>Nap Object</returns>
        public Nap AddNap(Nap nap);
        /// <summary>
        /// Deletes a nap
        /// </summary>
        /// <param name="napId"></param>
        /// <returns>Int</returns>
        public int DeleteNap(int napId);
        /// <summary>
        /// Lists all the naps by session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <returns>List<Nap> Objects</returns>
        public List<Nap> GetAllNapsBySession(int sessionId, int careTakerId);
        /// <summary>
        /// Gets a nap by id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <param name="napId"></param>
        /// <returns></returns>
        public Nap GetANapBySession(int sessionId, int careTakerId, int napId);
        /// <summary>
        /// Updates a nap
        /// </summary>
        /// <param name="nap"></param>
        /// <param name="careTakerId"></param>
        /// <returns></returns>
        public Nap UpdateNap(Nap nap, int careTakerId);
    }
}
