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
        // TODO: Comment all the endpoints and edit README
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

        /// <summary>
        /// GET /api/sessions
        /// Will get a full list of the current session in progress now
        /// </summary>
        /// <returns>List of Sessions</returns>
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

        /// <summary>
        /// GET /api/sessions/{sessionId}
        /// Gets all the sessions by id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>Session</returns>
        [HttpGet("{sessionId}")]
        public ActionResult<Session> GetSessionByID(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
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

        /// <summary>
        /// GET /api/sessions/child/{childId}
        /// Gets a list of the sessions by child
        /// </summary>
        /// <param name="childId"></param>
        /// <returns>List of Sessions</returns>
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

        /// <summary>
        /// GET /api/sessions/all
        /// Returns a list of all the sessions in by caretaker
        /// </summary>
        /// <returns>List of sessions</returns>
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

        /// <summary>
        /// GET /api/sessions/{sessionId}/naps
        /// Gets a list of all the naps per session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>List of Naps</returns>
        [HttpGet("{sessionId}/naps")]
        public ActionResult<List<Nap>> GetAllNaps(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            List<Nap> naps = napDao.GetAllNapsBySession(sessionId, userId);
            return Ok(naps);
        }

        /// <summary>
        /// GET /api/sessions/naps/{napId}
        /// Gets a Nap by the Id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="napId"></param>
        /// <returns>Nap</returns>
        [HttpGet("{sessionId}/naps/{napId}")]
        public ActionResult<Nap> GetNapById(int sessionId, int napId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            Nap nap = napDao.GetANapBySession(sessionId, userId, napId);
            
            if (nap.NapId == 0)
            {
                return NotFound();
            }

            return Ok(nap);
        }
        
        [HttpGet("{sessionId}/meals")]
        public ActionResult<List<Meal>> GetAllMeals(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            List<Meal> meals = mealDao.GetAllMealsBySession(sessionId, userId);
            return Ok(meals);
        }

        [HttpGet("{sessionId}/meals/{mealId}")]
        public ActionResult<Nap> GetMealsById(int sessionId, int mealId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            Meal meal = mealDao.GetAMealBySession(sessionId, userId, mealId);

            if (meal.MealId == 0)
            {
                return NotFound();
            }

            return Ok(meal);
        }

        [HttpGet("{sessionId}/diapers")]
        public ActionResult<List<Diaper>> GetAllDiapers(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            List<Diaper> diapers = diaperDao.GetAllDiapersBySession(sessionId, userId);
            return Ok(diapers);
        }

        [HttpGet("{sessionId}/diapers/{diaperId}")]
        public ActionResult<Nap> GetDiapersById(int sessionId, int diaperId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            Diaper diaper = diaperDao.GetADiaperBySession(sessionId, userId, diaperId);

            if (diaper.DiaperId == 0)
            {
                return NotFound();
            }

            return Ok(diaper);
        }

        /// <summary>
        /// POST /api/sessions/child/{childId] 
        /// Creates a new session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="childId"></param>
        /// <returns>Session</returns>
        [HttpPost("child/{childId}")]
        public ActionResult<Session> CreateSession(Session session, int childId)
        {
            Session newSession = sessionDao.CreateNewSession(session, userId, childId);
            newSession.Child = childDao.GetChildById(newSession.ChildId, userId);
            return Created($"/api/sessions/{newSession.SessionId}", newSession);
        }

        /// <summary>
        /// POST /api/sessions/{sessionId}/naps
        /// Post a new nap given a session
        /// </summary>
        /// <param name="nap"></param>
        /// <param name="sessionId"></param>
        /// <returns>Nap</returns>
        [HttpPost("{sessionId}/naps")]
        public ActionResult<Nap> CreateNap(Nap nap, int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            nap.SessionId = sessionId;
            Nap newNap = napDao.AddNap(nap);
            return Created($"/api/sessions/{sessionId}/naps/{newNap.NapId}", newNap);
        }

        [HttpPost("{sessionId}/meals")]
        public ActionResult<Nap> CreateMeal(Meal meal, int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            meal.SessionId = sessionId;
            Meal newMeal = mealDao.AddMeal(meal);
            return Created($"/api/sessions/{sessionId}/naps/{newMeal.MealId}", newMeal);
        }

        [HttpPost("{sessionId}/diapers")]
        public ActionResult<Nap> CreateDiaper(Diaper diaper, int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            diaper.SessionId = sessionId;
            Diaper newMeal = diaperDao.AddDiaper(diaper);
            return Created($"/api/sessions/{sessionId}/naps/{newMeal.DiaperId}", newMeal);
        }

        /// <summary>
        /// PUT /api/sessions/end/{sessionId}
        /// Ends a session given an id and session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sessionId"></param>
        /// <returns>Session</returns>
        [HttpPut("end/{sessionId}")]
        public ActionResult<Session> EndSession(Session session, int sessionId) 
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession.SessionId == 0)
            {
                return NotFound();
            }
            Session endSession = sessionDao.EndSession(session, userId);
            return Created($"/api/sessions/{endSession.SessionId}", endSession);
        }

        /// <summary>
        /// PUT /api/sessions/{sessionId}
        /// Updates a session in progress
        /// </summary>
        /// <param name="session"></param>
        /// <param name="sessionId"></param>
        /// <returns>Session</returns>
        [HttpPut("{sessionId}")]
        public ActionResult<Session> UpdateCurrentSession(Session session, int sessionId)
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession.SessionId == 0)
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

            if (checkSession.SessionId == 0)
            {
                return NotFound();
            }
            Session updatedSession = sessionDao.UpdateClosedSession(session, userId);
            updatedSession.SessionId = sessionId;
            return Created($"/api/sessions/{updatedSession.SessionId}", updatedSession);
        }

        [HttpPut("{sessionId}/naps/{napId}")]
        public ActionResult<Nap> UpdateNap(int sessionId, int napId, Nap nap)
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession.SessionId == 0)
            {
                return NotFound();
            }

            nap.NapId = napId;
            Nap updatedNap = napDao.UpdateNap(nap, userId);
            return Created($"/api/sessions/{sessionId}/naps/{updatedNap.NapId}", updatedNap);
        }

        [HttpPut("{sessionId}/meals/{mealId}")]
        public ActionResult<Nap> UpdateMeal(int sessionId, int mealId, Meal meal)
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession.SessionId == 0)
            {
                return NotFound();
            }

            meal.MealId = mealId;
            Meal updatedMeal = mealDao.UpdateMeal(meal, userId);
            return Created($"/api/sessions/{sessionId}/naps/{updatedMeal.MealId}", updatedMeal);
        }

        [HttpPut("{sessionId}/diapers/{diaperId}")]
        public ActionResult<Nap> UpdateDiaper(int sessionId, int diaperId, Diaper diaper)
        {
            Session checkSession = sessionDao.GetSessionById(sessionId, userId);

            if (checkSession.SessionId == 0)
            {
                return NotFound();
            }

            diaper.DiaperId = diaperId;
            Diaper updatedDiaper = diaperDao.UpdateDiaper(diaper, userId);
            return Created($"/api/sessions/{sessionId}/naps/{updatedDiaper.DiaperId}", updatedDiaper);
        }

        [HttpDelete("{sessionId}")]
        public ActionResult DeleteSession(int sessionId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            sessionDao.DeleteSession(sessionId, userId);
            return NoContent();
        }

        [HttpDelete("{sessionId}/naps/{napId}")]
        public ActionResult DeleteNap(int sessionId, int napId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            napDao.DeleteNap(napId);
            return NoContent();
        }

        [HttpDelete("{sessionId}/meals/{mealId}")]
        public ActionResult DeleteMeal(int sessionId, int mealId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            mealDao.DeleteMeal(mealId);
            return NoContent();
        }

        [HttpDelete("{sessionId}/diapers/{diaperId}")]
        public ActionResult DeleteDiaper(int sessionId, int diaperId)
        {
            Session session = sessionDao.GetSessionById(sessionId, userId);

            if (session.SessionId == 0)
            {
                return NotFound();
            }

            diaperDao.DeleteDiaper(diaperId);
            return NoContent();
        }
    }
}
