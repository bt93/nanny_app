﻿using System;
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

            foreach (Child child in children)
            {
                List<Parent> parents = parentDao.GetParentsByChild(child.ChildId);

                child.Parents = parents;
            }

            return Ok(children);
        }
    }
}
