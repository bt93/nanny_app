using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;
using NannyApi.Models;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SessionsController : ControllerBase
    {
        private int userId
        {
            get
            {
                foreach (Claim claim in User.Claims)
                {
                    if (claim.Type == "sub")
                    {
                        return Convert.ToInt32(claim.Value);
                    }
                }

                return 0;
            }
        }

        private ISessionDAO sessionDao;
        private IChildDAO childDao;
        private IDiaperDAO diaperDao;
        private INapDAO napDao;
        private IMealDAO mealDao;
        private IParentDAO parentDao;

        public SessionsController(ISessionDAO sessionDao, IChildDAO childDao, IDiaperDAO diaperDao, 
            INapDAO napDao, IMealDAO mealDao, IParentDAO parentDao)
        {
            this.sessionDao = sessionDao;
            this.childDao = childDao;
            this.diaperDao = diaperDao;
            this.napDao = napDao;
            this.mealDao = mealDao;
            this.parentDao = parentDao;
        }

        // /api/sessions
        // Will get a full list of the current session in progress now
        [HttpGet]
        public ActionResult<List<Session>> GetCurrentSessions()
        {
            List<Session> sessions = sessionDao.GetCurrentSessionsByCareTakerId(userId);

            foreach (Session session in sessions)
            {
                session.Child = childDao.GetChildById(session.ChildId, userId);
                session.Child.Parents = parentDao.GetParentsByChild(session.ChildId, userId);

                session.Diapers = diaperDao.GetAllDiapersBySession(session.SessionId, userId);
                session.Naps = napDao.GetAllNapsBySession(session.SessionId, userId);
                session.Meals = mealDao.GetAllMealsBySession(session.SessionId, userId);
            }


            return Ok(sessions);
        }

        [HttpGet("{sessionId}")]
        public ActionResult<Session> GetSessionByID(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session == null)
            {
                return NotFound();
            }

            session.Child = childDao.GetChildById(session.ChildId, userId);
            session.Child.Parents = parentDao.GetParentsByChild(session.ChildId, userId);

            session.Diapers = diaperDao.GetAllDiapersBySession(session.SessionId, userId);
            session.Naps = napDao.GetAllNapsBySession(session.SessionId, userId);
            session.Meals = mealDao.GetAllMealsBySession(session.SessionId, userId);

            return Ok(session);
        }

        [HttpGet("child/{childId}")]
        public ActionResult<List<Session>> GetAllSessionsByChild(int childId)
        {
            List<int> sessionIds = sessionDao.GetAllSessionsByChildId(childId, userId);

            if (sessionIds.Count == 0)
            {
                return NotFound();
            }

            List<Session> sessions = new List<Session>();

            foreach (int sessionId in sessionIds)
            {
                Session session = sessionDao.GetSessionById(sessionId, userId);

                session.Child = childDao.GetChildById(session.ChildId, userId);
                session.Child.Parents = parentDao.GetParentsByChild(session.ChildId, userId);

                session.Diapers = diaperDao.GetAllDiapersBySession(session.SessionId, userId);
                session.Naps = napDao.GetAllNapsBySession(session.SessionId, userId);
                session.Meals = mealDao.GetAllMealsBySession(session.SessionId, userId);

                sessions.Add(session);
            }

            return Ok(sessions);
        }

        [HttpGet("all")]
        public ActionResult<List<Session>> GetAllSessions()
        {
            List<Session> sessions = sessionDao.GetAllSessionsByCareTakerId(userId);

            foreach (Session session in sessions)
            {
                session.Child = childDao.GetChildById(session.ChildId, userId);
                session.Child.Parents = parentDao.GetParentsByChild(session.ChildId, userId);

                session.Diapers = diaperDao.GetAllDiapersBySession(session.SessionId, userId);
                session.Naps = napDao.GetAllNapsBySession(session.SessionId, userId);
                session.Meals = mealDao.GetAllMealsBySession(session.SessionId, userId);
            }


            return Ok(sessions);
        }

        [HttpPost("child/{childId}")]
        public ActionResult<Session> CreateSession(Session session, int childId)
        {
            Session newSession = sessionDao.CreateNewSession(session, userId, childId);
            newSession.Child = childDao.GetChildById(newSession.ChildId, userId);
            return Created($"/api/sessions/{newSession.SessionId}", newSession);
        }

        [HttpPut("end/{sessionId}")]
        public ActionResult<Session> EndSession(Session session, int sessionId) 
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession == null)
            {
                return NotFound();
            }
            Session endSession = sessionDao.EndSession(session, userId);
            return Created($"/api/sessions/{endSession.SessionId}", endSession);
        }

        [HttpPut("{sessionId}")]
        public ActionResult<Session> UpdateCurrentSession(Session session, int sessionId)
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession == null)
            {
                return NotFound();
            }
            Session updatedSession = sessionDao.UpdateOpenSession(session, userId);
            updatedSession.SessionId = sessionId;
            return Created($"/api/sessions/{updatedSession.SessionId}", updatedSession);
        }

        [HttpPut("closed/{sessionId}")]
        public ActionResult<Session> UpdateClosedSession(Session session, int sessionId)
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession == null)
            {
                return NotFound();
            }
            Session updatedSession = sessionDao.UpdateClosedSession(session, userId);
            updatedSession.SessionId = sessionId;
            return Created($"/api/sessions/{updatedSession.SessionId}", updatedSession);
        }

        [HttpDelete("{sessionId}")]
        public ActionResult DeleteSession(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session == null)
            {
                return NotFound();
            }

            sessionDao.DeleteSession(sessionId, userId);
            return NoContent();
        }
    }
}
