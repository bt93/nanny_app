using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;
using NannyApi.Models;
using NannyApi.Security;

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
        private IPasswordHasher PasswordHasher;

        public CaretakersController(ICareTakerDAO careTakerDao, IPasswordHasher passwordHasher)
        {
            this.careTakerDao = careTakerDao;
            this.PasswordHasher = passwordHasher;
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

            if (careTaker == null)
            {
                return NotFound();
            }

            careTaker.Password = null;
            careTaker.Salt = null;
            return Ok(careTaker);
        }

        /// <summary>
        /// PUT /api/caretakers
        /// Will update the current caretaker
        /// </summary>
        /// <param name="careTaker"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<CareTakerSettings> UpdateCareTaker(CareTakerSettings careTaker)
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

            bool isChanged = careTakerDao.UpdatePassword(careTaker.Password, userId, PasswordHasher);

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