using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;
using NannyApi.Models;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private IParentDAO parentDao;

        public ParentsController(IParentDAO parentDao)
        {
            this.parentDao = parentDao;
        }

        [HttpGet]
        public ActionResult<List<Parent>> GetParents()
        {
            return Ok(parentDao.GetParents());
        }

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
    }
}
