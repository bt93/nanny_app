using System.Collections.Generic;
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
            return Ok(parentDao.GetParents());
        }

        /// <summary>
        /// Gets list of parents associated with a single child
        /// </summary>
        /// <param name="childId"></param>
        /// <returns>ActionResult<List<Parent>></returns>
        [HttpGet("child/{childId}")]
        public ActionResult<List<Parent>> GetParentsByChild(int childId)
        {
            List<Parent> parents = parentDao.GetParentsByChild(childId);

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
            Parent parent = parentDao.GetParentById(id);

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
        [HttpPost]
        public ActionResult<Parent> AddParent(Parent parent)
        {
            Parent newParent = parentDao.AddParent(parent);
            return Created($"api/parents/{newParent.ParentId}", newParent);
        }

        [HttpPut("{id}")]
        public ActionResult<Parent> UpdateParent(Parent parent)
        {
            Parent parentCheck = parentDao.GetParentById(parent.ParentId);

            if (parentCheck == null)
            {
                return NotFound();
            }

            parent.ParentId = parent.ParentId;
            return Created($"api/parents/{parent.ParentId}", parentDao.UpdateParent(parent));
        }
    }
}
