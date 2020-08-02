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

        public SessionsController(ISessionDAO sessionDao, IChildDAO childDao, IDiaperDAO diaperDao, INapDAO napDao, IMealDAO mealDao)
        {
            this.sessionDao = sessionDao;
            this.childDao = childDao;
            this.diaperDao = diaperDao;
            this.napDao = napDao;
            this.mealDao = mealDao;
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
    }
}
