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
                List<Parent> parents = parentDao.GetParentsByChild(child.ChildId);

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
                return NotFound("Child either doesn't exist in the database, or you are not authorized to view such child.");
            }

            List<Parent> parents = parentDao.GetParentsByChild(childId);
            child.Parents = parents;

            return Ok(child);
        }
    }
}
