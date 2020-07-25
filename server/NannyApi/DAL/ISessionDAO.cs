using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public interface ISessionDAO
    {
        /// <summary>
        /// Creates a new session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="caretakerId"></param>
        /// <param name="childId"></param>
        /// <returns>Session Object</returns>
        public Session CreateNewSession(Session session, int caretakerId, int childId);
        /// <summary>
        /// Gets a session by its Id and caretaker Id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="careTakerId"></param>
        /// <returns>Session Object</returns>
        public Session GetSessionById(int sessionId, int careTakerId);
        /// <summary>
        /// Gets a list of all sessions for a specific caretaker
        /// </summary>
        /// <param name="careTakerId"></param>
        /// <returns></returns>
        public List<Session> GetAllSessionsByCareTakerId(int careTakerId);
    }
}
