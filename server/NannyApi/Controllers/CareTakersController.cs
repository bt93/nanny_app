using System;
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

        // Get for /api/caretakers
        //[HttpGet]
        //public ActionResult<IList<CareTaker>> GetCareTakers()
        //{
        //    return Ok(careTakerDao.GetAllCareTakers());
        //}

        /// <summary>
        /// Get api/caretakers/current
        /// Returns a single caretaker that is loged in
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public ActionResult<CareTaker> GetCareTaker()
        {
            CareTaker careTaker = careTakerDao.GetCareTakerById(userId);

            if (careTaker != null)
            {
                return Ok(careTaker);
            }

            return NotFound();
        }

        /// <summary>
        /// PUT /api/caretakers
        /// Will update the current caretaker
        /// </summary>
        /// <param name="careTaker"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<CareTaker> UpdateCareTaker(CareTaker careTaker)
        {
            CareTaker careTakerCheck = careTakerDao.GetCareTakerById(userId);

            if (careTakerCheck == null)
            {
                return NotFound();
            }

            careTaker.CareTakerId = userId;
            return Created($"api/caretakers/{careTaker.CareTakerId}", careTakerDao.UpdateCareTaker(careTaker));
        }

        [HttpPut("password")]
        public ActionResult UpdatePassword(CareTaker careTaker)
        {
            CareTaker careTakerCheck = careTakerDao.GetCareTakerById(userId);

            if (careTakerCheck == null)
            {
                return NotFound();
            }

            bool isChanged = careTakerDao.UpdatePassword(careTaker.Password, userId);

            if (isChanged)
            {
                return NoContent();
            } else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// DELETE /api/caretakers
        /// Deletes the current caretaker
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteCareTaker()
        {
            CareTaker careTaker = careTakerDao.GetCareTakerById(userId);

            if (careTaker == null)
            {
                return NotFound();
            }

            careTakerDao.DeleteCareTaker(careTaker);
            return NoContent();
        }
    }
}