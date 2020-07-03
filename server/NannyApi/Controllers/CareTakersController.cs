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
    public class CaretakersController : ControllerBase
    {
        private string userName
        {
            get
            {
                return User?.Identity?.Name;
            }
        }

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

        private ICareTakerDAO careTakerDao;

        public CaretakersController(ICareTakerDAO careTakerDao)
        {
            this.careTakerDao = careTakerDao;
        }

        // Get for api/caretakers
        [HttpGet]
        public ActionResult<IList<CareTaker>> GetCareTakers()
        {
            return Ok(careTakerDao.GetAllCareTakers());
        }

        // Get for api/caretakers/current
        [HttpGet("current")]
        public ActionResult<CareTaker> GetCareTaker()
        {
            CareTaker careTaker = careTakerDao.GetCareTakerById(userId);

            if (careTaker != null)
            {
                return careTaker;
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<CareTaker> AddCareTaker(CareTaker careTaker)
        {
            CareTaker newCareTaker = careTakerDao.AddCareTaker(careTaker);
            return Created($"api/caretakers/{newCareTaker.CareTakerId}", newCareTaker);
        }

        [HttpPut("{id}")]
        public ActionResult<CareTaker> UpdateCareTaker(CareTaker careTaker, int id)
        {
            if (careTakerDao.GetCareTakerById(id) == null)
            {
                return NotFound();
            }

            careTaker.CareTakerId = id;
            return Created($"api/caretakers/{careTaker.CareTakerId}", careTakerDao.UpdateCareTaker(careTaker));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCareTaker(int id)
        {
            if (careTakerDao.GetCareTakerById(id) == null)
            {
                return NotFound();
            }

            careTakerDao.DeleteCareTaker(id);
            return NoContent();
        }
    }
}