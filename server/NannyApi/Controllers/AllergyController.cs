using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;
using NannyApi.Models;
using System.Collections.Generic;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AllergyController : ControllerBase
    {
        private IAllergyDAO allergyDao;

        public AllergyController(IAllergyDAO allergyDao)
        {
            this.allergyDao = allergyDao;
        }

        [HttpGet]
        public ActionResult<List<Allergy>> getAllAllergies()
        {
            return Ok(allergyDao.GetAllergies());
        }

        [HttpGet("{allergyTypeId}")]
        public ActionResult<List<Allergy>> getAllergiesById(int allergyTypeId)
        {
            return Ok(allergyDao.GetAllergiesByType(allergyTypeId));
        }

        [HttpGet("types")]
        public ActionResult<List<AllergyType>> getAllergyTypes()
        {
            return Ok(allergyDao.getAllergyTypes());
        }
    }
}
