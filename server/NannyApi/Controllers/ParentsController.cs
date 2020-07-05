using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;
using NannyApi.Models;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParentsController : ControllerBase
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

        private IParentDAO parentDao;

        public ParentsController(IParentDAO parentDao)
        {
            this.parentDao = parentDao;
        }

        /// <summary>
        /// Returns list of all parents
        /// </summary>
        /// <returns>ActionResult<List<Parent>></returns>
        [HttpGet]
        public ActionResult<List<Parent>> GetParents()
        {
            return Ok(parentDao.GetParents(userId));
        }

        /// <summary>
        /// Gets list of parents associated with a single child
        /// </summary>
        /// <param name="childId"></param>
        /// <returns>ActionResult<List<Parent>></returns>
        [HttpGet("child/{childId}")]
        public ActionResult<List<Parent>> GetParentsByChild(int childId)
        {
            List<Parent> parents = parentDao.GetParentsByChild(childId, userId);

            if (parents.Count == 0)
            {
                return NotFound();
            }

            return parents;
        }

        /// <summary>
        /// Gets a parent by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parent Object</returns>
        [HttpGet("{id}")]
        public ActionResult<Parent> GetParentById(int id)
        {
            Parent parent = parentDao.GetParentById(id, userId);

            if (parent == null)
            {
                return NotFound();
            }

            return parent;
        }

        /// <summary>
        /// Adds a new parent to the database
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>ActionResult<Parent></returns>
        [HttpPost("child/{childId}")]
        public ActionResult<Parent> AddParent(Parent parent, int childId)
        {
            Parent newParent = parentDao.AddParent(parent, childId);
            return Created($"api/parents/{newParent.ParentId}", newParent);
        }

        [HttpPost("{id}/child/{childId}")]
        public ActionResult AddExsitingParent(int id, int childId)
        {
            bool isCreated = parentDao.AddExsistingParent(childId, id);

            if (isCreated)
            {
                return Created($"api/parents/{id}", parentDao.GetParentById(id, userId));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<Parent> UpdateParent(Parent parent, int id)
        {
            Parent parentCheck = parentDao.GetParentById(id, userId);

            if (parentCheck == null)
            {
                return NotFound();
            }

            parent.ParentId = parentCheck.ParentId;
            parent.AddressId = parentCheck.AddressId;
            return Created($"api/parents/{parent.ParentId}", parentDao.UpdateParent(parent));
        }
    }
}
