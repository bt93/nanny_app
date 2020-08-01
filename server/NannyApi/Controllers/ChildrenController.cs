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
    public class ChildrenController : ControllerBase
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

        private IChildDAO childDao;
        private IParentDAO parentDao;

        public ChildrenController(IChildDAO childDao, IParentDAO parentDao)
        {
            this.childDao = childDao;
            this.parentDao = parentDao;
        }

        [HttpGet]
        public ActionResult<List<Child>> GetAllChildren()
        {
            List<Child> children = childDao.GetChildren(userId);

            if (children.Count == 0)
            {
                return NotFound();
            }

            foreach (Child child in children)
            {
                List<Parent> parents = parentDao.GetParentsByChild(child.ChildId, userId);

                child.Parents = parents;
            }

            return Ok(children);
        }

        [HttpGet("{childId}")]
        public ActionResult<Child> GetChildById(int childId)
        {
            Child child = childDao.GetChildById(childId, userId);

            if (child == null)
            {
                return Forbid();
            }

            List<Parent> parents = parentDao.GetParentsByChild(childId, userId);
            child.Parents = parents;

            return Ok(child);
        }

        [HttpGet("deactivated")]
        public ActionResult<List<Child>> GetDeactivatedChildren()
        {
            List<Child> children = childDao.GetDeactivedChildren(userId);

            if (children.Count == 0)
            {
                return NotFound();
            }

            foreach (Child child in children)
            {
                List<Parent> parents = parentDao.GetParentsByChild(child.ChildId, userId);

                child.Parents = parents;
            }

            return Ok(children);
        }

        [HttpPost]
        public ActionResult<Child> AddChild(Child child)
        {
            Child newChild = childDao.AddChild(child, userId);
            return Created($"/api/children/{newChild.ChildId}", newChild);
        }

        [HttpPut("{childId}")]
        public ActionResult<Child> UpdateChild(Child child, int childId)
        {
            Child checkChild = childDao.GetChildById(childId, userId);

            if (checkChild == null)
            {
                return NotFound();
            }

            child.ChildId = childId;
            return Created($"api/children/{childId}", childDao.UpdateChild(child, childId, userId));
        }

        [HttpPut("reinstate/{childId}")]
        public ActionResult<Child> ReinstateChild(int childId)
        {
            Child checkChild = childDao.GetDeactivatedChildById(childId, userId);

            if (checkChild == null)
            {
                return NotFound();
            }

            checkChild.ChildId = childId;
            return Created($"api/children/reinstate/{childId}", childDao.ReinstateChild(childId, userId));
        }

        [HttpDelete("{childId}")]
        public ActionResult DeleteChild(int childId)
        {
            Child checkChild = childDao.GetChildById(childId, userId);

            if (checkChild == null)
            {
                return NotFound();
            }

            childDao.DeleteChild(childId, userId);
            return NoContent();
        }
    }
}
